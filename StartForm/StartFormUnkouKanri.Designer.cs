namespace StartForm {
    partial class StartFormUnkouKanri {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartFormUnkouKanri));
            RsTableLayoutPanelBase = new RisControls.RsTableLayoutPanel();
            RsMenuStrip1 = new RisControls.RsMenuStrip();
            RsStatusStrip1 = new RisControls.RsStatusStrip();
            RsPanel1 = new RisControls.RsPanel();
            RsGroupBoxServer = new RisControls.RsGroupBox();
            RsLabelDbStatus = new RisControls.RsLabel();
            RsLabelDbName = new RisControls.RsLabel();
            RsLabelServerIpAddress = new RisControls.RsLabel();
            RsLabelServerName = new RisControls.RsLabel();
            rsLabel7 = new RisControls.RsLabel();
            rsLabel4 = new RisControls.RsLabel();
            rsLabel5 = new RisControls.RsLabel();
            rsLabel6 = new RisControls.RsLabel();
            RsGroupBoxClient = new RisControls.RsGroupBox();
            RsComboBoxMonitor1 = new RisControls.RsComboBoxMonitor();
            rsLabel8 = new RisControls.RsLabel();
            RsLabelDefaultGateway = new RisControls.RsLabel();
            RsLabelClientIpAddress = new RisControls.RsLabel();
            RsLabelClientName = new RisControls.RsLabel();
            rsLabel3 = new RisControls.RsLabel();
            rsLabel2 = new RisControls.RsLabel();
            rsLabel1 = new RisControls.RsLabel();
            RsTabControl1 = new RisControls.RsTabControl();
            TabPage1 = new TabPage();
            RsTableLayoutPanel1 = new RisControls.RsTableLayoutPanel();
            RsLabelVehicleDispatchBoardAdachi = new RisControls.RsLabel();
            TabPage2 = new TabPage();
            RsTableLayoutPanel2 = new RisControls.RsTableLayoutPanel();
            RsLabelVehicleDispatchBoardMisato = new RisControls.RsLabel();
            RsTableLayoutPanelBase.SuspendLayout();
            RsPanel1.SuspendLayout();
            RsGroupBoxServer.SuspendLayout();
            RsGroupBoxClient.SuspendLayout();
            RsTabControl1.SuspendLayout();
            TabPage1.SuspendLayout();
            RsTableLayoutPanel1.SuspendLayout();
            TabPage2.SuspendLayout();
            RsTableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // RsTableLayoutPanelBase
            // 
            RsTableLayoutPanelBase.ColumnCount = 1;
            RsTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            RsTableLayoutPanelBase.Controls.Add(RsMenuStrip1, 0, 0);
            RsTableLayoutPanelBase.Controls.Add(RsStatusStrip1, 0, 3);
            RsTableLayoutPanelBase.Controls.Add(RsPanel1, 0, 1);
            RsTableLayoutPanelBase.Controls.Add(RsTabControl1, 0, 2);
            RsTableLayoutPanelBase.Dock = DockStyle.Fill;
            RsTableLayoutPanelBase.Location = new Point(0, 0);
            RsTableLayoutPanelBase.Name = "RsTableLayoutPanelBase";
            RsTableLayoutPanelBase.RowCount = 4;
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 142F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.Size = new Size(784, 561);
            RsTableLayoutPanelBase.TabIndex = 0;
            // 
            // RsMenuStrip1
            // 
            RsMenuStrip1.Location = new Point(0, 0);
            RsMenuStrip1.Name = "RsMenuStrip1";
            RsMenuStrip1.Size = new Size(784, 24);
            RsMenuStrip1.TabIndex = 0;
            RsMenuStrip1.Text = "rsMenuStrip1";
            // 
            // RsStatusStrip1
            // 
            RsStatusStrip1.Location = new Point(0, 539);
            RsStatusStrip1.Name = "RsStatusStrip1";
            RsStatusStrip1.Size = new Size(784, 22);
            RsStatusStrip1.SizingGrip = false;
            RsStatusStrip1.TabIndex = 1;
            RsStatusStrip1.Text = "rsStatusStrip1";
            // 
            // RsPanel1
            // 
            RsPanel1.Controls.Add(RsGroupBoxServer);
            RsPanel1.Controls.Add(RsGroupBoxClient);
            RsPanel1.Dock = DockStyle.Fill;
            RsPanel1.Location = new Point(3, 27);
            RsPanel1.Name = "RsPanel1";
            RsPanel1.Size = new Size(778, 136);
            RsPanel1.TabIndex = 2;
            // 
            // RsGroupBoxServer
            // 
            RsGroupBoxServer.Controls.Add(RsLabelDbStatus);
            RsGroupBoxServer.Controls.Add(RsLabelDbName);
            RsGroupBoxServer.Controls.Add(RsLabelServerIpAddress);
            RsGroupBoxServer.Controls.Add(RsLabelServerName);
            RsGroupBoxServer.Controls.Add(rsLabel7);
            RsGroupBoxServer.Controls.Add(rsLabel4);
            RsGroupBoxServer.Controls.Add(rsLabel5);
            RsGroupBoxServer.Controls.Add(rsLabel6);
            RsGroupBoxServer.Location = new Point(396, 4);
            RsGroupBoxServer.Name = "RsGroupBoxServer";
            RsGroupBoxServer.Size = new Size(372, 124);
            RsGroupBoxServer.TabIndex = 1;
            RsGroupBoxServer.TabStop = false;
            RsGroupBoxServer.Text = "Server Status";
            // 
            // RsLabelDbStatus
            // 
            RsLabelDbStatus.Location = new Point(116, 100);
            RsLabelDbStatus.Name = "RsLabelDbStatus";
            RsLabelDbStatus.Size = new Size(244, 16);
            RsLabelDbStatus.TabIndex = 9;
            RsLabelDbStatus.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelDbStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RsLabelDbName
            // 
            RsLabelDbName.Location = new Point(116, 76);
            RsLabelDbName.Name = "RsLabelDbName";
            RsLabelDbName.Size = new Size(244, 16);
            RsLabelDbName.TabIndex = 8;
            RsLabelDbName.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelDbName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RsLabelServerIpAddress
            // 
            RsLabelServerIpAddress.Location = new Point(116, 52);
            RsLabelServerIpAddress.Name = "RsLabelServerIpAddress";
            RsLabelServerIpAddress.Size = new Size(244, 16);
            RsLabelServerIpAddress.TabIndex = 7;
            RsLabelServerIpAddress.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelServerIpAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RsLabelServerName
            // 
            RsLabelServerName.Location = new Point(116, 28);
            RsLabelServerName.Name = "RsLabelServerName";
            RsLabelServerName.Size = new Size(244, 16);
            RsLabelServerName.TabIndex = 6;
            RsLabelServerName.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelServerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rsLabel7
            // 
            rsLabel7.Location = new Point(12, 100);
            rsLabel7.Name = "rsLabel7";
            rsLabel7.Size = new Size(96, 16);
            rsLabel7.TabIndex = 6;
            rsLabel7.Text = "DB Status";
            rsLabel7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rsLabel4
            // 
            rsLabel4.Location = new Point(12, 76);
            rsLabel4.Name = "rsLabel4";
            rsLabel4.Size = new Size(96, 16);
            rsLabel4.TabIndex = 5;
            rsLabel4.Text = "DB Name";
            rsLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rsLabel5
            // 
            rsLabel5.Location = new Point(12, 52);
            rsLabel5.Name = "rsLabel5";
            rsLabel5.Size = new Size(96, 16);
            rsLabel5.TabIndex = 4;
            rsLabel5.Text = "IP Address";
            rsLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rsLabel6
            // 
            rsLabel6.Location = new Point(12, 28);
            rsLabel6.Name = "rsLabel6";
            rsLabel6.Size = new Size(96, 16);
            rsLabel6.TabIndex = 3;
            rsLabel6.Text = "Server Name";
            rsLabel6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RsGroupBoxClient
            // 
            RsGroupBoxClient.Controls.Add(RsComboBoxMonitor1);
            RsGroupBoxClient.Controls.Add(rsLabel8);
            RsGroupBoxClient.Controls.Add(RsLabelDefaultGateway);
            RsGroupBoxClient.Controls.Add(RsLabelClientIpAddress);
            RsGroupBoxClient.Controls.Add(RsLabelClientName);
            RsGroupBoxClient.Controls.Add(rsLabel3);
            RsGroupBoxClient.Controls.Add(rsLabel2);
            RsGroupBoxClient.Controls.Add(rsLabel1);
            RsGroupBoxClient.Location = new Point(12, 4);
            RsGroupBoxClient.Name = "RsGroupBoxClient";
            RsGroupBoxClient.Size = new Size(372, 124);
            RsGroupBoxClient.TabIndex = 0;
            RsGroupBoxClient.TabStop = false;
            RsGroupBoxClient.Text = "Client Status";
            // 
            // RsComboBoxMonitor1
            // 
            RsComboBoxMonitor1.DisplayMember = "DisplayName";
            RsComboBoxMonitor1.DropDownStyle = ComboBoxStyle.DropDownList;
            RsComboBoxMonitor1.FormattingEnabled = true;
            RsComboBoxMonitor1.Location = new Point(112, 96);
            RsComboBoxMonitor1.Name = "RsComboBoxMonitor1";
            RsComboBoxMonitor1.Size = new Size(248, 23);
            RsComboBoxMonitor1.TabIndex = 7;
            RsComboBoxMonitor1.ValueMember = "Screen";
            // 
            // rsLabel8
            // 
            rsLabel8.Location = new Point(12, 100);
            rsLabel8.Name = "rsLabel8";
            rsLabel8.Size = new Size(96, 16);
            rsLabel8.TabIndex = 6;
            rsLabel8.Text = "Monitor";
            rsLabel8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RsLabelDefaultGateway
            // 
            RsLabelDefaultGateway.Location = new Point(116, 76);
            RsLabelDefaultGateway.Name = "RsLabelDefaultGateway";
            RsLabelDefaultGateway.Size = new Size(244, 16);
            RsLabelDefaultGateway.TabIndex = 5;
            RsLabelDefaultGateway.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelDefaultGateway.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RsLabelClientIpAddress
            // 
            RsLabelClientIpAddress.Location = new Point(116, 52);
            RsLabelClientIpAddress.Name = "RsLabelClientIpAddress";
            RsLabelClientIpAddress.Size = new Size(244, 16);
            RsLabelClientIpAddress.TabIndex = 4;
            RsLabelClientIpAddress.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelClientIpAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RsLabelClientName
            // 
            RsLabelClientName.Location = new Point(116, 28);
            RsLabelClientName.Name = "RsLabelClientName";
            RsLabelClientName.Size = new Size(244, 16);
            RsLabelClientName.TabIndex = 3;
            RsLabelClientName.Text = "ーーーーーーーーーーーーーーーーーーーー";
            RsLabelClientName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rsLabel3
            // 
            rsLabel3.Location = new Point(12, 76);
            rsLabel3.Name = "rsLabel3";
            rsLabel3.Size = new Size(96, 16);
            rsLabel3.TabIndex = 2;
            rsLabel3.Text = "Default Gateway";
            rsLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rsLabel2
            // 
            rsLabel2.Location = new Point(12, 52);
            rsLabel2.Name = "rsLabel2";
            rsLabel2.Size = new Size(96, 16);
            rsLabel2.TabIndex = 1;
            rsLabel2.Text = "IP Address";
            rsLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rsLabel1
            // 
            rsLabel1.Location = new Point(12, 28);
            rsLabel1.Name = "rsLabel1";
            rsLabel1.Size = new Size(96, 16);
            rsLabel1.TabIndex = 0;
            rsLabel1.Text = "Client Name";
            rsLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RsTabControl1
            // 
            RsTabControl1.Controls.Add(TabPage1);
            RsTabControl1.Controls.Add(TabPage2);
            RsTabControl1.Dock = DockStyle.Fill;
            RsTabControl1.Location = new Point(3, 169);
            RsTabControl1.Name = "RsTabControl1";
            RsTabControl1.SelectedIndex = 0;
            RsTabControl1.Size = new Size(778, 365);
            RsTabControl1.SizeMode = TabSizeMode.Fixed;
            RsTabControl1.TabIndex = 3;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(RsTableLayoutPanel1);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(770, 337);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "本社営業所";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // RsTableLayoutPanel1
            // 
            RsTableLayoutPanel1.ColumnCount = 4;
            RsTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            RsTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            RsTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            RsTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            RsTableLayoutPanel1.Controls.Add(RsLabelVehicleDispatchBoardAdachi, 1, 0);
            RsTableLayoutPanel1.Dock = DockStyle.Fill;
            RsTableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            RsTableLayoutPanel1.Location = new Point(3, 3);
            RsTableLayoutPanel1.Name = "RsTableLayoutPanel1";
            RsTableLayoutPanel1.RowCount = 6;
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel1.Size = new Size(764, 331);
            RsTableLayoutPanel1.TabIndex = 0;
            // 
            // RsLabelVehicleDispatchBoardAdachi
            // 
            RsLabelVehicleDispatchBoardAdachi.AutoSize = true;
            RsLabelVehicleDispatchBoardAdachi.Dock = DockStyle.Fill;
            RsLabelVehicleDispatchBoardAdachi.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelVehicleDispatchBoardAdachi.Location = new Point(65, 5);
            RsLabelVehicleDispatchBoardAdachi.Margin = new Padding(5);
            RsLabelVehicleDispatchBoardAdachi.Name = "RsLabelVehicleDispatchBoardAdachi";
            RsLabelVehicleDispatchBoardAdachi.Padding = new Padding(3);
            RsLabelVehicleDispatchBoardAdachi.Size = new Size(312, 45);
            RsLabelVehicleDispatchBoardAdachi.TabIndex = 0;
            RsLabelVehicleDispatchBoardAdachi.Text = "配車システム\r\n　ドラッグ＆ドロップによるUI配車システム";
            RsLabelVehicleDispatchBoardAdachi.Click += RsLabel_Click;
            RsLabelVehicleDispatchBoardAdachi.MouseEnter += RsLabel_MouseEnter;
            RsLabelVehicleDispatchBoardAdachi.MouseLeave += RsLabel_MouseLeave;
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(RsTableLayoutPanel2);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(770, 337);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "三郷車庫";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // RsTableLayoutPanel2
            // 
            RsTableLayoutPanel2.ColumnCount = 4;
            RsTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            RsTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            RsTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            RsTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            RsTableLayoutPanel2.Controls.Add(RsLabelVehicleDispatchBoardMisato, 1, 0);
            RsTableLayoutPanel2.Dock = DockStyle.Fill;
            RsTableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            RsTableLayoutPanel2.Location = new Point(3, 3);
            RsTableLayoutPanel2.Name = "RsTableLayoutPanel2";
            RsTableLayoutPanel2.RowCount = 6;
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            RsTableLayoutPanel2.Size = new Size(764, 331);
            RsTableLayoutPanel2.TabIndex = 1;
            // 
            // RsLabelVehicleDispatchBoardMisato
            // 
            RsLabelVehicleDispatchBoardMisato.AutoSize = true;
            RsLabelVehicleDispatchBoardMisato.Dock = DockStyle.Fill;
            RsLabelVehicleDispatchBoardMisato.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelVehicleDispatchBoardMisato.Location = new Point(65, 5);
            RsLabelVehicleDispatchBoardMisato.Margin = new Padding(5);
            RsLabelVehicleDispatchBoardMisato.Name = "RsLabelVehicleDispatchBoardMisato";
            RsLabelVehicleDispatchBoardMisato.Padding = new Padding(3);
            RsLabelVehicleDispatchBoardMisato.Size = new Size(312, 45);
            RsLabelVehicleDispatchBoardMisato.TabIndex = 1;
            RsLabelVehicleDispatchBoardMisato.Text = "配車システム(三郷車庫)\r\n　ドラッグ＆ドロップによるUI配車システム";
            RsLabelVehicleDispatchBoardMisato.Click += RsLabel_Click;
            RsLabelVehicleDispatchBoardMisato.MouseEnter += RsLabel_MouseEnter;
            RsLabelVehicleDispatchBoardMisato.MouseLeave += RsLabel_MouseLeave;
            // 
            // StartFormUnkouKanri
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(RsTableLayoutPanelBase);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = RsMenuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartFormUnkouKanri";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StartFormUnkouKanri";
            FormClosed += StartFormUnkouKanri_FormClosed;
            Load += StartFormUnkouKanri_LoadAsync;
            RsTableLayoutPanelBase.ResumeLayout(false);
            RsTableLayoutPanelBase.PerformLayout();
            RsPanel1.ResumeLayout(false);
            RsGroupBoxServer.ResumeLayout(false);
            RsGroupBoxClient.ResumeLayout(false);
            RsTabControl1.ResumeLayout(false);
            TabPage1.ResumeLayout(false);
            RsTableLayoutPanel1.ResumeLayout(false);
            RsTableLayoutPanel1.PerformLayout();
            TabPage2.ResumeLayout(false);
            RsTableLayoutPanel2.ResumeLayout(false);
            RsTableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RisControls.RsTableLayoutPanel RsTableLayoutPanelBase;
        private RisControls.RsMenuStrip RsMenuStrip1;
        private RisControls.RsStatusStrip RsStatusStrip1;
        private RisControls.RsPanel RsPanel1;
        private RisControls.RsTabControl RsTabControl1;
        private TabPage TabPage1;
        private TabPage TabPage2;
        private RisControls.RsGroupBox RsGroupBoxServer;
        private RisControls.RsGroupBox RsGroupBoxClient;
        private RisControls.RsLabel rsLabel4;
        private RisControls.RsLabel rsLabel5;
        private RisControls.RsLabel rsLabel6;
        private RisControls.RsLabel rsLabel3;
        private RisControls.RsLabel rsLabel2;
        private RisControls.RsLabel rsLabel1;
        private RisControls.RsLabel rsLabel7;
        private RisControls.RsLabel RsLabelClientName;
        private RisControls.RsLabel RsLabelDbStatus;
        private RisControls.RsLabel RsLabelDbName;
        private RisControls.RsLabel RsLabelServerIpAddress;
        private RisControls.RsLabel RsLabelServerName;
        private RisControls.RsLabel RsLabelDefaultGateway;
        private RisControls.RsLabel RsLabelClientIpAddress;
        private RisControls.RsTableLayoutPanel RsTableLayoutPanel1;
        private RisControls.RsTableLayoutPanel RsTableLayoutPanel2;
        private RisControls.RsLabel RsLabelVehicleDispatchBoardAdachi;
        private RisControls.RsLabel RsLabelVehicleDispatchBoardMisato;
        private RisControls.RsLabel rsLabel8;
        private RisControls.RsComboBoxMonitor RsComboBoxMonitor1;
    }
}