using System.Drawing;

namespace Vatsim.Vatis.Config;

public class WindowProperties
{
    public Point Location { get; set; }
    public bool TopMost { get; set; }
    public Size? Size { get; set; }

    public WindowProperties()
    {
        Location = new Point(0, 0);
    }
}