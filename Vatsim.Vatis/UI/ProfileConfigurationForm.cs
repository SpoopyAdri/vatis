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

    private bool mFrequencyChanged = false;
    private bool mAtisTypeChanged = false;
    private bool mCodeRangeLowChanged = false;
    private bool mCodeRangeHighChanged = false;
    private bool mFaaFormatChanged = false;
    private bool mObservationTimeChanged = false;
    private bool mMagneticVariationChanged = false;
    private bool mVoiceOptionsChanged = false;
    private bool mIdsEndpointChanged = false;
    private bool mContractionsChanged = false;
    private bool mTransitionLevelsChanged = false;
    private bool mExternalAtisGeneratorChanged = false;
    private bool mPrefixNotamsChanged = false;
    private bool mTransitionLevelPrefixChanged = false;
    private bool mConvertMetricChanged = false;
    private bool mSurfaceWindPrefixChanged = false;
    private bool mVisibilitySuffixChanged = false;
    private bool mUseDecimalTerminologyChanged = false;
    private bool mUseTemperaturePlusPrefixChanged = false;
    private bool mUseZuluTimeSuffixChanged = false;

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
                var composite = TreeMenu.SelectedNode.Tag as Composite;

                if (composite == null)
                    return;

                mCurrentComposite = mAppConfig.CurrentProfile.Composites
                    .FirstOrDefault(x => x.Id == composite.Id);

                LoadComposite();
                RefreshPresetList();
            }
        }
    }

    private void LoadComposite()
    {
        vhfFrequency.Text = ((mCurrentComposite.AtisFrequency + 100000) / 1000.0).ToString("000.000");

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

        chkFaaFormat.Checked = mCurrentComposite.UseFaaFormat;
        chkPrefixNotams.Checked = mCurrentComposite.UseNotamPrefix;
        chkTransitionLevelPrefix.Checked = mCurrentComposite.UseTransitionLevelPrefix;
        chkConvertMetric.Checked = mCurrentComposite.UseMetricUnits;
        chkVisibilitySuffix.Checked = mCurrentComposite.UseVisibilitySuffix;
        chkSurfaceWindPrefix.Checked = mCurrentComposite.UseSurfaceWindPrefix;
        chkDecimalTerminology.Checked = mCurrentComposite.UseDecimalTerminology;
        chkPrefixTemperature.Checked = mCurrentComposite.UseTemperaturePlusPrefix;
        chkAppendZulu.Checked = mCurrentComposite.UseZuluTimeSuffix;

        if (mCurrentComposite.ObservationTime != null)
        {
            chkObservationTime.Checked = mCurrentComposite.ObservationTime.Enabled;
            observationTime.Value = mCurrentComposite.ObservationTime.Time;
            observationTime.Enabled = chkObservationTime.Checked;
        }

        if (mCurrentComposite.MagneticVariation != null)
        {
            chkMagneticVar.Checked = mCurrentComposite.MagneticVariation.Enabled;
            magneticVar.Value = mCurrentComposite.MagneticVariation.MagneticDegrees;
            magneticVar.Enabled = chkMagneticVar.Checked;
        }

        groupVoiceOption.Enabled = !mCurrentComposite.Connection.IsConnected;

        if (mCurrentComposite.AtisVoice != null)
        {
            radioTextToSpeech.Checked = mCurrentComposite.AtisVoice.UseTextToSpeech == true;
            radioVoiceRecorded.Checked = mCurrentComposite.AtisVoice.UseTextToSpeech == false;

            if (radioTextToSpeech.Checked)
            {
                ddlVoices.Enabled = true;

                var voices = ddlVoices.DataSource as List<VoiceMetaData>;
                if(!voices.Any(n => n.Name == mCurrentComposite.AtisVoice.Voice))
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

        txtIdsEndpoint.Text = mCurrentComposite.IDSEndpoint;

        gridContractions.Rows.Clear();
        foreach (var contraction in mCurrentComposite.Contractions)
        {
            gridContractions.Rows.Add(new object[]
            {
                contraction.String,
                contraction.Spoken
            });
        }

        gridTransitionLevels.Rows.Clear();
        foreach (var tl in mCurrentComposite.TransitionLevels.OrderByDescending(t => t.Low)
                     .ThenByDescending(t => t.High))
        {
            gridTransitionLevels.Rows.Add(new object[]
            {
                tl.Low,
                tl.High,
                tl.Altitude
            });
        }

        if (mCurrentComposite.Identifier.StartsWith("K") || mCurrentComposite.Identifier.StartsWith("P"))
        {
            pageTransitionLevel.SetVisible(false);
        }
        else
        {
            pageTransitionLevel.SetVisible(true);
        }

        ToggleNonFaaOptions();
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
            MessageBox.Show("The maximum ATIS composite count has been exceeded for this profile.", "Error");
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
                    if (MessageBox.Show(this, $"ICAO identifier not found: {dlg.Identifier}", "Invalid Identifier",
                            MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                    {
                        continue;
                    }
                }

                if (mAppConfig.CurrentProfile != null
                    && mAppConfig.CurrentProfile.Composites.Any(x => x.Identifier == dlg.Identifier
                                                                     && x.AtisType == dlg.Type))
                {
                    if (MessageBox.Show(this,
                            $"{dlg.Identifier} ({dlg.Type}) already exists. Would you like to overwrite it?",
                            "Duplicate Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    {
                        continue;
                    }
                    else
                    {
                        mAppConfig.CurrentProfile.Composites.RemoveAll(x =>
                            x.Identifier == dlg.Identifier && x.AtisType == dlg.Type);
                        mAppConfig.SaveConfig();
                    }
                }

                var composite = new Composite
                {
                    Identifier = dlg.Identifier,
                    Name = dlg.CompositeName,
                    AtisType = dlg.Type,
                    UseFaaFormat = dlg.Identifier.StartsWith("K") || dlg.Identifier.StartsWith("P"),
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
            MessageBox.Show("The maximum ATIS composite count has been exceeded for this profile.", "Error");
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
                        if (MessageBox.Show(this, $"ICAO identifier not found: {dlg.Identifier}", "Invalid Identifier",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
                        {
                            continue;
                        }
                    }

                    if (mAppConfig.CurrentProfile != null
                        && mAppConfig.CurrentProfile.Composites.Any(x => x.Identifier == dlg.Identifier
                                                                         && x.AtisType == dlg.Type))
                    {
                        if (MessageBox.Show(this, $"{dlg.Identifier} ({dlg.Type}) already exists.",
                                "Duplicate Composite", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
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

            if (MessageBox.Show(this,
                    string.Format(
                        $"Are you sure you want to delete the selected ATIS Composite? This action will also delete all associated ATIS presets.\r\n\r\n{composite.Identifier} {composite.AtisType}"),
                    "Delete ATIS Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
        observationTime.Value = 0;
        magneticVar.Value = 0;
        chkMagneticVar.Checked = false;
        chkObservationTime.Checked = false;
        txtIdsEndpoint.Text = "";

        pageTransitionLevel.SetVisible(false);

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
            if (MessageBox.Show(this, "You have unsaved changes. Are you sure you want to cancel?", "Unsaved Changes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
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
            MessageBox.Show("IDS endpoint URL is not a valid hyperlink format.");
            return false;
        }

        if (mFrequencyChanged)
        {
            if (decimal.TryParse(vhfFrequency.Text, out var frequency))
            {
                frequency = frequency.ToVatsimFrequencyFormat();
                if (frequency < 18000 || frequency > 37000)
                {
                    MessageBox.Show(this,
                        "Invalid frequency range. The accepted frequency range is 118.000-137.000 MHz", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                else
                {
                    mCurrentComposite.AtisFrequency = (int)frequency;
                    mFrequencyChanged = false;
                }
            }
        }

        if (mAtisTypeChanged)
        {
            if (typeCombined.Checked)
            {
                if (mAppConfig.CurrentProfile.Composites.Any(x =>
                        x.Identifier == mCurrentComposite.Identifier && x.AtisType == AtisType.Combined &&
                        x != mCurrentComposite))
                {
                    MessageBox.Show(this, $"A Combined ATIS already exists for {mCurrentComposite.Identifier}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                else
                {
                    mCurrentComposite.AtisType = AtisType.Combined;
                    mAtisTypeChanged = false;
                }
            }
            else if (typeDeparture.Checked)
            {
                if (mAppConfig.CurrentProfile.Composites.Any(x =>
                        x.Identifier == mCurrentComposite.Identifier && x.AtisType == AtisType.Departure &&
                        x != mCurrentComposite))
                {
                    MessageBox.Show(this, $"A Departure ATIS already exists for {mCurrentComposite.Identifier}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                else
                {
                    mCurrentComposite.AtisType = AtisType.Departure;
                    mAtisTypeChanged = false;
                }
            }
            else if (typeArrival.Checked)
            {
                if (mAppConfig.CurrentProfile.Composites.Any(x =>
                        x.Identifier == mCurrentComposite.Identifier && x.AtisType == AtisType.Arrival &&
                        x != mCurrentComposite))
                {
                    MessageBox.Show(this, $"An Arrival ATIS already exists for {mCurrentComposite.Identifier}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                else
                {
                    mCurrentComposite.AtisType = AtisType.Arrival;
                    mAtisTypeChanged = false;
                }
            }
        }

        if (mCodeRangeLowChanged || mCodeRangeHighChanged)
        {
            char low = char.Parse(txtCodeRangeLow.Text);
            char high = char.Parse(txtCodeRangeHigh.Text);
            if (char.ToLower(high) < char.ToLower(low))
            {
                MessageBox.Show("ATIS code range must be in alphabetical order.");
                return false;
            }
        }

        if (mCodeRangeLowChanged)
        {
            mCurrentComposite.CodeRange.Low = char.Parse(txtCodeRangeLow.Text);
            mCodeRangeLowChanged = false;
        }

        if (mCodeRangeHighChanged)
        {
            mCurrentComposite.CodeRange.High = char.Parse(txtCodeRangeHigh.Text);
            mCodeRangeHighChanged = false;
        }

        if (mFaaFormatChanged)
        {
            mCurrentComposite.UseFaaFormat = chkFaaFormat.Checked;
            mFaaFormatChanged = false;
        }

        if (mExternalAtisGeneratorChanged)
        {
            mCurrentPreset.ExternalGenerator.Enabled = chkExternalAtisGenerator.Checked;
            mExternalAtisGeneratorChanged = false;
        }

        if (mObservationTimeChanged)
        {
            var meta = new ObservationTimeMeta
            {
                Enabled = chkObservationTime.Checked,
                Time = (uint)observationTime.Value
            };
            mCurrentComposite.ObservationTime = meta;
            mObservationTimeChanged = false;
        }

        if (mMagneticVariationChanged)
        {
            var meta = new MagneticVariationMeta
            {
                Enabled = chkMagneticVar.Checked,
                MagneticDegrees = (int)magneticVar.Value
            };
            mCurrentComposite.MagneticVariation = meta;
            mMagneticVariationChanged = false;
        }

        if (mIdsEndpointChanged)
        {
            mCurrentComposite.IDSEndpoint = txtIdsEndpoint.Text;
            mIdsEndpointChanged = false;
        }

        if (mVoiceOptionsChanged)
        {
            var selectedVoice = ddlVoices.SelectedItem as VoiceMetaData;
            mCurrentComposite.AtisVoice.UseTextToSpeech = radioTextToSpeech.Checked;
            mCurrentComposite.AtisVoice.Voice = selectedVoice == null ? "Default" : selectedVoice.Name;
            mVoiceOptionsChanged = false;
        }

        List<string> usedContractions = new List<string>();
        foreach (DataGridViewRow row in gridContractions.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var stringValue = row.Cells[0].Value.ToString();
                    if (usedContractions.Contains(stringValue))
                    {
                        MessageBox.Show(this, "Duplicate contraction: " + stringValue, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Hand);
                        gridContractions.Focus();
                        return false;
                    }

                    usedContractions.Add(stringValue);
                }
                catch
                {
                }
            }
        }

        if (mContractionsChanged)
        {
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
        }

        List<Tuple<int, int>> usedTransitionLevels = new List<Tuple<int, int>>();
        foreach (DataGridViewRow row in gridTransitionLevels.Rows)
        {
            if (!row.IsNewRow)
            {
                try
                {
                    var low = int.Parse(row.Cells[0].Value.ToString());
                    var high = int.Parse(row.Cells[1].Value.ToString());
                    if (usedTransitionLevels.Any(t => t.Item1 == low && t.Item2 == high))
                    {
                        MessageBox.Show(this, $"Duplicate Transition Level: {low}-{high}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        gridTransitionLevels.Focus();
                        return false;
                    }

                    usedTransitionLevels.Add(new Tuple<int, int>(low, high));
                }
                catch
                {
                }
            }
        }

        if (mTransitionLevelsChanged)
        {
            mCurrentComposite.TransitionLevels.Clear();
            foreach (DataGridViewRow row in gridTransitionLevels.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        var low = int.Parse(row.Cells[0].Value.ToString());
                        var high = int.Parse(row.Cells[1].Value.ToString());
                        var tl = int.Parse(row.Cells[2].Value.ToString());
                        mCurrentComposite.TransitionLevels.Add(new TransitionLevel
                        {
                            Low = low,
                            High = high,
                            Altitude = tl
                        });
                    }
                }
            }
        }

        if (mPrefixNotamsChanged)
        {
            mCurrentComposite.UseNotamPrefix = chkPrefixNotams.Checked;
            mPrefixNotamsChanged = false;
        }

        if (mTransitionLevelPrefixChanged)
        {
            mCurrentComposite.UseTransitionLevelPrefix = chkTransitionLevelPrefix.Checked;
            mTransitionLevelPrefixChanged = false;
        }

        if (mConvertMetricChanged)
        {
            mCurrentComposite.UseMetricUnits = chkConvertMetric.Checked;
            mConvertMetricChanged = false;
        }

        if (mSurfaceWindPrefixChanged)
        {
            mCurrentComposite.UseSurfaceWindPrefix = chkSurfaceWindPrefix.Checked;
            mSurfaceWindPrefixChanged = false;
        }

        if (mVisibilitySuffixChanged)
        {
            mCurrentComposite.UseVisibilitySuffix = chkVisibilitySuffix.Checked;
            mVisibilitySuffixChanged = false;
        }

        if (mUseDecimalTerminologyChanged)
        {
            mCurrentComposite.UseDecimalTerminology = chkDecimalTerminology.Checked;
            mUseDecimalTerminologyChanged = false;
        }

        if (mUseTemperaturePlusPrefixChanged)
        {
            mCurrentComposite.UseTemperaturePlusPrefix = chkPrefixTemperature.Checked;
            mUseTemperaturePlusPrefixChanged = false;
        }

        if(mUseZuluTimeSuffixChanged)
        {
            mCurrentComposite.UseZuluTimeSuffix = chkAppendZulu.Checked;
            mUseZuluTimeSuffixChanged = false;
        }

        mTransitionLevelsChanged = false;
        mContractionsChanged = false;
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

    private void vhfFrequency_TextChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!vhfFrequency.Focused)
            return;

        if (decimal.TryParse(vhfFrequency.Text, out var frequency))
        {
            if (frequency.ToVatsimFrequencyFormat() != mCurrentComposite.AtisFrequency)
            {
                mFrequencyChanged = true;
                btnApply.Enabled = true;
            }
            else
            {
                mFrequencyChanged = false;
                btnApply.Enabled = false;
            }
        }
    }

    private void typeCombined_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!typeCombined.Focused)
            return;

        if (typeCombined.Checked)
        {
            txtCodeRangeLow.Enabled = false;
            txtCodeRangeHigh.Enabled = false;
        }

        if (typeCombined.Checked && mCurrentComposite.AtisType != AtisType.Combined)
        {
            mAtisTypeChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mAtisTypeChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void typeDeparture_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!typeDeparture.Focused)
            return;

        if (typeDeparture.Checked)
        {
            txtCodeRangeLow.Enabled = true;
            txtCodeRangeHigh.Enabled = true;
        }

        if (typeDeparture.Checked && mCurrentComposite.AtisType != AtisType.Departure)
        {
            mAtisTypeChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mAtisTypeChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void typeArrival_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!typeArrival.Focused)
            return;

        if (typeArrival.Checked)
        {
            txtCodeRangeLow.Enabled = true;
            txtCodeRangeHigh.Enabled = true;
        }

        if (typeArrival.Checked && mCurrentComposite.AtisType != AtisType.Arrival)
        {
            mAtisTypeChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mAtisTypeChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void txtCodeRangeLow_TextChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!txtCodeRangeLow.Focused)
            return;

        if ((txtCodeRangeLow.Text ?? "") != mCurrentComposite.CodeRange.Low.ToString())
        {
            mCodeRangeLowChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mCodeRangeLowChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void txtCodeRangeHigh_TextChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!txtCodeRangeHigh.Focused)
            return;

        if ((txtCodeRangeHigh.Text ?? "") != mCurrentComposite.CodeRange.High.ToString())
        {
            mCodeRangeHighChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mCodeRangeHighChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkFaaFormat_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkFaaFormat.Focused)
            return;

        ToggleNonFaaOptions();

        if (chkFaaFormat.Checked != mCurrentComposite.UseFaaFormat)
        {
            mFaaFormatChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            btnApply.Enabled = false;
        }
    }

    private void chkExternalAtisGenerator_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentPreset == null)
            return;

        if (!chkExternalAtisGenerator.Focused)
            return;

        AddDynamicPresetControl();

        if (chkExternalAtisGenerator.Checked != mCurrentPreset.ExternalGenerator.Enabled)
        {
            mExternalAtisGeneratorChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            btnApply.Enabled = false;
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

    private void chkObservationTime_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkObservationTime.Focused)
            return;

        observationTime.Enabled = chkObservationTime.Checked;

        if (chkObservationTime.Checked != mCurrentComposite.ObservationTime.Enabled)
        {
            mObservationTimeChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            btnApply.Enabled = false;
        }
    }

    private void observationTime_ValueChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!observationTime.Focused)
            return;

        if (observationTime.Value != mCurrentComposite.ObservationTime.Time)
        {
            mObservationTimeChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mObservationTimeChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkMagneticVar_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkMagneticVar.Focused)
            return;

        magneticVar.Enabled = chkMagneticVar.Checked;

        if (chkMagneticVar.Checked != mCurrentComposite.MagneticVariation.Enabled)
        {
            mMagneticVariationChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mMagneticVariationChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void magneticVar_ValueChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!magneticVar.Focused)
            return;

        if (magneticVar.Value != mCurrentComposite.MagneticVariation.MagneticDegrees)
        {
            mMagneticVariationChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mMagneticVariationChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void radioVoiceRecorded_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!radioVoiceRecorded.Focused)
            return;

        ddlVoices.Enabled = !radioVoiceRecorded.Checked;

        if (mCurrentComposite.AtisVoice != null)
        {
            if (radioVoiceRecorded.Checked && mCurrentComposite.AtisVoice.UseTextToSpeech)
            {
                mVoiceOptionsChanged = true;
                btnApply.Enabled = true;
            }
            else
            {
                mVoiceOptionsChanged = false;
                btnApply.Enabled = false;
            }
        }
        else
        {
            mVoiceOptionsChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void radioTextToSpeech_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!radioTextToSpeech.Focused)
            return;

        ddlVoices.Enabled = radioTextToSpeech.Checked;

        if (mCurrentComposite.AtisVoice != null)
        {
            if (mCurrentComposite.AtisVoice.UseTextToSpeech == false && radioTextToSpeech.Checked)
            {
                mVoiceOptionsChanged = true;
                btnApply.Enabled = true;
            }
            else
            {
                mVoiceOptionsChanged = false;
                btnApply.Enabled = false;
            }
        }
        else
        {
            mVoiceOptionsChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void ddlVoices_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!ddlVoices.Focused)
            return;

        var selected = ddlVoices.SelectedItem as VoiceMetaData;

        if (mCurrentComposite.AtisVoice != null)
        {
            if (selected.Name != mCurrentComposite.AtisVoice.Voice)
            {
                mVoiceOptionsChanged = true;
                btnApply.Enabled = true;
            }
        }
        else
        {
            mCurrentComposite.AtisVoice = new AtisVoiceMeta
            {
                UseTextToSpeech = true,
                Voice = selected == null ? "Default" : selected.Name,
            };

            mVoiceOptionsChanged = true;
            btnApply.Enabled = true;
        }
    }

    private void txtIdsEndpoint_TextChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!txtIdsEndpoint.Focused)
            return;

        if ((txtIdsEndpoint.Text ?? "") != (mCurrentComposite.IDSEndpoint ?? ""))
        {
            mIdsEndpointChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mIdsEndpointChanged = false;
            btnApply.Enabled = false;
        }
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
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose another.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose another.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    MessageBox.Show(this, "Another profile already exists with that name. Please choose another.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (MessageBox.Show(this, "Are you sure you want to delete the selected preset?", "Delete Preset",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
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
        if (base.ActiveControl == btnCancel)
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
            if (!gridContractions.SelectedRows[0].IsNewRow && MessageBox.Show(this,
                    "Are you sure you want to delete the selected contraction?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                gridContractions.Rows.RemoveAt(gridContractions.SelectedRows[0].Index);
                btnApply.Enabled = true;
                mContractionsChanged = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No contraction selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void gridContractions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridContractions.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridContractions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
        mContractionsChanged = true;
    }

    private void gridContractions_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (MessageBox.Show(this, "Are you sure you want to delete the selected contraction?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void gridContractions_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        btnApply.Enabled = true;
        mContractionsChanged = true;
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
                    MessageBox.Show(this,
                        $"A maximum of {Constants.MAX_COMPOSITES} composites can be imported into a single profile",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            MessageBox.Show(this, "Composite Import Error: " + ex.Message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
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
                    if (MessageBox.Show(this,
                            $"A composite already exists for {composite.Identifier}. Do you want to overwrite it?",
                            "Duplicate Composite", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
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
            MessageBox.Show(this, "Import Error: " + ex.Message, "Import Error", MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
        }
    }

    private void ImportLegacyProfile(string fullName)
    {
        FileStream fs = new FileStream(fullName, FileMode.Open, FileAccess.Read);
        using Stream stream = new GZipStream(fs, CompressionMode.Decompress);
        XmlSerializer xml = new XmlSerializer(typeof(LegacyFacility));
        var profile = (LegacyFacility)xml.Deserialize(stream);

        if (mAppConfig.CurrentProfile != null)
        {
            if (mAppConfig.CurrentProfile.Composites.Any(t => t.Identifier == profile.ID))
            {
                if (MessageBox.Show(this,
                        "A composite with that identifier already exists. Would you like to overwrite it?",
                        "Duplicate Composite", MessageBoxButtons.YesNo) == DialogResult.No)
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
                        existing.AtisFrequency = (int)(double.Parse(profile.AtisFrequency) * 1000) - 100000;
                    }
                    else
                    {
                        existing.AtisFrequency = profile.Frequency;
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
                        existing.ObservationTime.Enabled = profile.MetarObservation.Enable;
                        existing.ObservationTime.Time = (uint)profile.MetarObservation.ObservationTimeValue;
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
                        existing.AirportConditionDefinitions.Add(new DefinedText
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
                        existing.NotamDefinitions.Add(new DefinedText
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
                    composite.AtisFrequency = (int)(double.Parse(profile.AtisFrequency) * 1000) - 100000;
                }
                else
                {
                    composite.AtisFrequency = profile.Frequency;
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
                    composite.ObservationTime.Enabled = profile.MetarObservation.Enable;
                    composite.ObservationTime.Time = (uint)profile.MetarObservation.ObservationTimeValue;
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
                    composite.AirportConditionDefinitions.Add(new DefinedText
                    {
                        Ordinal = idx++,
                        Text = condition.Message,
                        Enabled = condition.IsSelected
                    });
                }

                idx = 1;
                foreach (var condition in profile.Notams)
                {
                    composite.NotamDefinitions.Add(new DefinedText
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
                File.WriteAllText(saveDialog.FileName,
                    JsonConvert.SerializeObject(mCurrentComposite, Formatting.Indented));
                MessageBox.Show(this, "Composite exported successfully.", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }
    }

    private void gridTransitionLevels_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        gridTransitionLevels.Rows[e.RowIndex].ErrorText = "";
    }

    private void gridTransitionLevels_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        if (base.ActiveControl == btnCancel)
        {
            e.Cancel = true;
            btnCancel_Click(null, EventArgs.Empty);
            return;
        }
    }

    private void gridTransitionLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        btnApply.Enabled = true;
        mTransitionLevelsChanged = true;
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
            if (!gridTransitionLevels.SelectedRows[0].IsNewRow && MessageBox.Show(this,
                    "Are you sure you want to delete the selected transition level?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                gridTransitionLevels.Rows.RemoveAt(gridTransitionLevels.SelectedRows[0].Index);
                btnApply.Enabled = true;
                mTransitionLevelsChanged = true;
            }
        }
        else
        {
            MessageBox.Show(this, "No transition level selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void chkPrefixNotams_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkPrefixNotams.Focused)
            return;

        if (chkPrefixNotams.Checked != mCurrentComposite.UseNotamPrefix)
        {
            mPrefixNotamsChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mPrefixNotamsChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkTransitionLevelPrefix_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkTransitionLevelPrefix.Focused)
            return;

        if (chkTransitionLevelPrefix.Checked != mCurrentComposite.UseTransitionLevelPrefix)
        {
            mTransitionLevelPrefixChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mTransitionLevelPrefixChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkConvertMetric_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkConvertMetric.Focused)
            return;

        if (chkConvertMetric.Checked != mCurrentComposite.UseMetricUnits)
        {
            mConvertMetricChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mConvertMetricChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkSurfaceWindPrefix_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkSurfaceWindPrefix.Focused)
            return;

        if (chkSurfaceWindPrefix.Checked != mCurrentComposite.UseSurfaceWindPrefix)
        {
            mSurfaceWindPrefixChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mSurfaceWindPrefixChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkVisibilitySuffix_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkVisibilitySuffix.Focused)
            return;

        if (chkVisibilitySuffix.Checked != mCurrentComposite.UseVisibilitySuffix)
        {
            mVisibilitySuffixChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mVisibilitySuffixChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkDecimalTerminology_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkDecimalTerminology.Focused)
            return;

        if (chkDecimalTerminology.Checked != mCurrentComposite.UseDecimalTerminology)
        {
            mUseDecimalTerminologyChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mUseDecimalTerminologyChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkPrefixTemperature_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkPrefixTemperature.Focused)
            return;

        if (chkPrefixTemperature.Checked != mCurrentComposite.UseTemperaturePlusPrefix)
        {
            mUseTemperaturePlusPrefixChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mUseTemperaturePlusPrefixChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void chkAppendZulu_CheckedChanged(object sender, EventArgs e)
    {
        if (mCurrentComposite == null)
            return;

        if (!chkAppendZulu.Focused)
            return;

        if (chkAppendZulu.Checked != mCurrentComposite.UseZuluTimeSuffix)
        {
            mUseZuluTimeSuffixChanged = true;
            btnApply.Enabled = true;
        }
        else
        {
            mUseZuluTimeSuffixChanged = false;
            btnApply.Enabled = false;
        }
    }

    private void ToggleNonFaaOptions()
    {
        if (chkFaaFormat.Checked)
        {
            chkConvertMetric.Checked = false;
            chkConvertMetric.Enabled = false;
            mConvertMetricChanged = true;

            chkTransitionLevelPrefix.Checked = false;
            chkTransitionLevelPrefix.Enabled = false;
            mTransitionLevelPrefixChanged = true;

            chkSurfaceWindPrefix.Checked = false;
            chkSurfaceWindPrefix.Enabled = false;
            mSurfaceWindPrefixChanged = true;

            chkVisibilitySuffix.Checked = false;
            chkVisibilitySuffix.Enabled = false;
            mVisibilitySuffixChanged = true;

            chkDecimalTerminology.Checked = false;
            chkDecimalTerminology.Enabled = false;
            mUseDecimalTerminologyChanged = true;

            chkPrefixTemperature.Checked = false;
            chkPrefixTemperature.Enabled = false;
            mUseTemperaturePlusPrefixChanged = true;
        }
        else
        {
            chkConvertMetric.Enabled = true;
            chkTransitionLevelPrefix.Enabled = true;
            chkSurfaceWindPrefix.Enabled = true;
            chkVisibilitySuffix.Enabled = true;
            chkDecimalTerminology.Enabled = true;
            chkPrefixTemperature.Enabled = true;
        }
    }

    private void AlphaOnly(object sender, KeyPressEventArgs e)
    {
        e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
    }
}