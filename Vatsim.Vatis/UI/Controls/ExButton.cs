using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vatsim.Vatis.UI.Controls;

public class ExButton : Button
{
	private bool mPushed = false;
	private bool mClicked = false;
	private Color mPushedColor = Color.FromArgb(0, 120, 206);
	private Color mClickedColor = Color.FromArgb(0, 120, 206);
	private Color mBorderColor = Color.FromArgb(100, 100, 100);
	private Color mDisabledTextColor = Color.FromArgb(100, 100, 100);

	public ExButton()
	{
		SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		Cursor = Cursors.Hand;
	}

	public bool Pushed
	{
		get { return mPushed; }
		set { mPushed = value; Invalidate(); }
	}

	public Color PushedColor
	{
		get { return mPushedColor; }
		set { mPushedColor = value; Invalidate(); }
	}

	public bool Clicked
	{
		get { return mClicked; }
		set { mClicked = value; Invalidate(); }
	}

	public Color ClickedColor
	{
		get { return mClickedColor; }
		set { mClickedColor = value; Invalidate(); }
	}

	public Color BorderColor
	{
		get { return mBorderColor; }
		set { mBorderColor = value; Invalidate(); }
	}

	public Color DisabledTextColor
	{
		get { return mDisabledTextColor; }
		set { mDisabledTextColor = value; Invalidate(); }
	}

	protected override void OnDoubleClick(EventArgs e)
	{
		base.OnClick(e);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mPushed = true;
			Focus();
		}
		base.OnMouseDown(e);
		Invalidate();
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			mPushed = false;
		}
		base.OnMouseUp(e);
		Invalidate();
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		Invalidate();
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		// Fill the background.
		using (Brush backgroundBrush = new SolidBrush(mPushed ? PushedColor : mClicked ? ClickedColor : BackColor))
		{
			e.Graphics.FillRectangle(backgroundBrush, 0, 0, ClientSize.Width, ClientSize.Height);
		}

		if (Image != null)
		{
			var x = (ClientSize.Width - Image.Width) / 2;
			var y = (ClientSize.Height - Image.Height) / 2;
			e.Graphics.DrawImage(Image, x, y);
		}

		// Draw the border.
		Rectangle borderRect = new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
		using (Pen borderPen = new Pen(BorderColor)) e.Graphics.DrawRectangle(borderPen, borderRect);

		// Draw the text, if any.
		if (Text != "")
		{
			using (Brush textBrush = new SolidBrush(Enabled ? ForeColor : DisabledTextColor))
			{
				StringFormat fmt = new StringFormat();
				fmt.Alignment = StringAlignment.Center;
				fmt.LineAlignment = StringAlignment.Center;
				e.Graphics.DrawString(Text, Font, textBrush, ClientRectangle, fmt);
				fmt.Dispose();
			}
		}
	}

	protected override bool ShowFocusCues => false;
}