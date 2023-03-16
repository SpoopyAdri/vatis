using System;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class DewpointNode : AtisNode
{
    public DewpointNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.Temperature);
    }

    public void Parse(TemperatureInfo node)
    {
        if (Composite.UseTemperaturePlusPrefix && node.DewPoint > 0)
        {
            VoiceAtis = $"Dewpoint plus {node.DewPoint.NumberToSingular()}";
        }
        else
        {
            VoiceAtis = $"Dewpoint {node.DewPoint.NumberToSingular()}";
        }

        TextAtis = string.Concat((node.DewPoint < 0) ? "M" : "", Math.Abs(node.DewPoint).ToString("00"));
    }
}