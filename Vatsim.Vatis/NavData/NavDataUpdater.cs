using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Io;

namespace Vatsim.Vatis.NavData;
public class NavDataUpdater : INavDataUpdater
{
    private readonly IDownloader mDownloader;
    private const string NAVDATA_VERSION_URL = "https://vatis.clowd.io/api/v4/navdata";

    public NavDataUpdater(IDownloader downloader)
    {
        mDownloader = downloader;
    }

    public async Task CheckForNewNavData()
    {
        EventBus.Publish(this, new StartupStatusChanged("Checking for new navigation data"));
        var localNavDataSerial = await GetLocalNavDataSerial();
        Log.Debug($"Local NavData serial number {localNavDataSerial}");
        var availableNavData = await mDownloader.DownloadJsonStringAsync<AvailableNavData>(NAVDATA_VERSION_URL);
        if (availableNavData != null)
        {
            if (File.Exists(PathProvider.AirportsFilePath)
                && File.Exists(PathProvider.NavaidsFilePath)
                && availableNavData.NavDataSerial == localNavDataSerial)
            {
                Log.Information("Navigation data is up to date");
            }
            else
            {
                await DownloadNavData(availableNavData);
            }
        }
    }

    private async Task DownloadNavData(AvailableNavData availableNavData)
    {
        if (!string.IsNullOrEmpty(availableNavData.AirportDataUrl))
        {
            Log.Information($"Downloading airport navdata from {availableNavData.AirportDataUrl}");
            await mDownloader.DownloadFileAsync(availableNavData.AirportDataUrl, PathProvider.AirportsFilePath, new Progress<int>((int percent) =>
            {
                EventBus.Publish(this, new StartupStatusChanged($"Downloading airport navdata: {percent}%"));
            }));
        }

        if (!string.IsNullOrEmpty(availableNavData.NavaidDataUrl))
        {
            Log.Information($"Downloading navaid navdata from {availableNavData.NavaidDataUrl}");
            await mDownloader.DownloadFileAsync(availableNavData.NavaidDataUrl, PathProvider.NavaidsFilePath, new Progress<int>((int percent) =>
            {
                EventBus.Publish(this, new StartupStatusChanged($"Downloading navaid navdata: {percent}%"));
            }));
        }

        await File.WriteAllTextAsync(PathProvider.NavDataSerialFilePath, JsonConvert.SerializeObject(availableNavData.NavDataSerial));
    }

    private async Task<string> GetLocalNavDataSerial()
    {
        if (!File.Exists(PathProvider.NavDataSerialFilePath))
        {
            return "";
        }
        return JsonConvert.DeserializeObject<string>(await File.ReadAllTextAsync(PathProvider.NavDataSerialFilePath));
    }
}
