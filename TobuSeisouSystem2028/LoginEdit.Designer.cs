namespace TobuSeisouSystem {
    partial class LoginEdit {
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
            RsComboBoxRole = new RisControls.RsComboBox();
            rsLabel2 = new RisControls.RsLabel();
            RsLabelPasswordConfirm = new RisControls.RsLabel();
            RsTextBoxPasswordConfirm = new RisControls.RsTextBox();
            RsLabelLoginDelete = new RisControls.RsLabel();
            RsLabelLoginEdit = new RisControls.RsLabel();
            rsLabel1 = new RisControls.RsLabel();
            RsTextBoxDisplayName = new RisControls.RsTextBox();
            RsLabelPassword = new RisControls.RsLabel();
            RsLabelId = new RisControls.RsLabel();
            RsTextBoxPassword = new RisControls.RsTextBox();
            RsTextBoxId = new RisControls.RsTextBox();
            RsToolTip1 = new RisControls.RsToolTip();
            rsLabel3 = new RisControls.RsLabel();
            RsLabelPasswordStrength = new RisControls.RsLabel();
            SuspendLayout();
            // 
            // RsComboBoxRole
            // 
            RsComboBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;
            RsComboBoxRole.FormattingEnabled = true;
            RsComboBoxRole.Items.AddRange(new object[] { "ユーザー", "管理者" });
            RsComboBoxRole.Location = new Point(151, 164);
            RsComboBoxRole.Name = "RsComboBoxRole";
            RsComboBoxRole.Size = new Size(252, 23);
            RsComboBoxRole.TabIndex = 32;
            // 
            // rsLabel2
            // 
            rsLabel2.AutoSize = true;
            rsLabel2.Location = new Point(111, 168);
            rsLabel2.Name = "rsLabel2";
            rsLabel2.Size = new Size(34, 15);
            rsLabel2.TabIndex = 31;
            rsLabel2.Text = "ロール";
            // 
            // RsLabelPasswordConfirm
            // 
            RsLabelPasswordConfirm.AutoSize = true;
            RsLabelPasswordConfirm.Location = new Point(59, 104);
            RsLabelPasswordConfirm.Name = "RsLabelPasswordConfirm";
            RsLabelPasswordConfirm.Size = new Size(87, 15);
            RsLabelPasswordConfirm.TabIndex = 30;
            RsLabelPasswordConfirm.Text = "確認用パスワード";
            // 
            // RsTextBoxPasswordConfirm
            // 
            RsTextBoxPasswordConfirm.Location = new Point(151, 100);
            RsTextBoxPasswordConfirm.Name = "RsTextBoxPasswordConfirm";
            RsTextBoxPasswordConfirm.Size = new Size(252, 23);
            RsTextBoxPasswordConfirm.TabIndex = 29;
            RsTextBoxPasswordConfirm.UseSystemPasswordChar = true;
            // 
            // RsLabelLoginDelete
            // 
            RsLabelLoginDelete.Enabled = false;
            RsLabelLoginDelete.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLoginDelete.Location = new Point(8, 240);
            RsLabelLoginDelete.Name = "RsLabelLoginDelete";
            RsLabelLoginDelete.Size = new Size(420, 25);
            RsLabelLoginDelete.TabIndex = 27;
            RsLabelLoginDelete.Text = "登録情報削除";
            RsLabelLoginDelete.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLoginDelete.Click += RsLabel_Click;
            RsLabelLoginDelete.MouseEnter += RsLabel_MouseEnter;
            RsLabelLoginDelete.MouseLeave += RsLabel_MouseLeave;
            // 
            // RsLabelLoginEdit
            // 
            RsLabelLoginEdit.Enabled = false;
            RsLabelLoginEdit.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLoginEdit.Location = new Point(8, 204);
            RsLabelLoginEdit.Name = "RsLabelLoginEdit";
            RsLabelLoginEdit.Size = new Size(420, 25);
            RsLabelLoginEdit.TabIndex = 26;
            RsLabelLoginEdit.Text = "登録情報編集";
            RsLabelLoginEdit.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLoginEdit.Click += RsLabel_Click;
            RsLabelLoginEdit.MouseEnter += RsLabel_MouseEnter;
            RsLabelLoginEdit.MouseLeave += RsLabel_MouseLeave;
            // 
            // rsLabel1
            // 
            rsLabel1.AutoSize = true;
            rsLabel1.Location = new Point(103, 136);
            rsLabel1.Name = "rsLabel1";
            rsLabel1.Size = new Size(43, 15);
            rsLabel1.TabIndex = 25;
            rsLabel1.Text = "表示名";
            // 
            // RsTextBoxDisplayName
            // 
            RsTextBoxDisplayName.Location = new Point(151, 132);
            RsTextBoxDisplayName.Name = "RsTextBoxDisplayName";
            RsTextBoxDisplayName.Size = new Size(252, 23);
            RsTextBoxDisplayName.TabIndex = 24;
            // 
            // RsLabelPassword
            // 
            RsLabelPassword.AutoSize = true;
            RsLabelPassword.Location = new Point(95, 48);
            RsLabelPassword.Name = "RsLabelPassword";
            RsLabelPassword.Size = new Size(51, 15);
            RsLabelPassword.TabIndex = 23;
            RsLabelPassword.Text = "パスワード";
            // 
            // RsLabelId
            // 
            RsLabelId.AutoSize = true;
            RsLabelId.Location = new Point(11, 16);
            RsLabelId.Name = "RsLabelId";
            RsLabelId.Size = new Size(136, 15);
            RsLabelId.TabIndex = 22;
            RsLabelId.Text = "ログインID又はメールアドレス";
            // 
            // RsTextBoxPassword
            // 
            RsTextBoxPassword.Location = new Point(151, 44);
            RsTextBoxPassword.Name = "RsTextBoxPassword";
            RsTextBoxPassword.Size = new Size(252, 23);
            RsTextBoxPassword.TabIndex = 21;
            RsToolTip1.SetToolTip(RsTextBoxPassword, "パスワードを空にした場合はパスワードは変更されません\r\nパスワードは８文字以上・数字を含める・記号を含める");
            RsTextBoxPassword.UseSystemPasswordChar = true;
            RsTextBoxPassword.TextChanged += RsTextBoxPassword_TextChanged;
            // 
            // RsTextBoxId
            // 
            RsTextBoxId.Location = new Point(151, 12);
            RsTextBoxId.Name = "RsTextBoxId";
            RsTextBoxId.Size = new Size(252, 23);
            RsTextBoxId.TabIndex = 20;
            RsTextBoxId.KeyDown += RsTextBoxId_KeyDown;
            // 
            // rsLabel3
            // 
            rsLabel3.AutoSize = true;
            rsLabel3.Location = new Point(116, 76);
            rsLabel3.Name = "rsLabel3";
            rsLabel3.Size = new Size(31, 15);
            rsLabel3.TabIndex = 36;
            rsLabel3.Text = "強度";
            // 
            // RsLabelPasswordStrength
            // 
            RsLabelPasswordStrength.AutoSize = true;
            RsLabelPasswordStrength.Location = new Point(152, 76);
            RsLabelPasswordStrength.Name = "RsLabelPasswordStrength";
            RsLabelPasswordStrength.Size = new Size(75, 15);
            RsLabelPasswordStrength.TabIndex = 35;
            RsLabelPasswordStrength.Text = "パスワード強度";
            // 
            // LoginEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 311);
            Controls.Add(rsLabel3);
            Controls.Add(RsLabelPasswordStrength);
            Controls.Add(RsComboBoxRole);
            Controls.Add(rsLabel2);
            Controls.Add(RsLabelPasswordConfirm);
            Controls.Add(RsTextBoxPasswordConfirm);
            Controls.Add(RsLabelLoginDelete);
            Controls.Add(RsLabelLoginEdit);
            Controls.Add(rsLabel1);
            Controls.Add(RsTextBoxDisplayName);
            Controls.Add(RsLabelPassword);
            Controls.Add(RsLabelId);
            Controls.Add(RsTextBoxPassword);
            Controls.Add(RsTextBoxId);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginEdit";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginEdit";
            FormClosing += LoginAdmin_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RisControls.RsComboBox RsComboBoxRole;
        private RisControls.RsLabel rsLabel2;
        private RisControls.RsLabel RsLabelPasswordConfirm;
        private RisControls.RsTextBox RsTextBoxPasswordConfirm;
        private RisControls.RsLabel RsLabelLoginDelete;
        private RisControls.RsLabel RsLabelLoginEdit;
        private RisControls.RsLabel rsLabel1;
        private RisControls.RsTextBox RsTextBoxDisplayName;
        private RisControls.RsLabel RsLabelPassword;
        private RisControls.RsLabel RsLabelId;
        private RisControls.RsTextBox RsTextBoxPassword;
        private RisControls.RsTextBox RsTextBoxId;
        private RisControls.RsToolTip RsToolTip1;
        private RisControls.RsLabel rsLabel3;
        private RisControls.RsLabel RsLabelPasswordStrength;
    }
}