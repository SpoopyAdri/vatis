using Vatsim.Vatis.Config;
using Vatsim.Vatis.Utils;
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
            VoiceAtis = $"Altimeter {value.NumberToSingular()}";
            if (mComposite.UseFaaFormat)
            {
                TextAtis = $"A{value} ({value.ToString("0000").NumberToSingular().ToUpper()})";
            }
            else
            {
                TextAtis = $"A{value}";
            }
        }
        else
        {
            VoiceAtis = $"QNH {value.NumberToSingular()}";
            TextAtis = $"Q{value}";
        }
    }
}