namespace TobuSeisouSystem {
    partial class LoginAdmin {
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
            RsLabelPassword = new RisControls.RsLabel();
            RsLabelId = new RisControls.RsLabel();
            RsTextBoxPassword = new RisControls.RsTextBox();
            RsTextBoxId = new RisControls.RsTextBox();
            RsLabelLoginNew = new RisControls.RsLabel();
            RsLabelLoginEdit = new RisControls.RsLabel();
            RsLabelLogin = new RisControls.RsLabel();
            RsCheckBoxRemember = new RisControls.RsCheckBox();
            SuspendLayout();
            // 
            // RsLabelPassword
            // 
            RsLabelPassword.AutoSize = true;
            RsLabelPassword.Location = new Point(96, 48);
            RsLabelPassword.Name = "RsLabelPassword";
            RsLabelPassword.Size = new Size(51, 15);
            RsLabelPassword.TabIndex = 9;
            RsLabelPassword.Text = "パスワード";
            // 
            // RsLabelId
            // 
            RsLabelId.AutoSize = true;
            RsLabelId.Location = new Point(12, 16);
            RsLabelId.Name = "RsLabelId";
            RsLabelId.Size = new Size(136, 15);
            RsLabelId.TabIndex = 8;
            RsLabelId.Text = "ログインID又はメールアドレス";
            // 
            // RsTextBoxPassword
            // 
            RsTextBoxPassword.Location = new Point(152, 44);
            RsTextBoxPassword.Name = "RsTextBoxPassword";
            RsTextBoxPassword.Size = new Size(252, 23);
            RsTextBoxPassword.TabIndex = 7;
            RsTextBoxPassword.UseSystemPasswordChar = true;
            // 
            // RsTextBoxId
            // 
            RsTextBoxId.Location = new Point(152, 12);
            RsTextBoxId.Name = "RsTextBoxId";
            RsTextBoxId.Size = new Size(252, 23);
            RsTextBoxId.TabIndex = 6;
            // 
            // RsLabelLoginNew
            // 
            RsLabelLoginNew.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLoginNew.Location = new Point(8, 156);
            RsLabelLoginNew.Name = "RsLabelLoginNew";
            RsLabelLoginNew.Size = new Size(420, 25);
            RsLabelLoginNew.TabIndex = 5;
            RsLabelLoginNew.Text = "新規ユーザー登録へ";
            RsLabelLoginNew.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLoginNew.Click += RsLabel_Click;
            RsLabelLoginNew.MouseEnter += RsLabel_MouseEnter;
            RsLabelLoginNew.MouseLeave += RsLabel_MouseLeave;
            // 
            // RsLabelLoginEdit
            // 
            RsLabelLoginEdit.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLoginEdit.Location = new Point(8, 192);
            RsLabelLoginEdit.Name = "RsLabelLoginEdit";
            RsLabelLoginEdit.Size = new Size(420, 25);
            RsLabelLoginEdit.TabIndex = 12;
            RsLabelLoginEdit.Text = "登録情報編集へ";
            RsLabelLoginEdit.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLoginEdit.Click += RsLabel_Click;
            RsLabelLoginEdit.MouseEnter += RsLabel_MouseEnter;
            RsLabelLoginEdit.MouseLeave += RsLabel_MouseLeave;
            // 
            // RsLabelLogin
            // 
            RsLabelLogin.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLogin.Location = new Point(8, 120);
            RsLabelLogin.Name = "RsLabelLogin";
            RsLabelLogin.Size = new Size(420, 25);
            RsLabelLogin.TabIndex = 14;
            RsLabelLogin.Text = "ログイン";
            RsLabelLogin.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLogin.Click += RsLabel_Click;
            RsLabelLogin.MouseEnter += RsLabel_MouseEnter;
            RsLabelLogin.MouseLeave += RsLabel_MouseLeave;
            // 
            // RsCheckBoxRemember
            // 
            RsCheckBoxRemember.AutoSize = true;
            RsCheckBoxRemember.Location = new Point(152, 76);
            RsCheckBoxRemember.Name = "RsCheckBoxRemember";
            RsCheckBoxRemember.Size = new Size(157, 19);
            RsCheckBoxRemember.TabIndex = 15;
            RsCheckBoxRemember.Text = "ID と Password を記憶する";
            RsCheckBoxRemember.UseVisualStyleBackColor = true;
            // 
            // LoginAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 311);
            Controls.Add(RsCheckBoxRemember);
            Controls.Add(RsLabelLogin);
            Controls.Add(RsLabelLoginEdit);
            Controls.Add(RsLabelPassword);
            Controls.Add(RsLabelId);
            Controls.Add(RsTextBoxPassword);
            Controls.Add(RsTextBoxId);
            Controls.Add(RsLabelLoginNew);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginAdmin";
            FormClosing += LoginAdmin_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RisControls.RsLabel RsLabelPassword;
        private RisControls.RsLabel RsLabelId;
        private RisControls.RsTextBox RsTextBoxPassword;
        private RisControls.RsTextBox RsTextBoxId;
        private RisControls.RsLabel RsLabelLoginNew;
        private RisControls.RsLabel RsLabelLoginEdit;
        private RisControls.RsLabel RsLabelLogin;
        private RisControls.RsCheckBox RsCheckBoxRemember;
    }
}