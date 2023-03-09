using System;
using System.Drawing;
using System.Windows.Forms;
using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.UI.Controls;

public partial class MiniDisplayItem : UserControl
{
    public EventHandler<EventArgs> AtisUpdateAcknowledged;

    private readonly System.Threading.SynchronizationContext mSyncContext;
    private bool mAlternateColor;
    private bool mIsNewAtis;
    private System.Timers.Timer mBlinkTimer;

    public AtisComposite Composite { get; set; }
    public string Icao
    {
        get => txtIcao.Text;
        set => txtIcao.Text = value;
    }
    public string AtisLetter
    {
        get => txtAtisLetter.Text;
        set => txtAtisLetter.Text = value;
    }
    public string Metar
    {
        set => mSyncContext.Post(o => { metarTooltip.SetToolTip(txtIcao, value); }, null);
    }
    public bool IsNewAtis
    {
        get => mIsNewAtis;
        set
        {
            mIsNewAtis = value;
            mBlinkTimer.Enabled = value;
            txtAtisLetter.ForeColor = Color.Cyan;
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
            txtAtisLetter.ForeColor = mAlternateColor ? Color.Cyan : Color.FromArgb(241, 196, 15);
            mAlternateColor = !mAlternateColor;
        };
    }

    private void txtAtisLetter_Click(object sender, EventArgs e)
    {
        IsNewAtis = false;
        AtisUpdateAcknowledged?.Invoke(this, EventArgs.Empty);
    }
}