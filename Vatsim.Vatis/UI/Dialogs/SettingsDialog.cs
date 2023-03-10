using System;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class SettingsDialog : Form
{
    private readonly IAppConfig mAppConfig;

    public SettingsDialog(IAppConfig appConfig)
    {
        InitializeComponent();

        mAppConfig = appConfig;

        LoadNetworkServers();

        ddlNetworkRating.DataSource = Enum.GetValues(typeof(Vatsim.Network.NetworkRating));
        txtName.Text = mAppConfig.Name;
        txtVatsimId.Text = mAppConfig.UserId;
        txtVatsimPassword.Text = mAppConfig.Password;
        ddlNetworkRating.SelectedIndex = ddlNetworkRating.FindStringExact(mAppConfig.NetworkRating.ToString());
        ddlServerName.SelectedIndex = ddlServerName.FindStringExact(mAppConfig.ServerName);
        chkSuppressNotifications.Checked = mAppConfig.SuppressNotifications;
        if (mAppConfig.WindowProperties != null)
        {
            chkKeepVisible.Checked = mAppConfig.WindowProperties.TopMost;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        EventBus.Register(this);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        EventBus.Unregister(this);
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
            mAppConfig.UserId = txtVatsimId.Text;
            mAppConfig.Password = txtVatsimPassword.Text;
            mAppConfig.NetworkRating = (Vatsim.Network.NetworkRating)ddlNetworkRating.SelectedItem;
            mAppConfig.ServerName = (ddlServerName.SelectedItem as ComboBoxItem)?.Text;
            mAppConfig.SuppressNotifications = chkSuppressNotifications.Checked;
            mAppConfig.WindowProperties.TopMost = chkKeepVisible.Checked;
            mAppConfig.SaveConfig();

            EventBus.Publish(this, new GeneralSettingsUpdated());

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