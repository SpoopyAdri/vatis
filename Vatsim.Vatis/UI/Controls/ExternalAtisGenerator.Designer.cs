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
        groupBox1 = new System.Windows.Forms.GroupBox();
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        groupTest = new System.Windows.Forms.GroupBox();
        btnFetchMetar = new System.Windows.Forms.Button();
        label10 = new System.Windows.Forms.Label();
        btnTest = new System.Windows.Forms.Button();
        txtMetar = new System.Windows.Forms.TextBox();
        txtResponse = new System.Windows.Forms.TextBox();
        tlpVariables = new System.Windows.Forms.TableLayoutPanel();
        label8 = new System.Windows.Forms.Label();
        txtExternalDep = new System.Windows.Forms.TextBox();
        label7 = new System.Windows.Forms.Label();
        txtExternalArr = new System.Windows.Forms.TextBox();
        label9 = new System.Windows.Forms.Label();
        txtExternalApp = new System.Windows.Forms.TextBox();
        txtExternalRemarks = new System.Windows.Forms.TextBox();
        label12 = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        label1 = new System.Windows.Forms.Label();
        txtExternalUrl = new System.Windows.Forms.TextBox();
        groupBox1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupTest.SuspendLayout();
        tlpVariables.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(tableLayoutPanel1);
        groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox1.Location = new System.Drawing.Point(0, 0);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(710, 279);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "External ATIS Generator";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(groupTest, 0, 2);
        tableLayoutPanel1.Controls.Add(tlpVariables, 0, 1);
        tableLayoutPanel1.Controls.Add(panel1, 0, 0);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
        tableLayoutPanel1.Size = new System.Drawing.Size(704, 257);
        tableLayoutPanel1.TabIndex = 4;
        // 
        // groupTest
        // 
        groupTest.Controls.Add(btnFetchMetar);
        groupTest.Controls.Add(label10);
        groupTest.Controls.Add(btnTest);
        groupTest.Controls.Add(txtMetar);
        groupTest.Controls.Add(txtResponse);
        groupTest.Dock = System.Windows.Forms.DockStyle.Fill;
        groupTest.Location = new System.Drawing.Point(3, 119);
        groupTest.Name = "groupTest";
        groupTest.Size = new System.Drawing.Size(698, 135);
        groupTest.TabIndex = 8;
        groupTest.TabStop = false;
        groupTest.Text = "Test URL";
        // 
        // btnFetchMetar
        // 
        btnFetchMetar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnFetchMetar.Location = new System.Drawing.Point(542, 20);
        btnFetchMetar.Name = "btnFetchMetar";
        btnFetchMetar.Size = new System.Drawing.Size(61, 23);
        btnFetchMetar.TabIndex = 7;
        btnFetchMetar.Text = "Fetch";
        btnFetchMetar.UseVisualStyleBackColor = true;
        btnFetchMetar.Click += btnFetchMetar_Click;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(12, 24);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(47, 15);
        label10.TabIndex = 6;
        label10.Text = "METAR:";
        // 
        // btnTest
        // 
        btnTest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        btnTest.Enabled = false;
        btnTest.Location = new System.Drawing.Point(609, 20);
        btnTest.Name = "btnTest";
        btnTest.Size = new System.Drawing.Size(78, 23);
        btnTest.TabIndex = 3;
        btnTest.Text = "Test URL";
        btnTest.UseVisualStyleBackColor = true;
        btnTest.Click += btnTest_Click;
        // 
        // txtMetar
        // 
        txtMetar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtMetar.Location = new System.Drawing.Point(64, 20);
        txtMetar.Name = "txtMetar";
        txtMetar.Size = new System.Drawing.Size(472, 23);
        txtMetar.TabIndex = 5;
        txtMetar.TextChanged += txtMetar_TextChanged;
        // 
        // txtResponse
        // 
        txtResponse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtResponse.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        txtResponse.Location = new System.Drawing.Point(12, 48);
        txtResponse.Multiline = true;
        txtResponse.Name = "txtResponse";
        txtResponse.ReadOnly = true;
        txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtResponse.Size = new System.Drawing.Size(675, 75);
        txtResponse.TabIndex = 4;
        // 
        // tlpVariables
        // 
        tlpVariables.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
        tlpVariables.ColumnCount = 4;
        tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
        tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
        tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tlpVariables.Controls.Add(label8, 0, 1);
        tlpVariables.Controls.Add(txtExternalDep, 1, 1);
        tlpVariables.Controls.Add(label7, 0, 0);
        tlpVariables.Controls.Add(txtExternalArr, 1, 0);
        tlpVariables.Controls.Add(label9, 2, 0);
        tlpVariables.Controls.Add(txtExternalApp, 3, 0);
        tlpVariables.Controls.Add(txtExternalRemarks, 3, 1);
        tlpVariables.Controls.Add(label12, 2, 1);
        tlpVariables.Dock = System.Windows.Forms.DockStyle.Fill;
        tlpVariables.Location = new System.Drawing.Point(3, 42);
        tlpVariables.Name = "tlpVariables";
        tlpVariables.RowCount = 2;
        tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tlpVariables.Size = new System.Drawing.Size(698, 71);
        tlpVariables.TabIndex = 3;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Dock = System.Windows.Forms.DockStyle.Fill;
        label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        label8.Location = new System.Drawing.Point(4, 36);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(124, 34);
        label8.TabIndex = 2;
        label8.Text = "Departure Runways:";
        label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtExternalDep
        // 
        txtExternalDep.Dock = System.Windows.Forms.DockStyle.Fill;
        txtExternalDep.Location = new System.Drawing.Point(135, 39);
        txtExternalDep.Name = "txtExternalDep";
        txtExternalDep.Size = new System.Drawing.Size(210, 23);
        txtExternalDep.TabIndex = 3;
        txtExternalDep.TextChanged += txtExternalDep_TextChanged;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Dock = System.Windows.Forms.DockStyle.Fill;
        label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        label7.Location = new System.Drawing.Point(4, 1);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(124, 34);
        label7.TabIndex = 0;
        label7.Text = "Arrival Runways:";
        label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtExternalArr
        // 
        txtExternalArr.Dock = System.Windows.Forms.DockStyle.Fill;
        txtExternalArr.Location = new System.Drawing.Point(135, 4);
        txtExternalArr.Name = "txtExternalArr";
        txtExternalArr.Size = new System.Drawing.Size(210, 23);
        txtExternalArr.TabIndex = 1;
        txtExternalArr.TextChanged += txtExternalArr_TextChanged;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Dock = System.Windows.Forms.DockStyle.Fill;
        label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        label9.Location = new System.Drawing.Point(352, 1);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(124, 34);
        label9.TabIndex = 4;
        label9.Text = "Approaches in Use:";
        label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // txtExternalApp
        // 
        txtExternalApp.Dock = System.Windows.Forms.DockStyle.Fill;
        txtExternalApp.Location = new System.Drawing.Point(483, 4);
        txtExternalApp.Name = "txtExternalApp";
        txtExternalApp.Size = new System.Drawing.Size(211, 23);
        txtExternalApp.TabIndex = 5;
        txtExternalApp.TextChanged += txtExternalApp_TextChanged;
        // 
        // txtExternalRemarks
        // 
        txtExternalRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
        txtExternalRemarks.Location = new System.Drawing.Point(483, 39);
        txtExternalRemarks.Name = "txtExternalRemarks";
        txtExternalRemarks.Size = new System.Drawing.Size(211, 23);
        txtExternalRemarks.TabIndex = 7;
        txtExternalRemarks.TextChanged += txtExternalRemarks_TextChanged;
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Dock = System.Windows.Forms.DockStyle.Fill;
        label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        label12.Location = new System.Drawing.Point(352, 36);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(124, 34);
        label12.TabIndex = 6;
        label12.Text = "Remarks:";
        label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // panel1
        // 
        panel1.Controls.Add(label1);
        panel1.Controls.Add(txtExternalUrl);
        panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        panel1.Location = new System.Drawing.Point(0, 0);
        panel1.Margin = new System.Windows.Forms.Padding(0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(704, 39);
        panel1.TabIndex = 9;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(7, 12);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(31, 15);
        label1.TabIndex = 7;
        label1.Text = "URL:";
        // 
        // txtExternalUrl
        // 
        txtExternalUrl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtExternalUrl.Location = new System.Drawing.Point(44, 8);
        txtExternalUrl.Name = "txtExternalUrl";
        txtExternalUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtExternalUrl.Size = new System.Drawing.Size(655, 23);
        txtExternalUrl.TabIndex = 3;
        txtExternalUrl.TextChanged += txtExternalUrl_TextChanged;
        // 
        // ExternalAtisGenerator
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(groupBox1);
        DoubleBuffered = true;
        Name = "ExternalAtisGenerator";
        Size = new System.Drawing.Size(710, 279);
        groupBox1.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupTest.ResumeLayout(false);
        groupTest.PerformLayout();
        tlpVariables.ResumeLayout(false);
        tlpVariables.PerformLayout();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
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
