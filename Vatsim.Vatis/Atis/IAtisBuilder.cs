using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.Atis;

public interface IAtisBuilder
{
    Task<(string, byte[])> BuildVoiceAtis(Composite composite, CancellationToken cancellationToken, bool sandbox = false);
    string BuildTextAtis(Composite composite);
    Task UpdateIds(Composite composite, CancellationToken cancellationToken);
}