/*
 * 2026-10-15
 */
namespace RisControls {
    public partial class RsComboBoxMonitor : ComboBox {
        public RsComboBoxMonitor() {
            /*
             * CcComboBoxMonitors
             */
            this.Items.Clear();
            List<ComboBoxItems> listComboBoxItems = new();
            foreach(Screen screen in GetAllScreen()) {
                listComboBoxItems.Add(new ComboBoxItems(string.Concat(screen.DeviceName, "　{ ", screen.Bounds.Width, "×", screen.Bounds.Height, " }"), screen));
            }
            // ComboBoxにデータをバインド
            this.DataSource = listComboBoxItems;
            this.DisplayMember = "DisplayName";                                           // 表示名を設定
            this.ValueMember = "Screen";                                                  // 値を設定
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 現在のシステムに接続されているすべてのスクリーンを取得
        /// </summary>
        /// <returns></returns>
        private List<Screen> GetAllScreen() {
            List<Screen> listScreen = new();
            foreach(Screen screen in Screen.AllScreens)
                listScreen.Add(screen);
            return listScreen;
        }

        /// <summary>
        /// 内部クラス
        /// </summary>
        private class ComboBoxItems {
            string _displayName;
            Screen _screen;
            public ComboBoxItems(string displayName, Screen screen) {
                _displayName = displayName;
                _screen = screen;
            }
            public string DisplayName {
                get => this._displayName;
                set => this._displayName = value;
            }
            public Screen Screen {
                get => this._screen;
                set => this._screen = value;
            }
        }
    }
}
