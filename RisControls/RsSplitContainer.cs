/*
 * 2026-11-14
 */
namespace RisControls {
    public partial class RsSplitContainer : SplitContainer {
        public RsSplitContainer() {
            /* 
             * ダブルバッファリングを有効にする
             */
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}
