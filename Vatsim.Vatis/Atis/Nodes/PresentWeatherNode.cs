using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class PresentWeatherNode : BaseNode<WeatherPhenomena>
{
    public PresentWeatherNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.PresentWeather);
    }

    public void Parse(WeatherPhenomena[] nodes)
    {
        if (nodes == null)
            return;

        var voiceAtis = new List<string>();
        var textAtis = new List<string>();

        foreach (var node in nodes)
        {
            voiceAtis.Add(FormatWeather(node));
            textAtis.Add(node.RawValue);
        }

        var voiceTemplate = Composite.AtisFormat.PresentWeather.Template.Voice;
        var textTemplate = Composite.AtisFormat.PresentWeather.Template.Text;

        VoiceAtis = Regex.Replace(voiceTemplate, "{weather}", string.Join(", ", voiceAtis).Trim(',').Trim(' '), RegexOptions.IgnoreCase);
        TextAtis = Regex.Replace(textTemplate, "{weather}", string.Join(" ", textAtis).Trim(' '), RegexOptions.IgnoreCase);
    }

    private string FormatWeather(WeatherPhenomena node)
    {
        var result = new List<string>();

        var validTypes = new string[] { "RA", "SN", "PL", "GS", "GR" };
        if (node.Descriptor == "SH" && validTypes.Contains(node.Type))
        {
            if (node.IntensityProximity == "-")
            {
                result.Add(Composite.AtisFormat.PresentWeather.LightIntensity);
            }
            else if (node.IntensityProximity == "+")
            {
                result.Add(Composite.AtisFormat.PresentWeather.HeavyIntensity);
            }
            else
            {
                result.Add(Composite.AtisFormat.PresentWeather.ModerateIntensity);
            }

            result.Add(Composite.AtisFormat.PresentWeather.WeatherDescriptors[node.Type]);
            result.Add(Composite.AtisFormat.PresentWeather.WeatherDescriptors[node.Descriptor]);
        }
        else
        {
            if (!string.IsNullOrEmpty(node.IntensityProximity))
            {
                switch (node.IntensityProximity)
                {
                    case "+":
                        result.Add(Composite.AtisFormat.PresentWeather.HeavyIntensity);
                        break;
                    case "-":
                        result.Add(Composite.AtisFormat.PresentWeather.LightIntensity);
                        break;
                    default:
                        result.Add(Composite.AtisFormat.PresentWeather.ModerateIntensity);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(node.Descriptor))
            {
                result.Add(Composite.AtisFormat.PresentWeather.WeatherDescriptors[node.Descriptor]);
            }

            if (!string.IsNullOrEmpty(node.Type))
            {
                result.Add(Composite.AtisFormat.PresentWeather.WeatherDescriptors[node.Type]);
            }

            if (node.IntensityProximity == "VC")
            {
                result.Add(Composite.AtisFormat.PresentWeather.Vicinity);
            }
        }

        return string.Join(" ", result);
    }

    public override string ParseTextVariables(WeatherPhenomena node, string format)
    {
        throw new System.NotImplementedException();
    }

    public override string ParseVoiceVariables(WeatherPhenomena node, string format)
    {
        throw new System.NotImplementedException();
    }
}