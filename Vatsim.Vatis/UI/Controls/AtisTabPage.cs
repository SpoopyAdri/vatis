using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Vatsim.Vatis.Config;
using Vatsim.Vatis.Network;
using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.UI.Controls;

internal class AtisTabPage : TabPage
{
    private bool mAlternateColor;
    private bool mIsNewAtis;
    private readonly System.Timers.Timer mBlinkTimer;

    public Connection Connection { get; private set; }
    public Composite Composite { get; private set; }
    public CompositePanel CompositeMeta { get; private set; }
    public bool IsNewAtis
    {
        get => mIsNewAtis;
        set
        {
            mIsNewAtis = value;
            mBlinkTimer.Enabled = value;
            CompositeMeta.IsNewAtis = value;
            ForeColor = Color.Aqua;
            Parent?.Invalidate();
        }
    }

    public AtisTabPage(Connection connection, Composite composite, IAppConfig appConfig)
    {
        Connection = connection;
        Composite = composite;

        CompositeMeta = new CompositePanel(Connection, Composite, appConfig)
        {
            Dock = DockStyle.Fill
        };
        CompositeMeta.AtisLetterChanged += (sender, args) =>
        {
            Parent?.Invalidate();
        };
        Controls.Add(CompositeMeta);
        BackColor = Color.FromArgb(50, 50, 50);
        ForeColor = Color.Aqua; // atis letter color

        mBlinkTimer = new System.Timers.Timer();
        mBlinkTimer.Interval = 500;
        mBlinkTimer.Elapsed += (e, v) =>
        {
            ForeColor = mAlternateColor ? Color.Aqua : Color.FromArgb(241, 196, 15);
            mAlternateColor = !mAlternateColor;
            Parent?.Invalidate();
        };
    }
}


internal class TabPageComparer : IComparer<AtisTabPage>
{
    public int Compare(AtisTabPage x, AtisTabPage y)
    {
        if (x.Connection.IsConnected && !y.Connection.IsConnected) return -1;
        if (!x.Connection.IsConnected && y.Connection.IsConnected) return 1;

        return string.Compare(x.Composite.Identifier, y.Composite.Identifier);
    }
}