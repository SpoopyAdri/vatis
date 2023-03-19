using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public abstract class AtisNode
{
    public abstract void Parse(Metar metar);
    public Composite Composite { get; set; }
    public string VoiceAtis { get; set; }
    public string TextAtis { get; set; }
}