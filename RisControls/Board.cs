/*
 * 2026-10-16
 */
namespace RisControls {
    public partial class Board : TableLayoutPanel {
        /*
         * デリゲート
         */
        public event EventHandler Board_ContextMenuStrip_Opened = delegate { };
        public event EventHandler Board_ToolStripMenuItem_Click = delegate { };
        public event EventHandler Board_MouseEnter = delegate { };
        public event EventHandler Board_MouseLeave = delegate { };
        public event MouseEventHandler Board_MouseClick = delegate { };
        public event MouseEventHandler Board_MouseDoubleClick = delegate { };
        public event MouseEventHandler Board_MouseDown = delegate { };
        public event MouseEventHandler Board_MouseMove = delegate { };
        public event MouseEventHandler Board_MouseUp = delegate { };
        public event DragEventHandler Board_DragDrop = delegate { };
        public event DragEventHandler Board_DragEnter = delegate { };
        public event DragEventHandler Board_DragOver = delegate { };
        /*
         * Labelのサイズ
         */
        private const int _cellDummyHeight = 20;
        private const int _cellWidth = 74;
        private const int _cellHeight = 118;
        /*
         * BoardのCell数
         */
        private const int _columnNumber = 40;                       // SetControlを配置出来るColumn数
        private const int _rowNumber = 4;                           // SetControlを配置出来るRow数
        /*
         * SetControlのサイズ
         */
        private const int _columnWidth = _cellWidth;
        private const int _rowHeight = _cellDummyHeight + (_cellHeight * 4);

        /// <summary>
        /// 
        /// </summary>
        public Board() {
            /* 
             * ダブルバッファリングを有効にする
             */
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * InitializeControls
             */
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(16, 8, 16, 8);
            this.Name = "Board";
            this.Padding = new Padding(0);
            /*
             * Column追加(SetControlを配置出来るColumn数)
             */
            this.ColumnCount = _columnNumber;
            for(int i = 0; i < _columnNumber; i++)
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _columnWidth));
            /*
             * Row追加(SetControlを配置出来るRow数)
             */
            this.RowCount = _rowNumber;
            for(int i = 0; i < _rowNumber; i++) {
                this.RowStyles.Add(new RowStyle(SizeType.Absolute, _rowHeight));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber">0～159</param>
        /// <returns></returns>
        public bool AddSetControl(int cellNumber, bool vehicleDispatchFlag = false, bool purposeFlag = false) {
            SetControl setControl = new();
            setControl.SetControlIndex = cellNumber;
            setControl.VehicleDispatchFlag = vehicleDispatchFlag;
            setControl.PurposeFlag = purposeFlag;
            setControl.Controls.Add(new SetLabel(new Entity.SetMaster()), 0, 1);
            setControl.Controls.Add(new CarLabel(new Entity.CarMaster()), 0, 2);


            this.Controls.Add(setControl, GetCellPoint(cellNumber).X, GetCellPoint(cellNumber).Y);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber">0～159</param>
        /// <returns></returns>
        private Point GetCellPoint(int cellNumber) {
            return new Point(cellNumber % _columnNumber, cellNumber / _columnNumber);
        }

        /*
         * WM_SETREDRAW で描画そのものを止める
         * 配置されている全てのControlを解放する
         * メソッドをClear呼び出してもコントロール ハンドルはメモリから削除されません。 メモリリークを回避するにはメソッドをDispose明示的に呼び出す必要があります。
         * ※後ろから解放している点が重要らしい。
         */
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);

        private const int WM_SETREDRAW = 0x000B;

        public void RemoveAllControls() {
            // 描画停止
            SendMessage(this.Handle, WM_SETREDRAW, false, 0);

            try {
                for(int i = this.Controls.Count - 1; 0 <= i; i--)
                    this.Controls[i].Dispose();
            } finally {
                // 描画再開
                SendMessage(this.Handle, WM_SETREDRAW, true, 0);
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {
            base.OnCellPaint(e);
        }
    }
}
