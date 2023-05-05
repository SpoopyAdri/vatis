using System.Threading.Tasks;

namespace Vatsim.Vatis.Network;

public interface IAuthTokenManager
{
    Task<string?> GetAuthToken();
    string? AuthToken { get; }
}