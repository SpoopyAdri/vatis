using System.Threading.Tasks;
using Vatsim.Vatis.Io;
using Serilog;
using Newtonsoft.Json;
using System.Reflection;
using System;
using MoreLinq;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Vatsim.Vatis.Events;

namespace Vatsim.Vatis.Updates;

public class ClientUpdater : IClientUpdater
{
    private const string VersionInfoUrl = "https://vatis.clowd.io/api/v4/VersionCheck";
    private readonly IDownloader mDownloader;

    public ClientUpdater(IDownloader downloader)
    {
        mDownloader = downloader;
    }

    public async Task<bool> Run()
    {
        Log.Information("Downloading latest version information");

        var availableVersionInfo = JsonConvert.DeserializeObject<ClientVersionInfo>(await mDownloader.DownloadStringAsync(VersionInfoUrl)) ?? throw new JsonSerializationException("Could not deserialize version info");
        var executingVersion = Assembly.GetExecutingAssembly().GetName().Version;

        Log.Information($"Available version is {availableVersionInfo.LatestVersion}, executing version is {executingVersion}");

        if (executingVersion < availableVersionInfo.LatestVersion)
        {
            var progress = new Progress<int>((int percent) =>
            {
                EventBus.Publish(this, new StartupStatusChanged($"Downloading new version: {percent}%"));
            });
            await InstallNewVersion(availableVersionInfo.LatestVersionUrl, progress);
            return true;
        }

        return false;
    }

    private async Task InstallNewVersion(string downloadUrl, Progress<int> progress)
    {
        var filename = new Uri(downloadUrl).Segments.Last();
        var tempFilePath = Path.Combine(Path.GetTempPath(), filename);
        Log.Information($"Downloading installer from {downloadUrl}");
        await mDownloader.DownloadFileAsync(downloadUrl, tempFilePath, progress);
        Process.Start(tempFilePath, $"\"{string.Join("\" \"", Environment.GetCommandLineArgs())}\"");
    }
}
