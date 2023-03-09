
namespace Vatsim.Vatis.UI.Dialogs
{
    partial class NewCompositeDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.typeArrival = new System.Windows.Forms.RadioButton();
            this.typeDeparture = new System.Windows.Forms.RadioButton();
            this.typeCombined = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtCompositeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 161);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "ATIS Type:";
            // 
            // typeArrival
            // 
            this.typeArrival.AutoSize = true;
            this.typeArrival.Location = new System.Drawing.Point(70, 232);
            this.typeArrival.Name = "typeArrival";
            this.typeArrival.Size = new System.Drawing.Size(59, 19);
            this.typeArrival.TabIndex = 4;
            this.typeArrival.TabStop = true;
            this.typeArrival.Text = "Arrival";
            this.typeArrival.UseVisualStyleBackColor = true;
            this.typeArrival.CheckedChanged += new System.EventHandler(this.typeArrival_CheckedChanged);
            // 
            // typeDeparture
            // 
            this.typeDeparture.AutoSize = true;
            this.typeDeparture.Location = new System.Drawing.Point(70, 207);
            this.typeDeparture.Name = "typeDeparture";
            this.typeDeparture.Size = new System.Drawing.Size(77, 19);
            this.typeDeparture.TabIndex = 3;
            this.typeDeparture.TabStop = true;
            this.typeDeparture.Text = "Departure";
            this.typeDeparture.UseVisualStyleBackColor = true;
            this.typeDeparture.CheckedChanged += new System.EventHandler(this.typeDeparture_CheckedChanged);
            // 
            // typeCombined
            // 
            this.typeCombined.AutoSize = true;
            this.typeCombined.Location = new System.Drawing.Point(70, 182);
            this.typeCombined.Name = "typeCombined";
            this.typeCombined.Size = new System.Drawing.Size(81, 19);
            this.typeCombined.TabIndex = 2;
            this.typeCombined.TabStop = true;
            this.typeCombined.Text = "Combined";
            this.typeCombined.UseVisualStyleBackColor = true;
            this.typeCombined.CheckedChanged += new System.EventHandler(this.typeCombined_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(179, 270);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 27);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(70, 270);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentifier.Location = new System.Drawing.Point(70, 63);
            this.txtIdentifier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtIdentifier.MaxLength = 4;
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(197, 23);
            this.txtIdentifier.TabIndex = 0;
            this.txtIdentifier.TextChanged += new System.EventHandler(this.txtIdentifier_TextChanged);
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(68, 43);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(158, 15);
            this.lblPrompt.TabIndex = 24;
            this.lblPrompt.Text = "Airport Identifier (e.g. KLAX):";
            // 
            // txtCompositeName
            // 
            this.txtCompositeName.Location = new System.Drawing.Point(70, 120);
            this.txtCompositeName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCompositeName.MaxLength = 50;
            this.txtCompositeName.Name = "txtCompositeName";
            this.txtCompositeName.Size = new System.Drawing.Size(197, 23);
            this.txtCompositeName.TabIndex = 1;
            this.txtCompositeName.TextChanged += new System.EventHandler(this.txtCompositeName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 15);
            this.label2.TabIndex = 32;
            this.label2.Text = "Composite Name (e.g. Los Angeles):";
            // 
            // NewCompositeDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 341);
            this.ControlBox = false;
            this.Controls.Add(this.txtCompositeName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.typeCombined);
            this.Controls.Add(this.typeDeparture);
            this.Controls.Add(this.typeArrival);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtIdentifier);
            this.Controls.Add(this.lblPrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NewCompositeDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Composite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton typeArrival;
        private System.Windows.Forms.RadioButton typeDeparture;
        private System.Windows.Forms.RadioButton typeCombined;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtCompositeName;
        private System.Windows.Forms.Label label2;
    }
}