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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelAtisText = new HitTestPanel();
            this.saveAirportConditions = new System.Windows.Forms.PictureBox();
            this.txtArptCond = new System.Windows.Forms.RichTextBox();
            this.btnAirportConditions = new System.Windows.Forms.Label();
            this.atisLetter = new ExButton();
            this.panelMetar = new HitTestPanel();
            this.rtbMetar = new RichTextBoxReadOnly();
            this.panelQuickReadout = new HitTestPanel();
            this.lblAltimeter = new HitTestLabel();
            this.lblWind = new HitTestLabel();
            this.btnNotams = new System.Windows.Forms.Label();
            this.hitTestPanel2 = new HitTestPanel();
            this.saveNotams = new System.Windows.Forms.PictureBox();
            this.txtNotams = new System.Windows.Forms.RichTextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnRecord = new ExButton();
            this.btnConnect = new ExButton();
            this.ddlPresets = new ExComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelAtisText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveAirportConditions)).BeginInit();
            this.panelMetar.SuspendLayout();
            this.panelQuickReadout.SuspendLayout();
            this.hitTestPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveNotams)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.Controls.Add(this.panelAtisText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAirportConditions, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.atisLetter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelMetar, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelQuickReadout, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNotams, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.hitTestPanel2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpButtons, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(995, 263);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelAtisText
            // 
            this.panelAtisText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelAtisText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panelAtisText, 2);
            this.panelAtisText.Controls.Add(this.saveAirportConditions);
            this.panelAtisText.Controls.Add(this.txtArptCond);
            this.panelAtisText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAtisText.Location = new System.Drawing.Point(4, 118);
            this.panelAtisText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelAtisText.Name = "panelAtisText";
            this.panelAtisText.Padding = new System.Windows.Forms.Padding(6);
            this.panelAtisText.ShowBorder = true;
            this.panelAtisText.Size = new System.Drawing.Size(457, 104);
            this.panelAtisText.TabIndex = 70;
            // 
            // saveAirportConditions
            // 
            this.saveAirportConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveAirportConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveAirportConditions.Image = global::Vatsim.Vatis.Properties.Resources.icon_save;
            this.saveAirportConditions.Location = new System.Drawing.Point(7, 82);
            this.saveAirportConditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.saveAirportConditions.Name = "saveAirportConditions";
            this.saveAirportConditions.Size = new System.Drawing.Size(16, 16);
            this.saveAirportConditions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.saveAirportConditions.TabIndex = 2;
            this.saveAirportConditions.TabStop = false;
            this.saveAirportConditions.Visible = false;
            this.saveAirportConditions.Click += new System.EventHandler(this.saveAirportConditions_Click);
            // 
            // txtArptCond
            // 
            this.txtArptCond.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtArptCond.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtArptCond.DetectUrls = false;
            this.txtArptCond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArptCond.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtArptCond.ForeColor = System.Drawing.Color.White;
            this.txtArptCond.Location = new System.Drawing.Point(6, 6);
            this.txtArptCond.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtArptCond.Name = "txtArptCond";
            this.txtArptCond.ReadOnly = true;
            this.txtArptCond.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtArptCond.Size = new System.Drawing.Size(445, 92);
            this.txtArptCond.TabIndex = 0;
            this.txtArptCond.Text = "";
            this.txtArptCond.TextChanged += new System.EventHandler(this.txtArptCond_TextChanged);
            this.txtArptCond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClearFormatting);
            this.txtArptCond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToUppercase);
            // 
            // btnAirportConditions
            // 
            this.btnAirportConditions.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.btnAirportConditions, 2);
            this.btnAirportConditions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAirportConditions.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAirportConditions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnAirportConditions.Location = new System.Drawing.Point(4, 95);
            this.btnAirportConditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAirportConditions.Name = "btnAirportConditions";
            this.btnAirportConditions.Size = new System.Drawing.Size(70, 15);
            this.btnAirportConditions.TabIndex = 68;
            this.btnAirportConditions.Text = "ARPT COND";
            this.btnAirportConditions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAirportConditions.Click += new System.EventHandler(this.btnAirportConditions_Click);
            // 
            // atisLetter
            // 
            this.atisLetter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.atisLetter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.atisLetter.Clicked = false;
            this.atisLetter.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.atisLetter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.atisLetter.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.atisLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.atisLetter.Enabled = false;
            this.atisLetter.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.atisLetter.ForeColor = System.Drawing.Color.White;
            this.atisLetter.Location = new System.Drawing.Point(4, 3);
            this.atisLetter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.atisLetter.Name = "atisLetter";
            this.atisLetter.Pushed = false;
            this.atisLetter.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.atisLetter.Size = new System.Drawing.Size(74, 86);
            this.atisLetter.TabIndex = 57;
            this.atisLetter.TabStop = false;
            this.atisLetter.Text = "A";
            this.atisLetter.UseVisualStyleBackColor = false;
            this.atisLetter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.atisLetter_MouseUp);
            // 
            // panelMetar
            // 
            this.panelMetar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelMetar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panelMetar, 3);
            this.panelMetar.Controls.Add(this.rtbMetar);
            this.panelMetar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMetar.Location = new System.Drawing.Point(86, 3);
            this.panelMetar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelMetar.Name = "panelMetar";
            this.panelMetar.Padding = new System.Windows.Forms.Padding(6);
            this.panelMetar.ShowBorder = true;
            this.panelMetar.Size = new System.Drawing.Size(764, 86);
            this.panelMetar.TabIndex = 58;
            // 
            // rtbMetar
            // 
            this.rtbMetar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rtbMetar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMetar.DetectUrls = false;
            this.rtbMetar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMetar.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbMetar.ForeColor = System.Drawing.Color.White;
            this.rtbMetar.Location = new System.Drawing.Point(6, 6);
            this.rtbMetar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtbMetar.Name = "rtbMetar";
            this.rtbMetar.ReadOnly = true;
            this.rtbMetar.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbMetar.Size = new System.Drawing.Size(752, 74);
            this.rtbMetar.TabIndex = 0;
            this.rtbMetar.TabStop = false;
            this.rtbMetar.Text = "";
            // 
            // panelQuickReadout
            // 
            this.panelQuickReadout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelQuickReadout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.panelQuickReadout.Controls.Add(this.lblAltimeter);
            this.panelQuickReadout.Controls.Add(this.lblWind);
            this.panelQuickReadout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuickReadout.Location = new System.Drawing.Point(858, 3);
            this.panelQuickReadout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelQuickReadout.Name = "panelQuickReadout";
            this.panelQuickReadout.Padding = new System.Windows.Forms.Padding(6);
            this.panelQuickReadout.ShowBorder = true;
            this.panelQuickReadout.Size = new System.Drawing.Size(133, 86);
            this.panelQuickReadout.TabIndex = 67;
            // 
            // lblAltimeter
            // 
            this.lblAltimeter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblAltimeter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAltimeter.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAltimeter.ForeColor = System.Drawing.Color.White;
            this.lblAltimeter.Location = new System.Drawing.Point(6, 43);
            this.lblAltimeter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltimeter.Name = "lblAltimeter";
            this.lblAltimeter.ShowBorder = false;
            this.lblAltimeter.Size = new System.Drawing.Size(121, 37);
            this.lblAltimeter.TabIndex = 1;
            this.lblAltimeter.Text = "-----";
            this.lblAltimeter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWind
            // 
            this.lblWind.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblWind.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWind.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWind.ForeColor = System.Drawing.Color.White;
            this.lblWind.Location = new System.Drawing.Point(6, 6);
            this.lblWind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWind.Name = "lblWind";
            this.lblWind.ShowBorder = false;
            this.lblWind.Size = new System.Drawing.Size(121, 37);
            this.lblWind.TabIndex = 0;
            this.lblWind.Text = "-----";
            this.lblWind.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnNotams
            // 
            this.btnNotams.AutoSize = true;
            this.btnNotams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotams.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNotams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnNotams.Location = new System.Drawing.Point(475, 95);
            this.btnNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNotams.Name = "btnNotams";
            this.btnNotams.Size = new System.Drawing.Size(49, 15);
            this.btnNotams.TabIndex = 69;
            this.btnNotams.Text = "NOTAMS";
            this.btnNotams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNotams.Click += new System.EventHandler(this.btnNotams_Click);
            // 
            // hitTestPanel2
            // 
            this.hitTestPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.hitTestPanel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.tableLayoutPanel1.SetColumnSpan(this.hitTestPanel2, 2);
            this.hitTestPanel2.Controls.Add(this.saveNotams);
            this.hitTestPanel2.Controls.Add(this.txtNotams);
            this.hitTestPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hitTestPanel2.Location = new System.Drawing.Point(475, 118);
            this.hitTestPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hitTestPanel2.Name = "hitTestPanel2";
            this.hitTestPanel2.Padding = new System.Windows.Forms.Padding(6);
            this.hitTestPanel2.ShowBorder = true;
            this.hitTestPanel2.Size = new System.Drawing.Size(516, 104);
            this.hitTestPanel2.TabIndex = 71;
            // 
            // saveNotams
            // 
            this.saveNotams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveNotams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveNotams.Image = global::Vatsim.Vatis.Properties.Resources.icon_save;
            this.saveNotams.Location = new System.Drawing.Point(7, 82);
            this.saveNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.saveNotams.Name = "saveNotams";
            this.saveNotams.Size = new System.Drawing.Size(16, 16);
            this.saveNotams.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.saveNotams.TabIndex = 3;
            this.saveNotams.TabStop = false;
            this.saveNotams.Visible = false;
            this.saveNotams.Click += new System.EventHandler(this.saveNotams_Click);
            // 
            // txtNotams
            // 
            this.txtNotams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtNotams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotams.DetectUrls = false;
            this.txtNotams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotams.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNotams.ForeColor = System.Drawing.Color.White;
            this.txtNotams.Location = new System.Drawing.Point(6, 6);
            this.txtNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNotams.Name = "txtNotams";
            this.txtNotams.ReadOnly = true;
            this.txtNotams.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtNotams.Size = new System.Drawing.Size(504, 92);
            this.txtNotams.TabIndex = 0;
            this.txtNotams.Text = "";
            this.txtNotams.TextChanged += new System.EventHandler(this.txtNotams_TextChanged);
            this.txtNotams.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClearFormatting);
            this.txtNotams.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToUppercase);
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpButtons, 5);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpButtons.Controls.Add(this.btnRecord, 0, 0);
            this.tlpButtons.Controls.Add(this.btnConnect, 2, 0);
            this.tlpButtons.Controls.Add(this.ddlPresets, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 225);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(995, 38);
            this.tlpButtons.TabIndex = 72;
            // 
            // btnRecord
            // 
            this.btnRecord.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRecord.Clicked = false;
            this.btnRecord.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecord.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecord.Enabled = false;
            this.btnRecord.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRecord.ForeColor = System.Drawing.Color.White;
            this.btnRecord.Location = new System.Drawing.Point(4, 3);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Pushed = false;
            this.btnRecord.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRecord.Size = new System.Drawing.Size(120, 32);
            this.btnRecord.TabIndex = 65;
            this.btnRecord.TabStop = false;
            this.btnRecord.Text = "RECORD ATIS";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnConnect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnConnect.Clicked = false;
            this.btnConnect.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.btnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConnect.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Enabled = false;
            this.btnConnect.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(871, 3);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Pushed = false;
            this.btnConnect.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnConnect.Size = new System.Drawing.Size(120, 32);
            this.btnConnect.TabIndex = 67;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnTransmit_Click);
            // 
            // ddlPresets
            // 
            this.ddlPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ddlPresets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ddlPresets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlPresets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ddlPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPresets.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ddlPresets.ForeColor = System.Drawing.Color.White;
            this.ddlPresets.FormattingEnabled = true;
            this.ddlPresets.IntegralHeight = false;
            this.ddlPresets.ItemHeight = 21;
            this.ddlPresets.Location = new System.Drawing.Point(132, 3);
            this.ddlPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlPresets.Name = "ddlPresets";
            this.ddlPresets.Size = new System.Drawing.Size(731, 27);
            this.ddlPresets.TabIndex = 68;
            this.ddlPresets.SelectedIndexChanged += new System.EventHandler(this.ddlPresets_SelectedIndexChanged);
            // 
            // CompositePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CompositePanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(1015, 283);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelAtisText.ResumeLayout(false);
            this.panelAtisText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveAirportConditions)).EndInit();
            this.panelMetar.ResumeLayout(false);
            this.panelQuickReadout.ResumeLayout(false);
            this.hitTestPanel2.ResumeLayout(false);
            this.hitTestPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saveNotams)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

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
