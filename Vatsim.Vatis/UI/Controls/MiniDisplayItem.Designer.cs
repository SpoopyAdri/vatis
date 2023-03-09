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
            this.txtIcao = new System.Windows.Forms.Label();
            this.txtAtisLetter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // metarTooltip
            // 
            this.metarTooltip.AutomaticDelay = 1000;
            // 
            // txtIcao
            // 
            this.txtIcao.AutoSize = true;
            this.txtIcao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtIcao.ForeColor = System.Drawing.Color.White;
            this.txtIcao.Location = new System.Drawing.Point(2, 5);
            this.txtIcao.Name = "txtIcao";
            this.txtIcao.Size = new System.Drawing.Size(50, 21);
            this.txtIcao.TabIndex = 0;
            this.txtIcao.Text = "KXXX";
            // 
            // txtAtisLetter
            // 
            this.txtAtisLetter.AutoSize = true;
            this.txtAtisLetter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtAtisLetter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtAtisLetter.ForeColor = System.Drawing.Color.Cyan;
            this.txtAtisLetter.Location = new System.Drawing.Point(49, 5);
            this.txtAtisLetter.Name = "txtAtisLetter";
            this.txtAtisLetter.Size = new System.Drawing.Size(20, 21);
            this.txtAtisLetter.TabIndex = 1;
            this.txtAtisLetter.Text = "X";
            this.txtAtisLetter.Click += new System.EventHandler(this.txtAtisLetter_Click);
            // 
            // MiniDisplayItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.txtAtisLetter);
            this.Controls.Add(this.txtIcao);
            this.MaximumSize = new System.Drawing.Size(70, 30);
            this.MinimumSize = new System.Drawing.Size(70, 30);
            this.Name = "MiniDisplayItem";
            this.Size = new System.Drawing.Size(70, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip metarTooltip;
        private System.Windows.Forms.Label txtIcao;
        private System.Windows.Forms.Label txtAtisLetter;
    }
}
