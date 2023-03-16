using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class AltimeterSettingNode : AtisNode
{
    public AltimeterSettingNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.AltimeterSetting);
    }

    public void Parse(AltimeterSetting node)
    {
        var value = node.Value;

        if (node.UnitType == Weather.Enums.AltimeterUnitType.InchesOfMercury)
        {
            VoiceAtis = $"Altimeter {value.NumberToSingular()}";
            if (Composite.UseFaaFormat)
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