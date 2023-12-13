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

    public Dictionary<string, string> WeatherDescriptors { get; set; } = new()
    {
        { "BC", "patches" },
        { "BL", "blowing" },
        { "BR", "mist" },
        { "DZ", "drizzle" },
        { "DS", "dust storm" },
        { "DR", "low drifting" },
        { "DU", "widespread dust" },
        { "FC", "funnel cloud tornado waterspout" },
        { "FG", "fog" },
        { "FZ", "freezing" },
        { "FU", "smoke" },
        { "GR", "hail" },
        { "GS", "small hail, snow pellets" },
        { "HZ", "haze" },
        { "IC", "ice crystals" },
        { "MI", "shallow" },
        { "PL", "ice pellets" },
        { "PO", "well developed dust, sand whirls" },
        { "PR", "partial" },
        { "PY", "spray" },
        { "RA", "rain" },
        { "SA", "sand" },
        { "SG", "snow grains" },
        { "SH", "showers" },
        { "SN", "snow" },
        { "SQ", "squalls" },
        { "SS", "sandstorm" },
        { "TS", "thunderstorm" },
        { "UP", "unknown precipitation" },
        { "VA", "volcanic ash" },
    };
}