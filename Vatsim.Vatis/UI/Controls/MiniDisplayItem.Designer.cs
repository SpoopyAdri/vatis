namespace Vatsim.Vatis.UI.Controls
{
    partial class MiniDisplayItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metarTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.lblIdentifier = new System.Windows.Forms.Label();
            this.lblAtisLetter = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblAltimeter = new System.Windows.Forms.Label();
            this.lblWind = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // metarTooltip
            // 
            this.metarTooltip.AutomaticDelay = 1000;
            // 
            // lblIdentifier
            // 
            this.lblIdentifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblIdentifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIdentifier.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIdentifier.ForeColor = System.Drawing.Color.White;
            this.lblIdentifier.Location = new System.Drawing.Point(3, 0);
            this.lblIdentifier.Name = "lblIdentifier";
            this.lblIdentifier.Size = new System.Drawing.Size(64, 40);
            this.lblIdentifier.TabIndex = 0;
            this.lblIdentifier.Text = "KXXX";
            this.lblIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAtisLetter
            // 
            this.lblAtisLetter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblAtisLetter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAtisLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAtisLetter.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAtisLetter.ForeColor = System.Drawing.Color.Cyan;
            this.lblAtisLetter.Location = new System.Drawing.Point(73, 0);
            this.lblAtisLetter.Name = "lblAtisLetter";
            this.lblAtisLetter.Size = new System.Drawing.Size(24, 40);
            this.lblAtisLetter.TabIndex = 1;
            this.lblAtisLetter.Text = "X";
            this.lblAtisLetter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAtisLetter.Click += new System.EventHandler(this.txtAtisLetter_Click);
            this.lblAtisLetter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblAtisLetter_MouseDown);
            this.lblAtisLetter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblAtisLetter_MouseUp);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 4;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpMain.Controls.Add(this.lblAltimeter, 3, 0);
            this.tlpMain.Controls.Add(this.lblWind, 2, 0);
            this.tlpMain.Controls.Add(this.lblAtisLetter, 1, 0);
            this.tlpMain.Controls.Add(this.lblIdentifier, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(300, 40);
            this.tlpMain.TabIndex = 2;
            // 
            // lblAltimeter
            // 
            this.lblAltimeter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblAltimeter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAltimeter.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAltimeter.ForeColor = System.Drawing.Color.White;
            this.lblAltimeter.Location = new System.Drawing.Point(233, 0);
            this.lblAltimeter.Name = "lblAltimeter";
            this.lblAltimeter.Size = new System.Drawing.Size(64, 40);
            this.lblAltimeter.TabIndex = 3;
            this.lblAltimeter.Text = "A3020";
            this.lblAltimeter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWind
            // 
            this.lblWind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblWind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWind.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWind.ForeColor = System.Drawing.Color.White;
            this.lblWind.Location = new System.Drawing.Point(103, 0);
            this.lblWind.Name = "lblWind";
            this.lblWind.Size = new System.Drawing.Size(124, 40);
            this.lblWind.TabIndex = 2;
            this.lblWind.Text = "26010G25KT 260V280";
            this.lblWind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MiniDisplayItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.tlpMain);
            this.Name = "MiniDisplayItem";
            this.Size = new System.Drawing.Size(300, 40);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip metarTooltip;
        private System.Windows.Forms.Label lblIdentifier;
        private System.Windows.Forms.Label lblAtisLetter;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblAltimeter;
        private System.Windows.Forms.Label lblWind;
    }
}
