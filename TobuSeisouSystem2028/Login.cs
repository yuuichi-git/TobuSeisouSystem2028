using Dao;

using Entity;

using RisControls;

using StartForm;

namespace TobuSeisouSystem {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        private async void RsLabel_Click(object sender, EventArgs e) {
            string id = RsTextBoxId.Text.Trim();
            string pw = RsTextBoxPassword.Text.Trim();

            switch(((RsLabel)sender).Name) {
                case "RsLabelLogin":
                    LoginUser? loginUser = await LoginService.AuthenticateAsync(id, pw);

                    if(loginUser is null) {
                        MessageBox.Show("ユーザーIDまたはパスワードが違います。", "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 認証成功
                    this.Hide();
                    StartFormUnkouKanri startFormUnkouKanri = new(this);

                    startFormUnkouKanri.Show();
                    break;
            }
        }

        private void RsLabel_MouseEnter(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = Color.Red;
        }

        private void RsLabel_MouseLeave(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = SystemColors.ControlText;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
