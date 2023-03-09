using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public abstract class AtisMeta
{
    public string TextToSpeech { get; set; }
    public string Acars { get; set; }
    public abstract void Parse(DecodedMetar metar);
}