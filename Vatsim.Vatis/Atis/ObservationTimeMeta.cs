using Vatsim.Vatis.Config;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class ObservationTimeMeta : AtisMeta
{
    public ObservationTimeMeta()
    { }

    public override void Parse(Metar metar)
    {
        var minutes = metar.ObservationDayTime.Time.Minutes;

        var isSpecial = Composite.ObservationTime != null
            && Composite.ObservationTime.Enabled
            && Composite.ObservationTime.Time != minutes;

        var useZuluPrefix = Composite.UseZuluTimeSuffix || Composite.UseFaaFormat;

        VoiceAtis = string.Join(" ", string.Join(" ",
            metar.ObservationDayTime.Time.Hours.ToString("00").NumberToSingular(),
            metar.ObservationDayTime.Time.Minutes.ToString("00").NumberToSingular(),
            useZuluPrefix ? "zulu" : ""), isSpecial ? "special" : "");

        TextAtis = $"{metar.ObservationDayTime.Time.Hours:00}{metar.ObservationDayTime.Time.Minutes:00}{(useZuluPrefix ? "Z" : "")}";
    }
}