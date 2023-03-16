using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class ObservationTimeNode : AtisNode
{
    public ObservationTimeNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.ObservationDayTime);
    }

    public void Parse(ObservationDayTime node)
    {
        var minutes = node.Time.Minutes;

        var isSpecial = Composite.ObservationTime != null
            && Composite.ObservationTime.Enabled
            && Composite.ObservationTime.Time != minutes;

        var useZuluPrefix = Composite.UseZuluTimeSuffix || Composite.UseFaaFormat;

        VoiceAtis = string.Join(" ", string.Join(" ",
            node.Time.Hours.ToString("00").NumberToSingular(),
            node.Time.Minutes.ToString("00").NumberToSingular(),
            useZuluPrefix ? "zulu" : ""), isSpecial ? "special" : "");

        TextAtis = $"{node.Time.Hours:00}{node.Time.Minutes:00}{(useZuluPrefix ? "Z" : "")}";
    }
}