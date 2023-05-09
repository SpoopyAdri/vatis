
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
            groupBox3 = new System.Windows.Forms.GroupBox();
            ddlNetworkRating = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            txtVatsimPassword = new System.Windows.Forms.TextBox();
            txtVatsimId = new System.Windows.Forms.TextBox();
            txtName = new System.Windows.Forms.TextBox();
            ddlServerName = new System.Windows.Forms.ComboBox();
            label9 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            chkKeepVisible = new System.Windows.Forms.CheckBox();
            chkSuppressNotifications = new System.Windows.Forms.CheckBox();
            btnCancel = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ddlNetworkRating);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(txtVatsimPassword);
            groupBox3.Controls.Add(txtVatsimId);
            groupBox3.Controls.Add(txtName);
            groupBox3.Controls.Add(ddlServerName);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new System.Drawing.Point(45, 34);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(276, 306);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Network Settings";
            // 
            // ddlNetworkRating
            // 
            ddlNetworkRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ddlNetworkRating.FormattingEnabled = true;
            ddlNetworkRating.Location = new System.Drawing.Point(17, 205);
            ddlNetworkRating.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ddlNetworkRating.Name = "ddlNetworkRating";
            ddlNetworkRating.Size = new System.Drawing.Size(245, 23);
            ddlNetworkRating.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 183);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(92, 15);
            label1.TabIndex = 3;
            label1.Text = "Network Rating:";
            // 
            // txtVatsimPassword
            // 
            txtVatsimPassword.Location = new System.Drawing.Point(17, 153);
            txtVatsimPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtVatsimPassword.Name = "txtVatsimPassword";
            txtVatsimPassword.PasswordChar = '●';
            txtVatsimPassword.Size = new System.Drawing.Size(245, 23);
            txtVatsimPassword.TabIndex = 2;
            // 
            // txtVatsimId
            // 
            txtVatsimId.Location = new System.Drawing.Point(17, 101);
            txtVatsimId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtVatsimId.Name = "txtVatsimId";
            txtVatsimId.Size = new System.Drawing.Size(245, 23);
            txtVatsimId.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(17, 49);
            txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(245, 23);
            txtName.TabIndex = 0;
            // 
            // ddlServerName
            // 
            ddlServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ddlServerName.FormattingEnabled = true;
            ddlServerName.Location = new System.Drawing.Point(17, 257);
            ddlServerName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ddlServerName.Name = "ddlServerName";
            ddlServerName.Size = new System.Drawing.Size(245, 23);
            ddlServerName.TabIndex = 4;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(15, 235);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(90, 15);
            label9.TabIndex = 4;
            label9.Text = "Network Server:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(15, 27);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(69, 15);
            label7.TabIndex = 0;
            label7.Text = "Your Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(15, 131);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(102, 15);
            label6.TabIndex = 2;
            label6.Text = "VATSIM Password:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(15, 79);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(63, 15);
            label5.TabIndex = 1;
            label5.Text = "VATSIM ID:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkKeepVisible);
            groupBox1.Controls.Add(chkSuppressNotifications);
            groupBox1.Location = new System.Drawing.Point(45, 360);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(276, 104);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Settings";
            // 
            // chkKeepVisible
            // 
            chkKeepVisible.AutoSize = true;
            chkKeepVisible.Location = new System.Drawing.Point(15, 65);
            chkKeepVisible.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkKeepVisible.Name = "chkKeepVisible";
            chkKeepVisible.Size = new System.Drawing.Size(164, 19);
            chkKeepVisible.TabIndex = 1;
            chkKeepVisible.Text = "Keep vATIS window visible";
            chkKeepVisible.UseVisualStyleBackColor = true;
            // 
            // chkSuppressNotifications
            // 
            chkSuppressNotifications.AutoSize = true;
            chkSuppressNotifications.Location = new System.Drawing.Point(15, 32);
            chkSuppressNotifications.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkSuppressNotifications.Name = "chkSuppressNotifications";
            chkSuppressNotifications.Size = new System.Drawing.Size(238, 19);
            chkSuppressNotifications.TabIndex = 0;
            chkSuppressNotifications.Text = "Suppress ATIS update notification sound";
            chkSuppressNotifications.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(236, 479);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(86, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(43, 479);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(112, 27);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save Settings";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // SettingsDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(364, 541);
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            Controls.Add(groupBox3);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsDialog";
            Padding = new System.Windows.Forms.Padding(10);
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "vATIS Settings";
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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