using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public abstract class AtisMeta
{
    public string VoiceAtis { get; set; } = "";
    public string TextAtis { get; set; } = "";
    public abstract void Parse(Metar metar);
}