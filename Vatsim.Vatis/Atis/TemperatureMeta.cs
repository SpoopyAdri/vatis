using System;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class TemperatureMeta : AtisMeta
{
    private readonly AtisComposite mComposite;

    public TemperatureMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(Metar metar)
    {
        if (!mComposite.UseFaaFormat && metar.Temperature.DewPoint > 0)
        {
            TextToSpeech = $"Temperature plus {metar.Temperature.Value.NumberToSingular()}";
        }
        else
        {
            TextToSpeech = $"Temperature {metar.Temperature.Value.NumberToSingular()}";
        }

        Acars = string.Concat((metar.Temperature.Value < 0) ? "M" : "", Math.Abs(metar.Temperature.Value).ToString("00"));
    }
}