using System.Collections.Generic;

namespace Vatsim.Vatis.Config;

public interface IProfile
{
    string Name { get; set; }
    List<AtisComposite> Composites { get; set; }
}