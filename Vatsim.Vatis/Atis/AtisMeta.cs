using Vatsim.Vatis.MetarParser.Entity;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public abstract class AtisMeta
{
    public string TextToSpeech { get; set; } = "";
    public string Acars { get; set; } = "";
    public abstract void Parse(Metar metar);
}