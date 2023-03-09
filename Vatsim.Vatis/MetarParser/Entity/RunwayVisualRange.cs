namespace Vatsim.Vatis.MetarParser.Entity;

public sealed class RunwayVisualRange
{
    public enum Tendency
    {
        NONE,
        U,
        D,
        N,
    }

    public enum RunwayPositionCode
    {
        None,
        Left,
        Right,
        Center
    }

    /// <summary>
    /// Concerned runway
    /// </summary>
    public string Runway { get; set; }

    public RunwayPositionCode RunwayPosition { get; set; }

    /// <summary>
    /// Visual range defined by one value
    /// </summary>
    public Value VisualRange { get; set; }

    /// <summary>
    /// Visual range defined by an interval (because it is variable)
    /// </summary>
    public Value[] VisualRangeInterval { get; set; }

    public bool ValueIsLower { get; set; }

    public bool ValueIsHigher { get; set; }

    /// <summary>
    /// Is it a variable range ?
    /// </summary>
    public bool Variable { get; set; }

    /// <summary>
    /// Past tendency (optional) (U, D, or N)
    /// </summary>
    public Tendency PastTendency { get; set; }

    public string Raw { get; set; }
}