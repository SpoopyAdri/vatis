using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Io;
using Vatsim.Vatis.Utils;

namespace Vatsim.Vatis.UI.Controls;
public partial class ExternalAtisGenerator : UserControl
{
    private IDownloader mDownloader;

    public string ExternalUrl
    {
        get => txtExternalUrl.Text;
        set => txtExternalUrl.Text = value;
    }

    public string ArrivalRunways
    {
        get => txtExternalArr.Text;
        set => txtExternalArr.Text = value;
    }

    public string DepartureRunways
    {
        get => txtExternalDep.Text;
        set => txtExternalDep.Text = value;
    }

    public string Approaches
    {
        get => txtExternalApp.Text;
        set => txtExternalApp.Text = value;
    }

    public string Remarks
    {
        get => txtExternalRemarks.Text;
        set => txtExternalRemarks.Text = value;
    }

    public AtisComposite Composite { get; set; }

    public ExternalAtisGenerator(IDownloader downloader)
    {
        InitializeComponent();
        mDownloader = downloader;

        EventBus.Register(this);
    }

    private void txtExternalUrl_TextChanged(object sender, EventArgs e)
    {
        if (!txtExternalUrl.Focused)
            return;

        EventBus.Publish(this, 
            new ExternalAtisConfigChanged(ExternalAtisComponent.Url, ExternalUrl));
    }

    private void txtExternalArr_TextChanged(object sender, EventArgs e)
    {
        if (!txtExternalArr.Focused)
            return;

        EventBus.Publish(this, 
            new ExternalAtisConfigChanged(ExternalAtisComponent.ArrivalRunways, ArrivalRunways));
    }

    private void txtExternalDep_TextChanged(object sender, EventArgs e)
    {
        if (!txtExternalDep.Focused)
            return;

        EventBus.Publish(this,
            new ExternalAtisConfigChanged(ExternalAtisComponent.DepartureRunways, DepartureRunways));
    }

    private void txtExternalApp_TextChanged(object sender, EventArgs e)
    {
        if (!txtExternalApp.Focused)
            return;

        EventBus.Publish(this,
            new ExternalAtisConfigChanged(ExternalAtisComponent.Approaches, Approaches));
    }

    private void txtExternalRemarks_TextChanged(object sender, EventArgs e)
    {
        if (!txtExternalRemarks.Focused)
            return;

        EventBus.Publish(this,
            new ExternalAtisConfigChanged(ExternalAtisComponent.Remarks, Remarks));
    }

    private void txtMetar_TextChanged(object sender, EventArgs e)
    {
        btnTest.Enabled = !string.IsNullOrEmpty(txtMetar.Text);
    }

    private async void btnFetchMetar_Click(object sender, EventArgs e)
    {
        btnFetchMetar.Enabled = false;

        var metar = await mDownloader.DownloadStringAsync("https://metar.vatsim.net/metar.php?id=" + Composite.Identifier);
        txtMetar.Text = metar;
        
        btnFetchMetar.Enabled = true;
        btnTest.Enabled = true;
    }

    private async void btnTest_Click(object sender, EventArgs e)
    {
        var url = txtExternalUrl.Text;
        if (!string.IsNullOrEmpty(url))
        {
            url = url.Replace("$metar", System.Web.HttpUtility.UrlEncode(txtMetar.Text));
            url = url.Replace("$arrrwy", txtExternalArr.Text);
            url = url.Replace("$deprwy", txtExternalDep.Text);
            url = url.Replace("$app", txtExternalApp.Text);
            url = url.Replace("$remarks", txtExternalRemarks.Text);
            url = url.Replace("$atiscode", StringExtensions.RandomLetter());

            btnTest.Text = "Loading...";
            btnTest.Enabled = false;

            var response = await mDownloader.DownloadStringAsync(url);

            response = Regex.Replace(response, @"\[(.*?)\]", " $1 ");
            response = Regex.Replace(response, @"\s+", " ");

            txtResponse.Text = response.Trim(' ');
            btnTest.Text = "Test URL";
            btnTest.Enabled = true;
        }
    }
}
