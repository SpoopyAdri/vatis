using Vatsim.Vatis.UI.Controls;

namespace Vatsim.Vatis.UI
{
    partial class MiniDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniDisplayForm));
            this.btnRestore = new Vatsim.Vatis.UI.Controls.ExButton();
            this.utcClock = new Vatsim.Vatis.UI.Controls.HitTestLabel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.hitTestPanel1 = new Vatsim.Vatis.UI.Controls.HitTestPanel();
            this.hitTestPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(84)))), ((int)(((byte)(51)))));
            this.btnRestore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnRestore.Clicked = false;
            this.btnRestore.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestore.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.Location = new System.Drawing.Point(280, 6);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Pushed = false;
            this.btnRestore.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnRestore.Size = new System.Drawing.Size(20, 20);
            this.btnRestore.TabIndex = 6;
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // utcClock
            // 
            this.utcClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.utcClock.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.utcClock.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.utcClock.ForeColor = System.Drawing.Color.White;
            this.utcClock.Location = new System.Drawing.Point(191, 5);
            this.utcClock.Name = "utcClock";
            this.utcClock.ShowBorder = false;
            this.utcClock.Size = new System.Drawing.Size(90, 22);
            this.utcClock.TabIndex = 7;
            this.utcClock.Text = "00:00/00";
            this.utcClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(5, 35);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(305, 35);
            this.tlpMain.TabIndex = 8;
            // 
            // hitTestPanel1
            // 
            this.hitTestPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.hitTestPanel1.Controls.Add(this.utcClock);
            this.hitTestPanel1.Controls.Add(this.btnRestore);
            this.hitTestPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hitTestPanel1.Location = new System.Drawing.Point(5, 5);
            this.hitTestPanel1.Name = "hitTestPanel1";
            this.hitTestPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.hitTestPanel1.ShowBorder = false;
            this.hitTestPanel1.Size = new System.Drawing.Size(305, 30);
            this.hitTestPanel1.TabIndex = 9;
            // 
            // MiniDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(315, 75);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.hitTestPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MiniDisplayForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MiniDisplay";
            this.TopMost = true;
            this.hitTestPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExButton btnRestore;
        private HitTestLabel utcClock;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private HitTestPanel hitTestPanel1;
    }
}