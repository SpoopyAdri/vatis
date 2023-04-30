namespace Vatsim.Vatis.UI.Controls;

partial class NodeFormatTemplate
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
        components = new System.ComponentModel.Container();
        groupBox1 = new System.Windows.Forms.GroupBox();
        variablePanel = new System.Windows.Forms.FlowLayoutPanel();
        label8 = new System.Windows.Forms.Label();
        txtVoiceTemplate = new System.Windows.Forms.TextBox();
        txtTextTemplate = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        variableToolTip = new System.Windows.Forms.ToolTip(components);
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(variablePanel);
        groupBox1.Controls.Add(label8);
        groupBox1.Controls.Add(txtVoiceTemplate);
        groupBox1.Controls.Add(txtTextTemplate);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(label7);
        groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox1.Location = new System.Drawing.Point(0, 0);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(615, 210);
        groupBox1.TabIndex = 21;
        groupBox1.TabStop = false;
        groupBox1.Text = "Template";
        // 
        // variablePanel
        // 
        variablePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        variablePanel.Location = new System.Drawing.Point(99, 150);
        variablePanel.Name = "variablePanel";
        variablePanel.Size = new System.Drawing.Size(480, 40);
        variablePanel.TabIndex = 27;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(36, 150);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(56, 15);
        label8.TabIndex = 22;
        label8.Text = "Variables:";
        // 
        // txtVoiceTemplate
        // 
        txtVoiceTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtVoiceTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        txtVoiceTemplate.Location = new System.Drawing.Point(99, 88);
        txtVoiceTemplate.Multiline = true;
        txtVoiceTemplate.Name = "txtVoiceTemplate";
        txtVoiceTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtVoiceTemplate.Size = new System.Drawing.Size(480, 49);
        txtVoiceTemplate.TabIndex = 21;
        txtVoiceTemplate.TextChanged += txtVoiceTemplate_TextChanged;
        // 
        // txtTextTemplate
        // 
        txtTextTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtTextTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        txtTextTemplate.Location = new System.Drawing.Point(99, 26);
        txtTextTemplate.Multiline = true;
        txtTextTemplate.Name = "txtTextTemplate";
        txtTextTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtTextTemplate.Size = new System.Drawing.Size(480, 49);
        txtTextTemplate.TabIndex = 20;
        txtTextTemplate.TextChanged += txtTextTemplate_TextChanged;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(61, 26);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(31, 15);
        label6.TabIndex = 18;
        label6.Text = "Text:";
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(54, 88);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(38, 15);
        label7.TabIndex = 19;
        label7.Text = "Voice:";
        // 
        // NodeFormatTemplate
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(groupBox1);
        DoubleBuffered = true;
        Name = "NodeFormatTemplate";
        Size = new System.Drawing.Size(615, 210);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtVoiceTemplate;
    private System.Windows.Forms.TextBox txtTextTemplate;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.FlowLayoutPanel variablePanel;
    private System.Windows.Forms.ToolTip variableToolTip;
}
