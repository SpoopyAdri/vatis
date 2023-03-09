using System;
using Vatsim.Vatis.Common;
using Vatsim.Vatis.MetarParser.Entity;

namespace Vatsim.Vatis.Atis;

public class DewpointMeta : AtisMeta
{
    public override void Parse(DecodedMetar metar)
    {
        if (metar.DewPointTemperature != null)
        {
            if (metar.IsInternational && metar.DewPointTemperature.ActualValue > 0)
            {
                TextToSpeech = $"Dewpoint plus {Convert.ToInt32(metar.DewPointTemperature.ActualValue).NumberToSingular()}";
            }
            else
            {
                TextToSpeech = $"Dewpoint {Convert.ToInt32(metar.DewPointTemperature.ActualValue).NumberToSingular()}";
            }
            Acars = string.Concat((metar.DewPointTemperature.ActualValue < 0) ? "M" : "", Math.Abs(metar.DewPointTemperature.ActualValue).ToString("00"));
        }
    }
}