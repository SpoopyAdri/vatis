namespace Vatsim.Vatis.UI.Dialogs
{
    partial class ReadOnlyDefinitionsDialog
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
            lstConditions = new System.Windows.Forms.CheckedListBox();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lstConditions
            // 
            lstConditions.FormattingEnabled = true;
            lstConditions.HorizontalScrollbar = true;
            lstConditions.Location = new System.Drawing.Point(28, 22);
            lstConditions.Name = "lstConditions";
            lstConditions.Size = new System.Drawing.Size(412, 184);
            lstConditions.TabIndex = 0;
            lstConditions.ThreeDCheckBoxes = true;
            lstConditions.ItemCheck += lstConditions_ItemCheck;
            lstConditions.Format += lstConditions_Format;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(28, 216);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(412, 33);
            label1.TabIndex = 1;
            label1.Text = "These definitions are read-only. To make any changes, you must update them via the main vATIS interface.";
            // 
            // ReadOnlyDefinitionsDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(469, 271);
            Controls.Add(label1);
            Controls.Add(lstConditions);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReadOnlyDefinitionsDialog";
            Padding = new System.Windows.Forms.Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Definitions Dialog";
            FormClosing += ReadOnlyDefinitionsDialog_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstConditions;
        private System.Windows.Forms.Label label1;
    }
}