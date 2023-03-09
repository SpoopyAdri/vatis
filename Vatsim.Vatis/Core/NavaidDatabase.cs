using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.Core;

public class NavaidDatabase : INavaidDatabase
{
    private readonly IAppConfig mAppConfig;
    private List<Airport> mAirports = new List<Airport>();
    private List<Navaid> mNavaids = new List<Navaid>();

    public NavaidDatabase(IAppConfig appConfig)
    {
        mAppConfig = appConfig;
        LoadAirportDatabase();
        LoadNavaidDatabase();
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

    private void LoadAirportDatabase()
    {
        try
        {
            var path = Path.Combine(mAppConfig.AppPath, "airports.json");
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    mAirports = JsonConvert.DeserializeObject<List<Airport>>(sr.ReadToEnd());
                }
            }
        }
        catch (FileNotFoundException)
        {
            try
            {
                using var wc = new WebClient();
                wc.DownloadFile("https://vatis.clowd.io/api/v4/Airports", Path.Combine(mAppConfig.AppPath, "airports.json"));
            }
            catch { }
        }
    }

    private void LoadNavaidDatabase()
    {
        try
        {
            var path = Path.Combine(mAppConfig.AppPath, "navaids.json");
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    mNavaids = JsonConvert.DeserializeObject<List<Navaid>>(sr.ReadToEnd());
                }
            }
        }
        catch (FileNotFoundException)
        {
            try
            {
                using var wc = new WebClient();
                wc.DownloadFile("https://vatis.clowd.io/api/v4/Navaids", Path.Combine(mAppConfig.AppPath, "navaids.json"));
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