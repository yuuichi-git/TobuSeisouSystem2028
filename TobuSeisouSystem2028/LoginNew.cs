/*
 * 2026-10-09
 */
using Dao;

using Entity;

using Microsoft.EntityFrameworkCore;

using RisControls;

namespace TobuSeisouSystem {
    public partial class LoginNew : Form {
        /// <summary>
        /// 親フォームの参照
        /// </summary>
        private Form _parentForm;

        public LoginNew(Form parentForm) {
            _parentForm = parentForm;

            InitializeComponent();

            RsLabelPasswordStrength.Text = string.Empty;    // パスワード強度表示ラベル初期化
            RsComboBoxRole.ValueMember = "Code";            // 内部的に扱う値
            RsComboBoxRole.DisplayMember = "Name";          // 画面に表示する文字列
            this.Load += LoginAdmin_Load;                   // ロードイベント登録
        }

        /// <summary>
        /// ログイン画面ロード時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginAdmin_Load(object? sender, EventArgs e) {
            using AppDbContext? db = new();
            List<RoleMaster> listRoleMaster = await db.RoleMaster.Where(x => !x.DeleteFlag).OrderBy(x => x.Code).ToListAsync();
            RsComboBoxRole.Items.Clear();
            foreach(var roleMaster in listRoleMaster) {
                RsComboBoxRole.Items.Add(roleMaster);
            }
            if(RsComboBoxRole.Items.Count > 0) {
                RsComboBoxRole.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RsLabel_Click(object sender, EventArgs e) {
            string userId = RsTextBoxId.Text.Trim();
            string password = RsTextBoxPassword.Text.Trim();
            string passwordConfirm = RsTextBoxPasswordConfirm.Text.Trim();
            string displayName = RsTextBoxDisplayName.Text.Trim();
            int roleCode = ((RoleMaster)RsComboBoxRole.SelectedItem).Code;

            switch(((RsLabel)sender).Name) {
                case "RsLabelLoginNew":
                    try {
                        if(password != passwordConfirm) {
                            MessageBox.Show("パスワードと確認用パスワードが一致しません。", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(displayName)) {
                            MessageBox.Show("必須項目が入力されていません。", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // ★ パスワード強度チェック
                        if(!LoginService.IsStrongPassword(password, out string msg)) {
                            MessageBox.Show(msg, "パスワード強度不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        await LoginService.RegisterUserAsync(userId, password, displayName, roleCode);
                        MessageBox.Show("ユーザー登録が完了しました。", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    } catch(Exception ex) {
                        MessageBox.Show("登録に失敗しました。\n" + ex.ToString(), "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void RsLabel_MouseEnter(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = Color.Red;
        }

        private void RsLabel_MouseLeave(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = SystemColors.ControlText;
        }

        /// <summary>
        /// パスワード入力欄のテキストが変更されたときに呼び出されるイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsTextBoxPassword_TextChanged(object sender, EventArgs e) {
            string password = RsTextBoxPassword.Text.Trim();
            string strength = LoginService.GetPasswordStrength(password);

            RsLabelPasswordStrength.Text = $"{strength}";

            // 色分け（視覚的にわかりやすく）
            switch(strength) {
                case "弱い":
                    RsLabelPasswordStrength.ForeColor = Color.Red;
                    break;
                case "普通":
                    RsLabelPasswordStrength.ForeColor = Color.Orange;
                    break;
                case "強い":
                    RsLabelPasswordStrength.ForeColor = Color.Green;
                    break;
                default:
                    RsLabelPasswordStrength.ForeColor = SystemColors.ControlText;
                    break;
            }
        }

        /// <summary>
        /// フォームが閉じられるときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginAdmin_FormClosing(object sender, FormClosingEventArgs e) {
            _parentForm.Show();
        }
    }
}
