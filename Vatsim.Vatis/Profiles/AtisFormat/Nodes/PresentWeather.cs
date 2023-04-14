using System.Collections.Generic;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class PresentWeather : BaseFormat
{
    public PresentWeather()
    {
        Template = new()
        {
            Text = "{weather}",
            Voice = "{weather}"
        };
    }

    public string LightIntensity { get; set; } = "light";
    public string ModerateIntensity { get; set; } = "";
    public string HeavyIntensity { get; set; } = "heavy";
    public string Vicinity { get; set; } = "in vicinity";

    public Dictionary<string, string> WeatherTypes { get; set; } = new()
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

    public Dictionary<string, string> WeatherDescriptors { get; set; } = new()
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