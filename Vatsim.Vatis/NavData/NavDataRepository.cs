using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vatsim.Vatis.Io;

namespace Vatsim.Vatis.NavData;

public class NavDataRepository : INavDataRepository
{
    private List<Airport> mAirports = new();
    private List<Navaid> mNavaids = new();

    public async Task Initialize()
    {
        await Task.WhenAll(LoadNavaidDatabase(), LoadAirportDatabase());
    }

    public Airport? GetAirport(string id)
    {
        return mAirports.FirstOrDefault(a => a.ID == id.ToUpper());
    }

    public Navaid? GetNavaid(string id)
    {
        return mNavaids.FirstOrDefault(t => t.ID == id.ToUpper());
    }

    private async Task LoadAirportDatabase()
    {
        mAirports = await Task.Run(() =>
        {
            using FileStream source = File.OpenRead(PathProvider.AirportsFilePath);
            using StreamReader reader = new StreamReader(source);
            return JsonConvert.DeserializeObject<List<Airport>>(reader.ReadToEnd());
        });
    }

    private async Task LoadNavaidDatabase()
    {
        mNavaids = await Task.Run(() =>
        {
            using FileStream source = File.OpenRead(PathProvider.NavaidsFilePath);
            using StreamReader reader = new StreamReader(source);
            return JsonConvert.DeserializeObject<List<Navaid>>(reader.ReadToEnd());
        });
    }
}
