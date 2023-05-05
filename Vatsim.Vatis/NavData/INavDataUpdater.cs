using System.Threading.Tasks;

namespace Vatsim.Vatis.NavData;

public interface INavDataUpdater
{
    Task CheckForNewNavData();
}