/*
 * 2026-10-08
 */
using System.Security.Cryptography;
using System.Text;

using Dao;

using Entity;

using RisControls;

using StartForm;

namespace TobuSeisouSystem {
    public partial class LoginAdmin : Form {

        public LoginAdmin() {
            InitializeComponent();

            // ★ フォームロード時に保存済みのID／PWを読み込む
            this.Load += LoginAdmin_Load;
        }

        /// <summary>
        /// フォームロード時：保存されているログイン情報を復元
        /// </summary>
        private void LoginAdmin_Load(object? sender, EventArgs e) {
            /*
             * ID/PWを記憶するチェックボックスの状態を復元
             */
            RsCheckBoxRemember.Checked = Properties.Settings.Default.Remember;

            if(Properties.Settings.Default.Remember) {
                // ID復元
                RsTextBoxId.Text = Properties.Settings.Default.UserId;
                // パスワードが保存されている場合のみ復元
                if(!string.IsNullOrEmpty(Properties.Settings.Default.Password)) {
                    try {
                        RsTextBoxPassword.Text = Decrypt(Properties.Settings.Default.Password);
                    } catch {
                        // 復号に失敗した場合は破損扱いで削除
                        Properties.Settings.Default.Password = "";
                        Properties.Settings.Default.Save();
                    }
                }
            } else {
                // チェックがOFFの場合はID／PWをクリア
                RsCheckBoxRemember.Checked = false;
                RsTextBoxId.Text = "";
                RsTextBoxPassword.Text = "";
            }
        }


        private async void RsLabel_Click(object sender, EventArgs e) {
            string userId = RsTextBoxId.Text.Trim();
            string password = RsTextBoxPassword.Text.Trim();

            switch(((RsLabel)sender).Name) {

                case "RsLabelLogin":
                    LoginUser? loginUser = await LoginService.AuthenticateAsync(userId, password);
                    if(loginUser is null) {
                        MessageBox.Show("ユーザーIDまたはパスワードが違います。", "認証失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ★ ログイン成功 → 記憶するチェックがONなら保存
                    if(RsCheckBoxRemember.Checked) {
                        Properties.Settings.Default.Remember = true;
                        Properties.Settings.Default.UserId = userId;
                        Properties.Settings.Default.Password = Encrypt(password);
                    } else {
                        // チェックがOFFなら削除
                        Properties.Settings.Default.Remember = false;
                        Properties.Settings.Default.UserId = "";
                        Properties.Settings.Default.Password = "";
                    }
                    Properties.Settings.Default.Save();

                    // 認証成功
                    this.Hide();
                    StartFormUnkouKanri startFormUnkouKanri = new(this);
                    startFormUnkouKanri.Show();
                    break;


                case "RsLabelLoginNew":
                    this.Hide();
                    LoginNew loginNew = new(this);
                    loginNew.Show();
                    break;

                case "RsLabelLoginEdit":
                    this.Hide();
                    LoginEdit loginEdit = new(this);
                    loginEdit.Show();
                    break;
            }
        }


        private void RsLabel_MouseEnter(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = Color.Red;
        }

        private void RsLabel_MouseLeave(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = SystemColors.ControlText;
        }

        private void LoginAdmin_FormClosing(object sender, FormClosingEventArgs e) {

        }


        /// <summary>
        /// 暗号化して保存する文字列を取得
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Encrypt(string plainText) {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// 復号化して元の文字列を取得
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private string Decrypt(string cipherText) {
            byte[] data = Convert.FromBase64String(cipherText);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}
