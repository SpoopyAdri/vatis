using System;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class DewpointMeta : AtisMeta
{
    private AtisComposite mComposite;

    public DewpointMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(Metar metar)
    {
        if (!mComposite.UseFaaFormat && metar.Temperature.DewPoint > 0)
        {
            TextToSpeech = $"Dewpoint plus {metar.Temperature.DewPoint.NumberToSingular()}";
        }
        else
        {
            TextToSpeech = $"Dewpoint {metar.Temperature.DewPoint.NumberToSingular()}";
        }

        Acars = string.Concat((metar.Temperature.DewPoint < 0) ? "M" : "", Math.Abs(metar.Temperature.DewPoint).ToString("00"));
    }
}