namespace VehicleDispatch {
    partial class VehicleDispatchBoard {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            rsPanel1 = new RisControls.RsPanel();
            RsButtonUpdate = new RisControls.RsButton();
            rsLabel1 = new RisControls.RsLabel();
            RsDateTimeOperationDate = new RisControls.RsDateTime();
            RsMenuStrip1 = new RisControls.RsMenuStrip();
            RsTableLayoutPanelBase = new RisControls.RsTableLayoutPanel();
            RsSplitContainer1 = new RisControls.RsSplitContainer();
            RsStatusStrip1 = new RisControls.RsStatusStrip();
            rsPanel1.SuspendLayout();
            RsTableLayoutPanelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RsSplitContainer1).BeginInit();
            RsSplitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // rsPanel1
            // 
            rsPanel1.Controls.Add(RsButtonUpdate);
            rsPanel1.Controls.Add(rsLabel1);
            rsPanel1.Controls.Add(RsDateTimeOperationDate);
            rsPanel1.Dock = DockStyle.Fill;
            rsPanel1.Location = new Point(5, 29);
            rsPanel1.Name = "rsPanel1";
            rsPanel1.Size = new Size(1894, 32);
            rsPanel1.TabIndex = 2;
            // 
            // RsButtonUpdate
            // 
            RsButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RsButtonUpdate.Location = new Point(1656, 0);
            RsButtonUpdate.Name = "RsButtonUpdate";
            RsButtonUpdate.Size = new Size(180, 32);
            RsButtonUpdate.TabIndex = 2;
            RsButtonUpdate.Text = "最　新　化";
            RsButtonUpdate.UseVisualStyleBackColor = true;
            RsButtonUpdate.Click += RsButton_Click;
            // 
            // rsLabel1
            // 
            rsLabel1.AutoSize = true;
            rsLabel1.Location = new Point(28, 8);
            rsLabel1.Name = "rsLabel1";
            rsLabel1.Size = new Size(43, 15);
            rsLabel1.TabIndex = 1;
            rsLabel1.Text = "配車日";
            // 
            // RsDateTimeOperationDate
            // 
            RsDateTimeOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            RsDateTimeOperationDate.Format = DateTimePickerFormat.Custom;
            RsDateTimeOperationDate.Location = new Point(76, 4);
            RsDateTimeOperationDate.Name = "RsDateTimeOperationDate";
            RsDateTimeOperationDate.Size = new Size(188, 23);
            RsDateTimeOperationDate.TabIndex = 0;
            RsDateTimeOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // RsMenuStrip1
            // 
            RsMenuStrip1.Location = new Point(2, 2);
            RsMenuStrip1.Name = "RsMenuStrip1";
            RsMenuStrip1.Size = new Size(1900, 24);
            RsMenuStrip1.TabIndex = 0;
            RsMenuStrip1.Text = "rsMenuStrip1";
            // 
            // RsTableLayoutPanelBase
            // 
            RsTableLayoutPanelBase.ColumnCount = 1;
            RsTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            RsTableLayoutPanelBase.Controls.Add(RsMenuStrip1, 0, 0);
            RsTableLayoutPanelBase.Controls.Add(rsPanel1, 0, 1);
            RsTableLayoutPanelBase.Controls.Add(RsSplitContainer1, 0, 2);
            RsTableLayoutPanelBase.Dock = DockStyle.Fill;
            RsTableLayoutPanelBase.Location = new Point(0, 0);
            RsTableLayoutPanelBase.Margin = new Padding(2);
            RsTableLayoutPanelBase.Name = "RsTableLayoutPanelBase";
            RsTableLayoutPanelBase.Padding = new Padding(2);
            RsTableLayoutPanelBase.RowCount = 4;
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.Size = new Size(1904, 1041);
            RsTableLayoutPanelBase.TabIndex = 0;
            // 
            // RsSplitContainer1
            // 
            RsSplitContainer1.BorderStyle = BorderStyle.Fixed3D;
            RsSplitContainer1.Dock = DockStyle.Fill;
            RsSplitContainer1.Location = new Point(4, 66);
            RsSplitContainer1.Margin = new Padding(2);
            RsSplitContainer1.Name = "RsSplitContainer1";
            RsSplitContainer1.Size = new Size(1896, 947);
            RsSplitContainer1.SplitterDistance = 1493;
            RsSplitContainer1.TabIndex = 3;
            // 
            // RsStatusStrip1
            // 
            RsStatusStrip1.Location = new Point(0, 1019);
            RsStatusStrip1.Name = "RsStatusStrip1";
            RsStatusStrip1.Size = new Size(1904, 22);
            RsStatusStrip1.SizingGrip = false;
            RsStatusStrip1.TabIndex = 1;
            RsStatusStrip1.Text = "rsStatusStrip1";
            // 
            // VehicleDispatchBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(RsTableLayoutPanelBase);
            MainMenuStrip = RsMenuStrip1;
            Name = "VehicleDispatchBoard";
            Text = "VehicleDispatchBoard";
            FormClosing += VehicleDispatchBoard_FormClosing;
            Load += VehicleDispatchBoard_Load;
            rsPanel1.ResumeLayout(false);
            rsPanel1.PerformLayout();
            RsTableLayoutPanelBase.ResumeLayout(false);
            RsTableLayoutPanelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RsSplitContainer1).EndInit();
            RsSplitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RisControls.RsPanel rsPanel1;
        private RisControls.RsButton RsButtonUpdate;
        private RisControls.RsLabel rsLabel1;
        private RisControls.RsDateTime RsDateTimeOperationDate;
        private RisControls.RsMenuStrip RsMenuStrip1;
        private RisControls.RsTableLayoutPanel RsTableLayoutPanelBase;
        private RisControls.RsStatusStrip RsStatusStrip1;
        private RisControls.RsSplitContainer RsSplitContainer1;
    }
}
