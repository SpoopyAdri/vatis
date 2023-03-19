using System.Collections.Generic;

namespace Vatsim.Vatis.Profiles;

public class Profile : IProfile
{
    public string Name { get; set; }
    public List<Composite> Composites { get; set; }
    public override string ToString() => Name;

    public Profile()
    {
        Composites = new List<Composite>();
    }
}