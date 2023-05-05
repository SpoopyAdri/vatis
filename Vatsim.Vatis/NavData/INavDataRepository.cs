using System.Threading.Tasks;

namespace Vatsim.Vatis.NavData;

public interface INavDataRepository
{
    Task Initialize();
    Airport? GetAirport(string id);
    Navaid? GetNavaid(string id);
}