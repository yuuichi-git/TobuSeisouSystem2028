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
            RsTableLayoutPanelBase = new RisControls.RsTableLayoutPanel();
            RsMenuStrip1 = new RisControls.RsMenuStrip();
            RsStatusStrip1 = new RisControls.RsStatusStrip();
            rsPanel1 = new RisControls.RsPanel();
            RsButtonUpdate = new RisControls.RsButton();
            rsLabel1 = new RisControls.RsLabel();
            rsDateTime1 = new RisControls.RsDateTime();
            RsTableLayoutPanelBase.SuspendLayout();
            rsPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // RsTableLayoutPanelBase
            // 
            RsTableLayoutPanelBase.ColumnCount = 2;
            RsTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.5189056F));
            RsTableLayoutPanelBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4810925F));
            RsTableLayoutPanelBase.Controls.Add(RsMenuStrip1, 0, 0);
            RsTableLayoutPanelBase.Controls.Add(RsStatusStrip1, 0, 3);
            RsTableLayoutPanelBase.Controls.Add(rsPanel1, 0, 1);
            RsTableLayoutPanelBase.Dock = DockStyle.Fill;
            RsTableLayoutPanelBase.Location = new Point(0, 0);
            RsTableLayoutPanelBase.Name = "RsTableLayoutPanelBase";
            RsTableLayoutPanelBase.RowCount = 4;
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            RsTableLayoutPanelBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            RsTableLayoutPanelBase.Size = new Size(1904, 1041);
            RsTableLayoutPanelBase.TabIndex = 0;
            // 
            // RsMenuStrip1
            // 
            RsTableLayoutPanelBase.SetColumnSpan(RsMenuStrip1, 2);
            RsMenuStrip1.Location = new Point(0, 0);
            RsMenuStrip1.Name = "RsMenuStrip1";
            RsMenuStrip1.Size = new Size(1904, 24);
            RsMenuStrip1.TabIndex = 0;
            RsMenuStrip1.Text = "rsMenuStrip1";
            // 
            // RsStatusStrip1
            // 
            RsTableLayoutPanelBase.SetColumnSpan(RsStatusStrip1, 2);
            RsStatusStrip1.Location = new Point(0, 1019);
            RsStatusStrip1.Name = "RsStatusStrip1";
            RsStatusStrip1.Size = new Size(1904, 22);
            RsStatusStrip1.SizingGrip = false;
            RsStatusStrip1.TabIndex = 1;
            RsStatusStrip1.Text = "rsStatusStrip1";
            // 
            // rsPanel1
            // 
            RsTableLayoutPanelBase.SetColumnSpan(rsPanel1, 2);
            rsPanel1.Controls.Add(RsButtonUpdate);
            rsPanel1.Controls.Add(rsLabel1);
            rsPanel1.Controls.Add(rsDateTime1);
            rsPanel1.Dock = DockStyle.Fill;
            rsPanel1.Location = new Point(3, 27);
            rsPanel1.Name = "rsPanel1";
            rsPanel1.Size = new Size(1898, 32);
            rsPanel1.TabIndex = 2;
            // 
            // RsButtonUpdate
            // 
            RsButtonUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RsButtonUpdate.Location = new Point(1660, 0);
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
            // rsDateTime1
            // 
            rsDateTime1.CustomFormat = " 明治33年01月01日(月曜日)";
            rsDateTime1.Format = DateTimePickerFormat.Custom;
            rsDateTime1.Location = new Point(76, 4);
            rsDateTime1.Name = "rsDateTime1";
            rsDateTime1.Size = new Size(188, 23);
            rsDateTime1.TabIndex = 0;
            rsDateTime1.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
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
            RsTableLayoutPanelBase.ResumeLayout(false);
            RsTableLayoutPanelBase.PerformLayout();
            rsPanel1.ResumeLayout(false);
            rsPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RisControls.RsTableLayoutPanel RsTableLayoutPanelBase;
        private RisControls.RsMenuStrip RsMenuStrip1;
        private RisControls.RsStatusStrip RsStatusStrip1;
        private RisControls.RsPanel rsPanel1;
        private RisControls.RsLabel rsLabel1;
        private RisControls.RsDateTime rsDateTime1;
        private RisControls.RsButton RsButtonUpdate;
    }
}
