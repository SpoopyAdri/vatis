using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class ObservationTimeMeta : AtisMeta
{
    private AtisComposite mComposite;

    public ObservationTimeMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(Metar metar)
    {
        var minutes = metar.ObservationDayTime.Time.Minutes;

        var isSpecial = mComposite.ObservationTime != null
            && mComposite.ObservationTime.Enabled
            && mComposite.ObservationTime.Time != minutes;

        var useZuluPrefix = mComposite.UseZuluTimeSuffix || mComposite.UseFaaFormat;

        VoiceAtis = string.Join(" ", string.Join(" ",
            metar.ObservationDayTime.Time.Hours.ToString("00").NumberToSingular(),
            metar.ObservationDayTime.Time.Minutes.ToString("00").NumberToSingular(),
            useZuluPrefix ? "zulu" : ""), isSpecial ? "special" : "");

        TextAtis = $"{metar.ObservationDayTime.Time.Hours}{metar.ObservationDayTime.Time.Minutes}{(useZuluPrefix ? "Z" : "")}";
    }
}