namespace Vatsim.Vatis.UI.Controls
{
    partial class CompositePanel
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panelAtisText = new HitTestPanel();
            saveAirportConditions = new System.Windows.Forms.PictureBox();
            txtArptCond = new System.Windows.Forms.RichTextBox();
            btnAirportConditions = new System.Windows.Forms.Label();
            atisLetter = new ExButton();
            panelMetar = new HitTestPanel();
            rtbMetar = new RichTextBoxReadOnly();
            panelQuickReadout = new HitTestPanel();
            lblAltimeter = new HitTestLabel();
            lblWind = new HitTestLabel();
            btnNotams = new System.Windows.Forms.Label();
            hitTestPanel2 = new HitTestPanel();
            saveNotams = new System.Windows.Forms.PictureBox();
            txtNotams = new System.Windows.Forms.RichTextBox();
            tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            btnRecord = new ExButton();
            btnConnect = new ExButton();
            ddlPresets = new ExComboBox();
            tableLayoutPanel1.SuspendLayout();
            panelAtisText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)saveAirportConditions).BeginInit();
            panelMetar.SuspendLayout();
            panelQuickReadout.SuspendLayout();
            hitTestPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)saveNotams).BeginInit();
            tlpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(panelAtisText, 0, 2);
            tableLayoutPanel1.Controls.Add(btnAirportConditions, 0, 1);
            tableLayoutPanel1.Controls.Add(atisLetter, 0, 0);
            tableLayoutPanel1.Controls.Add(panelMetar, 1, 0);
            tableLayoutPanel1.Controls.Add(panelQuickReadout, 4, 0);
            tableLayoutPanel1.Controls.Add(btnNotams, 3, 1);
            tableLayoutPanel1.Controls.Add(hitTestPanel2, 3, 2);
            tableLayoutPanel1.Controls.Add(tlpButtons, 0, 3);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel1.Size = new System.Drawing.Size(995, 263);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelAtisText
            // 
            panelAtisText.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            panelAtisText.BorderColor = System.Drawing.Color.FromArgb(92, 92, 92);
            tableLayoutPanel1.SetColumnSpan(panelAtisText, 2);
            panelAtisText.Controls.Add(saveAirportConditions);
            panelAtisText.Controls.Add(txtArptCond);
            panelAtisText.Dock = System.Windows.Forms.DockStyle.Fill;
            panelAtisText.Location = new System.Drawing.Point(4, 118);
            panelAtisText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelAtisText.Name = "panelAtisText";
            panelAtisText.Padding = new System.Windows.Forms.Padding(6);
            panelAtisText.ShowBorder = true;
            panelAtisText.Size = new System.Drawing.Size(457, 104);
            panelAtisText.TabIndex = 70;
            // 
            // saveAirportConditions
            // 
            saveAirportConditions.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            saveAirportConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            saveAirportConditions.Image = Properties.Resources.icon_save;
            saveAirportConditions.Location = new System.Drawing.Point(7, 82);
            saveAirportConditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveAirportConditions.Name = "saveAirportConditions";
            saveAirportConditions.Size = new System.Drawing.Size(16, 16);
            saveAirportConditions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            saveAirportConditions.TabIndex = 2;
            saveAirportConditions.TabStop = false;
            saveAirportConditions.Visible = false;
            saveAirportConditions.Click += saveAirportConditions_Click;
            // 
            // txtArptCond
            // 
            txtArptCond.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            txtArptCond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtArptCond.DetectUrls = false;
            txtArptCond.Dock = System.Windows.Forms.DockStyle.Fill;
            txtArptCond.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtArptCond.ForeColor = System.Drawing.Color.White;
            txtArptCond.Location = new System.Drawing.Point(6, 6);
            txtArptCond.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtArptCond.Name = "txtArptCond";
            txtArptCond.ReadOnly = true;
            txtArptCond.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            txtArptCond.Size = new System.Drawing.Size(445, 92);
            txtArptCond.TabIndex = 0;
            txtArptCond.Text = "";
            txtArptCond.TextChanged += txtArptCond_TextChanged;
            txtArptCond.KeyDown += ClearFormatting;
            txtArptCond.KeyPress += ToUppercase;
            // 
            // btnAirportConditions
            // 
            btnAirportConditions.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(btnAirportConditions, 2);
            btnAirportConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAirportConditions.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAirportConditions.ForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            btnAirportConditions.Location = new System.Drawing.Point(4, 95);
            btnAirportConditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAirportConditions.Name = "btnAirportConditions";
            btnAirportConditions.Size = new System.Drawing.Size(70, 15);
            btnAirportConditions.TabIndex = 68;
            btnAirportConditions.Text = "ARPT COND";
            btnAirportConditions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAirportConditions.Click += btnAirportConditions_Click;
            // 
            // atisLetter
            // 
            atisLetter.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            atisLetter.BorderColor = System.Drawing.Color.FromArgb(100, 100, 100);
            atisLetter.Clicked = false;
            atisLetter.ClickedColor = System.Drawing.Color.FromArgb(20, 20, 20);
            atisLetter.Cursor = System.Windows.Forms.Cursors.Hand;
            atisLetter.DisabledTextColor = System.Drawing.Color.FromArgb(100, 100, 100);
            atisLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            atisLetter.Enabled = false;
            atisLetter.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            atisLetter.ForeColor = System.Drawing.Color.White;
            atisLetter.Location = new System.Drawing.Point(4, 3);
            atisLetter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            atisLetter.Name = "atisLetter";
            atisLetter.Pushed = false;
            atisLetter.PushedColor = System.Drawing.Color.FromArgb(20, 20, 20);
            atisLetter.Size = new System.Drawing.Size(74, 86);
            atisLetter.TabIndex = 57;
            atisLetter.TabStop = false;
            atisLetter.Text = "A";
            atisLetter.UseVisualStyleBackColor = false;
            atisLetter.MouseUp += atisLetter_MouseUp;
            // 
            // panelMetar
            // 
            panelMetar.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            panelMetar.BorderColor = System.Drawing.Color.FromArgb(92, 92, 92);
            tableLayoutPanel1.SetColumnSpan(panelMetar, 3);
            panelMetar.Controls.Add(rtbMetar);
            panelMetar.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMetar.Location = new System.Drawing.Point(86, 3);
            panelMetar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelMetar.Name = "panelMetar";
            panelMetar.Padding = new System.Windows.Forms.Padding(6);
            panelMetar.ShowBorder = true;
            panelMetar.Size = new System.Drawing.Size(764, 86);
            panelMetar.TabIndex = 58;
            // 
            // rtbMetar
            // 
            rtbMetar.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            rtbMetar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbMetar.DetectUrls = false;
            rtbMetar.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbMetar.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rtbMetar.ForeColor = System.Drawing.Color.White;
            rtbMetar.Location = new System.Drawing.Point(6, 6);
            rtbMetar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rtbMetar.Name = "rtbMetar";
            rtbMetar.ReadOnly = true;
            rtbMetar.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            rtbMetar.Size = new System.Drawing.Size(752, 74);
            rtbMetar.TabIndex = 0;
            rtbMetar.TabStop = false;
            rtbMetar.Text = "";
            // 
            // panelQuickReadout
            // 
            panelQuickReadout.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            panelQuickReadout.BorderColor = System.Drawing.Color.FromArgb(92, 92, 92);
            panelQuickReadout.Controls.Add(lblAltimeter);
            panelQuickReadout.Controls.Add(lblWind);
            panelQuickReadout.Dock = System.Windows.Forms.DockStyle.Fill;
            panelQuickReadout.Location = new System.Drawing.Point(858, 3);
            panelQuickReadout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelQuickReadout.Name = "panelQuickReadout";
            panelQuickReadout.Padding = new System.Windows.Forms.Padding(6);
            panelQuickReadout.ShowBorder = true;
            panelQuickReadout.Size = new System.Drawing.Size(133, 86);
            panelQuickReadout.TabIndex = 67;
            // 
            // lblAltimeter
            // 
            lblAltimeter.BorderColor = System.Drawing.Color.FromArgb(100, 100, 100);
            lblAltimeter.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblAltimeter.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblAltimeter.ForeColor = System.Drawing.Color.White;
            lblAltimeter.Location = new System.Drawing.Point(6, 43);
            lblAltimeter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAltimeter.Name = "lblAltimeter";
            lblAltimeter.ShowBorder = false;
            lblAltimeter.Size = new System.Drawing.Size(121, 37);
            lblAltimeter.TabIndex = 1;
            lblAltimeter.Text = "-----";
            lblAltimeter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWind
            // 
            lblWind.BorderColor = System.Drawing.Color.FromArgb(100, 100, 100);
            lblWind.Dock = System.Windows.Forms.DockStyle.Top;
            lblWind.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblWind.ForeColor = System.Drawing.Color.White;
            lblWind.Location = new System.Drawing.Point(6, 6);
            lblWind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblWind.Name = "lblWind";
            lblWind.ShowBorder = false;
            lblWind.Size = new System.Drawing.Size(121, 37);
            lblWind.TabIndex = 0;
            lblWind.Text = "-----";
            lblWind.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnNotams
            // 
            btnNotams.AutoSize = true;
            btnNotams.Cursor = System.Windows.Forms.Cursors.Hand;
            btnNotams.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnNotams.ForeColor = System.Drawing.Color.FromArgb(230, 230, 230);
            btnNotams.Location = new System.Drawing.Point(475, 95);
            btnNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNotams.Name = "btnNotams";
            btnNotams.Size = new System.Drawing.Size(49, 15);
            btnNotams.TabIndex = 69;
            btnNotams.Text = "NOTAMS";
            btnNotams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnNotams.Click += btnNotams_Click;
            // 
            // hitTestPanel2
            // 
            hitTestPanel2.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            hitTestPanel2.BorderColor = System.Drawing.Color.FromArgb(92, 92, 92);
            tableLayoutPanel1.SetColumnSpan(hitTestPanel2, 2);
            hitTestPanel2.Controls.Add(saveNotams);
            hitTestPanel2.Controls.Add(txtNotams);
            hitTestPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            hitTestPanel2.Location = new System.Drawing.Point(475, 118);
            hitTestPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            hitTestPanel2.Name = "hitTestPanel2";
            hitTestPanel2.Padding = new System.Windows.Forms.Padding(6);
            hitTestPanel2.ShowBorder = true;
            hitTestPanel2.Size = new System.Drawing.Size(516, 104);
            hitTestPanel2.TabIndex = 71;
            // 
            // saveNotams
            // 
            saveNotams.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            saveNotams.Cursor = System.Windows.Forms.Cursors.Hand;
            saveNotams.Image = Properties.Resources.icon_save;
            saveNotams.Location = new System.Drawing.Point(7, 82);
            saveNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveNotams.Name = "saveNotams";
            saveNotams.Size = new System.Drawing.Size(16, 16);
            saveNotams.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            saveNotams.TabIndex = 3;
            saveNotams.TabStop = false;
            saveNotams.Visible = false;
            saveNotams.Click += saveNotams_Click;
            // 
            // txtNotams
            // 
            txtNotams.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            txtNotams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtNotams.DetectUrls = false;
            txtNotams.Dock = System.Windows.Forms.DockStyle.Fill;
            txtNotams.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtNotams.ForeColor = System.Drawing.Color.White;
            txtNotams.Location = new System.Drawing.Point(6, 6);
            txtNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtNotams.Name = "txtNotams";
            txtNotams.ReadOnly = true;
            txtNotams.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            txtNotams.Size = new System.Drawing.Size(504, 92);
            txtNotams.TabIndex = 0;
            txtNotams.Text = "";
            txtNotams.TextChanged += txtNotams_TextChanged;
            txtNotams.KeyDown += ClearFormatting;
            txtNotams.KeyPress += ToUppercase;
            // 
            // tlpButtons
            // 
            tlpButtons.ColumnCount = 3;
            tableLayoutPanel1.SetColumnSpan(tlpButtons, 5);
            tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            tlpButtons.Controls.Add(btnRecord, 0, 0);
            tlpButtons.Controls.Add(btnConnect, 2, 0);
            tlpButtons.Controls.Add(ddlPresets, 1, 0);
            tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpButtons.Location = new System.Drawing.Point(0, 225);
            tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpButtons.Size = new System.Drawing.Size(995, 38);
            tlpButtons.TabIndex = 72;
            // 
            // btnRecord
            // 
            btnRecord.BorderColor = System.Drawing.Color.FromArgb(100, 100, 100);
            btnRecord.Clicked = false;
            btnRecord.ClickedColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRecord.DisabledTextColor = System.Drawing.Color.FromArgb(100, 100, 100);
            btnRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            btnRecord.Enabled = false;
            btnRecord.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnRecord.ForeColor = System.Drawing.Color.White;
            btnRecord.Location = new System.Drawing.Point(4, 3);
            btnRecord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRecord.Name = "btnRecord";
            btnRecord.Pushed = false;
            btnRecord.PushedColor = System.Drawing.Color.FromArgb(20, 20, 20);
            btnRecord.Size = new System.Drawing.Size(120, 32);
            btnRecord.TabIndex = 65;
            btnRecord.TabStop = false;
            btnRecord.Text = "RECORD ATIS";
            btnRecord.Click += btnRecord_Click;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnConnect.BorderColor = System.Drawing.Color.FromArgb(100, 100, 100);
            btnConnect.Clicked = false;
            btnConnect.ClickedColor = System.Drawing.Color.FromArgb(0, 70, 150);
            btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            btnConnect.DisabledTextColor = System.Drawing.Color.FromArgb(100, 100, 100);
            btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            btnConnect.Enabled = false;
            btnConnect.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnConnect.ForeColor = System.Drawing.Color.White;
            btnConnect.Location = new System.Drawing.Point(871, 3);
            btnConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Pushed = false;
            btnConnect.PushedColor = System.Drawing.Color.FromArgb(20, 20, 20);
            btnConnect.Size = new System.Drawing.Size(120, 32);
            btnConnect.TabIndex = 67;
            btnConnect.Text = "CONNECT";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnTransmit_Click;
            // 
            // ddlPresets
            // 
            ddlPresets.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ddlPresets.Cursor = System.Windows.Forms.Cursors.Hand;
            ddlPresets.Dock = System.Windows.Forms.DockStyle.Fill;
            ddlPresets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            ddlPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ddlPresets.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ddlPresets.ForeColor = System.Drawing.Color.White;
            ddlPresets.FormattingEnabled = true;
            ddlPresets.IntegralHeight = false;
            ddlPresets.ItemHeight = 21;
            ddlPresets.Location = new System.Drawing.Point(132, 3);
            ddlPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ddlPresets.Name = "ddlPresets";
            ddlPresets.Size = new System.Drawing.Size(731, 27);
            ddlPresets.TabIndex = 68;
            ddlPresets.SelectedIndexChanged += ddlPresets_SelectedIndexChanged;
            // 
            // CompositePanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "CompositePanel";
            Padding = new System.Windows.Forms.Padding(10);
            Size = new System.Drawing.Size(1015, 283);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panelAtisText.ResumeLayout(false);
            panelAtisText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)saveAirportConditions).EndInit();
            panelMetar.ResumeLayout(false);
            panelQuickReadout.ResumeLayout(false);
            hitTestPanel2.ResumeLayout(false);
            hitTestPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)saveNotams).EndInit();
            tlpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ExButton atisLetter;
        private HitTestPanel panelMetar;
        private RichTextBoxReadOnly rtbMetar;
        private HitTestPanel panelQuickReadout;
        private HitTestLabel lblAltimeter;
        private HitTestLabel lblWind;
        private System.Windows.Forms.Label btnAirportConditions;
        private System.Windows.Forms.Label btnNotams;
        private HitTestPanel panelAtisText;
        private System.Windows.Forms.RichTextBox txtArptCond;
        private HitTestPanel hitTestPanel2;
        private System.Windows.Forms.RichTextBox txtNotams;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private ExButton btnRecord;
        private ExButton btnConnect;
        private ExComboBox ddlPresets;
        private System.Windows.Forms.PictureBox saveAirportConditions;
        private System.Windows.Forms.PictureBox saveNotams;
    }
}
