using System.Collections.Generic;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class Clouds : BaseFormat
{
    public Clouds()
    {
        Template = new()
        {
            Text = "{clouds}",
            Voice = "{clouds}"
        };
    }

    public bool IdentifyCeilingLayer { get; set; } = true;
    public bool ConvertToMetric { get; set; }

    public Dictionary<string, string> Types { get; set; } = new()
    {
        { "FEW", "few clouds at {altitude}" },
        { "SCT", "{altitude} scattered {convective}" },
        { "BKN", "{altitude} broken {convective}" },
        { "OVC", "{altitude} overcast {convective}" },
        { "VV", "indefinite ceiling {altitude}" },
        { "NSC", "no significant clouds" },
        { "NCD", "no clouds detected" },
        { "CLR", "sky clear below one-two thousand" },
        { "SKC", "sky clear" }
    };

    public Dictionary<string, string> ConvectiveTypes { get; set; } = new()
    {
        { "CB", "cumulonimbus" },
        { "TCU", "towering cumulus" }
    };
}
