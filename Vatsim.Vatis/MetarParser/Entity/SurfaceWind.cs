using Vatsim.Vatis.Common;

namespace Vatsim.Vatis.MetarParser.Entity;

public sealed class SurfaceWind
{
    /// <summary>
    /// Wind direction
    /// </summary>
    public Value MeanDirection { get; set; }

    /// <summary>
    /// Wind variability (if true, direction is null)
    /// </summary>
    public bool VariableDirection { get; set; }

    /// <summary>
    /// Wind speed
    /// </summary>
    public Value MeanSpeed { get; set; }

    /// <summary>
    /// Wind speed variation (gusts)
    /// </summary>
    public Value SpeedVariations { get; set; }

    /// <summary>
    /// Boundaries for wind direction variation
    /// </summary>
    public Value[] DirectionVariations { get; private set; } = null;

    public void SetDirectionVariations(Value directionMax, Value directionMin)
    {
        DirectionVariations = new Value[] { directionMax, directionMin };
    }

    public override string ToString()
    {
        //if (MeanDirection != null && MeanSpeed != null)
        //{
        //    if (SpeedVariations != null)
        //    {
        //        return $"{ MeanDirection.ActualValue:000}{ MeanSpeed.ActualValue:00}G{ SpeedVariations.ActualValue:00}{ MeanSpeed.ActualUnit.PrintUnitShort()}";
        //    }
        //    return $"{ MeanDirection.ActualValue:000}{ MeanSpeed.ActualValue:00}{ MeanSpeed.ActualUnit.PrintUnitShort()}";
        //}
        //else if (VariableDirection)
        //{
        //    return $"VRB{ MeanSpeed.ActualValue:00}{ MeanSpeed.ActualUnit.PrintUnitShort()}";
        //}
        return "------";
    }
}