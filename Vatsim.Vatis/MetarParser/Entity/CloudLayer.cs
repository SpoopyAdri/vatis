using System.ComponentModel;

namespace Vatsim.Vatis.MetarParser.Entity;

public sealed class CloudLayer
{
    public enum CloudAmount
    {
        NULL,
        [Description("Few")]
        FEW,
        [Description("Scattered")]
        SCT,
        [Description("Broken")]
        BKN,
        [Description("Overcast")]
        OVC,
        [Description("Vertical Visibility")]
        VV,
        [Description("Clear")]
        CLR,
        [Description("No cloud detected")]
        NCD,
        [Description("No significant clouds")]
        NSC,
        [Description("Sky clear")]
        SKC
    }

    public enum CloudType
    {
        NULL,
        [Description("Cumulonimbus")]
        CB,
        [Description("Towering cumulonimbus")]
        TCU,
        [Description("Cannot measure")]
        CannotMeasure,
    }

    /// <summary>
    /// Annotation corresponding to amount of clouds (FEW/SCT/BKN/OVC)
    /// </summary>
    public CloudAmount Amount { get; set; } = CloudAmount.NULL;

    /// <summary>
    /// Height of cloud base
    /// </summary>
    public Value BaseHeight { get; set; }

    /// <summary>
    /// Cloud type cumulonimbus, towering cumulonimbus (CB/TCU)
    /// </summary>
    public CloudType Type { get; set; }

    /// <summary>
    /// Boolean flag if this cloud layer is the celing
    /// </summary>
    public bool IsCeiling { get; set; }

    public string RawValue { get; set; }
}