using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Dialogs;

public partial class VersionCheckDialog : Form, IDisposable
{
    private WebClient mWebClient;
    private string mTempFile;

    public string NewVersion
    {
        get => lblNewVersion.Text;
        set => lblNewVersion.Text = value;
    }
    public string DownloadUrl { get; set; }

    public VersionCheckDialog()
    {
        InitializeComponent();
        lblCurrentVersion.Text = Application.ProductVersion;
    }

    private void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            btnYes.Enabled = false;
            btnNo.Text = "Cancel";
            progressBar.Visible = true;

            mWebClient = new WebClient();
            mWebClient.DownloadProgressChanged += OnDownloadProgressChanged;
            mWebClient.DownloadFileCompleted += OnDownloadFileCompleted;

            Uri downloadUri = new Uri(DownloadUrl);
            mTempFile = Path.Combine(Path.GetTempPath(), downloadUri.Segments.Last());
            mWebClient.DownloadFileAsync(downloadUri, mTempFile);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error downloading update file: " + ex.Message, "Error");
            Close();
        }
    }

    private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            Close();
        }
        else if (e.Error != null)
        {
            string message = e.Error.Message;
            if (e.Error.InnerException != null)
            {
                message = e.Error.InnerException.Message;
            }
            MessageBox.Show(this, $"The following error occured while attempting to download the update:\r\n{ message }", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Close();
        }
        else
        {
            progressBar.Visible = false;
            Process.Start(mTempFile);
            Application.Exit();
        }
    }

    private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        progressBar.Value = e.ProgressPercentage;
    }

    private void btnNo_Click(object sender, EventArgs e)
    {
        if (mWebClient != null)
        {
            mWebClient.CancelAsync();
        }
        else
        {
            Close();
        }
    }
}