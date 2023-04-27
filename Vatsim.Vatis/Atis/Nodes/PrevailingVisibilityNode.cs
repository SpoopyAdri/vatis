using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class PrevailingVisibilityNode : BaseNode<PrevailingVisibility>
{
    public PrevailingVisibilityNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.PrevailingVisibility);
    }

    public void Parse(PrevailingVisibility node)
    {
        if (node == null)
            return;

        VoiceAtis = ParseVoiceVariables(node, Composite.AtisFormat.Visibility.Template.Voice);
        TextAtis = ParseTextVariables(node, Composite.AtisFormat.Visibility.Template.Text);
    }

    public override string ParseTextVariables(PrevailingVisibility node, string format)
    {
        if (node == null)
            return "";

        if (node.VisibilityInMeters != null && node.VisibilityInMeters.VisibilityValue == 9999)
        {
            return Composite.AtisFormat.Visibility.UnlimitedVisibilityText;
        }

        return Regex.Replace(format, "{visibility}", node.RawValue, RegexOptions.IgnoreCase);
    }

    public override string ParseVoiceVariables(PrevailingVisibility node, string format)
    {
        if (node == null)
            return "";

        var parsedValue = new List<string>();

        if (node.IsCavok)
        {
            return "CAV-OK";
        }
        else
        {
            if (node.VisibilityInMeters != null)
            {
                if (node.VisibilityInMeters.VisibilityValue == 9999)
                {
                    return Composite.AtisFormat.Visibility.UnlimitedVisibilityVoice;
                }
                else
                {
                    if (node.VisibilityInMeters.VisibilityDirection != Weather.Enums.VisibilityDirection.NotSet)
                    {
                        var minVis = node.VisibilityInMeters.VisibilityValue;

                        switch (node.VisibilityInMeters.VisibilityDirection)
                        {
                            case Weather.Enums.VisibilityDirection.North:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.North} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.NorthEast:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.NorthEast} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.East:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.East} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.SouthEast:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.SouthEast} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.South:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.South} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.SouthWest:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.SouthWest} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.West:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.West} {minVis.ToGroupForm()}");
                                break;
                            case Weather.Enums.VisibilityDirection.NorthWest:
                                parsedValue.Add($"{Composite.AtisFormat.Visibility.NorthWest} {minVis.ToGroupForm()}");
                                break;
                        }

                        if (Composite.AtisFormat.Visibility.IncludeVisibilitySuffix)
                        {
                            if (node.VisibilityInMeters.VisibilityValue > Composite.AtisFormat.Visibility.MetersCutoff)
                            {
                                parsedValue.Add("kilometers");
                            }
                            else
                            {
                                parsedValue.Add("meters");
                            }
                        }
                    }
                    else
                    {
                        if (node.VisibilityInMeters.VisibilityValue > Composite.AtisFormat.Visibility.MetersCutoff)
                        {
                            parsedValue.Add($"{node.VisibilityInMeters.VisibilityValue / 1000} {(Composite.AtisFormat.Visibility.IncludeVisibilitySuffix ? "kilometers" : "")}");
                        }
                        else
                        {
                            parsedValue.Add($"{node.VisibilityInMeters.VisibilityValue.ToWordString()} {(Composite.AtisFormat.Visibility.IncludeVisibilitySuffix ? "meters" : "")}");
                        }
                    }
                }
            }
            else
            {
                if (node.RawValue.Contains('/'))
                {
                    string result = "";
                    switch (node.RawValue)
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
                    parsedValue.Add(result);
                }
                else
                {
                    parsedValue.Add(node.VisibilityInStatuteMiles.WholeNumber.ToString());
                }
            }

            return Regex.Replace(format, "{visibility}", string.Join(", ", parsedValue), RegexOptions.IgnoreCase);
        }
    }
}