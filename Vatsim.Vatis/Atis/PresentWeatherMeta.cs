using System.Collections.Generic;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class PresentWeatherMeta : AtisMeta
{
    public override void Parse(DecodedMetar metar)
    {
        List<string> tts = new List<string>();
        List<string> acars = new List<string>();

        if (metar.PresentWeather != null)
        {
            foreach (WeatherPhenomenon wp in metar.PresentWeather)
            {
                string a = "";
                a += wp.IntensityProximity;
                a += wp.Characteristics;
                foreach (var x in wp.Types)
                {
                    a += x;
                }
                acars.Add(a);

                string result = "";

                if (wp.Characteristics == "SH")
                {
                    foreach (var x in wp.Types)
                    {
                        if (x == "RA" || x == "SN" || x == "PL" || x == "GS" || x == "GR")
                        {
                            if (wp.IntensityProximity == "-")
                            {
                                result += "light ";
                            }
                            else if (wp.IntensityProximity == "+")
                            {
                                result += "heavy ";
                            }
                            result += $"{WeatherTypes[x]}{WeatherCharacteristics[wp.Characteristics]}";
                        }
                    }
                }
                else
                {
                    if (wp.Characteristics == "FZ")
                    {
                        foreach (var x in wp.Types)
                        {
                            if (x == "FG")
                            {
                                result += $"{WeatherCharacteristics[wp.Characteristics]} {WeatherTypes[x]}";
                            }
                        }
                    }
                    else if (wp.Characteristics == "BC")
                    {
                        foreach (var x in wp.Types)
                        {
                            if (x == "FG")
                            {
                                result += $"{WeatherCharacteristics[wp.Characteristics]} {WeatherTypes[x]}";
                            }
                        }
                    }
                    else if (wp.Characteristics == "BL")
                    {
                        foreach (var x in wp.Types)
                        {
                            if (x == "SN")
                            {
                                result += $"{WeatherCharacteristics[wp.Characteristics]} {WeatherTypes[x]}";
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(wp.Characteristics))
                        {
                            if (wp.Characteristics == "TS")
                            {
                                result += WeatherCharacteristics[wp.Characteristics];
                            }
                        }

                        if (!string.IsNullOrEmpty(wp.IntensityProximity))
                        {
                            switch (wp.IntensityProximity)
                            {
                                case "+":
                                    result += "heavy ";
                                    break;
                                case "-":
                                    result += "light ";
                                    break;
                            }
                        }
                        if (wp.Types != null)
                        {
                            List<string> t = new List<string>();
                            foreach (var x in wp.Types)
                            {
                                if (WeatherTypes.ContainsKey(x))
                                {
                                    t.Add(WeatherTypes[x]);
                                }
                            }
                            result += string.Join(", ", t).Trim(',').Trim(' ');
                        }
                    }
                }
                tts.Add(result);
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

    public static Dictionary<string, string> WeatherCharacteristics => new Dictionary<string, string>
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