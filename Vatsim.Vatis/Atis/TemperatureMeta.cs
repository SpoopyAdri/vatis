using System;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class TemperatureMeta : AtisMeta
{
    public TemperatureMeta()
    { }

    public override void Parse(Metar metar)
    {
        if (Composite.UseTemperaturePlusPrefix && metar.Temperature.DewPoint > 0)
        {
            VoiceAtis = $"Temperature plus {metar.Temperature.Value.NumberToSingular()}";
        }
        else
        {
            VoiceAtis = $"Temperature {metar.Temperature.Value.NumberToSingular()}";
        }

        TextAtis = string.Concat((metar.Temperature.Value < 0) ? "M" : "", Math.Abs(metar.Temperature.Value).ToString("00"));
    }
}