using System;
using System.Collections.Generic;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class CloudsMeta : AtisMeta
{
    private AtisComposite mComposite;

    public CloudsMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(DecodedMetar metar)
    {
        List<string> tts = new List<string>();
        List<string> acars = new List<string>();

        if (metar.Clouds != null)
        {
            bool cloudPrefixIncluded = false;

            foreach (var cloud in metar.Clouds)
            {
                var cloudCoverage = CloudCoverage.ContainsKey(cloud.Amount.ToString())
                    ? CloudCoverage[cloud.Amount.ToString()] : "";

                var cloudType = CloudType.ContainsKey(cloud.Type.ToString())
                    ? CloudType[cloud.Type.ToString()] : "";

                if (metar.IsInternational)
                {
                    var temp = "";
                    if (!cloudPrefixIncluded && cloud.Amount != CloudLayer.CloudAmount.NSC && cloud.Amount != CloudLayer.CloudAmount.NCD && cloud.Amount != CloudLayer.CloudAmount.CLR)
                    {
                        temp += "clouds ";
                        cloudPrefixIncluded = true;
                    }
                    temp += cloudCoverage;
                    if (cloud.Type != CloudLayer.CloudType.NULL)
                    {
                        temp += string.Join(" ", cloudType);
                    }
                    if (cloud.BaseHeight != null && cloud.BaseHeight.ActualValue > 0)
                    {
                        if (mComposite.UseMetricUnits)
                        {
                            var value = cloud.BaseHeight.ActualValue * 0.3;
                            temp += string.Join(" ", ((int)value).NumbersToWordsGroup(), "meters");
                        }
                        else
                        {
                            temp += string.Join(" ", ((int)cloud.BaseHeight.ActualValue).NumbersToWords(), "feet");
                        }
                    }
                    tts.Add(temp);
                }
                else
                {
                    if (cloud.Amount == CloudLayer.CloudAmount.CLR)
                    {
                        tts.Add("Sky clear below one two thousand");
                        acars.Add("CLR");
                    }
                    else
                    {
                        if (cloud.Amount == CloudLayer.CloudAmount.FEW)
                        {
                            tts.Add($"few clouds at {Convert.ToInt32(cloud.BaseHeight.ActualValue).NumbersToWords()}");
                        }
                        else
                        {
                            tts.Add($"{(cloud.IsCeiling ? "ceiling " : "")}{(cloud.BaseHeight != null ? Convert.ToInt32(cloud.BaseHeight.ActualValue).NumbersToWords() + " " : "")}{cloudCoverage}{(cloud.Type != CloudLayer.CloudType.NULL ? string.Concat(" ", cloudType) : "")}");
                        }
                    }
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