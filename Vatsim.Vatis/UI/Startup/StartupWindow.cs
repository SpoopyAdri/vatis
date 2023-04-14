using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.TextToSpeech;
using Vatsim.Vatis.Updates;

namespace Vatsim.Vatis.UI.Startup
{
    public partial class StartupWindow : Form
    {
        private IClientUpdater mClientUpdater;
        private INavaidDatabase mNavData;
        private IWindowFactory mWindowFactory;
        private IDownloader mDownloader;
        private IAppConfig mAppConfig;

        public StartupWindow(IClientUpdater clientUpdater, IWindowFactory windowFactory, INavaidDatabase navData, IAppConfig appConfig, IDownloader downloader)
        {
            mClientUpdater = clientUpdater;
            mWindowFactory = windowFactory;
            mNavData = navData;
            mAppConfig = appConfig;
            mDownloader = downloader;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EventBus.Register(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, 
                ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            using Pen pen = new Pen(Color.FromArgb(0, 0, 0));
            e.Graphics.DrawRectangle(pen, rect);
        }

        protected override async void OnShown(EventArgs e)
        {
            UpdateStatusLabel("Checking for new version...");
            if (await CheckForClientUpdates())
            {
                Application.ExitThread();
            }

            UpdateStatusLabel("Loading NavData...");
            await mNavData.LoadDatabases();

            UpdateStatusLabel("Downloading network server list...");
            await DownloadServerList();

            UpdateStatusLabel("Downloading available voices...");
            await DownloadVoiceList();

            ShowMainForm();
        }

        private async Task DownloadVoiceList()
        {
            try
            {
                var voices = await mDownloader.DownloadJsonStringAsync<List<VoiceMetaData>>("https://tts.clowd.io/Voices");

                if (voices != null)
                {
                    mAppConfig.Voices = voices;
                    mAppConfig.SaveConfig();
                }
            }
            catch { }
        }

        private async Task DownloadServerList()
        {
            var servers = await Vatsim.Network.NetworkInfo.DownloadServerList("https://status.vatsim.net/status.json");
            if (servers != null && servers.Count > 0)
            {
                mAppConfig.CachedServers.Clear();

                foreach (var server in servers)
                {
                    mAppConfig.CachedServers.Add(new Vatsim.Network.NetworkServerInfo
                    {
                        Name = server.Name,
                        Address = server.Address,
                    });

                    if (server.Name == "AUTOMATIC")
                    {
                        mAppConfig.ServerName = "AUTOMATIC";
                    }
                }

                mAppConfig.SaveConfig();
            }
        }

        private async Task<bool> CheckForClientUpdates()
        {
            try
            {
                return await mClientUpdater.Run();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Error checking for client updates");
                return false;
            }
        }

        private void ShowError(Exception ex, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    ShowError(ex, message);
                }));
            }
            else
            {
                Log.Error(ex, message);
                MessageBox.Show(this, "An error has occurred. Please refer to the log file for details.\n\n" + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void UpdateStatusLabel(string status)
        {
            txtStatus.Text = status;
            Application.DoEvents();
        }

        private void ShowMainForm()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    ShowMainForm();
                }));
            }
            else
            {
                mWindowFactory.CreateProfileListDialog().Show();
                Close();
            }
        }

        public void HandleEvent(StartupStatusChanged e)
        {
            UpdateStatusLabel(e.Status);
        }
    }
}
