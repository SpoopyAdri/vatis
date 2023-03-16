using System.Collections.Generic;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class TrendNode : AtisNode
{
    public TrendNode()
    { }

    public override void Parse(Metar metar)
    {
        if (metar.Trends == null)
            return;

        var tts = new List<string>();
        var acars = new List<string>();

        foreach (var trend in metar.Trends)
        {
            tts.Add("TREND");

            switch (trend.TrendType)
            {
                case Weather.Enums.TrendType.Becoming:
                    tts.Add("BECOMING");
                    acars.Add("BECMG");
                    break;
                case Weather.Enums.TrendType.Tempo:
                    tts.Add("TEMPORARY");
                    acars.Add("TEMPO");
                    break;
                case Weather.Enums.TrendType.NoSignificantChanges:
                    tts.Add("NO SIGNIFICANT CHANGES");
                    acars.Add("NOSIG");
                    break;
            }

            if (trend.SurfaceWind != null)
            {
                var surfaceWind = new SurfaceWindNode();
                surfaceWind.Composite = Composite;
                surfaceWind.Parse(trend.SurfaceWind);
                tts.Add(surfaceWind.VoiceAtis.Trim(' '));
                acars.Add(surfaceWind.TextAtis.Trim(' '));
            }

            if (trend.PrevailingVisibility != null)
            {
                var prevailingVisibility = new PrevailingVisibilityNode();
                prevailingVisibility.Composite = Composite;
                prevailingVisibility.Parse(trend.PrevailingVisibility);
                tts.Add(prevailingVisibility.VoiceAtis.Trim(' '));
                acars.Add(prevailingVisibility.TextAtis.Trim(' '));
            }

            if (trend.PresentWeather != null)
            {
                var presentWeather = new PresentWeatherNode();
                presentWeather.Composite = Composite;
                presentWeather.Parse(trend.PresentWeather);
                tts.Add(presentWeather.VoiceAtis.Trim(' '));
                acars.Add(presentWeather.TextAtis.Trim(' '));
            }

            if (trend.CloudLayers != null)
            {
                var cloudLayers = new CloudNode();
                cloudLayers.Composite = Composite;
                cloudLayers.Parse(trend.CloudLayers);
                tts.Add(cloudLayers.VoiceAtis.Trim(' '));
                acars.Add(cloudLayers.TextAtis.Trim(' '));
            }
        }

        VoiceAtis = string.Join(". ", tts);
        TextAtis = string.Join(" ", acars);
    }
}
