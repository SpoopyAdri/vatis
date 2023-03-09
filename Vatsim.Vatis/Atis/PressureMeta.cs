using System;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class PressureMeta : AtisMeta
{
    public override void Parse(DecodedMetar metar)
    {
        if (metar.Pressure != null)
        {
            if (metar.Pressure.ActualUnit == Value.Unit.MercuryInch)
            {
                TextToSpeech = $"Altimeter {Convert.ToInt32(metar.Pressure.ActualValue).NumberToSingular()}";
                if (!metar.IsInternational)
                {
                    Acars = $"A{ metar.Pressure.ActualValue } ({ metar.Pressure.ActualValue.ToString("0000").NumberToSingular().ToUpper() })";
                }
                else
                {
                    Acars = $"A{ metar.Pressure.ActualValue }";
                }
            }
            else
            {
                TextToSpeech = $"QNH {Convert.ToInt32(metar.Pressure.ActualValue).NumberToSingular()}";
                Acars = $"Q{ metar.Pressure.ActualValue }";
            }
        }
    }
}