namespace Vatsim.Vatis.UI.Controls;

partial class ExternalAtisGenerator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupTest = new System.Windows.Forms.GroupBox();
            this.btnFetchMetar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtMetar = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.tlpVariables = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExternalDep = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExternalArr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExternalApp = new System.Windows.Forms.TextBox();
            this.txtExternalRemarks = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExternalUrl = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupTest.SuspendLayout();
            this.tlpVariables.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 279);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "External ATIS Generator";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupTest, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpVariables, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(704, 257);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // groupTest
            // 
            this.groupTest.Controls.Add(this.btnFetchMetar);
            this.groupTest.Controls.Add(this.label10);
            this.groupTest.Controls.Add(this.btnTest);
            this.groupTest.Controls.Add(this.txtMetar);
            this.groupTest.Controls.Add(this.txtResponse);
            this.groupTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTest.Location = new System.Drawing.Point(3, 119);
            this.groupTest.Name = "groupTest";
            this.groupTest.Size = new System.Drawing.Size(698, 135);
            this.groupTest.TabIndex = 8;
            this.groupTest.TabStop = false;
            this.groupTest.Text = "Test URL";
            // 
            // btnFetchMetar
            // 
            this.btnFetchMetar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchMetar.Location = new System.Drawing.Point(542, 20);
            this.btnFetchMetar.Name = "btnFetchMetar";
            this.btnFetchMetar.Size = new System.Drawing.Size(61, 23);
            this.btnFetchMetar.TabIndex = 7;
            this.btnFetchMetar.Text = "Fetch";
            this.btnFetchMetar.UseVisualStyleBackColor = true;
            this.btnFetchMetar.Click += new System.EventHandler(this.btnFetchMetar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "METAR:";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(609, 20);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(78, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test URL";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtMetar
            // 
            this.txtMetar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMetar.Location = new System.Drawing.Point(64, 20);
            this.txtMetar.Name = "txtMetar";
            this.txtMetar.Size = new System.Drawing.Size(472, 23);
            this.txtMetar.TabIndex = 5;
            this.txtMetar.TextChanged += new System.EventHandler(this.txtMetar_TextChanged);
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtResponse.Location = new System.Drawing.Point(12, 48);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(675, 75);
            this.txtResponse.TabIndex = 4;
            // 
            // tlpVariables
            // 
            this.tlpVariables.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpVariables.ColumnCount = 4;
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVariables.Controls.Add(this.label8, 0, 1);
            this.tlpVariables.Controls.Add(this.txtExternalDep, 1, 1);
            this.tlpVariables.Controls.Add(this.label7, 0, 0);
            this.tlpVariables.Controls.Add(this.txtExternalArr, 1, 0);
            this.tlpVariables.Controls.Add(this.label9, 2, 0);
            this.tlpVariables.Controls.Add(this.txtExternalApp, 3, 0);
            this.tlpVariables.Controls.Add(this.txtExternalRemarks, 3, 1);
            this.tlpVariables.Controls.Add(this.label12, 2, 1);
            this.tlpVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariables.Location = new System.Drawing.Point(3, 42);
            this.tlpVariables.Name = "tlpVariables";
            this.tlpVariables.RowCount = 2;
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVariables.Size = new System.Drawing.Size(698, 71);
            this.tlpVariables.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(4, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 34);
            this.label8.TabIndex = 2;
            this.label8.Text = "Departure Runways:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExternalDep
            // 
            this.txtExternalDep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalDep.Location = new System.Drawing.Point(135, 39);
            this.txtExternalDep.Name = "txtExternalDep";
            this.txtExternalDep.Size = new System.Drawing.Size(210, 23);
            this.txtExternalDep.TabIndex = 3;
            this.txtExternalDep.TextChanged += new System.EventHandler(this.txtExternalDep_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(4, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 34);
            this.label7.TabIndex = 0;
            this.label7.Text = "Arrival Runways:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExternalArr
            // 
            this.txtExternalArr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalArr.Location = new System.Drawing.Point(135, 4);
            this.txtExternalArr.Name = "txtExternalArr";
            this.txtExternalArr.Size = new System.Drawing.Size(210, 23);
            this.txtExternalArr.TabIndex = 1;
            this.txtExternalArr.TextChanged += new System.EventHandler(this.txtExternalArr_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(352, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 34);
            this.label9.TabIndex = 4;
            this.label9.Text = "Approaches in Use:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExternalApp
            // 
            this.txtExternalApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalApp.Location = new System.Drawing.Point(483, 4);
            this.txtExternalApp.Name = "txtExternalApp";
            this.txtExternalApp.Size = new System.Drawing.Size(211, 23);
            this.txtExternalApp.TabIndex = 5;
            this.txtExternalApp.TextChanged += new System.EventHandler(this.txtExternalApp_TextChanged);
            // 
            // txtExternalRemarks
            // 
            this.txtExternalRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalRemarks.Location = new System.Drawing.Point(483, 39);
            this.txtExternalRemarks.Name = "txtExternalRemarks";
            this.txtExternalRemarks.Size = new System.Drawing.Size(211, 23);
            this.txtExternalRemarks.TabIndex = 7;
            this.txtExternalRemarks.TextChanged += new System.EventHandler(this.txtExternalRemarks_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(352, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 34);
            this.label12.TabIndex = 6;
            this.label12.Text = "Remarks:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtExternalUrl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 39);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "URL:";
            // 
            // txtExternalUrl
            // 
            this.txtExternalUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExternalUrl.Location = new System.Drawing.Point(44, 8);
            this.txtExternalUrl.Name = "txtExternalUrl";
            this.txtExternalUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExternalUrl.Size = new System.Drawing.Size(655, 23);
            this.txtExternalUrl.TabIndex = 3;
            this.txtExternalUrl.TextChanged += new System.EventHandler(this.txtExternalUrl_TextChanged);
            // 
            // ExternalAtisGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "ExternalAtisGenerator";
            this.Size = new System.Drawing.Size(710, 279);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupTest.ResumeLayout(false);
            this.groupTest.PerformLayout();
            this.tlpVariables.ResumeLayout(false);
            this.tlpVariables.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtExternalUrl;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tlpVariables;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtExternalDep;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtExternalArr;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtExternalApp;
    private System.Windows.Forms.TextBox txtExternalRemarks;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.GroupBox groupTest;
    private System.Windows.Forms.Button btnFetchMetar;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.TextBox txtMetar;
    private System.Windows.Forms.TextBox txtResponse;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
}
