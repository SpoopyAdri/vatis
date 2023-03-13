using System.ComponentModel;

namespace Vatsim.Vatis.Weather.Enums
{
    /// <summary>
    /// Enum of RVR unit types
    /// </summary>
    public enum RvrUnitType
    {
        [Description("None")]
        None = 0,

        [Description("Meters")]
        Meters = 1,

        [Description("Feets")]
        Feets = 2
    }
}
