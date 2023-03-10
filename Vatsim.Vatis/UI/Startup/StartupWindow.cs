using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Updates;

namespace Vatsim.Vatis.UI.Startup
{
    public partial class StartupWindow : Form
    {
        private IClientUpdater mClientUpdater;
        private IWindowFactory mWindowFactory;

        public StartupWindow(IClientUpdater clientUpdater, IWindowFactory windowFactory)
        {
            mClientUpdater = clientUpdater;
            mWindowFactory = windowFactory;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EventBus.Register(this);
        }

        protected override async void OnShown(EventArgs e)
        {
            UpdateStatusLabel("Checking for new version...");

            if (await CheckForClientUpdates())
            {
                Application.ExitThread();
            }

            ShowMainForm();
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
