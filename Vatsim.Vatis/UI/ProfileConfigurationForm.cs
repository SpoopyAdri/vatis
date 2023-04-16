using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.Profiles.AtisFormat;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.UI.Controls;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Utils;

namespace Vatsim.Vatis.UI;

public partial class ProfileConfigurationForm : Form
{
    private readonly IAppConfig mAppConfig;
    private readonly IWindowFactory mWindowFactory;
    private readonly INavaidDatabase mNavaidDatabase;
    private Control mPresetControl;

    private Composite mCurrentComposite = null;
    private Preset mCurrentPreset = null;

    private List<Tuple<BaseFormat, string>> mPendingVoiceTemplateChanges = new();
    private List<Tuple<BaseFormat, string>> mPendingTextTemplateChanges = new();

    public ProfileConfigurationForm(IWindowFactory windowFactory, IAppConfig appConfig,
        INavaidDatabase navaidDatabase)
    {
        InitializeComponent();

        mAppConfig = appConfig;
        mWindowFactory = windowFactory;
        mNavaidDatabase = navaidDatabase;

        RefreshCompositeList();
        LoadVoiceList();
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        EventBus.Register(this);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        EventBus.Unregister(this);
        base.OnFormClosing(e);
    }

    private void LoadVoiceList()
    {
        ddlVoices.DataSource = mAppConfig.Voices;
        ddlVoices.ValueMember = "Id";
        ddlVoices.DisplayMember = "Name";
    }

    private TreeNode CreateTreeMenuNode(string name, object tag)
    {
        return new TreeNode
        {
            Tag = tag,
            Text = name,
        };
    }

    public void HandleEvent(AtisTemplateChanged evt)
    {
        if (mCurrentPreset == null)
            return;

        if (evt.Value != mCurrentPreset.Template)
        {
            mCurrentPreset.Template = evt.Value;
            btnApply.Enabled = true;
        }
    }

    public void HandleEvent(ExternalAtisConfigChanged evt)
    {
        if (mCurrentPreset == null)
            return;

        mCurrentPreset.ExternalGenerator ??= new();

        switch (evt.Component)
        {
            case ExternalAtisComponent.Url:
                if (evt.Value != mCurrentPreset.ExternalGenerator.Url)
                {
                    mCurrentPreset.ExternalGenerator.Url = evt.Value;
                    btnApply.Enabled = true;
                }
                break;
            case ExternalAtisComponent.ArrivalRunways:
                if (evt.Value != mCurrentPreset.ExternalGenerator.Arrival)
                {
                    mCurrentPreset.ExternalGenerator.Arrival = evt.Value;
                    btnApply.Enabled = true;
                }
                break;
            case ExternalAtisComponent.DepartureRunways:
                if (evt.Value != mCurrentPreset.ExternalGenerator.Departure)
                {
                    mCurrentPreset.ExternalGenerator.Departure = evt.Value;
                    btnApply.Enabled = true;
                }
                break;
            case ExternalAtisComponent.Approaches:
                if (evt.Value != mCurrentPreset.ExternalGenerator.Approaches)
                {
                    mCurrentPreset.ExternalGenerator.Approaches = evt.Value;
                    btnApply.Enabled = true;
                }
                break;
            case ExternalAtisComponent.Remarks:
                if (evt.Value != mCurrentPreset.ExternalGenerator.Remarks)
                {
                    mCurrentPreset.ExternalGenerator.Remarks = evt.Value;
                    btnApply.Enabled = true;
                }
                break;
        }
    }

    private void btnManageComposite_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ctxOptions.Show(Cursor.Position.X, Cursor.Position.Y);
        }
    }

    private void TreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (TreeMenu.SelectedNode != null)
        {
            mainTabControl.Enabled = true;
            ctxDelete.Enabled = true;
            ctxRename.Enabled = true;
            ctxCopy.Enabled = true;
            ctxExport.Enabled = true;
            Text = $"Profile Configuration: {TreeMenu.SelectedNode.Text}";
            btnApply.Enabled = false;
            if (mAppConfig.CurrentProfile != null)
            {
                if (TreeMenu.SelectedNode.Tag is not Composite composite)
                    return;

                mCurrentComposite = mAppConfig.CurrentProfile.Composites
                    .FirstOrDefault(x => x.Id == composite.Id);

                if (mCurrentComposite == null)
                    return;

                pageTransitionLevel.SetVisible(!mCurrentComposite.IsFaaAtis);

                LoadComposite();
                RefreshPresetList();
            }
        }
    }

    private void LoadComposite()
    {
        chkNotamPrefix.Checked = mCurrentComposite.UseNotamPrefix;
        chkNotamPrefix.Text = mCurrentComposite.IsFaaAtis ? "Prefix spoken NOTAMs with \"Notices to Air Missions\"" : "Prefix spoken NOTAMs with \"Notices to Air Men\"";

        vhfFrequency.Text = (mCurrentComposite.Frequency / 1000.0).ToString("000.000");

        switch (mCurrentComposite.AtisType)
        {
            case AtisType.Combined:
                typeCombined.Checked = true;
                txtCodeRangeLow.Enabled = false;
                txtCodeRangeHigh.Enabled = false;
                break;
            case AtisType.Departure:
                typeDeparture.Checked = true;
                txtCodeRangeLow.Enabled = true;
                txtCodeRangeHigh.Enabled = true;
                break;
            case AtisType.Arrival:
                typeArrival.Checked = true;
                txtCodeRangeLow.Enabled = true;
                txtCodeRangeHigh.Enabled = true;
                break;
        }

        txtCodeRangeLow.Text = mCurrentComposite.CodeRange.Low.ToString();
        txtCodeRangeHigh.Text = mCurrentComposite.CodeRange.High.ToString();

        if (mCurrentComposite.MagneticVariation != null)
        {
            chkMagneticVar.Checked = mCurrentComposite.MagneticVariation.Enabled;
            magneticVar.Value = mCurrentComposite.MagneticVariation.MagneticDegrees;
            magneticVar.Enabled = chkMagneticVar.Checked;
        }

        groupVoiceOption.Enabled = !mCurrentComposite.Connection.IsConnected;

        if (mCurrentComposite.AtisVoice != null)
        {
            radioTextToSpeech.Checked = mCurrentComposite.AtisVoice.UseTextToSpeech;
            radioVoiceRecorded.Checked = !mCurrentComposite.AtisVoice.UseTextToSpeech;

            if (radioTextToSpeech.Checked)
            {
                ddlVoices.Enabled = true;

                var voices = ddlVoices.DataSource as List<VoiceMetaData>;
                if (!voices.Any(n => n.Name == mCurrentComposite.AtisVoice.Voice))
                {
                    ddlVoices.SelectedText = "Default";
                }
                else
                {
                    ddlVoices.SelectedItem = voices.FirstOrDefault(n => n.Name == mCurrentComposite.AtisVoice.Voice);
                }

                var meta = new AtisVoiceMeta
                {
                    UseTextToSpeech = true,
                    Voice = (ddlVoices.SelectedItem as VoiceMetaData).Name
                };
                mCurrentComposite.AtisVoice = meta;
                mAppConfig.SaveConfig();
            }
            else
            {
                ddlVoices.Enabled = false;
            }
        }
        else
        {
            radioTextToSpeech.Checked = true;
            ddlVoices.SelectedItem = "Default";
            var meta = new AtisVoiceMeta
            {
                UseTextToSpeech = true,
                Voice = "Default"
            };
            mCurrentComposite.AtisVoice = meta;
            mAppConfig.SaveConfig();
        }

        chkUseDecimalTerminology.Checked = mCurrentComposite.UseDecimalTerminology;
        txtIdsEndpoint.Text = mCurrentComposite.IDSEndpoint;

        gridContractions.Rows.Clear();
        foreach (var contraction in mCurrentComposite.Contractions)
        {
            gridContractions.Rows.Add(contraction.String, contraction.Spoken);
        }

        gridTransitionLevels.Rows.Clear();
        foreach (var tl in mCurrentComposite.TransitionLevels.OrderByDescending(t => t.Low)
                     .ThenByDescending(t => t.High))
        {
            gridTransitionLevels.Rows.Add(tl.Low, tl.High, tl.Altitude);
        }

        templateObservationTime.TextTemplate = mCurrentComposite.AtisFormat.ObservationTime.Template.Text;
        templateObservationTime.VoiceTemplate = mCurrentComposite.AtisFormat.ObservationTime.Template.Voice;
        standardObservationTime.Value = mCurrentComposite.AtisFormat.ObservationTime.StandardUpdateTime;

        chkWindSpeakLeadingZero.Checked = mCurrentComposite.AtisFormat.SurfaceWind.SpeakLeadingZero;

        templateWindStandard.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Standard.Template.Text;
        templateWindStandard.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Standard.Template.Voice;

        templateWindStanardGust.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.StandardGust.Template.Text;
        templateWindStanardGust.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.StandardGust.Template.Voice;

        templateWindVariable.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Variable.Template.Text;
        templateWindVariable.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Variable.Template.Voice;

        templateWindVariableGust.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.VariableGust.Template.Text;
        templateWindVariableGust.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.VariableGust.Template.Voice;

        templateWindVariableDirection.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.VariableDirection.Template.Text;
        templateWindVariableDirection.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.VariableDirection.Template.Voice;

        templateWindCalm.TextTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Calm.Template.Text;
        templateWindCalm.VoiceTemplate = mCurrentComposite.AtisFormat.SurfaceWind.Calm.Template.Voice;
        calmWindSpeed.Value = mCurrentComposite.AtisFormat.SurfaceWind.Calm.CalmWindSpeed;

        templatePresentWeather.TextTemplate = mCurrentComposite.AtisFormat.PresentWeather.Template.Text;
        templatePresentWeather.VoiceTemplate = mCurrentComposite.AtisFormat.PresentWeather.Template.Voice;
        wxIntensityLight.Text = mCurrentComposite.AtisFormat.PresentWeather.LightIntensity;
        wxIntensityModerate.Text = mCurrentComposite.AtisFormat.PresentWeather.ModerateIntensity;
        wxIntensityHeavy.Text = mCurrentComposite.AtisFormat.PresentWeather.HeavyIntensity;
        wxProximityVicinity.Text = mCurrentComposite.AtisFormat.PresentWeather.Vicinity;

        templateVisibility.TextTemplate = mCurrentComposite.AtisFormat.Visibility.Template.Text;
        templateVisibility.VoiceTemplate = mCurrentComposite.AtisFormat.Visibility.Template.Voice;

        templateCloudLayers.TextTemplate = mCurrentComposite.AtisFormat.Clouds.Template.Text;
        templateCloudLayers.VoiceTemplate = mCurrentComposite.AtisFormat.Clouds.Template.Voice;

        templateTemperature.TextTemplate = mCurrentComposite.AtisFormat.Temperature.Template.Text;
        templateTemperature.VoiceTemplate = mCurrentComposite.AtisFormat.Temperature.Template.Voice;
        chkTempPlusPrefix.Checked = mCurrentComposite.AtisFormat.Temperature.UsePlusPrefix;
        chkTempLeadingZero.Checked = mCurrentComposite.AtisFormat.Temperature.PronounceLeadingZero;

        templateDewpoint.TextTemplate = mCurrentComposite.AtisFormat.Dewpoint.Template.Text;
        templateDewpoint.VoiceTemplate = mCurrentComposite.AtisFormat.Dewpoint.Template.Voice;
        chkDewPlusPrefix.Checked = mCurrentComposite.AtisFormat.Dewpoint.UsePlusPrefix;
        chkDewLeadingZero.Checked = mCurrentComposite.AtisFormat.Dewpoint.PronounceLeadingZero;

        templateAltimeter.TextTemplate = mCurrentComposite.AtisFormat.Altimeter.Template.Text;
        templateAltimeter.VoiceTemplate = mCurrentComposite.AtisFormat.Altimeter.Template.Voice;
        chkVisibilitySuffix.Checked = mCurrentComposite.AtisFormat.Visibility.IncludeVisibilitySuffix;
        visibilityMetersCutoff.Value = mCurrentComposite.AtisFormat.Visibility.MetersCutoff;
        visDirNorth.Text = mCurrentComposite.AtisFormat.Visibility.North;
        visDirNorthEast.Text = mCurrentComposite.AtisFormat.Visibility.NorthEast;
        visDirEast.Text = mCurrentComposite.AtisFormat.Visibility.East;
        visDirSouthEast.Text = mCurrentComposite.AtisFormat.Visibility.SouthEast;
        visDirSouth.Text = mCurrentComposite.AtisFormat.Visibility.South;
        visDirSouthWest.Text = mCurrentComposite.AtisFormat.Visibility.SouthWest;
        visDirWest.Text = mCurrentComposite.AtisFormat.Visibility.West;
        visDirNorthWest.Text = mCurrentComposite.AtisFormat.Visibility.NorthWest;
        visUnlimited.Text = mCurrentComposite.AtisFormat.Visibility.UnlimitedVisibility;

        gridWeatherTypes.Rows.Clear();
        if (mCurrentComposite.AtisFormat.PresentWeather.WeatherTypes != null)
        {
            foreach (var type in mCurrentComposite.AtisFormat.PresentWeather.WeatherTypes)
            {
                gridWeatherTypes.Rows.Add(type.Key, type.Value);
            }
        }

        gridWeatherDescriptors.Rows.Clear();
        if (mCurrentComposite.AtisFormat.PresentWeather.WeatherDescriptors != null)
        {
            foreach (var type in mCurrentComposite.AtisFormat.PresentWeather.WeatherDescriptors)
            {
                gridWeatherDescriptors.Rows.Add(type.Key, type.Value);
            }
        }

        chkIdentifyCeilingLayer.Checked = mCurrentComposite.AtisFormat.Clouds.IdentifyCeilingLayer;
        chkConvertCloudsMetric.Checked = mCurrentComposite.AtisFormat.Clouds.ConvertToMetric;

        gridCloudTypes.Rows.Clear();
        if (mCurrentComposite.AtisFormat.Clouds.Types != null)
        {
            foreach (var type in mCurrentComposite.AtisFormat.Clouds.Types)
            {
                gridCloudTypes.Rows.Add(type.Key, type.Value);
            }
        }

        gridConvectiveCloudTypes.Rows.Clear();
        if (mCurrentComposite.AtisFormat.Clouds.ConvectiveTypes != null)
        {
            foreach (var type in mCurrentComposite.AtisFormat.Clouds.ConvectiveTypes)
            {
                gridConvectiveCloudTypes.Rows.Add(type.Key, type.Value);
            }
        }

        chkAutoIncludeClosingStatement.Checked = mCurrentComposite.AtisFormat.ClosingStatement.AutoIncludeClosingStatement;
        templateClosingStatement.TextTemplate = mCurrentComposite.AtisFormat.ClosingStatement.Template.Text;
        templateClosingStatement.VoiceTemplate = mCurrentComposite.AtisFormat.ClosingStatement.Template.Voice;
    }

    private void TreeMenu_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
        if (e.Node == null)
            return;

        var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
        var unfocused = !e.Node.TreeView.Focused;

        if (selected && unfocused)
        {
            var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
            // override bounds to paint full width
            var bounds = new Rectangle(e.Bounds.X - 6, e.Bounds.Y, TreeMenu.Width, e.Bounds.Height);
            e.Graphics.FillRectangle(SystemBrushes.Highlight, bounds);
            TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText,
                TextFormatFlags.GlyphOverhangPadding);
        }
        else
        {
            e.DrawDefault = true;
        }
    }

    private void ctxNew_Click(object sender, EventArgs e)
    {
        if (mAppConfig.CurrentProfile.Composites.Count >= Constants.MAX_COMPOSITES)
        {
            MessageBox.Show("The maximum ATIS composite count has been exceeded for this profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        bool flag = false;

        var previousIdentifer = "";
        var previousName = "";
        var previousType = AtisType.Combined;

        while (!flag)
        {
            using var dlg = mWindowFactory.CreateNewCompositeDialog();
            dlg.Identifier = previousIdentifer;
            dlg.CompositeName = previousName;
            dlg.Type = previousType;
            dlg.TopMost = mAppConfig.WindowProperties.TopMost;

            DialogResult result = dlg.ShowDialog(this);
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Identifier))
            {
                previousIdentifer = dlg.Identifier;
                previousName = dlg.CompositeName;
                previousType = dlg.Type;

                if (mNavaidDatabase.GetAirport(dlg.Identifier) == null)
                {
                    if (MessageBox.Show(this, $"ICAO identifier not found: {dlg.Identifier}", "Invalid Identifier", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        continue;
                    }
                }

                if (mAppConfig.CurrentProfile != null && mAppConfig.CurrentProfile.Composites.Any(x => x.Identifier == dlg.Identifier && x.AtisType == dlg.Type))
                {
                    if (MessageBox.Show(this, $"{dlg.Identifier} ({dlg.Type}) already exists. Would you like to overwrite it?", "Duplicate Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        continue;
                    }
                    else
                    {
                        mAppConfig.CurrentProfile.Composites.RemoveAll(x => x.Identifier == dlg.Identifier && x.AtisType == dlg.Type);
                        mAppConfig.SaveConfig();
                    }
                }

                var composite = new Composite
                {
                    Identifier = dlg.Identifier,
                    Name = dlg.CompositeName,
                    AtisType = dlg.Type
                };

                mAppConfig.CurrentProfile.Composites.Add(composite);
                mAppConfig.SaveConfig();
                RefreshCompositeList();
                flag = true;
            }
            else
            {
                flag = true;
            }
        }
    }

    private void ctxCopy_Click(object sender, EventArgs e)
    {
        if (mAppConfig.CurrentProfile.Composites.Count >= Constants.MAX_COMPOSITES)
        {
            MessageBox.Show("The maximum ATIS composite count has been exceeded for this profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (TreeMenu.SelectedNode != null)
        {
            bool flag = false;
            var previousIdentifer = "";
            var previousName = "";
            var previousType = AtisType.Combined;

            var composite = TreeMenu.SelectedNode.Tag as Composite;

            while (!flag)
            {
                using var dlg = mWindowFactory.CreateNewCompositeDialog();
                dlg.Identifier = previousIdentifer;
                dlg.CompositeName = previousName;
                dlg.Type = previousType;
                dlg.TopMost = mAppConfig.WindowProperties.TopMost;

                DialogResult result = dlg.ShowDialog(this);
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Identifier))
                {
                    previousIdentifer = dlg.Identifier;
                    previousName = dlg.CompositeName;
                    previousType = dlg.Type;

                    if (mNavaidDatabase.GetAirport(dlg.Identifier) == null)
                    {
                        if (MessageBox.Show(this, $"ICAO identifier not found: {dlg.Identifier}", "Invalid Identifier", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            continue;
                        }
                    }

                    if (mAppConfig.CurrentProfile != null && mAppConfig.CurrentProfile.Composites
                        .Any(x => x.Identifier == dlg.Identifier && x.AtisType == dlg.Type))
                    {
                        if (MessageBox.Show(this, $"{dlg.Identifier} ({dlg.Type}) already exists.", "Duplicate Composite", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            continue;
                        }
                    }

                    var clone = (Composite)composite.Clone();
                    clone.Identifier = dlg.Identifier;
                    clone.Name = dlg.CompositeName;
                    clone.AtisType = dlg.Type;

                    var presets = new List<Preset>();
                    foreach (var preset in composite.Presets)
                    {
                        presets.Add((Preset)preset.Clone());
                    }
                    clone.Presets = presets;

                    mAppConfig.CurrentProfile.Composites.Add(clone);
                    mAppConfig.SaveConfig();
                    RefreshCompositeList();
                    flag = true;
                }
                else
                {
                    flag = true;
                }
            }
        }
    }

    private void ctxRename_Click(object sender, EventArgs e)
    {
        bool flag = false;

        if (TreeMenu.SelectedNode != null)
        {
            if (TreeMenu.SelectedNode.Tag is Composite composite)
            {
                var previousValue = "";

                while (!flag)
                {
                    using var dlg = mWindowFactory.CreateUserInputDialog();
                    previousValue = composite.Name;
                    dlg.PromptLabel = "Enter a new name for the ATIS Composite:";
                    dlg.WindowTitle = "Rename ATIS Composite";
                    dlg.ErrorMessage =
                        "Invalid composite name. It must consist of only letters, numbers, underscores and spaces.";
                    dlg.RegexExpression = "^[a-zA-Z0-9_ ]*$";
                    dlg.InitialValue = previousValue;
                    dlg.TopMost = mAppConfig.WindowProperties.TopMost;

                    DialogResult result2 = dlg.ShowDialog(this);
                    if (result2 == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
                    {
                        previousValue = dlg.Value;
                        composite.Name = dlg.Value;
                        mAppConfig.SaveConfig();
                        RefreshCompositeList();
                        flag = true;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
        }
    }

    private void ctxDelete_Click(object sender, EventArgs e)
    {
        if (TreeMenu.SelectedNode != null)
        {
            var composite = TreeMenu.SelectedNode.Tag as Composite;

            if (MessageBox.Show(this, $"Are you sure you want to delete the selected ATIS Composite? This action will also delete all associated ATIS presets.\r\n\r\n{composite.Identifier} {composite.AtisType}", "Delete ATIS Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                EventBus.Publish(this, new AtisCompositeDeleted(composite.Id));

                mAppConfig.CurrentProfile.Composites.Remove(composite);
                mAppConfig.SaveConfig();
                RefreshCompositeList();
            }
        }
    }

    private void RefreshCompositeList()
    {
        mainTabControl.Enabled = false;
        ctxDelete.Enabled = false;
        ctxRename.Enabled = false;
        ctxCopy.Enabled = false;
        ctxExport.Enabled = false;

        vhfFrequency.Text = "118.000";
        standardObservationTime.Value = 0;
        magneticVar.Value = 0;
        chkMagneticVar.Checked = false;
        txtIdsEndpoint.Text = "";

        TreeMenu.Nodes.Clear();
        if (mAppConfig.CurrentProfile != null)
        {
            foreach (var composite in mAppConfig.CurrentProfile.Composites.OrderBy(x => x.Identifier))
            {
                var type = composite.AtisType == AtisType.Departure ? "Departure" :
                    composite.AtisType == AtisType.Arrival ? "Arrival" : "";
                var node = CreateTreeMenuNode($"{composite.Name} ({composite.Identifier}) {type}", composite);

                TreeMenu.Nodes.Add(node);

                if (mAppConfig.CurrentComposite == composite)
                {
                    TreeMenu.SelectedNode = node;
                }
            }
        }

        RefreshPresetList();
        EventBus.Publish(this, new RefreshCompositesRequested());
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnApply.Enabled)
        {
            if (MessageBox.Show(this, "You have unsaved changes. Are you sure you want to cancel?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
        }
        else
        {
            Close();
        }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
        ApplyChanges();
    }

    private bool ApplyChanges()
    {
        if (!string.IsNullOrEmpty(txtIdsEndpoint.Text) && !txtIdsEndpoint.Text.IsValidUrl())
        {
            MessageBox.Show(this, "IDS endpoint URL is not a valid hyperlink format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        else
        {
            mCurrentComposite.IDSEndpoint = txtIdsEndpoint.Text;
        }

        mCurrentComposite.UseNotamPrefix = chkNotamPrefix.Checked;
        mCurrentComposite.UseDecimalTerminology = chkUseDecimalTerminology.Checked;

        if (double.TryParse(vhfFrequency.Text, out var frequency))
        {
            frequency = frequency * 1000 * 1000;

            if ((frequency < 118000000) || (frequency > 137000000))
            {
                MessageBox.Show(this, "Invalid frequency range. The accepted frequency range is 118.000-137.000 MHz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (frequency != mCurrentComposite.Frequency)
            {
                mCurrentComposite.Frequency = (uint)frequency;
            }
        }

        if (typeCombined.Checked)
        {
            if (mAppConfig.CurrentProfile.Composites
                .Any(x => x.Identifier == mCurrentComposite.Identifier
                && x.AtisType == AtisType.Combined
                && x != mCurrentComposite))
            {
                MessageBox.Show(this, $"A Combined ATIS already exists for {mCurrentComposite.Identifier}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                mCurrentComposite.AtisType = AtisType.Combined;
            }
        }
        else if (typeDeparture.Checked)
        {
            if (mAppConfig.CurrentProfile.Composites
                .Any(x => x.Identifier == mCurrentComposite.Identifier
                && x.AtisType == AtisType.Departure
                && x != mCurrentComposite))
            {
                MessageBox.Show(this, $"A Departure ATIS already exists for {mCurrentComposite.Identifier}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                mCurrentComposite.AtisType = AtisType.Departure;
            }
        }
        else if (typeArrival.Checked)
        {
            if (mAppConfig.CurrentProfile.Composites
                .Any(x => x.Identifier == mCurrentComposite.Identifier
                && x.AtisType == AtisType.Arrival
                && x != mCurrentComposite))
            {
                MessageBox.Show(this, $"An Arrival ATIS already exists for {mCurrentComposite.Identifier}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                mCurrentComposite.AtisType = AtisType.Arrival;
            }
        }

        char codeRangeLow = char.Parse(txtCodeRangeLow.Text);
        char codeRangeHigh = char.Parse(txtCodeRangeHigh.Text);
        if (char.ToLower(codeRangeHigh) < char.ToLower(codeRangeLow))
        {
            MessageBox.Show("ATIS code range must be in sequential order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        mCurrentComposite.CodeRange.Low = (typeDeparture.Checked || typeArrival.Checked) ? char.Parse(txtCodeRangeLow.Text) : 'A';
        mCurrentComposite.CodeRange.High = (typeDeparture.Checked || typeArrival.Checked) ? char.Parse(txtCodeRangeHigh.Text) : 'Z';

        if (mCurrentPreset != null)
        {
            mCurrentPreset.ExternalGenerator.Enabled = chkExternalAtisGenerator.Checked;
        }

        mCurrentComposite.MagneticVariation = new MagneticVariationMeta
        {
            Enabled = chkMagneticVar.Checked,
            MagneticDegrees = (int)magneticVar.Value
        };

        var selectedVoice = ddlVoices.SelectedItem as VoiceMetaData;
        mCurrentComposite.AtisVoice.UseTextToSpeech = radioTextToSpeech.Checked;
        mCurrentComposite.AtisVoice.Voice = selectedVoice == null ? "Default" : selectedVoice.Name;

        // pending ATIS node format changes
        foreach (var node in mPendingVoiceTemplateChanges.ToList())
        {
            node.Item1.Template.Voice = node.Item2;
            mPendingVoiceTemplateChanges.Remove(node);
        }
        foreach (var node in mPendingTextTemplateChanges.ToList())
        {
            node.Item1.Template.Text = node.Item2;
            mPendingTextTemplateChanges.Remove(node);
        }

        mCurrentComposite.AtisFormat.ObservationTime.StandardUpdateTime = (int)standardObservationTime.Value;

        mCurrentComposite.AtisFormat.SurfaceWind.SpeakLeadingZero = chkWindSpeakLeadingZero.Checked;
        mCurrentComposite.AtisFormat.SurfaceWind.Calm.CalmWindSpeed = (int)calmWindSpeed.Value;

        mCurrentComposite.AtisFormat.Visibility.IncludeVisibilitySuffix = chkVisibilitySuffix.Checked;
        mCurrentComposite.AtisFormat.Visibility.North = visDirNorth.Text;
        mCurrentComposite.AtisFormat.Visibility.NorthEast = visDirNorthEast.Text;
        mCurrentComposite.AtisFormat.Visibility.East = visDirEast.Text;
        mCurrentComposite.AtisFormat.Visibility.SouthEast = visDirSouthEast.Text;
        mCurrentComposite.AtisFormat.Visibility.South = visDirSouth.Text;
        mCurrentComposite.AtisFormat.Visibility.SouthWest = visDirSouthWest.Text;
        mCurrentComposite.AtisFormat.Visibility.West = visDirWest.Text;
        mCurrentComposite.AtisFormat.Visibility.NorthWest = visDirNorthWest.Text;
        mCurrentComposite.AtisFormat.Visibility.UnlimitedVisibility = visUnlimited.Text;
        mCurrentComposite.AtisFormat.Visibility.MetersCutoff = (int)visibilityMetersCutoff.Value;

        List<string> usedContractions = new();
        foreach (DataGridViewRow row in gridContractions.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var stringValue = row.Cells[0].Value.ToString();
                    if (usedContractions.Contains(stringValue))
                    {
                        MessageBox.Show(this, $"Duplicate contraction: {stringValue}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridContractions.Focus();
                        return false;
                    }

                    usedContractions.Add(stringValue);
                }
                catch { }
            }
        }

        mCurrentComposite.Contractions.Clear();
        foreach (DataGridViewRow row in gridContractions.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var stringValue = row.Cells[0].Value.ToString();
                    var spokenValue = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(stringValue) && !string.IsNullOrEmpty(spokenValue))
                    {
                        mCurrentComposite.Contractions.Add(new ContractionMeta
                        {
                            String = stringValue,
                            Spoken = spokenValue
                        });
                    }
                }
            }
        }

        mCurrentComposite.AtisFormat.PresentWeather.LightIntensity = wxIntensityLight.Text;
        mCurrentComposite.AtisFormat.PresentWeather.ModerateIntensity = wxIntensityModerate.Text;
        mCurrentComposite.AtisFormat.PresentWeather.HeavyIntensity = wxIntensityHeavy.Text;
        mCurrentComposite.AtisFormat.PresentWeather.Vicinity = wxProximityVicinity.Text;

        List<string> usedWeatherTypes = new();
        foreach (DataGridViewRow row in gridWeatherTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    if (usedWeatherTypes.Contains(acronymValue))
                    {
                        MessageBox.Show(this, $"Duplicate weather type: {acronymValue}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridWeatherTypes.Focus();
                        return false;
                    }

                    usedWeatherTypes.Add(acronymValue);
                }
                catch { }
            }
        }

        mCurrentComposite.AtisFormat.PresentWeather.WeatherTypes.Clear();
        foreach (DataGridViewRow row in gridWeatherTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    var spokenValue = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(acronymValue) && !string.IsNullOrEmpty(spokenValue))
                    {
                        mCurrentComposite.AtisFormat.PresentWeather.WeatherTypes.Add(acronymValue, spokenValue);
                    }
                }
            }
        }

        List<string> usedWeatherDescriptors = new();
        foreach (DataGridViewRow row in gridWeatherDescriptors.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    if (usedWeatherDescriptors.Contains(acronymValue))
                    {
                        MessageBox.Show(this, $"Duplicate weather descriptor: {acronymValue}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridWeatherDescriptors.Focus();
                        return false;
                    }

                    usedWeatherDescriptors.Add(acronymValue);
                }
                catch { }
            }
        }

        mCurrentComposite.AtisFormat.PresentWeather.WeatherDescriptors.Clear();
        foreach (DataGridViewRow row in gridWeatherDescriptors.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    var spokenValue = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(acronymValue) && !string.IsNullOrEmpty(spokenValue))
                    {
                        mCurrentComposite.AtisFormat.PresentWeather.WeatherDescriptors.Add(acronymValue, spokenValue);
                    }
                }
            }
        }

        mCurrentComposite.AtisFormat.Clouds.IdentifyCeilingLayer = chkIdentifyCeilingLayer.Checked;
        mCurrentComposite.AtisFormat.Clouds.ConvertToMetric = chkConvertCloudsMetric.Checked;

        List<string> usedCloudTypes = new();
        foreach (DataGridViewRow row in gridCloudTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    if (usedCloudTypes.Contains(acronymValue))
                    {
                        MessageBox.Show(this, $"Duplicate cloud type: {acronymValue}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridCloudTypes.Focus();
                        return false;
                    }

                    usedCloudTypes.Add(acronymValue);
                }
                catch { }
            }
        }

        mCurrentComposite.AtisFormat.Clouds.Types.Clear();
        foreach (DataGridViewRow row in gridCloudTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    var spokenValue = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(acronymValue) && !string.IsNullOrEmpty(spokenValue))
                    {
                        mCurrentComposite.AtisFormat.Clouds.Types.Add(acronymValue, spokenValue);
                    }
                }
            }
        }

        List<string> usedConvectiveCloudTypes = new();
        foreach (DataGridViewRow row in gridConvectiveCloudTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    if (usedConvectiveCloudTypes.Contains(acronymValue))
                    {
                        MessageBox.Show(this, $"Duplicate convective cloud type: {acronymValue}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridConvectiveCloudTypes.Focus();
                        return false;
                    }

                    usedConvectiveCloudTypes.Add(acronymValue);
                }
                catch { }
            }
        }

        mCurrentComposite.AtisFormat.Clouds.ConvectiveTypes.Clear();
        foreach (DataGridViewRow row in gridConvectiveCloudTypes.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    var acronymValue = row.Cells[0].Value.ToString();
                    var spokenValue = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(acronymValue) && !string.IsNullOrEmpty(spokenValue))
                    {
                        mCurrentComposite.AtisFormat.Clouds.ConvectiveTypes.Add(acronymValue, spokenValue);
                    }
                }
            }
        }

        List<Tuple<int, int>> usedTransitionLevels = new();
        foreach (DataGridViewRow row in gridTransitionLevels.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var trLow = int.Parse(row.Cells[0].Value.ToString());
                    var trHigh = int.Parse(row.Cells[1].Value.ToString());
                    if (usedTransitionLevels.Any(t => t.Item1 == trLow && t.Item2 == trHigh))
                    {
                        MessageBox.Show(this, $"Duplicate Transition Level: {trLow}-{trHigh}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        gridTransitionLevels.Focus();
                        return false;
                    }

                    usedTransitionLevels.Add(new Tuple<int, int>(trLow, trHigh));
                }
                catch { }
            }
        }

        mCurrentComposite.TransitionLevels.Clear();
        foreach (DataGridViewRow row in gridTransitionLevels.Rows)
        {
            if (!row.IsNewRow)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    var trLow = int.Parse(row.Cells[0].Value.ToString());
                    var trHigh = int.Parse(row.Cells[1].Value.ToString());
                    var tl = int.Parse(row.Cells[2].Value.ToString());
                    mCurrentComposite.TransitionLevels.Add(new TransitionLevelMeta
                    {
                        Low = trLow,
                        High = trHigh,
                        Altitude = tl
                    });
                }
            }
        }

        mCurrentComposite.AtisFormat.Temperature.UsePlusPrefix = chkTempPlusPrefix.Checked;
        mCurrentComposite.AtisFormat.Temperature.PronounceLeadingZero = chkTempLeadingZero.Checked;
        mCurrentComposite.AtisFormat.Dewpoint.UsePlusPrefix = chkDewPlusPrefix.Checked;
        mCurrentComposite.AtisFormat.Dewpoint.PronounceLeadingZero = chkDewLeadingZero.Checked;

        mCurrentComposite.AtisFormat.ClosingStatement.AutoIncludeClosingStatement = chkAutoIncludeClosingStatement.Checked;

        btnApply.Enabled = false;
        btnOK.Enabled = true;

        mAppConfig.SaveConfig();

        return true;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (!btnApply.Enabled || ApplyChanges())
        {
            EventBus.Publish(this, new RefreshCompositesRequested());
            Close();
        }
    }

    private void AddDynamicPresetControl()
    {
        mPresetControl = chkExternalAtisGenerator.Checked
            ? mWindowFactory.CreateExternalAtisGeneratorControl()
            : mWindowFactory.CreateAtisTemplateControl() as Control;
        mPresetControl.Dock = DockStyle.Fill;
        dynamicPresetControl.Controls.Clear();
        dynamicPresetControl.Controls.Add(mPresetControl);

        if (chkExternalAtisGenerator.Checked)
        {
            if (mPresetControl is ExternalAtisGenerator control)
            {
                control.Composite = mCurrentComposite;
                control.ExternalUrl = mCurrentPreset.ExternalGenerator.Url;
                control.ArrivalRunways = mCurrentPreset.ExternalGenerator.Arrival;
                control.DepartureRunways = mCurrentPreset.ExternalGenerator.Departure;
                control.Approaches = mCurrentPreset.ExternalGenerator.Approaches;
                control.Remarks = mCurrentPreset.ExternalGenerator.Remarks;
            }
        }
        else
        {
            if (mPresetControl is AtisTemplate control)
            {
                control.Template = mCurrentPreset.Template;
            }
        }
    }

    private void PopulateSelectedPreset()
    {
        mCurrentPreset = mCurrentComposite.Presets.FirstOrDefault(x => x.Id.ToString() == ddlPresets.SelectedValue.ToString());

        if (mCurrentPreset != null)
        {
            chkExternalAtisGenerator.Enabled = true;
            chkExternalAtisGenerator.Checked = mCurrentPreset.ExternalGenerator.Enabled;

            AddDynamicPresetControl();
        }
    }

    private void radioTextToSpeech_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!radioTextToSpeech.Focused)
            return;

        ddlVoices.Enabled = radioTextToSpeech.Checked;
        btnApply.Enabled = true;
    }

    private void btnNewPreset_Click(object sender, EventArgs e)
    {
        bool flag = false;
        var previousValue = "";

        while (!flag)
        {
            using var dlg = mWindowFactory.CreateUserInputDialog();
            dlg.PromptLabel = "Enter a name for the preset";
            dlg.WindowTitle = "New Preset";
            dlg.InitialValue = previousValue;
            dlg.TextUppercase = true;
            dlg.TopMost = mAppConfig.WindowProperties.TopMost;

            DialogResult result = dlg.ShowDialog(this);
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
            {
                previousValue = dlg.Value;

                if (mCurrentComposite.Presets.Any(x => x.Name == dlg.Value))
                {
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose a new name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var preset = new Preset
                {
                    Name = dlg.Value,
                    Template = "[FACILITY] ATIS INFO [ATIS_CODE] [OBS_TIME]. [FULL_WX_STRING]. [ARPT_COND] [NOTAMS]"
                };
                mCurrentComposite.Presets.Add(preset);
                mAppConfig.SaveConfig();

                RefreshPresetList(preset.Name);
                previousValue = "";

                flag = true;
            }
            else
            {
                flag = true;
            }
        }
    }

    private void btnRenamePreset_Click(object sender, EventArgs e)
    {
        bool flag = false;

        var previousValue = mCurrentPreset.Name;

        while (!flag)
        {
            using var dlg = mWindowFactory.CreateUserInputDialog();
            dlg.PromptLabel = "Enter a new name for the preset";
            dlg.WindowTitle = "Rename Preset";
            dlg.InitialValue = previousValue;
            dlg.TextUppercase = true;
            dlg.TopMost = mAppConfig.WindowProperties.TopMost;

            DialogResult result = dlg.ShowDialog(this);
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
            {
                previousValue = dlg.Value;

                if (mCurrentComposite.Presets.Any(x => x.Name == dlg.Value))
                {
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose a new name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                }
                else
                {
                    mCurrentPreset.Name = dlg.Value;
                    mAppConfig.SaveConfig();

                    RefreshPresetList();
                    previousValue = "";

                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
        }
    }

    private void btnCopyPreset_Click(object sender, EventArgs e)
    {
        bool flag = false;
        var previousValue = "";

        while (!flag)
        {
            using var dlg = mWindowFactory.CreateUserInputDialog();
            dlg.PromptLabel = "Enter a name for the preset";
            dlg.WindowTitle = "Copy Preset";
            dlg.InitialValue = previousValue;
            dlg.TextUppercase = true;
            dlg.TopMost = mAppConfig.WindowProperties.TopMost;

            DialogResult result = dlg.ShowDialog(this);
            if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
            {
                previousValue = dlg.Value;

                if (mCurrentComposite.Presets.Any(x => x.Name == dlg.Value))
                {
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose a new name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var clone = (Preset)mCurrentPreset.Clone();
                    clone.Name = dlg.Value;
                    mCurrentComposite.Presets.Add(clone);
                    mAppConfig.SaveConfig();

                    RefreshPresetList();
                    previousValue = "";

                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
        }
    }

    private void btnDeletePreset_Click(object sender, EventArgs e)
    {
        if (ddlPresets.SelectedItem != null)
        {
            if (MessageBox.Show(this, "Are you sure you want to delete the selected preset?", "Delete Preset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                mCurrentComposite.Presets.RemoveAll(x => x.Name == ddlPresets.SelectedItem.ToString());
                mAppConfig.SaveConfig();
                RefreshPresetList();
            }
        }
    }

    private void RefreshPresetList(string selectNewPreset = "")
    {
        if (mCurrentComposite != null)
        {
            ddlPresets.DataSource = null;
            ddlPresets.Items.Clear();

            ddlPresets.DataSource = mCurrentComposite.Presets;
            ddlPresets.DisplayMember = "Name";
            ddlPresets.ValueMember = "Id";

            if (mPresetControl != null)
                dynamicPresetControl.Controls.Remove(mPresetControl);

            if (!string.IsNullOrEmpty(selectNewPreset))
            {
                ddlPresets.SelectedIndex = ddlPresets.FindStringExact(selectNewPreset);
                PopulateSelectedPreset();
            }
            else if (mCurrentComposite.CurrentPreset != null)
            {
                ddlPresets.SelectedIndex = ddlPresets.FindStringExact(mCurrentComposite.CurrentPreset.Name);
                PopulateSelectedPreset();
            }
            else
            {
                ddlPresets.SelectedIndex = -1;
            }
        }

        btnCopyPreset.Enabled = ddlPresets.SelectedItem != null;
        btnDeletePreset.Enabled = ddlPresets.SelectedItem != null;
        btnRenamePreset.Enabled = ddlPresets.SelectedItem != null;

        EventBus.Publish(this, new RefreshCompositesRequested());
    }

    private void ddlPresets_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnCopyPreset.Enabled = ddlPresets.SelectedItem != null;
        btnDeletePreset.Enabled = ddlPresets.SelectedItem != null;
        btnRenamePreset.Enabled = ddlPresets.SelectedItem != null;

        if (ddlPresets.SelectedItem != null)
        {
            PopulateSelectedPreset();
        }
        else
        {
            chkExternalAtisGenerator.Enabled = false;
            chkExternalAtisGenerator.Checked = false;
        }
    }

    private void gridContractions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
            return;
        }
    }

    private void gridContractions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        TextBox textBox = e.Control as TextBox;
        if (textBox != null)
        {
            textBox.CharacterCasing = CharacterCasing.Upper;
        }
    }

    private void btnDeleteContraction_Click(object sender, EventArgs e)
    {
        if (gridContractions.SelectedRows.Count == 1)
        {
            if (!gridContractions.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected contraction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridContractions.Rows.RemoveAt(gridContractions.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No contraction selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void gridContractions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridContractions.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridContractions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridContractions_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected contraction?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridContractions_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void ctxImport_Click(object sender, EventArgs e)
    {
        try
        {
            var dialog = new OpenFileDialog
            {
                Title = "Import vATIS Composite",
                CheckFileExists = true,
                CheckPathExists = true,
                AddExtension = false,
                Multiselect = true,
                Filter = "Legacy vATIS Profile (*.gz)|*.gz|vATIS Composite (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1,
                DefaultExt = "gz",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                ShowHelp = false
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (dialog.FileNames.Length > Constants.MAX_COMPOSITES)
                {
                    MessageBox.Show(this, $"A maximum of {Constants.MAX_COMPOSITES} composites can be imported into a single profile", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var file in dialog.FileNames.Take(Constants.MAX_COMPOSITES))
                {
                    var fileInfo = new FileInfo(file);
                    switch (fileInfo.Extension)
                    {
                        case ".gz":
                            ImportLegacyProfile(fileInfo.FullName);
                            break;
                        case ".json":
                            ImportComposite(fileInfo.FullName);
                            break;
                        default:
                            throw new Exception("Unknown file type");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Composite Import Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ImportComposite(string fullName)
    {
        try
        {
            var composite = new Composite();

            using var fs = new FileStream(fullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                composite = JsonConvert.DeserializeObject<Composite>(sr.ReadToEnd(), new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }

            if (mAppConfig.CurrentProfile != null)
            {
                if (mAppConfig.CurrentProfile.Composites.Any(t => t.Identifier == composite.Identifier))
                {
                    if (MessageBox.Show(this, $"A composite already exists for {composite.Identifier}. Do you want to overwrite it?", "Duplicate Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        mAppConfig.CurrentProfile.Composites.RemoveAll(t => t.Identifier == composite.Identifier);
                        mAppConfig.CurrentProfile.Composites.Add(composite);
                        mAppConfig.SaveConfig();
                        RefreshCompositeList();
                        LoadComposite();
                    }
                }
                else
                {
                    mAppConfig.CurrentProfile.Composites.Add(composite);
                    mAppConfig.SaveConfig();
                    RefreshCompositeList();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Import Error: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ImportLegacyProfile(string fullName)
    {
        FileStream fs = new(fullName, FileMode.Open, FileAccess.Read);
        using Stream stream = new GZipStream(fs, CompressionMode.Decompress);
        XmlSerializer xml = new(typeof(LegacyFacility));
        var profile = (LegacyFacility)xml.Deserialize(stream);

        if (mAppConfig.CurrentProfile != null)
        {
            if (mAppConfig.CurrentProfile.Composites.Any(t => t.Identifier == profile.ID))
            {
                if (MessageBox.Show(this, "A composite with that identifier already exists. Would you like to overwrite it?", "Duplicate Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                var existing = mAppConfig.CurrentProfile.Composites.FirstOrDefault(t => t.Identifier == profile.ID);
                if (existing != null)
                {
                    existing.Name = profile.Name;
                    existing.Identifier = profile.ID;
                    if (!string.IsNullOrEmpty(profile.AtisFrequency))
                    {
                        existing.Frequency = (uint)(double.Parse(profile.AtisFrequency) * 1000);
                    }
                    else
                    {
                        existing.Frequency = (uint)(profile.Frequency + 100000) * 1000;
                    }

                    existing.IDSEndpoint = profile.InformationDisplaySystemEndpoint;
                    existing.AtisVoice.UseTextToSpeech = !profile.VoiceRecordEnabled;

                    if (profile.MagneticVariation != null)
                    {
                        if (profile.MagneticVariation.Option !=
                            LegacyMagneticVariation.LegacyMagneticVariationOption.None)
                        {
                            existing.MagneticVariation.Enabled = true;
                        }

                        switch (profile.MagneticVariation.Option)
                        {
                            case LegacyMagneticVariation.LegacyMagneticVariationOption.Add:
                                existing.MagneticVariation.MagneticDegrees =
                                    Math.Abs(profile.MagneticVariation.MagneticVariationValue);
                                break;
                            case LegacyMagneticVariation.LegacyMagneticVariationOption.Subtract:
                                existing.MagneticVariation.MagneticDegrees =
                                    profile.MagneticVariation.MagneticVariationValue * -1;
                                break;
                        }
                    }

                    if (profile.MetarObservation != null)
                    {
                        existing.AtisFormat.ObservationTime.StandardUpdateTime = profile.MetarObservation.ObservationTimeValue;
                    }

                    existing.Contractions.Clear();
                    foreach (var contraction in profile.Contractions)
                    {
                        existing.Contractions.Add(new ContractionMeta
                        {
                            String = contraction.Key,
                            Spoken = contraction.Value
                        });
                    }

                    int idx = 1;
                    existing.AirportConditionDefinitions.Clear();
                    foreach (var condition in profile.AirportConditions)
                    {
                        existing.AirportConditionDefinitions.Add(new DefinedTextMeta
                        {
                            Ordinal = idx++,
                            Text = condition.Message,
                            Enabled = condition.IsSelected
                        });
                    }

                    idx = 1;
                    existing.NotamDefinitions.Clear();
                    foreach (var condition in profile.Notams)
                    {
                        existing.NotamDefinitions.Add(new DefinedTextMeta
                        {
                            Ordinal = idx++,
                            Text = condition.Message,
                            Enabled = condition.IsSelected
                        });
                    }

                    existing.Presets.Clear();
                    foreach (var preset in profile.Profiles)
                    {
                        var p = new Preset
                        {
                            Name = preset.Name,
                            Template = preset.AtisTemplate,
                            AirportConditions = preset.AirportConditions,
                            Notams = preset.Notams
                        };
                        existing.Presets.Add(p);
                    }

                    mAppConfig.SaveConfig();
                    RefreshCompositeList();
                    LoadComposite();
                }
            }
            else
            {
                var composite = new Composite();
                composite.Name = profile.Name;
                composite.Identifier = profile.ID;
                if (!string.IsNullOrEmpty(profile.AtisFrequency))
                {
                    composite.Frequency = (uint)(double.Parse(profile.AtisFrequency) * 1000);
                }
                else
                {
                    composite.Frequency = (uint)(profile.Frequency + 100000) * 1000;
                }

                composite.IDSEndpoint = profile.InformationDisplaySystemEndpoint;
                composite.AtisVoice.UseTextToSpeech = !profile.VoiceRecordEnabled;

                if (profile.MagneticVariation != null)
                {
                    if (profile.MagneticVariation.Option != LegacyMagneticVariation.LegacyMagneticVariationOption.None)
                    {
                        composite.MagneticVariation.Enabled = true;
                    }

                    switch (profile.MagneticVariation.Option)
                    {
                        case LegacyMagneticVariation.LegacyMagneticVariationOption.Add:
                            composite.MagneticVariation.MagneticDegrees =
                                Math.Abs(profile.MagneticVariation.MagneticVariationValue);
                            break;
                        case LegacyMagneticVariation.LegacyMagneticVariationOption.Subtract:
                            composite.MagneticVariation.MagneticDegrees =
                                profile.MagneticVariation.MagneticVariationValue * -1;
                            break;
                    }
                }

                if (profile.MetarObservation != null)
                {
                    composite.AtisFormat.ObservationTime.StandardUpdateTime = profile.MetarObservation.ObservationTimeValue;
                }

                foreach (var contraction in profile.Contractions)
                {
                    composite.Contractions.Add(new ContractionMeta
                    {
                        String = contraction.Key,
                        Spoken = contraction.Value
                    });
                }

                int idx = 1;
                foreach (var condition in profile.AirportConditions)
                {
                    composite.AirportConditionDefinitions.Add(new DefinedTextMeta
                    {
                        Ordinal = idx++,
                        Text = condition.Message,
                        Enabled = condition.IsSelected
                    });
                }

                idx = 1;
                foreach (var condition in profile.Notams)
                {
                    composite.NotamDefinitions.Add(new DefinedTextMeta
                    {
                        Ordinal = idx++,
                        Text = condition.Message,
                        Enabled = condition.IsSelected
                    });
                }

                foreach (var preset in profile.Profiles)
                {
                    var p = new Preset
                    {
                        Name = preset.Name,
                        Template = preset.AtisTemplate,
                        AirportConditions = preset.AirportConditions,
                        Notams = preset.Notams
                    };
                    composite.Presets.Add(p);
                }

                mAppConfig.CurrentProfile.Composites.Add(composite);
                mAppConfig.SaveConfig();
                RefreshCompositeList();
            }
        }
    }

    private void ctxExport_Click(object sender, EventArgs e)
    {
        if (mCurrentComposite != null)
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = $"vATIS Composite - {mCurrentComposite.Name} ({mCurrentComposite.AtisType}).json",
                Filter = "vATIS Composite (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1,
                CheckPathExists = true,
                DefaultExt = "json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                OverwritePrompt = true,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                Title = "Export Composite",
                ValidateNames = true
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, JsonConvert.SerializeObject(mCurrentComposite, Formatting.Indented));
                MessageBox.Show(this, "Composite exported successfully.", "Success", MessageBoxButtons.OK);
            }
        }
    }

    private void gridTransitionLevels_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridTransitionLevels.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridTransitionLevels_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
        }
    }

    private void gridTransitionLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridTransitionLevels_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        TextBox textBox = e.Control as TextBox;
        if (textBox != null)
        {
            textBox.KeyPress += (sender, arg) =>
            {
                if (!char.IsControl(arg.KeyChar) && !char.IsDigit(arg.KeyChar))
                {
                    arg.Handled = true;
                }
            };
        }
    }

    private void btnDeleteTransitionLevel_Click(object sender, EventArgs e)
    {
        if (gridTransitionLevels.SelectedRows.Count == 1)
        {
            if (!gridTransitionLevels.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected transition level?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridTransitionLevels.Rows.RemoveAt(gridTransitionLevels.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No transition level selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AlphaOnly(object sender, KeyPressEventArgs e)
    {
        e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
    }

    private void OnVoiceTemplateChanged(object sender, EventArgs e)
    {
        var template = (sender as NodeFormatTemplate);

        if (template.NodeType.StartsWith("SurfaceWind"))
        {
            var windType = template.NodeType.Split('.')[1];
            if (mCurrentComposite.AtisFormat.SurfaceWind.GetType().GetProperty(windType)?.GetValue(mCurrentComposite.AtisFormat.SurfaceWind, null) is BaseFormat node)
            {
                if (node.Template.Voice != template.VoiceTemplate)
                {
                    btnApply.Enabled = true;
                    mPendingVoiceTemplateChanges.Add(new Tuple<BaseFormat, string>(node, template.VoiceTemplate));
                }
            }
        }
        else
        {
            if (mCurrentComposite.AtisFormat.GetType().GetProperty(template.NodeType)?.GetValue(mCurrentComposite.AtisFormat, null) is BaseFormat node)
            {
                if (node.Template.Voice != template.VoiceTemplate)
                {
                    btnApply.Enabled = true;
                    mPendingVoiceTemplateChanges.Add(new Tuple<BaseFormat, string>(node, template.VoiceTemplate));
                }
            }
        }
    }

    private void OnTextTemplateChanged(object sender, EventArgs e)
    {
        var template = (sender as NodeFormatTemplate);

        if (template.NodeType.StartsWith("SurfaceWind"))
        {
            var windType = template.NodeType.Split('.')[1];
            if (mCurrentComposite.AtisFormat.SurfaceWind.GetType().GetProperty(windType)?.GetValue(mCurrentComposite.AtisFormat.SurfaceWind, null) is BaseFormat node)
            {
                if (node.Template.Text != template.TextTemplate)
                {
                    btnApply.Enabled = true;
                    mPendingTextTemplateChanges.Add(new Tuple<BaseFormat, string>(node, template.TextTemplate));
                }
            }
        }
        else
        {
            if (mCurrentComposite.AtisFormat.GetType().GetProperty(template.NodeType)?.GetValue(mCurrentComposite.AtisFormat, null) is BaseFormat node)
            {
                if (node.Template.Text != template.TextTemplate)
                {
                    btnApply.Enabled = true;
                    mPendingTextTemplateChanges.Add(new Tuple<BaseFormat, string>(node, template.TextTemplate));
                }
            }
        }
    }

    private void btnDeleteWeatherType_Click(object sender, EventArgs e)
    {
        if (gridWeatherTypes.SelectedRows.Count == 1)
        {
            if (!gridWeatherTypes.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected weather type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridWeatherTypes.Rows.RemoveAt(gridWeatherTypes.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No weather type selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void gridWeatherTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridWeatherTypes.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridWeatherTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridWeatherTypes_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected weather type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridWeatherTypes_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridWeatherTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
        }
    }

    private void gridWeatherTypes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is TextBox textBox)
        {
            textBox.CharacterCasing = gridWeatherTypes.CurrentCell.ColumnIndex == 0
                ? CharacterCasing.Upper
                : CharacterCasing.Normal;
        }
    }

    private void btnDeleteWeatherDescriptor_Click(object sender, EventArgs e)
    {
        if (gridWeatherDescriptors.SelectedRows.Count == 1)
        {
            if (!gridWeatherDescriptors.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected weather descriptor?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridWeatherDescriptors.Rows.RemoveAt(gridWeatherDescriptors.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No weather descriptor selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void gridWeatherDescriptors_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridWeatherDescriptors.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridWeatherDescriptors_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridWeatherDescriptors_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected weather descriptor?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridWeatherDescriptors_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridWeatherDescriptors_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
        }
    }

    private void gridWeatherDescriptors_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is TextBox textBox)
        {
            textBox.CharacterCasing = gridWeatherDescriptors.CurrentCell.ColumnIndex == 0
                ? CharacterCasing.Upper
                : CharacterCasing.Normal;
        }
    }

    private void btnDeleteCloudType_Click(object sender, EventArgs e)
    {
        if (gridCloudTypes.SelectedRows.Count == 1)
        {
            if (!gridCloudTypes.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected cloud type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridCloudTypes.Rows.RemoveAt(gridCloudTypes.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No cloud type selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void gridCloudTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridCloudTypes.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridCloudTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridCloudTypes_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected cloud type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridCloudTypes_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridCloudTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
        }
    }

    private void gridCloudTypes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is TextBox textBox)
        {
            textBox.CharacterCasing = gridCloudTypes.CurrentCell.ColumnIndex == 0
                ? CharacterCasing.Upper
                : CharacterCasing.Normal;
        }
    }

    private void btnDeleteConvectiveCloudType_Click(object sender, EventArgs e)
    {
        if (gridConvectiveCloudTypes.SelectedRows.Count == 1)
        {
            if (!gridConvectiveCloudTypes.SelectedRows[0].IsNewRow && MessageBox.Show(this, "Are you sure you want to delete the selected convective cloud type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                gridConvectiveCloudTypes.Rows.RemoveAt(gridConvectiveCloudTypes.SelectedRows[0].Index);
                btnApply.Enabled = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No convective cloud type selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void gridConvectiveCloudTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridConvectiveCloudTypes.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridConvectiveCloudTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridConvectiveCloudTypes_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected convective cloud type?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridConvectiveCloudTypes_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
    }

    private void gridConvectiveCloudTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
        }
    }

    private void gridConvectiveCloudTypes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is TextBox textBox)
        {
            textBox.CharacterCasing = gridConvectiveCloudTypes.CurrentCell.ColumnIndex == 0
                ? CharacterCasing.Upper
                : CharacterCasing.Normal;
        }
    }

    private void HandleControlValueChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!(sender as Control).Focused)
            return;

        if ((sender as Control).Name == "chkExternalAtisGenerator")
        {
            AddDynamicPresetControl();
        }

        ddlVoices.Enabled = !radioVoiceRecorded.Checked;
        txtCodeRangeLow.Enabled = typeDeparture.Checked || typeArrival.Checked;
        txtCodeRangeHigh.Enabled = typeDeparture.Checked || typeArrival.Checked;
        magneticVar.Enabled = chkMagneticVar.Checked;

        btnApply.Enabled = true;
    }
}