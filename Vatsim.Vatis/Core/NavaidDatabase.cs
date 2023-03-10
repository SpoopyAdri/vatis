using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using Vatsim.Vatis.Io;

namespace Vatsim.Vatis.Core;

public class NavaidDatabase : INavaidDatabase
{
    private readonly IDownloader mDownloader;
    private List<Airport> mAirports = new List<Airport>();
    private List<Navaid> mNavaids = new List<Navaid>();

    public NavaidDatabase(IDownloader downloader)
    {
        mDownloader = downloader;
    }

    public Airport GetAirport(string id)
    {
        id = id.ToUpper();
        if (mAirports != null && mAirports.Exists(t => t.ID == id))
        {
            return mAirports.FirstOrDefault(t => t.ID == id);
        }
        return null;
    }

    public Navaid GetNavaid(string id)
    {
        id = id.ToUpper();
        if (mNavaids != null && mNavaids.Exists(t => t.ID == id))
        {
            return mNavaids.FirstOrDefault(t => t.ID == id);
        }
        return null;
    }

    public async Task LoadDatabases()
    {
        await Task.WhenAll(LoadNavaidDatabase(), LoadAirportDatabase());
    }

    private async Task LoadAirportDatabase()
    {
        try
        {
            using var fs = new FileStream(PathProvider.AirportsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs);
            mAirports = JsonConvert.DeserializeObject<List<Airport>>(sr.ReadToEnd());
        }
        catch (FileNotFoundException)
        {
            try
            {
                Log.Information("Downloading missing airport database");
                await mDownloader.DownloadFileAsync("https://vatis.clowd.io/api/v4/Airports", PathProvider.AirportsFilePath, null);
                await LoadAirportDatabase();
            }
            catch { }
        }
    }

    private async Task LoadNavaidDatabase()
    {
        try
        {
            using var fs = new FileStream(PathProvider.NavaidsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs);
            mNavaids = JsonConvert.DeserializeObject<List<Navaid>>(sr.ReadToEnd());
        }
        catch (FileNotFoundException)
        {
            try
            {
                Log.Information("Downloading missing navaid database");
                await mDownloader.DownloadFileAsync("https://vatis.clowd.io/api/v4/Navaids", PathProvider.NavaidsFilePath, null);
                await LoadNavaidDatabase();
            }
            catch { }
        }
    }
}

public class Navaid
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }
}

public class Airport
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Lat")]
    public double Latitude { get; set; }

    [JsonProperty("Lon")]
    public double Longitude { get; set; }
}