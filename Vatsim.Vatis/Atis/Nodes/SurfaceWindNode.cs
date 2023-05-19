using System.Collections.Generic;
using System.Text.RegularExpressions;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Extensions;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis.Nodes;

public class SurfaceWindNode : BaseNode<SurfaceWind>
{
    public SurfaceWindNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.SurfaceWind);
    }

    public void Parse(SurfaceWind node)
    {
        List<string> tts = new();
        List<string> acars = new();

        if (node == null)
            return;

        if (node.GustSpeed > 0)
        {
            // VRB10G20KT
            if (node.IsVariable)
            {
                var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.VariableGust.Template.Voice);
                tts.Add(voice);

                var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.VariableGust.Template.Text);
                acars.Add(text);
            }
            // 25010G16KT
            else
            {
                var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.StandardGust.Template.Voice);
                tts.Add(voice);

                var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.StandardGust.Template.Text);
                acars.Add(text);
            }
        }
        // 25010KT
        else
        {
            if (node.Direction > 0)
            {
                // calm wind
                if (node.Speed <= Composite.AtisFormat.SurfaceWind.Calm.CalmWindSpeed)
                {
                    var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.Calm.Template.Voice);
                    tts.Add(voice);

                    var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.Calm.Template.Text);
                    acars.Add(text);
                }
                else
                {
                    var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.Standard.Template.Voice);
                    tts.Add(voice);

                    var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.Standard.Template.Text);
                    acars.Add(text);
                }
            }
            // 00000KT (calm)
            else if(node.Direction == 0 && node.Speed == 0)
            {
                var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.Calm.Template.Voice);
                tts.Add(voice);

                var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.Calm.Template.Text);
                acars.Add(text);
            }
        }

        // VRB10KT
        if (node.GustSpeed == 0 && node.IsVariable)
        {
            var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.Variable.Template.Voice);
            tts.Add(voice);

            var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.Variable.Template.Text);
            acars.Add(text);
        }

        // 250V360
        if (node.ExtremeWindDirections != null)
        {
            var voice = ParseVoiceVariables(node, Composite.AtisFormat.SurfaceWind.VariableDirection.Template.Voice);
            tts.Add(voice);

            var text = ParseTextVariables(node, Composite.AtisFormat.SurfaceWind.VariableDirection.Template.Text);
            acars.Add(text);
        }

        VoiceAtis = string.Join(", ", tts).TrimEnd(',').TrimEnd(' ');
        TextAtis = string.Join(" ", acars).TrimEnd(' ');
    }

    private string GetSpokenWindUnit(SurfaceWind node)
    {
        var windUnitSpoken = "";
        switch (node.WindUnit)
        {
            case Weather.Enums.WindUnit.KilometersPerHour:
                windUnitSpoken = node.Speed > 1 ? "kilometers per hour" : "kilometer per hour";
                break;
            case Weather.Enums.WindUnit.MetersPerSecond:
                windUnitSpoken = node.Speed > 1 ? "meters per second" : "meter per second";
                break;
            case Weather.Enums.WindUnit.Knots:
                windUnitSpoken = node.Speed > 1 ? "knots" : "knot";
                break;
        }

        return windUnitSpoken;
    }

    public override string ParseVoiceVariables(SurfaceWind node, string format)
    {
        if (node == null)
            return "";

        var magVarDeg = Composite.AtisFormat.SurfaceWind.MagneticVariation?.MagneticDegrees ?? null;
        var leadingZero = Composite.AtisFormat.SurfaceWind.SpeakLeadingZero ? "00" : "";

        format = Regex.Replace(format, "{wind_dir}", node.Direction.ApplyMagVar(magVarDeg).ToString("000").ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{wind_spd}", node.Speed.ToString(leadingZero).ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{wind_spd\|kt}", node.ToKts(node.Speed).ToString(leadingZero).ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{wind_spd\|mps}", node.ToMps(node.Speed).ToString(leadingZero).ToSerialForm(), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{wind_gust}", node.GustSpeed?.ToString(leadingZero).ToSerialForm() ?? "", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{wind_gust\|kt}", (node.GustSpeed.HasValue ? node.ToKts(node.GustSpeed.Value).ToString(leadingZero).ToSerialForm() : ""), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, @"{wind_gust\|mps}", (node.GustSpeed.HasValue ? node.ToMps(node.GustSpeed.Value).ToString(leadingZero).ToSerialForm() : ""), RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{wind_vmin}", node.ExtremeWindDirections?.FirstExtremeDirection.ApplyMagVar(magVarDeg).ToString("000").ToSerialForm() ?? "", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{wind_vmax}", node.ExtremeWindDirections?.LastExtremeWindDirection.ApplyMagVar(magVarDeg).ToString("000").ToSerialForm() ?? "", RegexOptions.IgnoreCase);
        format = Regex.Replace(format, "{wind_unit}", GetSpokenWindUnit(node), RegexOptions.IgnoreCase);

        return format;
    }

    public override string ParseTextVariables(SurfaceWind node, string format)
    {
        if (node == null)
            return "";

        var magVarDeg = Composite.AtisFormat.SurfaceWind.MagneticVariation?.MagneticDegrees ?? null;

        format = Regex.Replace(format, "{wind_dir}", node.Direction.ApplyMagVar(magVarDeg).ToString("000"));
        format = Regex.Replace(format, "{wind_spd}", node.Speed.ToString("00"));
        format = Regex.Replace(format, @"{wind_spd\|kt}", node.ToKts(node.Speed).ToString("00"));
        format = Regex.Replace(format, @"{wind_spd\|mps}", node.ToMps(node.Speed).ToString("00"));
        format = Regex.Replace(format, "{wind_gust}", node.GustSpeed?.ToString("00") ?? "");
        format = Regex.Replace(format, @"{wind_gust\|kt}", (node.GustSpeed.HasValue ? node.ToKts(node.GustSpeed.Value).ToString("00") : ""));
        format = Regex.Replace(format, @"{wind_gust\|mps}", (node.GustSpeed.HasValue ? node.ToMps(node.GustSpeed.Value).ToString("00") : ""));
        format = Regex.Replace(format, "{wind_vmin}", node.ExtremeWindDirections?.FirstExtremeDirection.ApplyMagVar(magVarDeg).ToString("000") ?? "");
        format = Regex.Replace(format, "{wind_vmax}", node.ExtremeWindDirections?.LastExtremeWindDirection.ApplyMagVar(magVarDeg).ToString("000") ?? "");
        format = Regex.Replace(format, "{wind_unit}", EnumTranslator.GetEnumDescription(node.WindUnit));
        format = Regex.Replace(format, "{wind}", node.RawValue);
        return format;
    }
}