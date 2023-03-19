using System.Collections.Generic;

namespace Vatsim.Vatis.Profiles;

public interface IProfile
{
    string Name { get; set; }
    List<Composite> Composites { get; set; }
}