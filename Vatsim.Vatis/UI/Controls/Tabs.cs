using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Profiles;
using Vatsim.Vatis.UI.Dialogs;

namespace Vatsim.Vatis.UI.Controls;

internal class Tabs : TabControl
{
    [Description("Background Color")]
    public Color BackgroundColor { get; set; } = Color.FromArgb(50, 50, 50);

    [Description("Border Color")]
    public Color BorderColor { get; set; } = Color.FromArgb(120, 120, 120);

    public Tabs()
    {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        DrawMode = TabDrawMode.OwnerDrawFixed;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle TabControlArea = ClientRectangle;
        Rectangle TabArea = DisplayRectangle;

        using (SolidBrush brush = new SolidBrush(BackgroundColor))
        {
            e.Graphics.FillRectangle(brush, TabControlArea);
        }

        int borderWidth = SystemInformation.Border3DSize.Width;
        TabArea.Inflate(borderWidth, borderWidth);
        using (Pen pen = new Pen(BorderColor))
        {
            e.Graphics.DrawRectangle(pen, TabArea);
        }

        for (int i = 0; i < TabCount; i++)
        {
            DrawTab(e.Graphics, (AtisTabPage)TabPages[i], i);
        }

        // if there are no tabs, let user know
        if (TabCount == 0)
        {
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using Font font = new Font(this.Font.FontFamily, 16, FontStyle.Regular);
            using SolidBrush brush = new SolidBrush(Color.White);
            e.Graphics.DrawString("No ATIS Composites Defined", font, brush, TabArea.Width / 2, TabArea.Height / 2, stringFormat);
        }
    }

    private void DrawTab(Graphics g, AtisTabPage tabPage, int index)
    {
        using Pen borderPen = new Pen(BorderColor);
        Rectangle tabRect = GetTabRect(index);

        bool mSelected = SelectedIndex == index;
        Point[] pt = new Point[7];

        pt[0] = new Point(tabRect.Left, tabRect.Bottom);
        pt[1] = new Point(tabRect.Left, tabRect.Top + 3);
        pt[2] = new Point(tabRect.Left + 3, tabRect.Top);
        pt[3] = new Point(tabRect.Right - 3, tabRect.Top);
        pt[4] = new Point(tabRect.Right, tabRect.Top + 3);
        pt[5] = new Point(tabRect.Right, tabRect.Bottom);
        pt[6] = new Point(tabRect.Left, tabRect.Bottom);

        using (SolidBrush brush = new SolidBrush(tabPage.BackColor))
        {
            g.FillPolygon(brush, pt);
        }

        g.DrawPolygon(borderPen, pt);

        if (mSelected)
        {
            Pen pen = new Pen(tabPage.BackColor);
            g.DrawLine(pen, tabRect.Left + 1, tabRect.Bottom, tabRect.Right - 1, tabRect.Bottom);
            g.DrawLine(pen, tabRect.Left + 1, tabRect.Bottom + 1, tabRect.Right - 1, tabRect.Bottom + 1);
        }

        RectangleF layoutRect = tabRect;
        StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        using (SolidBrush brush = new SolidBrush(Color.White))
        {
            var comp = tabPage.Tag as Composite;
            if (tabPage.Connection.IsConnected)
            {
                layoutRect.X -= comp.AtisType == AtisType.Combined ? 5 : 4;
            }
            g.DrawString(tabPage.Text, Font, brush, layoutRect, stringFormat);
            layoutRect.X += comp.AtisType == AtisType.Combined ? 23 : 29;
        }
        if (tabPage.Connection.IsConnected)
        {
            using SolidBrush brush = new SolidBrush(tabPage.ForeColor);
            g.DrawString(tabPage.Composite.AtisLetter, Font, brush, layoutRect, stringFormat);
        }
    }

    public void Sort()
    {
        var tabList = TabPages.Cast<AtisTabPage>().ToList();
        tabList.Sort(new TabPageComparer());
        TabPages.Clear();
        TabPages.AddRange(tabList.ToArray());
    }

}