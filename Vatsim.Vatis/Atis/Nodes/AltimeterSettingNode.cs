using System;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class AltimeterSettingNode : BaseNode<AltimeterSetting>
{
    private int mPressureHpa;
    private double mPressureInHg;

    public AltimeterSettingNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.AltimeterSetting);
    }

    public void Parse(AltimeterSetting node)
    {
        if (node == null)
            return;

        if (node.UnitType == Weather.Enums.AltimeterUnitType.InchesOfMercury)
        {
            mPressureInHg = node.Value / 100.0;
            mPressureHpa = (int)Math.Floor((node.Value / 100.0) * 33.86);
        }
        else
        {
            mPressureHpa = node.Value;
            mPressureInHg = (int)Math.Floor((node.Value * 0.0295) * 100) / 100.0;
        }

        VoiceAtis = ParseVoiceVariables(node, Composite.AtisFormat.Altimeter.Template.Voice);
        TextAtis = ParseTextVariables(node, Composite.AtisFormat.Altimeter.Template.Text);
    }

    public override string ParseTextVariables(AltimeterSetting node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{altimeter}", node.Value.ToString(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{altimeter\|inhg}", mPressureInHg.ToString("00.00"), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{altimeter\|hpa}", mPressureHpa.ToString(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{altimeter\|text}", node.Value.ToString("0000").ToSerialForm().ToUpper(), RegexOptions.IgnoreCase);

        return format;
    }

    public override string ParseVoiceVariables(AltimeterSetting node, string format)
    {
        if (node == null)
            return "";

        format = Regex.Replace(format, "{altimeter}", node.Value.ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{altimeter\|inhg}", mPressureInHg.ToString("00.00").ToSerialForm(Composite.AtisFormat.Altimeter.PronounceDecimal), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{altimeter\|hpa}", mPressureHpa.ToSerialForm(), RegexOptions.IgnoreCase);

        return format;
    }
}