using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class ProfileListDialog : Form
{
    private readonly IWindowFactory mWindowFactory;
    private readonly IAppConfig mAppConfig;
    private string mPreviousInputValue = "";

    public ProfileListDialog(IWindowFactory windowFactory, IAppConfig appConfig)
    {
        InitializeComponent();

        mWindowFactory = windowFactory;
        mAppConfig = appConfig;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        EventBus.Register(this);

        lblVersion.Text = $"Version {Application.ProductVersion}";

        RefreshList();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        EventBus.Unregister(this);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Rectangle rect = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, 23);
        Rectangle rect2 = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
        Rectangle rect3 = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 24);
        using Pen pen = new(Color.FromArgb(100, 100, 100));
        using Brush brush = new SolidBrush(ForeColor);
        e.Graphics.DrawRectangle(pen, rect);
        e.Graphics.DrawRectangle(pen, rect2);
        e.Graphics.DrawRectangle(pen, rect3);
        e.Graphics.DrawString(Text, Font, brush, 5f, 5f);
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        bool flag = false;

        while (!flag)
        {
            using (var dlg = mWindowFactory.CreateUserInputDialog())
            {
                dlg.PromptLabel = "Enter a name for the profile:";
                dlg.WindowTitle = "Save Profile As";
                dlg.ErrorMessage = "Invalid profile name. It must consist of only letters, numbers, underscores and spaces.";
                dlg.RegexExpression = "[A-Za-z0-9_ ]+";
                dlg.InitialValue = mPreviousInputValue;

                DialogResult result = dlg.ShowDialog(this);
                if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
                {
                    mPreviousInputValue = dlg.Value;

                    if (mAppConfig.Profiles.Any(t => t.Name == dlg.Value))
                    {
                        if (MessageBox.Show(this, "Another session profile with that name already exists. Would you like to overwrite it?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var newProfile = new Profile
                            {
                                Name = dlg.Value
                            };

                            var existing = mAppConfig.Profiles.FirstOrDefault(t => t.Name == dlg.Value);
                            mAppConfig.Profiles.Remove(existing);
                            mAppConfig.Profiles.Add(newProfile);
                            mAppConfig.SaveConfig();

                            RefreshList();

                            return;
                        }

                        flag = false;
                    }
                    else
                    {
                        var profile = new Profile
                        {
                            Name = dlg.Value
                        };
                        mAppConfig.Profiles.Add(profile);
                        mAppConfig.SaveConfig();

                        RefreshList();

                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
        }
    }

    private void btnRename_Click(object sender, EventArgs e)
    {
        if (listProfiles.SelectedItem != null)
        {
            bool flag = false;
            if (listProfiles.SelectedItem is Profile profile)
            {
                while (!flag)
                {
                    using (var dlg = mWindowFactory.CreateUserInputDialog())
                    {
                        dlg.PromptLabel = "Enter a name for the profile:";
                        dlg.WindowTitle = "Save Profile As";
                        dlg.ErrorMessage = "Invalid profile name. It must consist of only letters, numbers, underscores and spaces.";
                        dlg.RegexExpression = "[A-Za-z0-9_ ]+";
                        dlg.InitialValue = profile.Name;

                        DialogResult result = dlg.ShowDialog(this);
                        if (result == DialogResult.OK && !string.IsNullOrEmpty(dlg.Value))
                        {
                            mPreviousInputValue = dlg.Value;

                            if (mAppConfig.Profiles.Any(t => t.Name == dlg.Value))
                            {
                                if (MessageBox.Show(this, "Another session profile with that name already exists. Would you like to overwrite it?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    var duplicate = mAppConfig.Profiles?.FirstOrDefault(t => t.Name == dlg.Value);

                                    if (duplicate == profile)
                                    {
                                        duplicate.Name = dlg.Value;
                                    }
                                    else
                                    {
                                        mAppConfig.Profiles.Remove(duplicate);
                                        var current = mAppConfig.Profiles?.FirstOrDefault(t => t == profile);
                                        current.Name = dlg.Value;
                                    }

                                    mAppConfig.SaveConfig();
                                    RefreshList();
                                    return;
                                }

                                flag = false;
                            }
                            else
                            {
                                var existing = mAppConfig.Profiles.FirstOrDefault(t => t == profile);
                                existing.Name = dlg.Value;
                                mAppConfig.SaveConfig();

                                RefreshList();

                                flag = true;
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
            }
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if(listProfiles.SelectedItems != null)
        {
            if (MessageBox.Show(this, "Are you sure you want to delete the selected profile? This action cannot be undone.", "Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                mAppConfig.Profiles.Remove(listProfiles.SelectedItem as Profile);
                mAppConfig.SaveConfig();
                RefreshList();
            }
        }
    }

    private void RefreshList()
    {
        mPreviousInputValue = "";

        listProfiles.Items.Clear();
        foreach (var sessionProfile in mAppConfig.Profiles.ToList())
        {
            if (string.IsNullOrEmpty(sessionProfile.Name))
            {
                mAppConfig.Profiles.Remove(sessionProfile);
                continue;
            }
            listProfiles.Items.Add(sessionProfile);
        }

        btnDelete.Enabled = (listProfiles.SelectedItem != null);
        btnExport.Enabled = (listProfiles.SelectedItem != null);
        btnRename.Enabled = (listProfiles.SelectedItem != null);
    }

    private void listProfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listProfiles.SelectedItem != null)
        {
            btnDelete.Enabled = (listProfiles.SelectedItem != null);
            btnExport.Enabled = (listProfiles.SelectedItem != null);
            btnRename.Enabled = (listProfiles.SelectedItem != null && listProfiles.SelectedItems.Count == 1);
        }
    }

    private void listProfiles_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        LoadSessionProfile();
    }

    private void LoadSessionProfile()
    {
        if (listProfiles.SelectedItem != null)
        {
            mAppConfig.CurrentProfile = (Profile)listProfiles.SelectedItem;
            var mainForm = mWindowFactory.CreateMainForm();
            mainForm.Show();
        }
    }

    private void listProfiles_KeyDown(object sender, KeyEventArgs e)
    {
        if (listProfiles.SelectedItem != null && e.KeyCode == Keys.Return)
        {
            LoadSessionProfile();
            e.Handled = true;
        }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Import vATIS Profile",
            CheckFileExists = true,
            CheckPathExists = true,
            AddExtension = false,
            Filter = "vATIS Profile (*.json)|*.json",
            FilterIndex = 1,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            Multiselect = true,
            ShowHelp = false
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            foreach (var file in openFileDialog.FileNames)
            {
                try
                {
                    var profile = new Profile();

                    using var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        profile = JsonConvert.DeserializeObject<Profile>(sr.ReadToEnd(), new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        });
                    }

                    if (mAppConfig.Profiles.Any(x => x.Name == profile.Name))
                    {
                        if (MessageBox.Show(this, string.Format($"You already have a profile for {profile.Name}. Would you like to overwrite it?"), "Overwrite Existing Profile?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            mAppConfig.Profiles.RemoveAll(x => x.Name == profile.Name);
                            mAppConfig.Profiles.Add(profile);
                            mAppConfig.SaveConfig();
                        }
                    }
                    else
                    {
                        mAppConfig.Profiles.Add(profile);
                        mAppConfig.SaveConfig();
                    }

                    RefreshList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (listProfiles.SelectedItem != null)
        {
            var profile = listProfiles.SelectedItem as Profile;

            var saveDialog = new SaveFileDialog
            {
                FileName = $"vATIS Profile - {profile.Name}.json",
                Filter = "vATIS Profile (*.json)|*.json",
                FilterIndex = 1,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                OverwritePrompt = true,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                Title = "Export Profile",
                ValidateNames = true
            };

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, JsonConvert.SerializeObject(profile, Formatting.Indented));
                MessageBox.Show(this, "Profile exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }

    public void HandleEvent(SessionStarted e)
    {
        Hide();
    }

    public void HandleEvent(SessionEnded e)
    {
        Show();
    }
}