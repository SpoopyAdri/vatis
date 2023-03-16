using System.Collections.Generic;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Extensions;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class SurfaceWindNode : AtisNode
{
    public SurfaceWindNode()
    { }

    public override void Parse(Metar metar)
    {
        List<string> tts = new();
        List<string> acars = new();

        var magVarDeg = Composite.MagneticVariation?.MagneticDegrees ?? null;

        if (metar == null)
            return;

        var windUnitSpoken = "";
        switch (metar.SurfaceWind.WindUnit)
        {
            case Weather.Enums.WindUnit.KilometersPerHour:
                windUnitSpoken = metar.SurfaceWind.Speed > 1 ? "kilometers per hour" : "kilometer per hour";
                break;
            case Weather.Enums.WindUnit.MetersPerSecond:
                windUnitSpoken = metar.SurfaceWind.Speed > 1 ? "meters per second" : "meter per second";
                break;
            case Weather.Enums.WindUnit.Knots:
                windUnitSpoken = metar.SurfaceWind.Speed > 1 ? "knots" : "knot";
                break;
        }

        var windUnitText = EnumTranslator.GetEnumDescription(metar.SurfaceWind.WindUnit);

        if (metar.SurfaceWind.GustSpeed > 0)
        {
            // VRB10G20KT
            if (metar.SurfaceWind.IsVariable)
            {
                if (Composite.UseSurfaceWindPrefix)
                {
                    tts.Add($"Surface wind variable {metar.SurfaceWind.Speed.NumberToSingular()} gusts {metar.SurfaceWind.GustSpeed.NumberToSingular()}");
                }
                else
                {
                    tts.Add($"Wind variable at {metar.SurfaceWind.Speed.NumberToSingular()} gusts {metar.SurfaceWind.GustSpeed.NumberToSingular()}");
                }

                acars.Add($"VRB{metar.SurfaceWind.Speed:00}G{metar.SurfaceWind.GustSpeed:00}{windUnitText}");
            }
            // 25010G16KT
            else
            {
                if (!Composite.UseFaaFormat)
                {
                    tts.Add($"{(Composite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}{metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} degrees, {metar.SurfaceWind.Speed.NumberToSingular()} {windUnitSpoken} gusts {metar.SurfaceWind.GustSpeed.NumberToSingular()}");
                }
                else
                {
                    tts.Add($"Wind {metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} at {metar.SurfaceWind.Speed.NumberToSingular()} gusts {metar.SurfaceWind.GustSpeed.NumberToSingular()}");
                }

                acars.Add($"{metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg):000}{metar.SurfaceWind.Speed:00}G{metar.SurfaceWind.GustSpeed:00}{windUnitText}");
            }
        }
        // 25010KT
        else
        {
            if (metar.SurfaceWind.Direction > 0)
            {
                if (!Composite.UseFaaFormat)
                {
                    tts.Add($"{(Composite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}{metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} degrees, {metar.SurfaceWind.Speed.NumberToSingular()} {windUnitSpoken}");
                }
                else
                {
                    if (metar.SurfaceWind.Direction == 0 && metar.SurfaceWind.Speed == 0)
                    {
                        tts.Add($"Wind calm");
                    }
                    else
                    {
                        tts.Add($"Wind {metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} at {metar.SurfaceWind.Speed.NumberToSingular()}");
                    }
                }

                acars.Add($"{metar.SurfaceWind.Direction.ApplyMagVar(magVarDeg):000}{metar.SurfaceWind.Speed:00}{windUnitText}");
            }
        }

        // VRB10KT
        if (metar.SurfaceWind.GustSpeed == 0 && metar.SurfaceWind.IsVariable)
        {
            if (!Composite.UseFaaFormat)
            {
                tts.Add($"{(Composite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}variable at {metar.SurfaceWind.Speed.NumberToSingular()} {windUnitSpoken}");
            }
            else
            {
                tts.Add($"Wind variable at {metar.SurfaceWind.Speed.NumberToSingular()}");
            }

            acars.Add($"VRB{metar.SurfaceWind.Speed:00}{windUnitText}");
        }

        // 250V360
        if (metar.SurfaceWind.ExtremeWindDirections != null)
        {
            if (!Composite.UseFaaFormat)
            {
                tts.Add($"Varying between {metar.SurfaceWind.ExtremeWindDirections.FirstExtremeDirection.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} and {metar.SurfaceWind.ExtremeWindDirections.LastExtremeWindDirection.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} degrees");
            }
            else
            {
                tts.Add($"Wind variable between {metar.SurfaceWind.ExtremeWindDirections.FirstExtremeDirection.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()} and {metar.SurfaceWind.ExtremeWindDirections.LastExtremeWindDirection.ApplyMagVar(magVarDeg).ToString("000").NumberToSingular()}");
            }

            acars.Add($"{metar.SurfaceWind.ExtremeWindDirections.FirstExtremeDirection.ApplyMagVar(magVarDeg):000}V{metar.SurfaceWind.ExtremeWindDirections.LastExtremeWindDirection.ApplyMagVar(magVarDeg):000}");
        }

        VoiceAtis = string.Join(", ", tts).TrimEnd(',').TrimEnd(' ');
        TextAtis = string.Join(" ", acars).TrimEnd(' ');
    }
}