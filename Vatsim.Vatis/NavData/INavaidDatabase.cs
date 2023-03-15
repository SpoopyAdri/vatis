using System.Threading.Tasks;

namespace Vatsim.Vatis.NavData;

public interface INavaidDatabase
{
    Airport GetAirport(string id);
    Navaid GetNavaid(string id);
    Task LoadDatabases();
}