using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.UI.Controls;

namespace Vatsim.Vatis.UI;

public partial class MiniDisplayForm : Form
{
    private readonly IAppConfig mAppConfig;
    private readonly Timer mUtcClock;
    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HT_CAPTION = 0x2;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    public MiniDisplayForm(IAppConfig appConfig)
    {
        InitializeComponent();

        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        mAppConfig = appConfig;

        utcClock.Text = DateTime.UtcNow.ToString("HH:mm/ss");
        mUtcClock = new System.Windows.Forms.Timer
        {
            Interval = 500
        };
        mUtcClock.Tick += (s, e) =>
        {
            utcClock.Text = DateTime.UtcNow.ToString("HH:mm/ss");
        };
        mUtcClock.Start();
    }
    
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams handleParam = base.CreateParams;
            handleParam.ExStyle |= 0x02000000;
            return handleParam;
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        RefreshDisplay();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Rectangle rect = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
        using Pen pen = new Pen(Color.FromArgb(0, 0, 0));
        e.Graphics.DrawRectangle(pen, rect);
    }

    private void btnRestore_Click(object sender, EventArgs e)
    {
        EventBus.Publish(this, new MiniWindowClosed());
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        EventBus.Publish(this, new MiniWindowClosed());
        EventBus.Unregister(this);
        base.OnFormClosing(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        EventBus.Register(this);
        TopMost = mAppConfig.WindowProperties.TopMost;
    }

    private void RefreshDisplay()
    {
        tlpMain.RowCount = 1;
        tlpMain.ColumnCount = 1;
        tlpMain.Controls.Clear();
        tlpMain.ColumnStyles.Clear();
        tlpMain.RowStyles.Clear();

        tlpMain.ColumnStyles.Add(new ColumnStyle
        {
            SizeType = SizeType.AutoSize
        });

        while (tlpMain.Controls.Count > 0)
            tlpMain.Controls[0].Dispose();

        if (mAppConfig == null || !mAppConfig.CurrentProfile.Composites.Any(x => x.Connection.IsConnected))
        {
            using Font font = new Font(Font.FontFamily, 14, FontStyle.Bold);
            tlpMain.Controls.Add(new Label
            {
                Text = "No Composites Connected",
                Dock = DockStyle.Fill,
                Font = font,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            });
            Size = new Size(315, 80);
            return;
        }

        foreach (var composite in mAppConfig.CurrentProfile.Composites.OrderBy(n => n.Identifier))
        {
            composite.MetarReceived = null;
            composite.NewAtisUpdate = null;

            if (composite.Connection.IsConnected)
            {
                var item = new MiniDisplayItem
                {
                    Icao = composite.Identifier,
                    AtisLetter = composite.AtisLetter,
                    Metar = composite?.DecodedMetar?.RawMetar ?? "",
                    Wind = composite?.DecodedMetar?.SurfaceWind?.RawValue ?? "-M-",
                    Altimeter = composite?.DecodedMetar?.AltimeterSetting?.RawValue ?? "-M-",
                    Composite = composite,
                    Dock = DockStyle.Fill
                };

                item.AtisUpdateAcknowledged += (sender, args) =>
                {
                    EventBus.Publish(this, new NewAtisAcknowledged(composite));
                };

                composite.MetarReceived += (sender, args) => item.Metar = args.Value;
                composite.DecodedMetarUpdated += (sender, args) =>
                {
                    item.Wind = composite.DecodedMetar?.SurfaceWind?.RawValue ?? "-M";
                    item.Altimeter = composite.DecodedMetar?.AltimeterSetting?.RawValue ?? "-M";
                };
                composite.NewAtisUpdate += (sender, args) =>
                {
                    item.IsNewAtis = true;
                    item.AtisLetter = args.Value;
                };

                tlpMain.RowStyles.Add(new RowStyle
                {
                    SizeType = SizeType.Percent
                });
                tlpMain.Controls.Add(item, 0, tlpMain.RowCount);
                tlpMain.RowCount++;
            }
        }

        Height = (tlpMain.RowCount * 45);
    }

    public void HandleEvent(UpdateMiniWindowRequested evt)
    {
        RefreshDisplay();
    }
}