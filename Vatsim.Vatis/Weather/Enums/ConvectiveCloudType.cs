using System.ComponentModel;

namespace Vatsim.Vatis.Weather.Enums
{
    /// <summary>
    /// Convective cloud types
    /// </summary>
    public enum ConvectiveCloudType
    {
        /// <summary>
        /// Not specified
        /// </summary>
        None = 0,

        [Description("CB")]
        Cumulonimbus = 1,

        [Description("TCU")]
        ToweringCumulus = 2,
    }
}
