
namespace Vatsim.Vatis.UI.Dialogs
{
    partial class SettingsDialog
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ddlNetworkRating = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVatsimPassword = new System.Windows.Forms.TextBox();
            this.txtVatsimId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ddlServerName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkKeepVisible = new System.Windows.Forms.CheckBox();
            this.chkSuppressNotifications = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ddlNetworkRating);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtVatsimPassword);
            this.groupBox3.Controls.Add(this.txtVatsimId);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.ddlServerName);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(45, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(276, 306);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Network Settings";
            // 
            // ddlNetworkRating
            // 
            this.ddlNetworkRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlNetworkRating.FormattingEnabled = true;
            this.ddlNetworkRating.Location = new System.Drawing.Point(17, 205);
            this.ddlNetworkRating.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlNetworkRating.Name = "ddlNetworkRating";
            this.ddlNetworkRating.Size = new System.Drawing.Size(245, 23);
            this.ddlNetworkRating.TabIndex = 3;
            this.ddlNetworkRating.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Network Rating:";
            // 
            // txtVatsimPassword
            // 
            this.txtVatsimPassword.Location = new System.Drawing.Point(17, 153);
            this.txtVatsimPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVatsimPassword.Name = "txtVatsimPassword";
            this.txtVatsimPassword.PasswordChar = '●';
            this.txtVatsimPassword.Size = new System.Drawing.Size(245, 23);
            this.txtVatsimPassword.TabIndex = 2;
            // 
            // txtVatsimId
            // 
            this.txtVatsimId.Location = new System.Drawing.Point(17, 101);
            this.txtVatsimId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVatsimId.Name = "txtVatsimId";
            this.txtVatsimId.Size = new System.Drawing.Size(245, 23);
            this.txtVatsimId.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(17, 49);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(245, 23);
            this.txtName.TabIndex = 0;
            // 
            // ddlServerName
            // 
            this.ddlServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlServerName.FormattingEnabled = true;
            this.ddlServerName.Location = new System.Drawing.Point(17, 257);
            this.ddlServerName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlServerName.Name = "ddlServerName";
            this.ddlServerName.Size = new System.Drawing.Size(245, 23);
            this.ddlServerName.TabIndex = 4;
            this.ddlServerName.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 235);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Network Server:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Your Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 131);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "VATSIM Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "VATSIM ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkKeepVisible);
            this.groupBox1.Controls.Add(this.chkSuppressNotifications);
            this.groupBox1.Location = new System.Drawing.Point(45, 360);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(276, 104);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // chkKeepVisible
            // 
            this.chkKeepVisible.AutoSize = true;
            this.chkKeepVisible.Location = new System.Drawing.Point(15, 65);
            this.chkKeepVisible.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkKeepVisible.Name = "chkKeepVisible";
            this.chkKeepVisible.Size = new System.Drawing.Size(164, 19);
            this.chkKeepVisible.TabIndex = 6;
            this.chkKeepVisible.TabStop = false;
            this.chkKeepVisible.Text = "Keep vATIS window visible";
            this.chkKeepVisible.UseVisualStyleBackColor = true;
            // 
            // chkSuppressNotifications
            // 
            this.chkSuppressNotifications.AutoSize = true;
            this.chkSuppressNotifications.Location = new System.Drawing.Point(15, 32);
            this.chkSuppressNotifications.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkSuppressNotifications.Name = "chkSuppressNotifications";
            this.chkSuppressNotifications.Size = new System.Drawing.Size(238, 19);
            this.chkSuppressNotifications.TabIndex = 5;
            this.chkSuppressNotifications.TabStop = false;
            this.chkSuppressNotifications.Text = "Suppress ATIS update notification sound";
            this.chkSuppressNotifications.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(236, 479);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 27);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(43, 479);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 27);
            this.btnSave.TabIndex = 11;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 541);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "vATIS Settings";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox ddlNetworkRating;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVatsimPassword;
        private System.Windows.Forms.TextBox txtVatsimId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox ddlServerName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkKeepVisible;
        private System.Windows.Forms.CheckBox chkSuppressNotifications;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}