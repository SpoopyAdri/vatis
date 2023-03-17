namespace Vatsim.Vatis.UI.Controls;

partial class AtisTemplate
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
            this.txtAtisTemplate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAtisTemplate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(721, 349);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ATIS Template";
            // 
            // txtAtisTemplate
            // 
            this.txtAtisTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAtisTemplate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAtisTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAtisTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAtisTemplate.Location = new System.Drawing.Point(3, 19);
            this.txtAtisTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAtisTemplate.Multiline = true;
            this.txtAtisTemplate.Name = "txtAtisTemplate";
            this.txtAtisTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAtisTemplate.Size = new System.Drawing.Size(715, 327);
            this.txtAtisTemplate.TabIndex = 5;
            this.txtAtisTemplate.TextChanged += new System.EventHandler(this.txtAtisTemplate_TextChanged);
            // 
            // AtisTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AtisTemplate";
            this.Size = new System.Drawing.Size(721, 349);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtAtisTemplate;
}
