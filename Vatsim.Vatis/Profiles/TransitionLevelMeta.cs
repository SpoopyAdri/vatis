using System;

namespace Vatsim.Vatis.Profiles;

[Serializable]
public class TransitionLevelMeta
{
    public int Low { get; set; }
    public int High { get; set; }
    public int Altitude { get; set; }
}
