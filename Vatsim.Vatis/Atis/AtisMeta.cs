using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public abstract class AtisMeta
{
    public abstract void Parse(Metar metar);
    public AtisComposite Composite { get; set; }
    public string VoiceAtis { get; set; }
    public string TextAtis { get; set; }
}