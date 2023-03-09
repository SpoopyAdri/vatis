using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Controls;

class HitTestPanel : Panel
{
    private const int WM_NCHITTEST = 0x0084;

    public bool ShowBorder { get; set; }

    public Color BorderColor { get; set; } = Color.FromArgb(92, 92, 92);

    public HitTestPanel()
    {
        base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_NCHITTEST && !DesignMode)
        {
            m.Result = new IntPtr(-1);
        }
        else
        {
            base.WndProc(ref m);
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        if (ShowBorder)
        {
            Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            using (Pen pen = new Pen(BorderColor))
            {
                pevent.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}