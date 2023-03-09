using System;
using System.Collections.Generic;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class VisibilityMeta : AtisMeta
{
    private AtisComposite mComposite;

    public VisibilityMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(DecodedMetar metar)
    {
        List<string> tts = new List<string>();
        if (metar.Cavok)
        {
            tts.Add("CAV-OK");
        }
        else if (metar.Visibility != null && metar.Visibility.PrevailingVisibility != null)
        {
            if (metar.IsInternational)
            {
                if (metar.Visibility.PrevailingVisibility.ActualValue == 9999)
                {
                    tts.Add("Visibility one, zero kilometers or more");
                }
                else
                {
                    if (metar.Visibility.PrevailingVisibility.ActualValue > 5000)
                    {
                        tts.Add($"Visibility {Math.Round(metar.Visibility.PrevailingVisibility.ActualValue / 1000)} {(mComposite.UseVisibilitySuffix ? "kilometers" : "")}");
                    }
                    else
                    {
                        tts.Add($"Visibility {Convert.ToInt32(metar.Visibility.PrevailingVisibility.ActualValue).NumbersToWords()} {(mComposite.UseVisibilitySuffix ? "meters" : "")}");
                    }
                }
            }
            else
            {
                if (metar.Visibility.RawValue != null && metar.Visibility.RawValue.Contains("/"))
                {
                    string result = "";
                    switch (metar.Visibility.RawValue)
                    {
                        case "M1/4SM":
                            result = "less than one quarter.";
                            break;
                        case "1 1/8SM":
                            result = "one and one eighth.";
                            break;
                        case "1 1/4SM":
                            result = "one and one quarter.";
                            break;
                        case "1 3/8SM":
                            result = "one and three eighths.";
                            break;
                        case "1 1/2SM":
                            result = "one and one half.";
                            break;
                        case "1 5/8SM":
                            result = "one and five eighths.";
                            break;
                        case "1 3/4SM":
                            result = "one and three quarters.";
                            break;
                        case "1 7/8SM":
                            result = "one and seven eighths.";
                            break;
                        case "2 1/4SM":
                            result = "two and one quarter.";
                            break;
                        case "2 1/2SM":
                            result = "two and one half.";
                            break;
                        case "2 3/4SM":
                            result = "two and three quarters.";
                            break;
                        case "1/16SM":
                            result = "one sixteenth.";
                            break;
                        case "1/8SM":
                            result = "one eighth.";
                            break;
                        case "3/16SM":
                            result = "three sixteenths.";
                            break;
                        case "1/4SM":
                            result = "one quarter.";
                            break;
                        case "5/16SM":
                            result = "five sixteenths.";
                            break;
                        case "3/8SM":
                            result = "three eighths.";
                            break;
                        case "1/2SM":
                            result = "one half.";
                            break;
                        case "5/8SM":
                            result = "five eighths.";
                            break;
                        case "3/4SM":
                            result = "three quarters.";
                            break;
                        case "7/8SM":
                            result = "seven eighths.";
                            break;
                    }
                    tts.Add($"Visibility { result }");
                }
                else
                {
                    tts.Add($"Visibility {metar.Visibility.PrevailingVisibility.ActualValue}");
                }
            }

            if (metar.Visibility.MinimumVisibility != null)
            {
                switch (metar.Visibility.MinimumVisibilityDirection)
                {
                    case "N":
                        tts.Add($"To the north {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "NE":
                        tts.Add($"To the north-east {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "NW":
                        tts.Add($"To the north-west {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "S":
                        tts.Add($"To the south {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "SE":
                        tts.Add($"To the south-east {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "SW":
                        tts.Add($"To the south-west {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "E":
                        tts.Add($"To the east {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                    case "W":
                        tts.Add($"To the west {metar.Visibility.MinimumVisibility.ActualValue.MetricToString()}");
                        break;
                }
            }
        }
        TextToSpeech = string.Join(", ", tts).TrimEnd(',').TrimEnd(' ');
        Acars = metar?.Visibility?.RawValue ?? "";
    }
}