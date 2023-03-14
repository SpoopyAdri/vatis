using System.Collections.Generic;
using System.Linq;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class PresentWeatherMeta : AtisMeta
{
    public override void Parse(Metar metar)
    {
        var tts = new List<string>();
        var acars = new List<string>();

        if (metar.PresentWeather != null)
        {
            foreach (var weather in metar.PresentWeather)
            {
                var result = new List<string>();

                if (weather.Descriptor == "SH" && 
                    !string.IsNullOrEmpty(weather.Type))
                {
                    string[] validTypes = { "RA", "SN", "PL", "GS", "GS" };
                    if (validTypes.Contains(weather.Type))
                    {
                        if (weather.IntensityProximity == "-")
                        {
                            result.Add("light");
                        }
                        else if (weather.IntensityProximity == "+")
                        {
                            result.Add("heavy");
                        }
                        result.Add(WeatherTypes[weather.Type]);
                        result.Add(WeatherDescriptors[weather.Descriptor]);
                    }
                }
                else
                {
                    string[] validDescriptors = { "FZ", "BC", "BL" };
                    if (!string.IsNullOrEmpty(weather.Descriptor) &&
                        !string.IsNullOrEmpty(weather.Type) &&
                        validDescriptors.Contains(weather.Descriptor))
                    {
                        if (weather.IntensityProximity == "-")
                        {
                            result.Add("light");
                        }
                        else if (weather.IntensityProximity == "+")
                        {
                            result.Add("heavy");
                        }

                        result.Add(WeatherDescriptors[weather.Descriptor]);
                        result.Add(WeatherTypes[weather.Type]);

                        if (weather.IntensityProximity == "VC")
                        {
                            result.Add("in vicinity");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(weather.IntensityProximity))
                        {
                            switch (weather.IntensityProximity)
                            {
                                case "+":
                                    result.Add("heavy");
                                    break;
                                case "-":
                                    result.Add("light");
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(weather.Descriptor))
                        {
                            result.Add(WeatherDescriptors[weather.Descriptor]);
                        }
                        if (!string.IsNullOrEmpty(weather.Type))
                        {
                            result.Add(WeatherTypes[weather.Type]);
                        }
                        if (weather.IntensityProximity == "VC")
                        {
                            result.Add("in vicinity");
                        }
                    }
                }

                acars.Add(weather.RawValue);
                tts.Add(string.Join(" ", result));
            }
        }

        TextToSpeech = string.Join(", ", tts).Trim(',').Trim(' ');
        Acars = string.Join(" ", acars).Trim(' ');
    }

    public static Dictionary<string, string> WeatherTypes => new Dictionary<string, string>()
    {
        { "DZ", "drizzle" },
        { "RA", "rain" },
        { "SN", "snow" },
        { "SG", "snow grains" },
        { "IC", "ice crystals" },
        { "PL", "ice pellets" },
        { "GR", "hail" },
        { "GS", "small hail" },
        { "UP", "unknown precipitation" },
        { "BR", "mist" },
        { "FG", "fog" },
        { "FU", "smoke" },
        { "VA", "volcanic ash" },
        { "DU", "widespread dust" },
        { "SA", "sand" },
        { "HZ", "haze" },
        { "PY", "spray" },
        { "PO", "well developed dust, sand whirls" },
        { "SQ", "squalls" },
        { "FC", "funnel cloud tornado waterspout" },
        { "SS", "sandstorm" },
        { "DS", "dust storm" }
    };

    public static Dictionary<string, string> WeatherDescriptors => new Dictionary<string, string>
    {
        { "PR", "partial" },
        { "BC", "patches" },
        { "MI", "shallow" },
        { "DR", "low drifting" },
        { "BL", "blowing" },
        { "SH", "showers" },
        { "TS", "thunderstorm" },
        { "FZ", "freezing" }
    };
}