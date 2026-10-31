namespace TobuSeisouSystem {
    partial class Login {
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
            RsLabelLogin = new RisControls.RsLabel();
            RsTextBoxId = new RisControls.RsTextBox();
            RsTextBoxPassword = new RisControls.RsTextBox();
            RsLabelId = new RisControls.RsLabel();
            RsLabelPassword = new RisControls.RsLabel();
            SuspendLayout();
            // 
            // RsLabelLogin
            // 
            RsLabelLogin.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            RsLabelLogin.Location = new Point(8, 92);
            RsLabelLogin.Name = "RsLabelLogin";
            RsLabelLogin.Size = new Size(268, 25);
            RsLabelLogin.TabIndex = 0;
            RsLabelLogin.Text = "ログイン";
            RsLabelLogin.TextAlign = ContentAlignment.MiddleCenter;
            RsLabelLogin.Click += RsLabel_Click;
            RsLabelLogin.MouseEnter += RsLabel_MouseEnter;
            RsLabelLogin.MouseLeave += RsLabel_MouseLeave;
            // 
            // RsTextBoxId
            // 
            RsTextBoxId.Location = new Point(68, 20);
            RsTextBoxId.Name = "RsTextBoxId";
            RsTextBoxId.Size = new Size(208, 23);
            RsTextBoxId.TabIndex = 1;
            // 
            // RsTextBoxPassword
            // 
            RsTextBoxPassword.Location = new Point(68, 52);
            RsTextBoxPassword.Name = "RsTextBoxPassword";
            RsTextBoxPassword.PasswordChar = '*';
            RsTextBoxPassword.Size = new Size(208, 23);
            RsTextBoxPassword.TabIndex = 2;
            // 
            // RsLabelId
            // 
            RsLabelId.Location = new Point(8, 24);
            RsLabelId.Name = "RsLabelId";
            RsLabelId.Size = new Size(53, 15);
            RsLabelId.TabIndex = 3;
            RsLabelId.Text = "ログインID";
            // 
            // RsLabelPassword
            // 
            RsLabelPassword.Location = new Point(8, 56);
            RsLabelPassword.Name = "RsLabelPassword";
            RsLabelPassword.Size = new Size(55, 15);
            RsLabelPassword.TabIndex = 4;
            RsLabelPassword.Text = "パスワード";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 139);
            Controls.Add(RsLabelPassword);
            Controls.Add(RsLabelId);
            Controls.Add(RsTextBoxPassword);
            Controls.Add(RsTextBoxId);
            Controls.Add(RsLabelLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            TopMost = true;
            FormClosing += Login_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RisControls.RsLabel RsLabelLogin;
        private RisControls.RsTextBox RsTextBoxId;
        private RisControls.RsTextBox RsTextBoxPassword;
        private RisControls.RsLabel RsLabelId;
        private RisControls.RsLabel RsLabelPassword;
    }
}
