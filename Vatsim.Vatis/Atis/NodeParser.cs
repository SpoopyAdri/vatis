using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public static class NodeParser
{
    public static ParsedResult Parse<T>(Metar metar, Composite composite) where T : AtisNode, new()
    {
        T obj = new T();
        obj.Composite = composite;
        obj.Parse(metar);

        return new ParsedResult
        {
            TextAtis = obj.TextAtis,
            VoiceAtis = obj.VoiceAtis,
            Node = obj.Node
        };
    }
}

public class ParsedResult
{
    public string TextAtis { get; set; }
    public string VoiceAtis { get; set; }
    public object Node { get; set; }
}