using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public abstract class BaseNode<T>
{
    public abstract void Parse(Metar metar);
    public Composite Composite { get; set; }
    public string VoiceAtis { get; set; }
    public string TextAtis { get; set; }
    public abstract string ParseVoiceVariables(T node, string format);
    public abstract string ParseTextVariables(T node, string format);
}