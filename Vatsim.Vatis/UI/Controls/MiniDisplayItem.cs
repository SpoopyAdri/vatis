using System;
using System.Drawing;
using System.Windows.Forms;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.UI.Controls;

public partial class MiniDisplayItem : UserControl
{
    public EventHandler<EventArgs> AtisUpdateAcknowledged;

    private readonly System.Threading.SynchronizationContext mSyncContext;
    private bool mAlternateColor;
    private bool mIsNewAtis;
    private System.Timers.Timer mBlinkTimer;

    public Composite Composite { get; set; }
    public string Icao
    {
        get => lblIdentifier.Text;
        set => lblIdentifier.Text = value;
    }
    public string AtisLetter
    {
        get => lblAtisLetter.Text;
        set => lblAtisLetter.Text = value;
    }
    public string Metar
    {
        set => mSyncContext.Post(o => { metarTooltip.SetToolTip(lblIdentifier, value); }, null);
    }
    public string Wind
    {
        set => lblWind.Text = value;
    }
    public string Altimeter
    {
        set => lblAltimeter.Text = value;
    }
    public bool IsNewAtis
    {
        get => mIsNewAtis;
        set
        {
            mIsNewAtis = value;
            mBlinkTimer.Enabled = value;
            lblAtisLetter.ForeColor = Color.Cyan;
        }
    }

    public MiniDisplayItem()
    {
        InitializeComponent();

        mSyncContext = System.Threading.SynchronizationContext.Current;

        mBlinkTimer = new System.Timers.Timer();
        mBlinkTimer.Interval = 500;
        mBlinkTimer.Elapsed += (e, v) =>
        {
            lblAtisLetter.ForeColor = mAlternateColor ? Color.Cyan : Color.FromArgb(241, 196, 15);
            mAlternateColor = !mAlternateColor;
        };
    }

    private void txtAtisLetter_Click(object sender, EventArgs e)
    {
        IsNewAtis = false;
        AtisUpdateAcknowledged?.Invoke(this, EventArgs.Empty);
    }

    private void lblAtisLetter_MouseDown(object sender, MouseEventArgs e)
    {
        lblAtisLetter.BackColor = Color.FromArgb(0, 0, 0);
    }

    private void lblAtisLetter_MouseUp(object sender, MouseEventArgs e)
    {
        lblAtisLetter.BackColor = Color.FromArgb(20, 20, 20);
    }
}