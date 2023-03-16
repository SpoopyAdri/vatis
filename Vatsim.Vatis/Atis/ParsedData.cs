using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public static class NodeParser
{
    public static ParsedData Parse<T>(Metar metar, AtisComposite composite) where T : AtisMeta, new()
    {
        T obj = new T();
        obj.Composite = composite;
        obj.Parse(metar);

        return new ParsedData
        {
            TextAtis = obj.TextAtis,
            VoiceAtis = obj.VoiceAtis,
        };
    }
}

public class ParsedData
{
    public string TextAtis { get; set; }
    public string VoiceAtis { get; set; }
}