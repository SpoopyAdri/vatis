using System.Collections.Generic;

namespace Vatsim.Vatis.Config;

public class Profile : IProfile
{
    public string Name { get; set; }
    public List<AtisComposite> Composites { get; set; }
    public override string ToString() => Name;

    public Profile()
    {
        Composites = new List<AtisComposite>();
    }
}