using System.Collections.Generic;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class SurfaceWindMeta : AtisMeta
{
    private AtisComposite mComposite;

    public SurfaceWindMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(DecodedMetar metar)
    {
        List<string> tts = new List<string>();
        List<string> acars = new List<string>();

        var magvar = mComposite.MagneticVariation?.MagneticDegrees ?? null;

        if (metar.SurfaceWind != null)
        {
            if (metar.SurfaceWind.SpeedVariations != null)
            {
                // VRB10G20KT
                if (metar.SurfaceWind.VariableDirection)
                {
                    if (mComposite.UseSurfaceWindPrefix)
                    {
                        tts.Add($"Surface wind variable { metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular() } gusts { metar.SurfaceWind.SpeedVariations.ActualValue.NumberToSingular() }");
                    }
                    else
                    {
                        tts.Add($"Wind variable at { metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular() } gusts { metar.SurfaceWind.SpeedVariations.ActualValue.NumberToSingular() }");
                    }

                    acars.Add($"VRB{ metar.SurfaceWind.MeanSpeed.ActualValue:00}G{ metar.SurfaceWind.SpeedVariations.ActualValue:00}{metar.SurfaceWind.MeanSpeed.ActualUnit.PrintUnitShort()}");
                }
                // 25010G16KT
                else
                {
                    if (metar.IsInternational)
                    {
                        var speedUnit = "";
                        switch (metar.SurfaceWind.MeanSpeed.ActualUnit)
                        {
                            case Value.Unit.KilometerPerHour:
                                speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "kilometers per hour" : "kilometer per hour";
                                break;
                            case Value.Unit.MeterPerSecond:
                                speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "meters per second" : "meter per second";
                                break;
                            case Value.Unit.KT:
                                speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "knots" : "knot";
                                break;
                        }

                        tts.Add($"{(mComposite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}{ metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } degrees, { metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular() } { speedUnit } gusts { metar.SurfaceWind.SpeedVariations.ActualValue.NumberToSingular() }");
                    }
                    else
                    {
                        tts.Add($"Wind { metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } at {metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular()} gusts {metar.SurfaceWind.SpeedVariations.ActualValue.NumberToSingular()}");
                    }

                    acars.Add($"{ metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar):000}{ metar.SurfaceWind.MeanSpeed.ActualValue:00}G{ metar.SurfaceWind.SpeedVariations.ActualValue:00}{metar.SurfaceWind.MeanSpeed.ActualUnit.PrintUnitShort()}");
                }
            }
            // 25010KT
            else
            {
                if (metar.SurfaceWind.MeanDirection != null)
                {
                    if (metar.IsInternational)
                    {
                        tts.Add($"{(mComposite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}{ metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } degrees, {metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular()} {metar.SurfaceWind.MeanSpeed.ActualUnit.PrintUnit(metar.SurfaceWind.MeanSpeed.ActualValue)}");
                    }
                    else
                    {
                        if (metar.SurfaceWind.MeanDirection.ActualValue == 0 && metar.SurfaceWind.MeanSpeed.ActualValue == 0)
                        {
                            tts.Add($"Wind calm");
                        }
                        else
                        {
                            tts.Add($"Wind { metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } at {metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular()}");
                        }
                    }

                    acars.Add($"{ metar.SurfaceWind.MeanDirection.ActualValue.ApplyMagVar(magvar):000}{ metar.SurfaceWind.MeanSpeed.ActualValue:00}{metar.SurfaceWind.MeanSpeed.ActualUnit.PrintUnitShort()}");
                }
            }

            // VRB10KT
            if (metar.SurfaceWind.SpeedVariations == null && metar.SurfaceWind.VariableDirection)
            {
                if (metar.IsInternational)
                {
                    var speedUnit = "";
                    switch (metar.SurfaceWind.MeanSpeed.ActualUnit)
                    {
                        case Value.Unit.KilometerPerHour:
                            speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "kilometers per hour" : "kilometer per hour";
                            break;
                        case Value.Unit.MeterPerSecond:
                            speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "meters per second" : "meter per second";
                            break;
                        case Value.Unit.KT:
                            speedUnit = metar.SurfaceWind.MeanSpeed.ActualValue > 1 ? "knots" : "knot";
                            break;
                    }
                    tts.Add($"{(mComposite.UseSurfaceWindPrefix ? "Surface Wind " : "Wind ")}variable at { metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular() } {speedUnit}");
                }
                else
                {
                    tts.Add($"Wind variable at { metar.SurfaceWind.MeanSpeed.ActualValue.NumberToSingular() }");
                }

                acars.Add($"VRB{ metar.SurfaceWind.MeanSpeed.ActualValue:00}");
            }

            // 250V360
            if (metar.SurfaceWind.DirectionVariations != null && metar.SurfaceWind.DirectionVariations.Length == 2)
            {
                if (metar.IsInternational)
                {
                    tts.Add($"Varying between { metar.SurfaceWind.DirectionVariations[0].ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } and { metar.SurfaceWind.DirectionVariations[1].ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } degrees");
                }
                else
                {
                    tts.Add($"Wind variable between { metar.SurfaceWind.DirectionVariations[0].ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() } and { metar.SurfaceWind.DirectionVariations[1].ActualValue.ApplyMagVar(magvar).ToString("000").NumberToSingular() }");
                }

                acars.Add($"{ metar.SurfaceWind.DirectionVariations[0].ActualValue.ApplyMagVar(magvar):000}V{ metar.SurfaceWind.DirectionVariations[1].ActualValue.ApplyMagVar(magvar):000}");
            }
        }

        TextToSpeech = string.Join(", ", tts).TrimEnd(',').TrimEnd(' ');
        Acars = string.Join(" ", acars).TrimEnd(' ');
    }
}