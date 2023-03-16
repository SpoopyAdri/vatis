using System;
using Vatsim.Vatis.Utils;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Atis;

public class TemperatureNode : AtisNode
{
    public TemperatureNode()
    { }

    public override void Parse(Metar metar)
    {
        Parse(metar.Temperature);
    }

    public void Parse(TemperatureInfo node)
    {
        if (Composite.UseTemperaturePlusPrefix && node.Value > 0)
        {
            VoiceAtis = $"Temperature plus {node.Value.NumberToSingular()}";
        }
        else
        {
            VoiceAtis = $"Temperature {node.Value.NumberToSingular()}";
        }

        TextAtis = string.Concat((node.Value < 0) ? "M" : "", Math.Abs(node.Value).ToString("00"));
    }
}