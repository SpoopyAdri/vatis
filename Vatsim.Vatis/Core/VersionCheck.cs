using System;
using System.Reflection;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using RestSharp;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.UI;
using Vatsim.Vatis.UI.Dialogs;

namespace Vatsim.Vatis.Core;

internal class VersionCheck : IVersionCheck
{
    private const string VersionCheckUrl = "https://vatis.clowd.io/api/v4/VersionCheck";
    private readonly IEventBroker mEventBroker;
    private readonly IWindowFactory mWindowFactory;
    private readonly IAppConfig mAppConfig;

    public VersionCheck(IEventBroker eventBroker, IAppConfig appConfig, IWindowFactory windowFactory)
    {
        mEventBroker = eventBroker;
        mEventBroker.Register(this);
        mAppConfig = appConfig;
        mWindowFactory = windowFactory;
    }

    [EventSubscription(EventTopics.PerformVersionCheck, typeof(OnPublisher))]
    public void OnPerformVersionCheck(object sender, EventArgs e)
    {
        PerformVersionCheck();
    }

    private void PerformVersionCheck()
    {
        try
        {
            var client = new RestClient();
            var request = new RestRequest(VersionCheckUrl);
            var response = client.Get<VersionCheckResponseDto>(request);

            if (response.IsSuccessful)
            {
                var availableVersion = new Version(response.Data.LatestVersion);
                if (availableVersion > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    using var dlg = mWindowFactory.CreateVersionCheckDialog();
                    dlg.TopMost = mAppConfig.WindowProperties.TopMost;
                    dlg.NewVersion = response.Data.LatestProductVersion;
                    dlg.DownloadUrl = response.Data.LatestVersionUrl;
                    dlg.ShowDialog();
                }
            }
        }
        catch { }
    }
}

[Serializable]
internal class VersionCheckResponseDto
{
    public string LatestProductVersion { get; set; }
    public string LatestVersion { get; set; }
    public string LatestVersionUrl { get; set; }
}