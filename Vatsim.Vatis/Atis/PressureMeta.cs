using Vatsim.Vatis.Common;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class PressureMeta : AtisMeta
{
    private AtisComposite mComposite;

    public PressureMeta(AtisComposite composite)
    {
        mComposite = composite;
    }

    public override void Parse(Metar metar)
    {
        var value = metar.AltimeterSetting.Value;

        if (metar.AltimeterSetting.UnitType == Weather.Enums.AltimeterUnitType.InchesOfMercury)
        {
            TextToSpeech = $"Altimeter {value.NumberToSingular()}";
            if (mComposite.UseFaaFormat)
            {
                Acars = $"A{value} ({value.ToString("0000").NumberToSingular().ToUpper()})";
            }
            else
            {
                Acars = $"A{value}";
            }
        }
        else
        {
            TextToSpeech = $"QNH {value.NumberToSingular()}";
            Acars = $"Q{value}";
        }
    }
}