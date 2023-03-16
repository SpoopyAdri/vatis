using System;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class DewpointMeta : AtisMeta
{
    public DewpointMeta()
    { }

    public override void Parse(Metar metar)
    {
        if (Composite.UseTemperaturePlusPrefix && metar.Temperature.DewPoint > 0)
        {
            VoiceAtis = $"Dewpoint plus {metar.Temperature.DewPoint.NumberToSingular()}";
        }
        else
        {
            VoiceAtis = $"Dewpoint {metar.Temperature.DewPoint.NumberToSingular()}";
        }

        TextAtis = string.Concat((metar.Temperature.DewPoint < 0) ? "M" : "", Math.Abs(metar.Temperature.DewPoint).ToString("00"));
    }
}