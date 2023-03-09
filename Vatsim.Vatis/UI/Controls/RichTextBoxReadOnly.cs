using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Controls;

public class RichTextBoxReadOnly : RichTextBox
{
    private const int WM_SETFOCUS = 0x0007;
    private const int WM_KILLFOCUS = 0x0008;

    public RichTextBoxReadOnly()
    {
        Cursor = Cursors.Arrow;
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_SETFOCUS)
        {
            m.Msg = WM_KILLFOCUS;
        }
        base.WndProc(ref m);
    }
}