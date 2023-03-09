using System;
using System.Windows.Forms;
using Appccelerate.EventBroker;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class SettingsDialog : Form
{
    [EventPublication(EventTopics.AppConfigUpdated)]
    public event EventHandler<EventArgs> RaiseAppConfigUpdated;

    private readonly IEventBroker mEventBroker;
    private readonly IAppConfig mAppConfig;

    public SettingsDialog(IEventBroker eventBroker, IAppConfig appConfig)
    {
        InitializeComponent();

        mAppConfig = appConfig;
        mEventBroker = eventBroker;
        mEventBroker.Register(this);

        LoadNetworkServers();

        ddlNetworkRating.DataSource = Enum.GetValues(typeof(Vatsim.Network.NetworkRating));
        txtName.Text = mAppConfig.Name;
        txtVatsimId.Text = mAppConfig.VatsimId;
        txtVatsimPassword.Text = mAppConfig.VatsimPasswordDecrypted;
        ddlNetworkRating.SelectedIndex = ddlNetworkRating.FindStringExact(mAppConfig.NetworkRating.ToString());
        ddlServerName.SelectedIndex = ddlServerName.FindStringExact(mAppConfig.ServerName);
        chkSuppressNotifications.Checked = mAppConfig.SuppressNotifications;
        if (mAppConfig.WindowProperties != null)
        {
            chkKeepVisible.Checked = mAppConfig.WindowProperties.TopMost;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        mEventBroker?.Unregister(this);
    }

    private void LoadNetworkServers()
    {
        ddlServerName.Items.Clear();
        if (mAppConfig.CachedServers != null && mAppConfig.CachedServers.Count > 0)
        {
            foreach (var server in mAppConfig.CachedServers)
            {
                ddlServerName.Items.Add(new ComboBoxItem()
                {
                    Text = server.Name,
                    Value = server.Address
                });
            }
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            ShowError("Your Name is requred");
            txtName.Select();
        }
        else if (string.IsNullOrEmpty(txtVatsimId.Text))
        {
            ShowError("VATSIM ID is required");
            txtVatsimId.Select();
        }
        else if (string.IsNullOrEmpty(txtVatsimPassword.Text))
        {
            ShowError("VATSIM Password is required");
            txtVatsimPassword.Select();
        }
        else if (ddlNetworkRating.SelectedIndex == -1)
        {
            ShowError("Please select your network rating");
            ddlNetworkRating.Select();
        }
        else if (ddlServerName.SelectedIndex == -1)
        {
            ShowError("Please select a network server");
            ddlServerName.Select();
        }
        else
        {
            mAppConfig.Name = txtName.Text;
            mAppConfig.VatsimId = txtVatsimId.Text;
            mAppConfig.VatsimPasswordDecrypted = txtVatsimPassword.Text;
            mAppConfig.NetworkRating = (Vatsim.Network.NetworkRating)ddlNetworkRating.SelectedItem;
            mAppConfig.ServerName = (ddlServerName.SelectedItem as ComboBoxItem).Text;
            mAppConfig.SuppressNotifications = chkSuppressNotifications.Checked;
            mAppConfig.WindowProperties.TopMost = chkKeepVisible.Checked;
            mAppConfig.SaveConfig();
            RaiseAppConfigUpdated?.Invoke(this, EventArgs.Empty);
            Close();
        }
    }

    private void ShowError(string message)
    {
        MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    private class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() => Text;
    }
}