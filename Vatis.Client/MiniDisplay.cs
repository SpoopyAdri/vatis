using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vatsim.Vatis.Client.Args;
using Vatsim.Vatis.Client.Common;
using Vatsim.Vatis.Client.Config;
using Vatsim.Vatis.Client.Core;
using Vatsim.Vatis.Client.UI;

namespace Vatsim.Vatis.Client
{
    [ResizableForm]
    public partial class MiniDisplay : Form
    {
        [EventPublication(EventTopics.MinifiedWindowClosed)]
        public event EventHandler<EventArgs> MinifiedWindowClosed;

        [EventPublication(EventTopics.AtisUpdateAcknowledged)]
        public event EventHandler<ClientEventArgs<AtisComposite>> AtisUpdateAcknowledged;

        private readonly IEventBroker mEventBroker;
        private readonly IAppConfig mAppConfig;
        private readonly Timer mUtcClock;
        private bool mInitializing = true;

        public MiniDisplay(IEventBroker eventBroker, IAppConfig appConfig)
        {
            InitializeComponent();

            mEventBroker = eventBroker;
            mEventBroker.Register(this);
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

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshDisplay();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            using Pen pen = new Pen(Color.FromArgb(0, 0, 0));
            pevent.Graphics.DrawRectangle(pen, rect);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            MinifiedWindowClosed?.Invoke(this, EventArgs.Empty);
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            MinifiedWindowClosed?.Invoke(this, EventArgs.Empty);
            mEventBroker?.Unregister(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshDisplay();
            utcClock.Visible = ClientSize.Width > 120;
            Invalidate();

            if (!mInitializing)
            {
                ScreenUtils.SaveWindowProperties(mAppConfig.MiniDisplayWindowProperties, this);
                mAppConfig.SaveConfig();
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (!mInitializing)
            {
                ScreenUtils.SaveWindowProperties(mAppConfig.MiniDisplayWindowProperties, this);
                mAppConfig.SaveConfig();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (mAppConfig.MiniDisplayWindowProperties == null)
            {
                mAppConfig.MiniDisplayWindowProperties = new WindowProperties();
                mAppConfig.MiniDisplayWindowProperties.Location = ScreenUtils.CenterOnScreen(this);
                mAppConfig.SaveConfig();
            }

            if (mAppConfig.MiniDisplayWindowProperties.TopMost != mAppConfig.WindowProperties.TopMost)
            {
                mAppConfig.MiniDisplayWindowProperties.TopMost = mAppConfig.WindowProperties.TopMost;
            }

            ScreenUtils.ApplyWindowProperties(mAppConfig.MiniDisplayWindowProperties, this);
            mInitializing = false;
        }

        [EventSubscription(EventTopics.RefreshMinifiedWindow, typeof(OnUserInterfaceAsync))]
        public void OnRefreshMinifiedWindow(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            tlpMain.Controls.Clear();
            tlpMain.RowStyles.Clear();
            tlpMain.ColumnStyles.Clear();

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
                MinimumSize = new Size(310, 75);
                return;
            }

            int colWidth = 75;
            int colCount = ClientSize.Width / colWidth;

            int rowHeight = 35;
            int rowCount = ClientSize.Height / rowHeight;

            tlpMain.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                tlpMain.RowStyles.Add(new RowStyle
                {
                    SizeType = SizeType.Absolute,
                    Height = rowHeight,
                });
            }

            tlpMain.ColumnCount = colCount;
            for (int i = 0; i < colCount; i++)
            {
                tlpMain.ColumnStyles.Add(new ColumnStyle
                {
                    SizeType = SizeType.Absolute,
                    Width = colWidth
                });
            }

            if (mAppConfig != null)
            {
                int row = 0;
                int col = 0;

                foreach (var composite in mAppConfig.CurrentProfile.Composites)
                {
                    composite.MetarReceived = null;
                    composite.NewAtisUpdate = null;

                    if (composite.Connection.IsConnected)
                    {
                        var item = new MiniDisplayItem
                        {
                            Icao = composite.Identifier,
                            AtisLetter = composite.CurrentAtisLetter,
                            Metar = composite?.DecodedMetar?.RawMetar ?? "",
                            Composite = composite
                        };

                        composite.MetarReceived += (x, y) => item.Metar = y.Value;
                        composite.NewAtisUpdate += (x, y) => item.IsNewAtis = true;

                        item.AtisUpdateAcknowledged += (x, y) => AtisUpdateAcknowledged?.Invoke(this, new ClientEventArgs<AtisComposite>(composite));

                        tlpMain.Controls.Add(item, col, row);

                        if (col > colCount)
                        {
                            col = 0;
                            row++;
                        }

                        col++;
                    }
                }
            }
        }
    }
}
