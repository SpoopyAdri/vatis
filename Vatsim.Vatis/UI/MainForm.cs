using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Vatsim.Vatis.Atis;
using Vatsim.Vatis.AudioForVatsim;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Network;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.UI.Controls;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Utils;

namespace Vatsim.Vatis.UI;

public partial class MainForm : Form
{
    private readonly IWindowFactory mWindowFactory;
    private readonly IAppConfig mAppConfig;
    private readonly IAudioManager mAudioManager;
    private readonly IAtisBuilder mAtisBuilder;
    private readonly IConnectionFactory mConnectionFactory;
    private readonly Weather.MetarParser mMetarParser;
    private readonly List<Connection> mConnections = new();
    private readonly System.Windows.Forms.Timer mUtcClock;
    private bool mInitializing = true;

    public MainForm(IWindowFactory windowFactory, IAppConfig appConfig,
        IAtisBuilder atisBuilder, IAudioManager audioManager, IConnectionFactory connectionFactory)
    {
        InitializeComponent();

        mWindowFactory = windowFactory;
        mConnectionFactory = connectionFactory;
        mAppConfig = appConfig;
        mAtisBuilder = atisBuilder;
        mAudioManager = audioManager;
        mMetarParser = new Weather.MetarParser();

        utcClock.Text = DateTime.UtcNow.ToString("HH:mm/ss");
        mUtcClock = new System.Windows.Forms.Timer
        {
            Interval = 500
        };
        mUtcClock.Tick += (s, e) => { utcClock.Text = DateTime.UtcNow.ToString("HH:mm/ss"); };
        mUtcClock.Start();
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02000000;
            return handleParam;
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Rectangle rect = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top,
            base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
        using (Pen pen = new Pen(Color.FromArgb(0, 0, 0)))
        {
            e.Graphics.DrawRectangle(pen, rect);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        EventBus.Register(this);
        EventBus.Publish(this, new SessionStarted());

        if (mAppConfig.WindowProperties == null)
        {
            mAppConfig.WindowProperties = new WindowProperties();
            mAppConfig.WindowProperties.Location = ScreenUtils.CenterOnScreen(this);
            mAppConfig.SaveConfig();
        }

        ScreenUtils.ApplyWindowProperties(mAppConfig.WindowProperties, this);
        mInitializing = false;
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (mAppConfig.ConfigRequired)
        {
            DialogResult dialogResult = MessageBox.Show(this,
                "It looks like this may be the first time you've run vATIS on this computer. Some configuration items are required before you can connect to the network. Would you like to configure vATIS now?",
                "Configuration Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (var dlg = mWindowFactory.CreateSettingsDialog())
                {
                    dlg.TopMost = mAppConfig.WindowProperties.TopMost;
                    dlg.ShowDialog(this);
                }
            }
        }

        RefreshAtisComposites();

        if (atisTabs.TabPages.Count > 0)
        {
            mAppConfig.CurrentComposite = (atisTabs.TabPages[0].Tag as Composite);
        }
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);

        if (!mInitializing)
        {
            ScreenUtils.SaveWindowProperties(mAppConfig.WindowProperties, this);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        EventBus.Publish(this, new SessionEnded());
        EventBus.Unregister(this);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        if (mAppConfig.Profiles.Any(x =>
                x.Composites.Any(y => y.Presets.Any(z => z.IsNotamsDirty || z.IsAirportConditionsDirty))))
        {
            if (MessageBox.Show(this,
                    "There are unsaved Airport Conditions or NOTAMs. Are you sure you want to exit anyways?",
                    "Confirm Close", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
        }

        if (mConnections.Any(x => x.IsConnected))
        {
            if (MessageBox.Show(this, "You still have active ATIS connections. Are you sure you want to exit?",
                    "Confirm Close", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
        }

        foreach (var connection in mConnections)
        {
            if (connection.IsConnected)
            {
                mAudioManager.RemoveBot(connection.Callsign);
                connection.Disconnect();
            }
        }

        Close();
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private void btnManageProfile_Click(object sender, EventArgs e)
    {
        using (var dlg = mWindowFactory.CreateProfileConfigurationForm())
        {
            dlg.TopMost = mAppConfig.WindowProperties.TopMost;
            dlg.ShowDialog(this);
        }
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
        using (var form = mWindowFactory.CreateSettingsDialog())
        {
            form.TopMost = mAppConfig.WindowProperties.TopMost;
            form.ShowDialog();
        }
    }

    private void RefreshAtisComposites()
    {
        if (mAppConfig.CurrentProfile == null)
            return;

        foreach (var composite in mAppConfig.CurrentProfile.Composites.OrderBy(x => x.Identifier)
                     .Take(Constants.MAX_COMPOSITES))
        {
            var tab = atisTabs.TabPages[composite.Id.ToString()] as AtisTabPage;

            var tabId = composite.Identifier;
            if (composite.AtisType == AtisType.Departure)
            {
                tabId = composite.Identifier + "/D";
            }
            else if (composite.AtisType == AtisType.Arrival)
            {
                tabId = composite.Identifier + "/A";
            }

            if (tab != null)
            {
                tab.Text = tabId;
                tab.CompositeMeta.BindPresets(composite.Presets.Select(x => x.Name).ToList());
                if (!composite.Connection.IsConnected)
                {
                    composite.AtisLetter = composite.CodeRange.Low.ToString();
                    tab.CompositeMeta.SyncAtisLetter();
                }
                atisTabs.Invalidate();
            }
            else
            {
                var connection = mConnectionFactory.CreateConnection();
                connection.AirportIcao = composite.Identifier;
                connection.Composite = composite;

                composite.Connection = connection;
                composite.AtisCallsign = connection.Callsign;
                composite.AtisLetter = composite.CodeRange.Low.ToString();

                var cancellationToken = new CancellationTokenSource();

                var tabPage = new AtisTabPage(connection, composite, mAppConfig)
                {
                    Name = composite.Id.ToString(),
                    Text = tabId,
                    Tag = composite
                };

                tabPage.CompositeMeta.ConnectButtonClicked += (sender, args) =>
                {
                    if (connection.IsConnected)
                    {
                        // If there's a previous request, cancel it.
                        cancellationToken?.Cancel();
                        cancellationToken = new CancellationTokenSource();

                        connection.Disconnect();

                        tabPage.CompositeMeta.Metar = null;
                        tabPage.CompositeMeta.Wind = null;
                        tabPage.CompositeMeta.Altimeter = null;
                        tabPage.CompositeMeta.Status = ConnectionStatus.Disconnected;
                        tabPage.Parent?.Invalidate();
                    }
                    else
                    {
                        if (mConnections.Count(x => x.IsConnected) >= Constants.MAX_CONNECTIONS)
                        {
                            tabPage.CompositeMeta.Error = "Maximum ATIS connections exceeded.";
                            return;
                        }

                        connection.Connect();
                    }
                };
                tabPage.Connection.MetarNotFoundReceived += (sender, args) =>
                {
                    tabPage.CompositeMeta.Error = "Error: METAR not found";
                    connection.Disconnect();
                };
                tabPage.Connection.MetarResponseReceived += async (sender, args) =>
                {
                    var metar = mMetarParser.Parse(args.Metar);

                    composite.DecodedMetar = metar;
                    composite.MetarReceived?.Invoke(this, new ClientEventArgs<string>(args.Metar));

                    tabPage.CompositeMeta.Error = null;
                    tabPage.CompositeMeta.Metar = args.Metar;
                    if (metar.SurfaceWind != null)
                        tabPage.CompositeMeta.Wind = metar.SurfaceWind.RawValue;
                    if (metar.AltimeterSetting != null)
                        tabPage.CompositeMeta.Altimeter = metar.AltimeterSetting.RawValue;
                    tabPage.CompositeMeta.Status = ConnectionStatus.Connected;

                    tabPage.Parent?.Invalidate();

                    if (args.IsUpdated)
                    {
                        // this will trigger a new ATIS build
                        tabPage.CompositeMeta.IncrementAtisLetter();

                        if (!mAppConfig.SuppressNotifications)
                        {
                            var sound = new SoundPlayer(Vatsim.Vatis.Properties.Resources.NewUpdate);
                            sound.Play();
                        }

                        FlashTaskbar.FlashWindow();
                    }
                    else
                    {
                        if (composite.AtisVoice.UseTextToSpeech)
                        {
                            try
                            {
                                // If there's a previous request, cancel it.
                                cancellationToken?.Cancel();
                                cancellationToken = new CancellationTokenSource();

                                await mAtisBuilder
                                .BuildVoiceAtis(composite, cancellationToken.Token)
                                .ContinueWith(c =>
                                {
                                    if (!args.IsUpdated)
                                    {
                                        tabPage.Connection.SendSubscriberNotification();
                                    }
                                }, cancellationToken.Token);
                            }
                            catch (OperationCanceledException) { }
                            catch (AggregateException ex)
                            {
                                tabPage.CompositeMeta.Error = "Error: " + string.Join(", ",
                                    ex.Flatten().InnerExceptions.Select(t => t.Message));
                                connection.Disconnect();
                            }
                            catch (Exception ex)
                            {
                                tabPage.CompositeMeta.Error = "Error: " + ex.Message;
                                connection.Disconnect();
                            }
                        }
                        else
                        {
                            tabPage.CompositeMeta.VoiceRecordEnabled = !composite.AtisVoice.UseTextToSpeech;
                            mAtisBuilder.BuildTextAtis(composite);
                            await mAtisBuilder.UpdateIds(composite, cancellationToken.Token);
                        }
                    }
                };
                tabPage.CompositeMeta.PresetChanged += async (sender, args) =>
                {
                    if (!connection.IsConnected)
                        return;

                    if (composite.DecodedMetar == null)
                        return;

                    if (composite.AtisVoice.UseTextToSpeech)
                    {
                        try
                        {
                            // If there's a previous request, cancel it.
                            cancellationToken?.Cancel();
                            cancellationToken = new CancellationTokenSource();

                            await mAtisBuilder.BuildVoiceAtis(composite, cancellationToken.Token);
                        }
                        catch (OperationCanceledException) { }
                        catch (AggregateException ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + string.Join(", ",
                                ex.Flatten().InnerExceptions.Select(t => t.Message));
                            connection.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + ex.Message;
                            connection.Disconnect();
                        }
                    }
                    else
                    {
                        mAtisBuilder.BuildTextAtis(composite);
                        await mAtisBuilder.UpdateIds(composite, cancellationToken.Token);
                    }
                };
                tabPage.CompositeMeta.AtisLetterChanged += async (sender, args) =>
                {
                    if (!connection.IsConnected)
                        return;

                    if (composite.DecodedMetar == null)
                        return;

                    composite.DecodedMetarUpdated?.Invoke(this, EventArgs.Empty);
                    composite.NewAtisUpdate?.Invoke(this, new ClientEventArgs<string>(tabPage.Composite.AtisLetter)); // update mini display

                    if (composite.AtisVoice.UseTextToSpeech)
                    {
                        try
                        {
                            // If there's a previous request, cancel it.
                            cancellationToken?.Cancel();
                            cancellationToken = new CancellationTokenSource();

                            await mAtisBuilder
                            .BuildVoiceAtis(composite, cancellationToken.Token)
                            .ContinueWith(c =>
                            {
                                tabPage.Connection.SendSubscriberNotification();
                            }, cancellationToken.Token);
                        }
                        catch (OperationCanceledException) { }
                        catch (AggregateException ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + string.Join(", ",
                                ex.Flatten().InnerExceptions.Select(t => t.Message));
                            connection.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + ex.Message;
                            connection.Disconnect();
                        }
                    }
                    else
                    {
                        mAtisBuilder.BuildTextAtis(composite);
                        await mAtisBuilder.UpdateIds(composite, cancellationToken.Token);
                    }
                };
                tabPage.CompositeMeta.GenerateNewAtis += async (sender, args) =>
                {
                    if (!connection.IsConnected)
                        return;

                    if (composite.AtisVoice.UseTextToSpeech)
                    {
                        try
                        {
                            // If there's a previous request, cancel it.
                            cancellationToken?.Cancel();
                            cancellationToken = new CancellationTokenSource();

                            await mAtisBuilder.BuildVoiceAtis(composite, cancellationToken.Token);
                        }
                        catch (OperationCanceledException) { }
                        catch (AggregateException ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + string.Join(", ",
                                ex.Flatten().InnerExceptions.Select(t => t.Message));
                            connection.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            tabPage.CompositeMeta.Error = "Error: " + ex.Message;
                            connection.Disconnect();
                        }
                    }
                    else
                    {
                        mAtisBuilder.BuildTextAtis(composite);
                        await mAtisBuilder.UpdateIds(composite, cancellationToken.Token);
                    }
                };
                tabPage.CompositeMeta.RecordedAtisMemoryStreamChanged += async (sender, args) =>
                {
                    if (!connection.IsConnected)
                        return;

                    try
                    {
                        // If there's a previous request, cancel it.
                        cancellationToken?.Cancel();
                        cancellationToken = new CancellationTokenSource();

                        composite.RecordedMemoryStream = args.AtisMemoryStream;

                        await mAtisBuilder
                        .BuildVoiceAtis(composite, cancellationToken.Token)
                        .ContinueWith(t =>
                        {
                            tabPage.CompositeMeta.VoiceRecordedAtisActive = true;
                            tabPage.Connection.SendSubscriberNotification();
                        }, cancellationToken.Token);
                    }
                    catch (OperationCanceledException) { }
                    catch (AggregateException ex)
                    {
                        tabPage.CompositeMeta.Error = "Error: " + string.Join(", ", ex.Flatten().InnerExceptions.Select(t => t.Message));
                        connection.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        tabPage.CompositeMeta.Error = "Error: " + ex.Message;
                        connection.Disconnect();
                    }
                };
                connection.NetworkErrorReceived += (sender, args) =>
                {
                    tabPage.CompositeMeta.Error = "Network Error: " + args.Error;
                    connection.Disconnect();
                };
                connection.KillRequestReceived += (sender, args) =>
                {
                    tabPage.CompositeMeta.Error = !string.IsNullOrEmpty(args.Reason)
                        ? $"Forcfully disconnected from network: {args.Reason}"
                        : "Forcfully disconnected from network.";
                    tabPage.CompositeMeta.Wind = null;
                    tabPage.CompositeMeta.Altimeter = null;

                    tabPage.CompositeMeta.Status = ConnectionStatus.Disconnected;
                    tabPage.Parent?.Invalidate();

                    mAudioManager.RemoveBot(connection.Callsign);
                };
                connection.NetworkDisconnectedChanged += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(tabPage.CompositeMeta.Error))
                        tabPage.CompositeMeta.Metar = null;
                    tabPage.CompositeMeta.Wind = null;
                    tabPage.CompositeMeta.Altimeter = null;

                    EventBus.Publish(this, new UpdateMiniWindowRequested());

                    tabPage.CompositeMeta.VoiceRecordEnabled = false;
                    tabPage.CompositeMeta.VoiceRecordedAtisActive = false;

                    tabPage.CompositeMeta.Status = ConnectionStatus.Disconnected;
                    tabPage.Parent?.Invalidate();

                    mAudioManager.RemoveBot(connection.Callsign);
                };
                connection.NetworkConnectedChanged += (sender, args) =>
                {
                    EventBus.Publish(this, new UpdateMiniWindowRequested());

                    mAudioManager.RemoveBot(connection.Callsign);

                    tabPage.CompositeMeta.Error = null;
                    tabPage.CompositeMeta.Status = ConnectionStatus.Connecting;
                    tabPage.CompositeMeta.VoiceRecordedAtisActive = false;
                };
                connection.ChangeServerReceived += (sender, args) =>
                {
                    connection.Disconnect();
                    connection.Connect();
                };

                tabPage.CompositeMeta.BindPresets(composite.Presets.Select(x => x.Name).ToList());

                mAppConfig.CurrentComposite = composite;
                atisTabs.TabPages.Add(tabPage);
                mConnections.Add(connection);
            }
        }

        atisTabs.Sort();
    }

    private void atisTabs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (atisTabs.SelectedTab != null)
        {
            mAppConfig.CurrentComposite = (atisTabs.SelectedTab.Tag as Composite);
        }
    }

    private void btnMinify_Click(object sender, EventArgs e)
    {
        Hide();

        using var dlg = mWindowFactory.CreateMiniDisplayForm();
        dlg.ShowDialog();
    }

    public void HandleEvent(MiniWindowClosed e)
    {
        Show();
    }

    public void HandleEvent(GeneralSettingsUpdated e)
    {
        ScreenUtils.ApplyWindowProperties(mAppConfig.WindowProperties, this);
    }

    public void HandleEvent(RefreshCompositesRequested e)
    {
        RefreshAtisComposites();
    }

    public void HandleEvent(AtisCompositeDeleted e)
    {
        foreach (TabPage tab in atisTabs.TabPages)
        {
            var composite = tab.Tag as Composite;
            if (composite != null && composite.Id == e.Id)
            {
                var connection = mConnections.FirstOrDefault(x => x.AirportIcao == composite.Identifier);
                if (connection != null)
                {
                    connection.Disconnect();
                    mConnections.Remove(connection);
                }

                atisTabs.TabPages.Remove(tab);
                atisTabs.Invalidate();
            }
        }
    }

    public void HandleEvent(NewAtisAcknowledged e)
    {
        var tab = atisTabs.TabPages.Cast<AtisTabPage>().FirstOrDefault(x => x.Composite == e.Composite);
        if (tab != null)
        {
            tab.IsNewAtis = false;
        }
    }
}