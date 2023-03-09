using System.Drawing;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Controls;

public class ExComboBox : ComboBox
{
    private Color Gray = Color.FromArgb(100, 100, 100);

    public ExComboBox()
    {
        base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            
        DrawMode = DrawMode.OwnerDrawVariable;
        DropDownStyle = ComboBoxStyle.DropDownList;
        Cursor = Cursors.Hand;
        ForeColor = Color.White;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (base.GetStyle(ControlStyles.AllPaintingInWmPaint))
        {
            OnPaintBackground(e);
        }

        // draw border
        using (Pen pen = new Pen(Gray))
        {
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            e.Graphics.DrawRectangle(pen, rect);
        }

        // draw dropdown icon
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(120, 120, 120)))
        {
            e.Graphics.FillPolygon(solidBrush, new Point[]
            {
                new Point(base.Width - 20, base.Height / 2 - 2),
                new Point(base.Width - 9, base.Height / 2 - 2),
                new Point(base.Width - 15, base.Height / 2 + 4)
            });
        }

        // draw selected text
        Rectangle bounds = new Rectangle(2, 2, Width - 20, Height - 4);
        TextRenderer.DrawText(e.Graphics, Text, Font, bounds, Color.White, TextFormatFlags.VerticalCenter);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        e.DrawBackground();
        if (e.Index >= 0)
        {
            using (Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? new SolidBrush(Color.FromArgb(50, 50, 50)) : new SolidBrush(e.BackColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);

                using (Brush textBrush = new SolidBrush(e.ForeColor))
                {
                    TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), e.Font, e.Bounds, Color.Silver, TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}