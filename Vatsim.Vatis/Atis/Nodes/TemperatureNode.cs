using System;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class TemperatureNode : BaseNode<TemperatureInfo>
{
    public TemperatureNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.Temperature);
    }

    public void Parse(TemperatureInfo node)
    {
        if (node == null || node.Temperature == null)
        {
            VoiceAtis = "Temperature missing";
            return;
        }

        VoiceAtis = ParseVoiceVariables(node, Composite.AtisFormat.Temperature.Template.Voice);
        TextAtis = ParseTextVariables(node, Composite.AtisFormat.Temperature.Template.Text);
    }

    public override string ParseTextVariables(TemperatureInfo node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{temp}", string.Concat(node.Temperature < 0 ? "M" : "", Math.Abs(node.Temperature.Value).ToString("00")), RegexOptions.IgnoreCase);

        return format;
    }

    public override string ParseVoiceVariables(TemperatureInfo node, string format)
    {
        if (node == null)
            return "";

        if (node.Temperature < 0)
        {
            format = Regex.Replace(format, "{temp}", "minus" + Math.Abs(node.Temperature.Value).ToString(Composite.AtisFormat.Temperature.PronounceLeadingZero ? "00" : "").NumberToSingular(), RegexOptions.IgnoreCase);
        }
        else
        {
            format = Regex.Replace(format, "{temp}", (Composite.AtisFormat.Temperature.UsePlusPrefix ? "plus" : "") + Math.Abs(node.Temperature.Value).ToString(Composite.AtisFormat.Temperature.PronounceLeadingZero ? "00" : "").NumberToSingular(), RegexOptions.IgnoreCase);
        }

        return format;
    }
}