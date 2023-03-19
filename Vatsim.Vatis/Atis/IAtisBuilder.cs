using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.Atis;

public interface IAtisBuilder
{
    Task BuildAtisAsync(Composite composite, CancellationToken cancellationToken);
    void GenerateAcarsText(Composite composite);
}