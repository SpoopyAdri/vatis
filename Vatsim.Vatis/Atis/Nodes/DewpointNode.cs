using System;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class DewpointNode : BaseNode<TemperatureInfo>
{
    public DewpointNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.Temperature);
    }

    public void Parse(TemperatureInfo node)
    {
        if (node == null || node.DewPoint == null)
        {
            VoiceAtis = "Dewpoint missing";
            return;
        }

        VoiceAtis = ParseVoiceVariables(node, Composite.AtisFormat.Dewpoint.Template.Voice);
        TextAtis = ParseTextVariables(node, Composite.AtisFormat.Dewpoint.Template.Text);
    }

    public override string ParseTextVariables(TemperatureInfo node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{dewpoint}", string.Concat(node.DewPoint < 0 ? "M" : "", Math.Abs(node.DewPoint.Value).ToString("00")), RegexOptions.IgnoreCase);

        return format;
    }

    public override string ParseVoiceVariables(TemperatureInfo node, string format)
    {
        if (node == null)
            return "";

        if (node.DewPoint < 0)
        {
            format = Regex.Replace(format, "{dewpoint}", "minus" + Math.Abs(node.DewPoint.Value).ToString(Composite.AtisFormat.Dewpoint.PronounceLeadingZero ? "00" : "").ToSerialForm(), RegexOptions.IgnoreCase);
        }
        else
        {
            format = Regex.Replace(format, "{dewpoint}", (Composite.AtisFormat.Dewpoint.UsePlusPrefix ? "plus" : "") + Math.Abs(node.DewPoint.Value).ToString(Composite.AtisFormat.Dewpoint.PronounceLeadingZero ? "00" : "").ToSerialForm(), RegexOptions.IgnoreCase);
        }

        return format;
    }
}