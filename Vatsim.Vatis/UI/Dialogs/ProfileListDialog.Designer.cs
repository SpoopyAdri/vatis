using Vatsim.Vatis.UI.Controls;

namespace Vatsim.Vatis.UI.Dialogs
{
    partial class ProfileListDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileListDialog));
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnExit = new Vatsim.Vatis.UI.Controls.ExButton();
            this.btnExport = new Vatsim.Vatis.UI.Controls.ExButton();
            this.btnImport = new Vatsim.Vatis.UI.Controls.ExButton();
            this.btnDelete = new Vatsim.Vatis.UI.Controls.ExButton();
            this.btnRename = new Vatsim.Vatis.UI.Controls.ExButton();
            this.btnNew = new Vatsim.Vatis.UI.Controls.ExButton();
            this.pnlFacilityList = new Vatsim.Vatis.UI.Controls.HitTestPanel();
            this.listProfiles = new System.Windows.Forms.ListBox();
            this.pnlFacilityList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.ForeColor = System.Drawing.Color.Silver;
            this.lblVersion.Location = new System.Drawing.Point(0, 320);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(280, 25);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "Version 1.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnExit.Clicked = false;
            this.btnExit.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnExit.Location = new System.Drawing.Point(185, 291);
            this.btnExit.Name = "btnExit";
            this.btnExit.Pushed = false;
            this.btnExit.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnExit.Size = new System.Drawing.Size(89, 23);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnExport.Clicked = false;
            this.btnExport.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(185, 145);
            this.btnExport.Name = "btnExport";
            this.btnExport.Pushed = false;
            this.btnExport.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnExport.Size = new System.Drawing.Size(89, 23);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnImport.Clicked = false;
            this.btnImport.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnImport.Location = new System.Drawing.Point(185, 116);
            this.btnImport.Name = "btnImport";
            this.btnImport.Pushed = false;
            this.btnImport.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnImport.Size = new System.Drawing.Size(89, 23);
            this.btnImport.TabIndex = 12;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnDelete.Clicked = false;
            this.btnDelete.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(185, 87);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Pushed = false;
            this.btnDelete.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnDelete.Size = new System.Drawing.Size(89, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRename
            // 
            this.btnRename.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRename.Clicked = false;
            this.btnRename.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRename.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRename.Enabled = false;
            this.btnRename.Location = new System.Drawing.Point(185, 58);
            this.btnRename.Name = "btnRename";
            this.btnRename.Pushed = false;
            this.btnRename.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRename.Size = new System.Drawing.Size(89, 23);
            this.btnRename.TabIndex = 10;
            this.btnRename.Text = "Rename";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnNew
            // 
            this.btnNew.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnNew.Clicked = false;
            this.btnNew.ClickedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnNew.Location = new System.Drawing.Point(185, 29);
            this.btnNew.Name = "btnNew";
            this.btnNew.Pushed = false;
            this.btnNew.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnNew.Size = new System.Drawing.Size(89, 23);
            this.btnNew.TabIndex = 9;
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlFacilityList
            // 
            this.pnlFacilityList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlFacilityList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.pnlFacilityList.Controls.Add(this.listProfiles);
            this.pnlFacilityList.Location = new System.Drawing.Point(6, 29);
            this.pnlFacilityList.Name = "pnlFacilityList";
            this.pnlFacilityList.Padding = new System.Windows.Forms.Padding(5);
            this.pnlFacilityList.ShowBorder = true;
            this.pnlFacilityList.Size = new System.Drawing.Size(173, 285);
            this.pnlFacilityList.TabIndex = 0;
            // 
            // listProfiles
            // 
            this.listProfiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listProfiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listProfiles.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listProfiles.ForeColor = System.Drawing.Color.White;
            this.listProfiles.FormattingEnabled = true;
            this.listProfiles.IntegralHeight = false;
            this.listProfiles.ItemHeight = 14;
            this.listProfiles.Location = new System.Drawing.Point(5, 5);
            this.listProfiles.Name = "listProfiles";
            this.listProfiles.Size = new System.Drawing.Size(163, 275);
            this.listProfiles.Sorted = true;
            this.listProfiles.TabIndex = 0;
            this.listProfiles.SelectedIndexChanged += new System.EventHandler(this.listProfiles_SelectedIndexChanged);
            this.listProfiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listProfiles_KeyDown);
            this.listProfiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listProfiles_MouseDoubleClick);
            // 
            // ProfileListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(280, 345);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pnlFacilityList);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vATIS Profiles";
            this.pnlFacilityList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HitTestPanel pnlFacilityList;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ListBox listProfiles;
        private ExButton btnNew;
        private ExButton btnRename;
        private ExButton btnDelete;
        private ExButton btnImport;
        private ExButton btnExport;
        private ExButton btnExit;
    }
}