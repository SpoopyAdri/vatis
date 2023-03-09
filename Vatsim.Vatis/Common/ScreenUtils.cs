using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Core;

namespace Vatsim.Vatis.Common;

internal static class ScreenUtils
{
    public static void EnsureOnScreen(Form form)
    {
        Point location = form.Location;
        location.Offset(10, 10);
        Screen[] allScreens = Screen.AllScreens;
        for (int i = 0; i < allScreens.Length; i++)
        {
            Screen screen = allScreens[i];
            bool flag = screen.WorkingArea.Contains(location);
            if (flag)
            {
                return;
            }
        }
        Screen screen2 = Screen.FromPoint(form.Location);
        form.Location = new Point(screen2.WorkingArea.Left, screen2.WorkingArea.Top);
    }

    public static Point CenterOnScreen(Form form)
    {
        var primary = Screen.PrimaryScreen.Bounds;
        var center = new Point((primary.Width - form.Width) / 2, (primary.Height - form.Height) / 2);
        form.Location = center;
        return center;
    }

    public static void ApplyWindowProperties(WindowProperties properties, Form form)
    {
        form.Location = properties.Location;
        form.TopMost = properties.TopMost;

        if (form.GetType().IsDefined(typeof(ResizableForm)))
        {
            form.Size = properties.Size ?? new Size();
        }

        EnsureOnScreen(form);
    }

    public static void SaveWindowProperties(WindowProperties properties, Form form)
    {
        properties.Location = form.Location;
        properties.TopMost = form.TopMost;

        if (form.GetType().IsDefined(typeof(ResizableForm)))
        {
            properties.Size = form.Size;
        }
    }
}