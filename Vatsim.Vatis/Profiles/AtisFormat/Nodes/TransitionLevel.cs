using System.Collections.Generic;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class TransitionLevel : BaseFormat
{
    public TransitionLevel()
    {
        Template = new()
        {
            Text = "TRANSITION LEVEL {trl}",
            Voice = "TRANSITION LEVEL {trl}"
        };
    }

    public List<TransitionLevelMeta> Values { get; set; } = new();
}
