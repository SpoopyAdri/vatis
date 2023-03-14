using System.Collections.Generic;
using System.Linq;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class CloudsMeta : AtisMeta
{
    private AtisComposite mComposite;

    public CloudsMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(Metar metar)
    {
        var tts = new List<string>();
        var acars = new List<string>();

        if (metar.CloudLayers == null)
            return;

        var cloudPrefixIncluded = false;

        var ceiling = metar.CloudLayers
            .Where(n => n.Altitude > 0 && (n.CloudType == Weather.Enums.CloudType.Overcast ||
                        n.CloudType == Weather.Enums.CloudType.Broken))
            .Select(n => n).OrderBy(n => n.Altitude).FirstOrDefault();

        foreach (var layer in metar.CloudLayers)
        {
            int altitude = layer.Altitude * 100;
            if (mComposite.UseMetricUnits)
                altitude = (int)(altitude * 0.30);
            var cloudType = layer.CloudType == Weather.Enums.CloudType.None 
                ? "" : layer.CloudType.ToString();
            var convectiveType = layer.ConvectiveCloudType == Weather.Enums.ConvectiveCloudType.None 
                ? "" : layer.ConvectiveCloudType.ToString();

            if (!mComposite.UseFaaFormat)
            {
                var result = new List<string>();

                if (!cloudPrefixIncluded &&
                    layer.CloudType != Weather.Enums.CloudType.NoSignificantClouds &&
                    layer.CloudType != Weather.Enums.CloudType.NoCloudDetected &&
                    layer.CloudType != Weather.Enums.CloudType.Clear)
                {
                    result.Add("clouds");
                    cloudPrefixIncluded = true;
                }

                result.Add(cloudType);

                if (layer.ConvectiveCloudType != Weather.Enums.ConvectiveCloudType.None)
                {
                    result.Add(convectiveType);
                }

                if (altitude > 0)
                {
                    result.Add(string.Join(" ", altitude.NumbersToWordsGroup(),
                        mComposite.UseMetricUnits ? "meters" : "feet"));
                }

                tts.Add(string.Join(" ", result));
            }
            else
            {
                if (layer.CloudType == Weather.Enums.CloudType.Few)
                {
                    tts.Add($"few clouds at {altitude.NumbersToWords()}");
                }
                else
                {
                    tts.Add($"{(layer == ceiling ? "ceiling " : "")}{altitude.NumbersToWords()} {cloudType} {convectiveType}");
                }

                acars.Add(cloud.RawValue);
            }
        }

        TextToSpeech = string.Join(", ", tts).Trim(',').Trim(' ');
        Acars = string.Join(" ", acars).TrimEnd(' ');
    }

    public static Dictionary<string, string> CloudCoverage => new Dictionary<string, string>()
    {
        {"FEW", "few "},
        {"SCT", "scattered "},
        {"BKN", "broken "},
        {"OVC", "overcast "},
        {"VV", "vertical visibility "},
        {"NSC", "no significant clouds "},
        {"NCD", "no clouds detected " },
        {"CLR", "sky clear "},
        {"SKC", "sky clear " }
    };

    public static Dictionary<string, string> CloudType => new Dictionary<string, string>()
    {
        {"CB", "cumulonimbus " },
        {"TCU", "towering cumulus " }
    };
}