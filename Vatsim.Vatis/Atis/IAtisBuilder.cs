using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.Atis;

public interface IAtisBuilder
{
    Task BuildVoiceAtis(Composite composite, CancellationToken cancellationToken);
    void BuildTextAtis(Composite composite);
    Task UpdateIds(Composite composite, CancellationToken cancellationToken);
}