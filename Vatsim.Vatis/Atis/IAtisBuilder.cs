using System.Threading;
using System.Threading.Tasks;
using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.Atis;

public interface IAtisBuilder
{
    Task BuildAtisAsync(AtisComposite composite, CancellationToken cancellationToken);
    void GenerateAcarsText(AtisComposite composite);
}