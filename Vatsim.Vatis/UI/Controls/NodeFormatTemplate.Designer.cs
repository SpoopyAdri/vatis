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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.variablePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtVoiceTemplate = new System.Windows.Forms.TextBox();
            this.txtTextTemplate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.variableToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.variablePanel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtVoiceTemplate);
            this.groupBox1.Controls.Add(this.txtTextTemplate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 210);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template";
            // 
            // variablePanel
            // 
            this.variablePanel.Location = new System.Drawing.Point(99, 150);
            this.variablePanel.Name = "variablePanel";
            this.variablePanel.Size = new System.Drawing.Size(480, 40);
            this.variablePanel.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Variables:";
            // 
            // txtVoiceTemplate
            // 
            this.txtVoiceTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtVoiceTemplate.Location = new System.Drawing.Point(99, 88);
            this.txtVoiceTemplate.Multiline = true;
            this.txtVoiceTemplate.Name = "txtVoiceTemplate";
            this.txtVoiceTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVoiceTemplate.Size = new System.Drawing.Size(480, 49);
            this.txtVoiceTemplate.TabIndex = 21;
            this.txtVoiceTemplate.TextChanged += new System.EventHandler(this.txtVoiceTemplate_TextChanged);
            // 
            // txtTextTemplate
            // 
            this.txtTextTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTextTemplate.Location = new System.Drawing.Point(99, 26);
            this.txtTextTemplate.Multiline = true;
            this.txtTextTemplate.Name = "txtTextTemplate";
            this.txtTextTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTextTemplate.Size = new System.Drawing.Size(480, 49);
            this.txtTextTemplate.TabIndex = 20;
            this.txtTextTemplate.TextChanged += new System.EventHandler(this.txtTextTemplate_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Text:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "Voice:";
            // 
            // NodeFormatTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "NodeFormatTemplate";
            this.Size = new System.Drawing.Size(615, 210);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
