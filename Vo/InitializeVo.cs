/*
 * 2026-10-15
 * ログイン情報を保持するためのVOクラス
 */
namespace Vo {
    public class InitializeVo {
        private int _managedSpaceCode = 0;
        private Screen? _screen;

        /// <summary>
        /// 管理対象スペースの識別コードを取得または設定します。
        /// </summary>
        public int ManagedSpaceCode {
            get => _managedSpaceCode;
            set => _managedSpaceCode = value;
        }

        /// <summary>
        /// モニター情報を取得または設定します。
        /// </summary>
        public Screen? Screen {
            get => _screen;
            set => _screen = value;
        }
    }
}
