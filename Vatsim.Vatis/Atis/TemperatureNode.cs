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
        if (node == null || node.Temperature == null)
        {
            VoiceAtis = "Temperature missing";
            return;
        }

        if (Composite.UseTemperaturePlusPrefix && node.Temperature > 0)
        {
            VoiceAtis = $"Temperature plus {node.Temperature?.NumberToSingular()}";
        }
        else
        {
            VoiceAtis = $"Temperature {node.Temperature?.NumberToSingular()}";
        }

        TextAtis = string.Concat((node.Temperature < 0) ? "M" : "", Math.Abs(node.Temperature.Value).ToString("00"));
    }
}