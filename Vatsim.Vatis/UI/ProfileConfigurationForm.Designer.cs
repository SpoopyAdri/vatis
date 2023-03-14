using Vatsim.Vatis.UI.Controls;

namespace Vatsim.Vatis.UI
{
    partial class ProfileConfigurationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileConfigurationForm));
            this.TlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.pageConfiguration = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageGeneral = new System.Windows.Forms.TabPage();
            this.groupVoiceOption = new System.Windows.Forms.Panel();
            this.radioTextToSpeech = new System.Windows.Forms.RadioButton();
            this.ddlVoices = new System.Windows.Forms.ComboBox();
            this.radioVoiceRecorded = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.vhfFrequency = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.observationTime = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.typeCombined = new System.Windows.Forms.RadioButton();
            this.typeDeparture = new System.Windows.Forms.RadioButton();
            this.typeArrival = new System.Windows.Forms.RadioButton();
            this.txtIdsEndpoint = new System.Windows.Forms.TextBox();
            this.chkObservationTime = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMagneticVar = new System.Windows.Forms.CheckBox();
            this.magneticVar = new System.Windows.Forms.NumericUpDown();
            this.pageFormat = new System.Windows.Forms.TabPage();
            this.chkDecimalTerminology = new System.Windows.Forms.CheckBox();
            this.chkVisibilitySuffix = new System.Windows.Forms.CheckBox();
            this.chkSurfaceWindPrefix = new System.Windows.Forms.CheckBox();
            this.chkConvertMetric = new System.Windows.Forms.CheckBox();
            this.chkTransitionLevelPrefix = new System.Windows.Forms.CheckBox();
            this.chkPrefixNotams = new System.Windows.Forms.CheckBox();
            this.chkFaaFormat = new System.Windows.Forms.CheckBox();
            this.chkExternalAtisGenerator = new System.Windows.Forms.CheckBox();
            this.pagePresets = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtAtisTemplate = new System.Windows.Forms.TextBox();
            this.btnRenamePreset = new System.Windows.Forms.Button();
            this.btnDeletePreset = new System.Windows.Forms.Button();
            this.btnCopyPreset = new System.Windows.Forms.Button();
            this.btnNewPreset = new System.Windows.Forms.Button();
            this.ddlPresets = new System.Windows.Forms.ComboBox();
            this.pageContractions = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeleteContraction = new System.Windows.Forms.Button();
            this.gridContractions = new System.Windows.Forms.DataGridView();
            this.ColumnFind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReplace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageExternalAtis = new System.Windows.Forms.TabPage();
            this.txtSelectedPreset = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupTest = new System.Windows.Forms.GroupBox();
            this.btnFetchMetar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtMetar = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.tlpVariables = new System.Windows.Forms.TableLayoutPanel();
            this.txtExternalRemarks = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExternalDep = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExternalArr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExternalApp = new System.Windows.Forms.TextBox();
            this.txtExternalUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pageTransitionLevel = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gridTransitionLevels = new System.Windows.Forms.DataGridView();
            this.low = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.high = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transitionLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDeleteTransitionLevel = new System.Windows.Forms.Button();
            this.btnManageComposite = new System.Windows.Forms.Button();
            this.ctxOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRename = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxImport = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxExport = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCompositeList = new Vatsim.Vatis.UI.Controls.HitTestPanel();
            this.TreeMenu = new System.Windows.Forms.TreeView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.compositeTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TlpMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.pageConfiguration.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pageGeneral.SuspendLayout();
            this.groupVoiceOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.observationTime)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magneticVar)).BeginInit();
            this.pageFormat.SuspendLayout();
            this.pagePresets.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pageContractions.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContractions)).BeginInit();
            this.pageExternalAtis.SuspendLayout();
            this.groupTest.SuspendLayout();
            this.tlpVariables.SuspendLayout();
            this.pageTransitionLevel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransitionLevels)).BeginInit();
            this.panel3.SuspendLayout();
            this.ctxOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCompositeList.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // TlpMain
            // 
            this.TlpMain.ColumnCount = 3;
            this.TlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 726F));
            this.TlpMain.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.TlpMain.Controls.Add(this.btnManageComposite, 1, 1);
            this.TlpMain.Controls.Add(this.panel1, 1, 0);
            this.TlpMain.Controls.Add(this.flowLayoutPanel1, 2, 1);
            this.TlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TlpMain.Location = new System.Drawing.Point(15, 15);
            this.TlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.TlpMain.Name = "TlpMain";
            this.TlpMain.RowCount = 2;
            this.TlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.TlpMain.Size = new System.Drawing.Size(934, 431);
            this.TlpMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainTabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(212, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 390);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.pageConfiguration);
            this.mainTabControl.Controls.Add(this.pagePresets);
            this.mainTabControl.Controls.Add(this.pageContractions);
            this.mainTabControl.Controls.Add(this.pageExternalAtis);
            this.mainTabControl.Controls.Add(this.pageTransitionLevel);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Enabled = false;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(718, 390);
            this.mainTabControl.TabIndex = 1;
            // 
            // pageConfiguration
            // 
            this.pageConfiguration.Controls.Add(this.tabControl1);
            this.pageConfiguration.Location = new System.Drawing.Point(4, 24);
            this.pageConfiguration.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageConfiguration.Name = "pageConfiguration";
            this.pageConfiguration.Padding = new System.Windows.Forms.Padding(15);
            this.pageConfiguration.Size = new System.Drawing.Size(710, 362);
            this.pageConfiguration.TabIndex = 2;
            this.pageConfiguration.Text = "Configuration";
            this.pageConfiguration.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pageGeneral);
            this.tabControl1.Controls.Add(this.pageFormat);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(15, 15);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 332);
            this.tabControl1.TabIndex = 44;
            // 
            // pageGeneral
            // 
            this.pageGeneral.Controls.Add(this.groupVoiceOption);
            this.pageGeneral.Controls.Add(this.label3);
            this.pageGeneral.Controls.Add(this.vhfFrequency);
            this.pageGeneral.Controls.Add(this.label4);
            this.pageGeneral.Controls.Add(this.observationTime);
            this.pageGeneral.Controls.Add(this.flowLayoutPanel2);
            this.pageGeneral.Controls.Add(this.txtIdsEndpoint);
            this.pageGeneral.Controls.Add(this.chkObservationTime);
            this.pageGeneral.Controls.Add(this.label2);
            this.pageGeneral.Controls.Add(this.chkMagneticVar);
            this.pageGeneral.Controls.Add(this.magneticVar);
            this.pageGeneral.Location = new System.Drawing.Point(4, 24);
            this.pageGeneral.Name = "pageGeneral";
            this.pageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.pageGeneral.Size = new System.Drawing.Size(672, 304);
            this.pageGeneral.TabIndex = 1;
            this.pageGeneral.Text = "General";
            this.pageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupVoiceOption
            // 
            this.groupVoiceOption.Controls.Add(this.radioTextToSpeech);
            this.groupVoiceOption.Controls.Add(this.ddlVoices);
            this.groupVoiceOption.Controls.Add(this.radioVoiceRecorded);
            this.groupVoiceOption.Location = new System.Drawing.Point(40, 194);
            this.groupVoiceOption.Name = "groupVoiceOption";
            this.groupVoiceOption.Size = new System.Drawing.Size(534, 25);
            this.groupVoiceOption.TabIndex = 38;
            // 
            // radioTextToSpeech
            // 
            this.radioTextToSpeech.AutoSize = true;
            this.radioTextToSpeech.Checked = true;
            this.radioTextToSpeech.Location = new System.Drawing.Point(4, 3);
            this.radioTextToSpeech.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioTextToSpeech.Name = "radioTextToSpeech";
            this.radioTextToSpeech.Size = new System.Drawing.Size(104, 19);
            this.radioTextToSpeech.TabIndex = 10;
            this.radioTextToSpeech.TabStop = true;
            this.radioTextToSpeech.Text = "Text to Speech:";
            this.compositeTooltip.SetToolTip(this.radioTextToSpeech, "Use synthesized text to speech to generate the ATIS voice.");
            this.radioTextToSpeech.UseVisualStyleBackColor = true;
            this.radioTextToSpeech.CheckedChanged += new System.EventHandler(this.radioTextToSpeech_CheckedChanged);
            // 
            // ddlVoices
            // 
            this.ddlVoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlVoices.FormattingEnabled = true;
            this.ddlVoices.Items.AddRange(new object[] {
            "Default",
            "US Male",
            "US Female",
            "UK Male",
            "UK Female"});
            this.ddlVoices.Location = new System.Drawing.Point(130, 1);
            this.ddlVoices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlVoices.Name = "ddlVoices";
            this.ddlVoices.Size = new System.Drawing.Size(264, 23);
            this.ddlVoices.TabIndex = 9;
            this.ddlVoices.SelectedIndexChanged += new System.EventHandler(this.ddlVoices_SelectedIndexChanged);
            // 
            // radioVoiceRecorded
            // 
            this.radioVoiceRecorded.AutoSize = true;
            this.radioVoiceRecorded.Location = new System.Drawing.Point(416, 3);
            this.radioVoiceRecorded.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioVoiceRecorded.Name = "radioVoiceRecorded";
            this.radioVoiceRecorded.Size = new System.Drawing.Size(106, 19);
            this.radioVoiceRecorded.TabIndex = 11;
            this.radioVoiceRecorded.Text = "Voice Recorded";
            this.compositeTooltip.SetToolTip(this.radioVoiceRecorded, "Manually voice record the ATIS using a microphone device.");
            this.radioVoiceRecorded.UseVisualStyleBackColor = true;
            this.radioVoiceRecorded.CheckedChanged += new System.EventHandler(this.radioVoiceRecorded_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Frequency:";
            this.compositeTooltip.SetToolTip(this.label3, "The VHF frequency of the ATIS station.");
            // 
            // vhfFrequency
            // 
            this.vhfFrequency.Location = new System.Drawing.Point(112, 35);
            this.vhfFrequency.Mask = "000.000";
            this.vhfFrequency.Name = "vhfFrequency";
            this.vhfFrequency.PromptChar = '#';
            this.vhfFrequency.Size = new System.Drawing.Size(114, 23);
            this.vhfFrequency.TabIndex = 16;
            this.vhfFrequency.TextChanged += new System.EventHandler(this.vhfFrequency_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 35;
            this.label4.Text = "ATIS Type:";
            // 
            // observationTime
            // 
            this.observationTime.Enabled = false;
            this.observationTime.Location = new System.Drawing.Point(218, 113);
            this.observationTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.observationTime.Name = "observationTime";
            this.observationTime.Size = new System.Drawing.Size(74, 23);
            this.observationTime.TabIndex = 13;
            this.observationTime.ValueChanged += new System.EventHandler(this.observationTime_ValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.typeCombined);
            this.flowLayoutPanel2.Controls.Add(this.typeDeparture);
            this.flowLayoutPanel2.Controls.Add(this.typeArrival);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(112, 73);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(235, 23);
            this.flowLayoutPanel2.TabIndex = 37;
            // 
            // typeCombined
            // 
            this.typeCombined.AutoSize = true;
            this.typeCombined.Location = new System.Drawing.Point(3, 3);
            this.typeCombined.Name = "typeCombined";
            this.typeCombined.Size = new System.Drawing.Size(81, 19);
            this.typeCombined.TabIndex = 32;
            this.typeCombined.TabStop = true;
            this.typeCombined.Text = "Combined";
            this.typeCombined.UseVisualStyleBackColor = true;
            this.typeCombined.CheckedChanged += new System.EventHandler(this.typeCombined_CheckedChanged);
            // 
            // typeDeparture
            // 
            this.typeDeparture.AutoSize = true;
            this.typeDeparture.Location = new System.Drawing.Point(90, 3);
            this.typeDeparture.Name = "typeDeparture";
            this.typeDeparture.Size = new System.Drawing.Size(77, 19);
            this.typeDeparture.TabIndex = 33;
            this.typeDeparture.TabStop = true;
            this.typeDeparture.Text = "Departure";
            this.typeDeparture.UseVisualStyleBackColor = true;
            this.typeDeparture.CheckedChanged += new System.EventHandler(this.typeDeparture_CheckedChanged);
            // 
            // typeArrival
            // 
            this.typeArrival.AutoSize = true;
            this.typeArrival.Location = new System.Drawing.Point(173, 3);
            this.typeArrival.Name = "typeArrival";
            this.typeArrival.Size = new System.Drawing.Size(59, 19);
            this.typeArrival.TabIndex = 34;
            this.typeArrival.TabStop = true;
            this.typeArrival.Text = "Arrival";
            this.typeArrival.UseVisualStyleBackColor = true;
            this.typeArrival.CheckedChanged += new System.EventHandler(this.typeArrival_CheckedChanged);
            // 
            // txtIdsEndpoint
            // 
            this.txtIdsEndpoint.Location = new System.Drawing.Point(136, 237);
            this.txtIdsEndpoint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtIdsEndpoint.Name = "txtIdsEndpoint";
            this.txtIdsEndpoint.Size = new System.Drawing.Size(438, 23);
            this.txtIdsEndpoint.TabIndex = 7;
            this.txtIdsEndpoint.TextChanged += new System.EventHandler(this.txtIdsEndpoint_TextChanged);
            // 
            // chkObservationTime
            // 
            this.chkObservationTime.AutoSize = true;
            this.chkObservationTime.Location = new System.Drawing.Point(43, 115);
            this.chkObservationTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkObservationTime.Name = "chkObservationTime";
            this.chkObservationTime.Size = new System.Drawing.Size(160, 19);
            this.chkObservationTime.TabIndex = 12;
            this.chkObservationTime.Text = "Offical Observation Time:";
            this.compositeTooltip.SetToolTip(this.chkObservationTime, resources.GetString("chkObservationTime.ToolTip"));
            this.chkObservationTime.UseVisualStyleBackColor = true;
            this.chkObservationTime.CheckedChanged += new System.EventHandler(this.chkObservationTime_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "IDS Endpoint:";
            this.compositeTooltip.SetToolTip(this.label2, "Specify the URL endpoint of an IDS. \r\nWhen an ATIS update occurs, vATIS will post" +
        " the update to the IDS to automatically update it.");
            // 
            // chkMagneticVar
            // 
            this.chkMagneticVar.AutoSize = true;
            this.chkMagneticVar.Location = new System.Drawing.Point(43, 157);
            this.chkMagneticVar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkMagneticVar.Name = "chkMagneticVar";
            this.chkMagneticVar.Size = new System.Drawing.Size(128, 19);
            this.chkMagneticVar.TabIndex = 4;
            this.chkMagneticVar.Text = "Magnetic Variation:";
            this.compositeTooltip.SetToolTip(this.chkMagneticVar, "Add or subtract the specified number of degrees from the wind direction.");
            this.chkMagneticVar.UseVisualStyleBackColor = true;
            this.chkMagneticVar.CheckedChanged += new System.EventHandler(this.chkMagneticVar_CheckedChanged);
            // 
            // magneticVar
            // 
            this.magneticVar.Enabled = false;
            this.magneticVar.Location = new System.Drawing.Point(190, 155);
            this.magneticVar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.magneticVar.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.magneticVar.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.magneticVar.Name = "magneticVar";
            this.magneticVar.Size = new System.Drawing.Size(79, 23);
            this.magneticVar.TabIndex = 1;
            this.magneticVar.ValueChanged += new System.EventHandler(this.magneticVar_ValueChanged);
            // 
            // pageFormat
            // 
            this.pageFormat.Controls.Add(this.chkDecimalTerminology);
            this.pageFormat.Controls.Add(this.chkVisibilitySuffix);
            this.pageFormat.Controls.Add(this.chkSurfaceWindPrefix);
            this.pageFormat.Controls.Add(this.chkConvertMetric);
            this.pageFormat.Controls.Add(this.chkTransitionLevelPrefix);
            this.pageFormat.Controls.Add(this.chkPrefixNotams);
            this.pageFormat.Controls.Add(this.chkFaaFormat);
            this.pageFormat.Controls.Add(this.chkExternalAtisGenerator);
            this.pageFormat.Location = new System.Drawing.Point(4, 24);
            this.pageFormat.Name = "pageFormat";
            this.pageFormat.Padding = new System.Windows.Forms.Padding(3);
            this.pageFormat.Size = new System.Drawing.Size(672, 304);
            this.pageFormat.TabIndex = 0;
            this.pageFormat.Text = "Formatting";
            this.pageFormat.UseVisualStyleBackColor = true;
            // 
            // chkDecimalTerminology
            // 
            this.chkDecimalTerminology.AutoSize = true;
            this.chkDecimalTerminology.Location = new System.Drawing.Point(40, 262);
            this.chkDecimalTerminology.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkDecimalTerminology.Name = "chkDecimalTerminology";
            this.chkDecimalTerminology.Size = new System.Drawing.Size(245, 19);
            this.chkDecimalTerminology.TabIndex = 49;
            this.chkDecimalTerminology.Text = "Use \"decimal\" terminology in spoken text";
            this.compositeTooltip.SetToolTip(this.chkDecimalTerminology, "Use the word \"decimal\" when speaking decimal numbers or frequencies. For example," +
        " 121.1 would be spoken as \"one two one decimal one\".");
            this.chkDecimalTerminology.UseVisualStyleBackColor = true;
            this.chkDecimalTerminology.CheckedChanged += new System.EventHandler(this.chkDecimalTerminology_CheckedChanged);
            // 
            // chkVisibilitySuffix
            // 
            this.chkVisibilitySuffix.AutoSize = true;
            this.chkVisibilitySuffix.Location = new System.Drawing.Point(40, 194);
            this.chkVisibilitySuffix.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkVisibilitySuffix.Name = "chkVisibilitySuffix";
            this.chkVisibilitySuffix.Size = new System.Drawing.Size(333, 19);
            this.chkVisibilitySuffix.TabIndex = 48;
            this.chkVisibilitySuffix.Text = "Append \"meters/kilometers\" to spoken prevailing visibility";
            this.compositeTooltip.SetToolTip(this.chkVisibilitySuffix, "Appends \"meters/kilometers\" to the spoken prevailing visibility value (e.g. \"visi" +
        "bility more than one zero kilometers\")");
            this.chkVisibilitySuffix.UseVisualStyleBackColor = true;
            this.chkVisibilitySuffix.CheckedChanged += new System.EventHandler(this.chkVisibilitySuffix_CheckedChanged);
            // 
            // chkSurfaceWindPrefix
            // 
            this.chkSurfaceWindPrefix.AutoSize = true;
            this.chkSurfaceWindPrefix.Location = new System.Drawing.Point(40, 160);
            this.chkSurfaceWindPrefix.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkSurfaceWindPrefix.Name = "chkSurfaceWindPrefix";
            this.chkSurfaceWindPrefix.Size = new System.Drawing.Size(234, 19);
            this.chkSurfaceWindPrefix.TabIndex = 47;
            this.chkSurfaceWindPrefix.Text = "Prepend \"surface wind\" to spoken wind";
            this.compositeTooltip.SetToolTip(this.chkSurfaceWindPrefix, "Prepends \"surface wind\" to the spoken wind (e.g. \"surface wind 270 degrees at 10\"" +
        ").");
            this.chkSurfaceWindPrefix.UseVisualStyleBackColor = true;
            this.chkSurfaceWindPrefix.CheckedChanged += new System.EventHandler(this.chkSurfaceWindPrefix_CheckedChanged);
            // 
            // chkConvertMetric
            // 
            this.chkConvertMetric.AutoSize = true;
            this.chkConvertMetric.Location = new System.Drawing.Point(40, 228);
            this.chkConvertMetric.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkConvertMetric.Name = "chkConvertMetric";
            this.chkConvertMetric.Size = new System.Drawing.Size(217, 19);
            this.chkConvertMetric.TabIndex = 46;
            this.chkConvertMetric.Text = "Convert cloud layer height to metric";
            this.compositeTooltip.SetToolTip(this.chkConvertMetric, "Converts the cloud layer height from feet to meters.");
            this.chkConvertMetric.UseVisualStyleBackColor = true;
            this.chkConvertMetric.CheckedChanged += new System.EventHandler(this.chkConvertMetric_CheckedChanged);
            // 
            // chkTransitionLevelPrefix
            // 
            this.chkTransitionLevelPrefix.AutoSize = true;
            this.chkTransitionLevelPrefix.Location = new System.Drawing.Point(40, 126);
            this.chkTransitionLevelPrefix.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkTransitionLevelPrefix.Name = "chkTransitionLevelPrefix";
            this.chkTransitionLevelPrefix.Size = new System.Drawing.Size(316, 19);
            this.chkTransitionLevelPrefix.TabIndex = 45;
            this.chkTransitionLevelPrefix.Text = "Prepend \"flight level\" to spoken transition level altitude";
            this.compositeTooltip.SetToolTip(this.chkTransitionLevelPrefix, "Prepends \"flight level\" to the spoken transition level altitude (e.g. \"transition" +
        " level, flight level 70\")");
            this.chkTransitionLevelPrefix.UseVisualStyleBackColor = true;
            this.chkTransitionLevelPrefix.CheckedChanged += new System.EventHandler(this.chkTransitionLevelPrefix_CheckedChanged);
            // 
            // chkPrefixNotams
            // 
            this.chkPrefixNotams.AutoSize = true;
            this.chkPrefixNotams.Location = new System.Drawing.Point(40, 92);
            this.chkPrefixNotams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkPrefixNotams.Name = "chkPrefixNotams";
            this.chkPrefixNotams.Size = new System.Drawing.Size(352, 19);
            this.chkPrefixNotams.TabIndex = 44;
            this.chkPrefixNotams.Text = "Prepend \"Notices to Airmen/Air Missions\" to spoken NOTAMs";
            this.compositeTooltip.SetToolTip(this.chkPrefixNotams, "Prepends \"Notices to Airmen\" or \"Notices to Air Missions\" (FAA) to the spoken NOT" +
        "AM text.");
            this.chkPrefixNotams.UseVisualStyleBackColor = true;
            this.chkPrefixNotams.CheckedChanged += new System.EventHandler(this.chkPrefixNotams_CheckedChanged);
            // 
            // chkFaaFormat
            // 
            this.chkFaaFormat.AutoSize = true;
            this.chkFaaFormat.Location = new System.Drawing.Point(40, 58);
            this.chkFaaFormat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkFaaFormat.Name = "chkFaaFormat";
            this.chkFaaFormat.Size = new System.Drawing.Size(184, 19);
            this.chkFaaFormat.TabIndex = 38;
            this.chkFaaFormat.Text = "Format ATIS using FAA format";
            this.compositeTooltip.SetToolTip(this.chkFaaFormat, "Formats the ATIS using the FAA style format.");
            this.chkFaaFormat.UseVisualStyleBackColor = true;
            this.chkFaaFormat.CheckedChanged += new System.EventHandler(this.chkFaaFormat_CheckedChanged);
            // 
            // chkExternalAtisGenerator
            // 
            this.chkExternalAtisGenerator.AutoSize = true;
            this.chkExternalAtisGenerator.Location = new System.Drawing.Point(40, 24);
            this.chkExternalAtisGenerator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExternalAtisGenerator.Name = "chkExternalAtisGenerator";
            this.chkExternalAtisGenerator.Size = new System.Drawing.Size(310, 19);
            this.chkExternalAtisGenerator.TabIndex = 43;
            this.chkExternalAtisGenerator.Text = "Use external source to produce ATIS text (e.g. UniATIS)";
            this.compositeTooltip.SetToolTip(this.chkExternalAtisGenerator, "Use an external source for producing the ATIS text (e.g. UniATIS).");
            this.chkExternalAtisGenerator.UseVisualStyleBackColor = true;
            this.chkExternalAtisGenerator.CheckedChanged += new System.EventHandler(this.chkExternalAtisGenerator_CheckedChanged);
            // 
            // pagePresets
            // 
            this.pagePresets.Controls.Add(this.label5);
            this.pagePresets.Controls.Add(this.panel2);
            this.pagePresets.Controls.Add(this.btnRenamePreset);
            this.pagePresets.Controls.Add(this.btnDeletePreset);
            this.pagePresets.Controls.Add(this.btnCopyPreset);
            this.pagePresets.Controls.Add(this.btnNewPreset);
            this.pagePresets.Controls.Add(this.ddlPresets);
            this.pagePresets.Location = new System.Drawing.Point(4, 24);
            this.pagePresets.Name = "pagePresets";
            this.pagePresets.Padding = new System.Windows.Forms.Padding(10);
            this.pagePresets.Size = new System.Drawing.Size(710, 362);
            this.pagePresets.TabIndex = 0;
            this.pagePresets.Text = "Presets";
            this.pagePresets.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "ATIS Template:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtAtisTemplate);
            this.panel2.Location = new System.Drawing.Point(14, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4, 6, 6, 6);
            this.panel2.Size = new System.Drawing.Size(680, 286);
            this.panel2.TabIndex = 6;
            // 
            // txtAtisTemplate
            // 
            this.txtAtisTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAtisTemplate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAtisTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAtisTemplate.Enabled = false;
            this.txtAtisTemplate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAtisTemplate.Location = new System.Drawing.Point(4, 6);
            this.txtAtisTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAtisTemplate.Multiline = true;
            this.txtAtisTemplate.Name = "txtAtisTemplate";
            this.txtAtisTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAtisTemplate.Size = new System.Drawing.Size(668, 272);
            this.txtAtisTemplate.TabIndex = 4;
            this.txtAtisTemplate.TextChanged += new System.EventHandler(this.txtTemplate_TextChanged);
            // 
            // btnRenamePreset
            // 
            this.btnRenamePreset.Enabled = false;
            this.btnRenamePreset.Location = new System.Drawing.Point(564, 14);
            this.btnRenamePreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRenamePreset.Name = "btnRenamePreset";
            this.btnRenamePreset.Size = new System.Drawing.Size(64, 24);
            this.btnRenamePreset.TabIndex = 5;
            this.btnRenamePreset.Text = "Rename";
            this.btnRenamePreset.UseVisualStyleBackColor = true;
            this.btnRenamePreset.Click += new System.EventHandler(this.btnRenamePreset_Click);
            // 
            // btnDeletePreset
            // 
            this.btnDeletePreset.Enabled = false;
            this.btnDeletePreset.Location = new System.Drawing.Point(631, 14);
            this.btnDeletePreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeletePreset.Name = "btnDeletePreset";
            this.btnDeletePreset.Size = new System.Drawing.Size(64, 24);
            this.btnDeletePreset.TabIndex = 3;
            this.btnDeletePreset.Text = "Delete";
            this.btnDeletePreset.UseVisualStyleBackColor = true;
            this.btnDeletePreset.Click += new System.EventHandler(this.btnDeletePreset_Click);
            // 
            // btnCopyPreset
            // 
            this.btnCopyPreset.Enabled = false;
            this.btnCopyPreset.Location = new System.Drawing.Point(496, 14);
            this.btnCopyPreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCopyPreset.Name = "btnCopyPreset";
            this.btnCopyPreset.Size = new System.Drawing.Size(64, 24);
            this.btnCopyPreset.TabIndex = 2;
            this.btnCopyPreset.Text = "Copy";
            this.btnCopyPreset.UseVisualStyleBackColor = true;
            this.btnCopyPreset.Click += new System.EventHandler(this.btnCopyPreset_Click);
            // 
            // btnNewPreset
            // 
            this.btnNewPreset.Location = new System.Drawing.Point(428, 14);
            this.btnNewPreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNewPreset.Name = "btnNewPreset";
            this.btnNewPreset.Size = new System.Drawing.Size(64, 24);
            this.btnNewPreset.TabIndex = 1;
            this.btnNewPreset.Text = "New";
            this.btnNewPreset.UseVisualStyleBackColor = true;
            this.btnNewPreset.Click += new System.EventHandler(this.btnNewPreset_Click);
            // 
            // ddlPresets
            // 
            this.ddlPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPresets.FormattingEnabled = true;
            this.ddlPresets.Location = new System.Drawing.Point(14, 15);
            this.ddlPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlPresets.Name = "ddlPresets";
            this.ddlPresets.Size = new System.Drawing.Size(408, 23);
            this.ddlPresets.TabIndex = 0;
            this.ddlPresets.SelectedIndexChanged += new System.EventHandler(this.ddlPresets_SelectedIndexChanged);
            // 
            // pageContractions
            // 
            this.pageContractions.Controls.Add(this.tableLayoutPanel3);
            this.pageContractions.Location = new System.Drawing.Point(4, 24);
            this.pageContractions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageContractions.Name = "pageContractions";
            this.pageContractions.Padding = new System.Windows.Forms.Padding(12);
            this.pageContractions.Size = new System.Drawing.Size(710, 362);
            this.pageContractions.TabIndex = 1;
            this.pageContractions.Text = "Contractions";
            this.pageContractions.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btnDeleteContraction, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.gridContractions, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(686, 338);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnDeleteContraction
            // 
            this.btnDeleteContraction.Location = new System.Drawing.Point(562, 306);
            this.btnDeleteContraction.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeleteContraction.Name = "btnDeleteContraction";
            this.btnDeleteContraction.Size = new System.Drawing.Size(120, 27);
            this.btnDeleteContraction.TabIndex = 1;
            this.btnDeleteContraction.Text = "Delete Selected";
            this.btnDeleteContraction.UseVisualStyleBackColor = true;
            this.btnDeleteContraction.Click += new System.EventHandler(this.btnDeleteContraction_Click);
            // 
            // gridContractions
            // 
            this.gridContractions.AllowUserToResizeColumns = false;
            this.gridContractions.AllowUserToResizeRows = false;
            this.gridContractions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridContractions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridContractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridContractions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFind,
            this.ColumnReplace});
            this.gridContractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridContractions.Location = new System.Drawing.Point(4, 3);
            this.gridContractions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridContractions.Name = "gridContractions";
            this.gridContractions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridContractions.RowHeadersVisible = false;
            this.gridContractions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridContractions.Size = new System.Drawing.Size(678, 297);
            this.gridContractions.TabIndex = 2;
            this.gridContractions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridContractions_CellEndEdit);
            this.gridContractions.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridContractions_CellValidating);
            this.gridContractions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridContractions_CellValueChanged);
            this.gridContractions.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridContractions_EditingControlShowing);
            this.gridContractions.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridContractions_UserDeletedRow);
            this.gridContractions.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gridContractions_UserDeletingRow);
            // 
            // ColumnFind
            // 
            this.ColumnFind.HeaderText = "String";
            this.ColumnFind.Name = "ColumnFind";
            // 
            // ColumnReplace
            // 
            this.ColumnReplace.HeaderText = "Spoken";
            this.ColumnReplace.Name = "ColumnReplace";
            // 
            // pageExternalAtis
            // 
            this.pageExternalAtis.Controls.Add(this.txtSelectedPreset);
            this.pageExternalAtis.Controls.Add(this.label11);
            this.pageExternalAtis.Controls.Add(this.groupTest);
            this.pageExternalAtis.Controls.Add(this.tlpVariables);
            this.pageExternalAtis.Controls.Add(this.txtExternalUrl);
            this.pageExternalAtis.Controls.Add(this.label6);
            this.pageExternalAtis.Location = new System.Drawing.Point(4, 24);
            this.pageExternalAtis.Name = "pageExternalAtis";
            this.pageExternalAtis.Size = new System.Drawing.Size(710, 362);
            this.pageExternalAtis.TabIndex = 4;
            this.pageExternalAtis.Text = "External ATIS Generator";
            this.pageExternalAtis.UseVisualStyleBackColor = true;
            // 
            // txtSelectedPreset
            // 
            this.txtSelectedPreset.AutoSize = true;
            this.txtSelectedPreset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtSelectedPreset.Location = new System.Drawing.Point(67, 15);
            this.txtSelectedPreset.Name = "txtSelectedPreset";
            this.txtSelectedPreset.Size = new System.Drawing.Size(45, 15);
            this.txtSelectedPreset.TabIndex = 10;
            this.txtSelectedPreset.Text = "[None]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Preset:";
            // 
            // groupTest
            // 
            this.groupTest.Controls.Add(this.btnFetchMetar);
            this.groupTest.Controls.Add(this.label10);
            this.groupTest.Controls.Add(this.btnTest);
            this.groupTest.Controls.Add(this.txtMetar);
            this.groupTest.Controls.Add(this.txtResponse);
            this.groupTest.Enabled = false;
            this.groupTest.Location = new System.Drawing.Point(21, 208);
            this.groupTest.Name = "groupTest";
            this.groupTest.Size = new System.Drawing.Size(669, 140);
            this.groupTest.TabIndex = 7;
            this.groupTest.TabStop = false;
            this.groupTest.Text = "Test URL";
            // 
            // btnFetchMetar
            // 
            this.btnFetchMetar.Location = new System.Drawing.Point(528, 23);
            this.btnFetchMetar.Name = "btnFetchMetar";
            this.btnFetchMetar.Size = new System.Drawing.Size(47, 23);
            this.btnFetchMetar.TabIndex = 7;
            this.btnFetchMetar.Text = "Fetch";
            this.btnFetchMetar.UseVisualStyleBackColor = true;
            this.btnFetchMetar.Click += new System.EventHandler(this.btnFetchMetar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "METAR:";
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(580, 23);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(68, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test URL";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtMetar
            // 
            this.txtMetar.Location = new System.Drawing.Point(72, 23);
            this.txtMetar.Name = "txtMetar";
            this.txtMetar.Size = new System.Drawing.Size(451, 23);
            this.txtMetar.TabIndex = 5;
            this.txtMetar.TextChanged += new System.EventHandler(this.txtMetar_TextChanged);
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtResponse.Location = new System.Drawing.Point(23, 52);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(625, 74);
            this.txtResponse.TabIndex = 4;
            // 
            // tlpVariables
            // 
            this.tlpVariables.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpVariables.ColumnCount = 2;
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.40575F));
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.59425F));
            this.tlpVariables.Controls.Add(this.txtExternalRemarks, 1, 3);
            this.tlpVariables.Controls.Add(this.label12, 0, 3);
            this.tlpVariables.Controls.Add(this.label8, 0, 1);
            this.tlpVariables.Controls.Add(this.txtExternalDep, 1, 1);
            this.tlpVariables.Controls.Add(this.label7, 0, 0);
            this.tlpVariables.Controls.Add(this.txtExternalArr, 1, 0);
            this.tlpVariables.Controls.Add(this.label9, 0, 2);
            this.tlpVariables.Controls.Add(this.txtExternalApp, 1, 2);
            this.tlpVariables.Enabled = false;
            this.tlpVariables.Location = new System.Drawing.Point(21, 67);
            this.tlpVariables.Name = "tlpVariables";
            this.tlpVariables.RowCount = 4;
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVariables.Size = new System.Drawing.Size(669, 130);
            this.tlpVariables.TabIndex = 2;
            // 
            // txtExternalRemarks
            // 
            this.txtExternalRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalRemarks.Location = new System.Drawing.Point(148, 101);
            this.txtExternalRemarks.Name = "txtExternalRemarks";
            this.txtExternalRemarks.Size = new System.Drawing.Size(516, 23);
            this.txtExternalRemarks.TabIndex = 7;
            this.txtExternalRemarks.TextChanged += new System.EventHandler(this.txtExternalRemarks_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(5, 98);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 30);
            this.label12.TabIndex = 6;
            this.label12.Text = "Remarks:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.compositeTooltip.SetToolTip(this.label12, "Variable: $remarks");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(5, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 30);
            this.label8.TabIndex = 2;
            this.label8.Text = "Departure Runways:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.compositeTooltip.SetToolTip(this.label8, "Variable: $deprwy");
            // 
            // txtExternalDep
            // 
            this.txtExternalDep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalDep.Location = new System.Drawing.Point(148, 37);
            this.txtExternalDep.Name = "txtExternalDep";
            this.txtExternalDep.Size = new System.Drawing.Size(516, 23);
            this.txtExternalDep.TabIndex = 3;
            this.txtExternalDep.TextChanged += new System.EventHandler(this.txtExternalDep_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(5, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Arrival Runways:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.compositeTooltip.SetToolTip(this.label7, "Variable: $arrrwy");
            // 
            // txtExternalArr
            // 
            this.txtExternalArr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalArr.Location = new System.Drawing.Point(148, 5);
            this.txtExternalArr.Name = "txtExternalArr";
            this.txtExternalArr.Size = new System.Drawing.Size(516, 23);
            this.txtExternalArr.TabIndex = 1;
            this.txtExternalArr.TextChanged += new System.EventHandler(this.txtExternalArr_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(5, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 30);
            this.label9.TabIndex = 4;
            this.label9.Text = "Approaches in Use:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.compositeTooltip.SetToolTip(this.label9, "Variable: $app");
            // 
            // txtExternalApp
            // 
            this.txtExternalApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalApp.Location = new System.Drawing.Point(148, 69);
            this.txtExternalApp.Name = "txtExternalApp";
            this.txtExternalApp.Size = new System.Drawing.Size(516, 23);
            this.txtExternalApp.TabIndex = 5;
            this.txtExternalApp.TextChanged += new System.EventHandler(this.txtExternalApp_TextChanged);
            // 
            // txtExternalUrl
            // 
            this.txtExternalUrl.Enabled = false;
            this.txtExternalUrl.Location = new System.Drawing.Point(70, 37);
            this.txtExternalUrl.Name = "txtExternalUrl";
            this.txtExternalUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExternalUrl.Size = new System.Drawing.Size(620, 23);
            this.txtExternalUrl.TabIndex = 1;
            this.txtExternalUrl.TextChanged += new System.EventHandler(this.txtExternalUrl_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "URL:";
            // 
            // pageTransitionLevel
            // 
            this.pageTransitionLevel.Controls.Add(this.tableLayoutPanel5);
            this.pageTransitionLevel.Location = new System.Drawing.Point(4, 24);
            this.pageTransitionLevel.Name = "pageTransitionLevel";
            this.pageTransitionLevel.Padding = new System.Windows.Forms.Padding(12);
            this.pageTransitionLevel.Size = new System.Drawing.Size(710, 362);
            this.pageTransitionLevel.TabIndex = 3;
            this.pageTransitionLevel.Text = "Transition Level";
            this.pageTransitionLevel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.gridTransitionLevels, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 99.99999F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(686, 338);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // gridTransitionLevels
            // 
            this.gridTransitionLevels.AllowUserToResizeColumns = false;
            this.gridTransitionLevels.AllowUserToResizeRows = false;
            this.gridTransitionLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTransitionLevels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridTransitionLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTransitionLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.low,
            this.high,
            this.transitionLevel});
            this.gridTransitionLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTransitionLevels.Location = new System.Drawing.Point(4, 3);
            this.gridTransitionLevels.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridTransitionLevels.Name = "gridTransitionLevels";
            this.gridTransitionLevels.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridTransitionLevels.RowHeadersVisible = false;
            this.gridTransitionLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTransitionLevels.Size = new System.Drawing.Size(678, 297);
            this.gridTransitionLevels.TabIndex = 2;
            this.gridTransitionLevels.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTransitionLevels_CellEndEdit);
            this.gridTransitionLevels.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridTransitionLevels_CellValidating);
            this.gridTransitionLevels.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTransitionLevels_CellValueChanged);
            this.gridTransitionLevels.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridTransitionLevels_EditingControlShowing);
            // 
            // low
            // 
            this.low.HeaderText = "QNH Low";
            this.low.Name = "low";
            // 
            // high
            // 
            this.high.HeaderText = "QNH High";
            this.high.Name = "high";
            // 
            // transitionLevel
            // 
            this.transitionLevel.HeaderText = "Transition Level";
            this.transitionLevel.Name = "transitionLevel";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.btnDeleteTransitionLevel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 303);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel3.Size = new System.Drawing.Size(686, 35);
            this.panel3.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(244, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "Both Low and High QNH values are required.\r\n";
            // 
            // btnDeleteTransitionLevel
            // 
            this.btnDeleteTransitionLevel.Location = new System.Drawing.Point(562, 4);
            this.btnDeleteTransitionLevel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeleteTransitionLevel.Name = "btnDeleteTransitionLevel";
            this.btnDeleteTransitionLevel.Size = new System.Drawing.Size(120, 27);
            this.btnDeleteTransitionLevel.TabIndex = 1;
            this.btnDeleteTransitionLevel.Text = "Delete Selected";
            this.btnDeleteTransitionLevel.UseVisualStyleBackColor = true;
            this.btnDeleteTransitionLevel.Click += new System.EventHandler(this.btnDeleteTransitionLevel_Click);
            // 
            // btnManageComposite
            // 
            this.btnManageComposite.AutoSize = true;
            this.btnManageComposite.ContextMenuStrip = this.ctxOptions;
            this.btnManageComposite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnManageComposite.Location = new System.Drawing.Point(4, 399);
            this.btnManageComposite.Margin = new System.Windows.Forms.Padding(4, 3, 3, 3);
            this.btnManageComposite.Name = "btnManageComposite";
            this.btnManageComposite.Size = new System.Drawing.Size(201, 29);
            this.btnManageComposite.TabIndex = 2;
            this.btnManageComposite.Text = "Manage Composite...";
            this.btnManageComposite.UseVisualStyleBackColor = true;
            this.btnManageComposite.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnManageComposite_MouseClick);
            // 
            // ctxOptions
            // 
            this.ctxOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNew,
            this.ctxCopy,
            this.ctxRename,
            this.ctxDelete,
            this.toolStripSeparator1,
            this.ctxImport,
            this.ctxExport});
            this.ctxOptions.Name = "ctxOptions";
            this.ctxOptions.Size = new System.Drawing.Size(120, 142);
            // 
            // ctxNew
            // 
            this.ctxNew.Name = "ctxNew";
            this.ctxNew.Size = new System.Drawing.Size(119, 22);
            this.ctxNew.Text = "New";
            this.ctxNew.Click += new System.EventHandler(this.ctxNew_Click);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Enabled = false;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(119, 22);
            this.ctxCopy.Text = "Copy";
            this.ctxCopy.Click += new System.EventHandler(this.ctxCopy_Click);
            // 
            // ctxRename
            // 
            this.ctxRename.Enabled = false;
            this.ctxRename.Name = "ctxRename";
            this.ctxRename.Size = new System.Drawing.Size(119, 22);
            this.ctxRename.Text = "Rename";
            this.ctxRename.Click += new System.EventHandler(this.ctxRename_Click);
            // 
            // ctxDelete
            // 
            this.ctxDelete.Enabled = false;
            this.ctxDelete.Name = "ctxDelete";
            this.ctxDelete.Size = new System.Drawing.Size(119, 22);
            this.ctxDelete.Text = "Delete";
            this.ctxDelete.Click += new System.EventHandler(this.ctxDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(116, 6);
            // 
            // ctxImport
            // 
            this.ctxImport.Name = "ctxImport";
            this.ctxImport.Size = new System.Drawing.Size(119, 22);
            this.ctxImport.Text = "Import...";
            this.ctxImport.Click += new System.EventHandler(this.ctxImport_Click);
            // 
            // ctxExport
            // 
            this.ctxExport.Enabled = false;
            this.ctxExport.Name = "ctxExport";
            this.ctxExport.Size = new System.Drawing.Size(119, 22);
            this.ctxExport.Text = "Export...";
            this.ctxExport.Click += new System.EventHandler(this.ctxExport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panelCompositeList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 390);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Composites";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCompositeList
            // 
            this.panelCompositeList.BackColor = System.Drawing.Color.White;
            this.panelCompositeList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panelCompositeList.Controls.Add(this.TreeMenu);
            this.panelCompositeList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCompositeList.Location = new System.Drawing.Point(0, 24);
            this.panelCompositeList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelCompositeList.Name = "panelCompositeList";
            this.panelCompositeList.Padding = new System.Windows.Forms.Padding(6);
            this.panelCompositeList.ShowBorder = true;
            this.panelCompositeList.Size = new System.Drawing.Size(202, 366);
            this.panelCompositeList.TabIndex = 2;
            // 
            // TreeMenu
            // 
            this.TreeMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeMenu.ContextMenuStrip = this.ctxOptions;
            this.TreeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeMenu.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.TreeMenu.FullRowSelect = true;
            this.TreeMenu.HideSelection = false;
            this.TreeMenu.Indent = 5;
            this.TreeMenu.Location = new System.Drawing.Point(6, 6);
            this.TreeMenu.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TreeMenu.Name = "TreeMenu";
            this.TreeMenu.ShowLines = false;
            this.TreeMenu.Size = new System.Drawing.Size(190, 354);
            this.TreeMenu.TabIndex = 0;
            this.TreeMenu.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TreeMenu_DrawNode);
            this.TreeMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMenu_AfterSelect);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnApply);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(208, 396);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(723, 35);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(632, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 29);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(556, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 29);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(480, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 29);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // compositeTooltip
            // 
            this.compositeTooltip.AutomaticDelay = 250;
            this.compositeTooltip.AutoPopDelay = 10000;
            this.compositeTooltip.InitialDelay = 250;
            this.compositeTooltip.ReshowDelay = 50;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 3);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "Delete Selected Contraction";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(192, 290);
            this.dataGridView1.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "String";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Spoken";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // ProfileConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 461);
            this.ControlBox = false;
            this.Controls.Add(this.TlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(954, 456);
            this.Name = "ProfileConfigurationForm";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Profile Configuration";
            this.TlpMain.ResumeLayout(false);
            this.TlpMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.pageConfiguration.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.pageGeneral.ResumeLayout(false);
            this.pageGeneral.PerformLayout();
            this.groupVoiceOption.ResumeLayout(false);
            this.groupVoiceOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.observationTime)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.magneticVar)).EndInit();
            this.pageFormat.ResumeLayout(false);
            this.pageFormat.PerformLayout();
            this.pagePresets.ResumeLayout(false);
            this.pagePresets.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pageContractions.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridContractions)).EndInit();
            this.pageExternalAtis.ResumeLayout(false);
            this.pageExternalAtis.PerformLayout();
            this.groupTest.ResumeLayout(false);
            this.groupTest.PerformLayout();
            this.tlpVariables.ResumeLayout(false);
            this.tlpVariables.PerformLayout();
            this.pageTransitionLevel.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTransitionLevels)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ctxOptions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelCompositeList.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TlpMain;
        private System.Windows.Forms.TreeView TreeMenu;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private HitTestPanel panelCompositeList;
        private System.Windows.Forms.Button btnManageComposite;
        private System.Windows.Forms.ContextMenuStrip ctxOptions;
        private System.Windows.Forms.ToolStripMenuItem ctxNew;
        private System.Windows.Forms.ToolStripMenuItem ctxRename;
        private System.Windows.Forms.ToolStripMenuItem ctxDelete;
        private System.Windows.Forms.ToolStripMenuItem ctxCopy;
        private System.Windows.Forms.ToolTip compositeTooltip;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ctxImport;
        private System.Windows.Forms.ToolStripMenuItem ctxExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage pagePresets;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtAtisTemplate;
        private System.Windows.Forms.Button btnRenamePreset;
        private System.Windows.Forms.Button btnDeletePreset;
        private System.Windows.Forms.Button btnCopyPreset;
        private System.Windows.Forms.Button btnNewPreset;
        private System.Windows.Forms.ComboBox ddlPresets;
        private System.Windows.Forms.TabPage pageContractions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnDeleteContraction;
        private System.Windows.Forms.DataGridView gridContractions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFind;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReplace;
        private System.Windows.Forms.TabPage pageConfiguration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown observationTime;
        private System.Windows.Forms.CheckBox chkObservationTime;
        private System.Windows.Forms.RadioButton radioVoiceRecorded;
        private System.Windows.Forms.RadioButton radioTextToSpeech;
        private System.Windows.Forms.ComboBox ddlVoices;
        private System.Windows.Forms.TextBox txtIdsEndpoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkMagneticVar;
        private System.Windows.Forms.NumericUpDown magneticVar;
        private System.Windows.Forms.TabPage pageTransitionLevel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnDeleteTransitionLevel;
        private System.Windows.Forms.DataGridView gridTransitionLevels;
        private System.Windows.Forms.DataGridViewTextBoxColumn low;
        private System.Windows.Forms.DataGridViewTextBoxColumn high;
        private System.Windows.Forms.DataGridViewTextBoxColumn transitionLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox vhfFrequency;
        private System.Windows.Forms.RadioButton typeCombined;
        private System.Windows.Forms.RadioButton typeDeparture;
        private System.Windows.Forms.RadioButton typeArrival;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox chkFaaFormat;
        private System.Windows.Forms.CheckBox chkExternalAtisGenerator;
        private System.Windows.Forms.TabPage pageExternalAtis;
        private System.Windows.Forms.TableLayoutPanel tlpVariables;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtExternalDep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtExternalArr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtExternalApp;
        private System.Windows.Forms.TextBox txtExternalUrl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMetar;
        private System.Windows.Forms.GroupBox groupTest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtSelectedPreset;
        private System.Windows.Forms.Button btnFetchMetar;
        private System.Windows.Forms.TextBox txtExternalRemarks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageFormat;
        private System.Windows.Forms.TabPage pageGeneral;
        private System.Windows.Forms.CheckBox chkTransitionLevelPrefix;
        private System.Windows.Forms.CheckBox chkPrefixNotams;
        private System.Windows.Forms.CheckBox chkConvertMetric;
        private System.Windows.Forms.CheckBox chkSurfaceWindPrefix;
        private System.Windows.Forms.CheckBox chkVisibilitySuffix;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel groupVoiceOption;
        private System.Windows.Forms.CheckBox chkDecimalTerminology;
    }
}