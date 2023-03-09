using System.Threading.Tasks;

namespace Vatsim.Vatis.AudioForVatsim;

public interface IAudioManager
{
    Task AddOrUpdateBot(byte[] audio, string callsign, uint frequency, double lat, double lon);
    Task RemoveBot(string callsign);
}