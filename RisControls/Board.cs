/*
 * 2026-10-16
 */
using Entity;

namespace RisControls {
    public partial class Board : TableLayoutPanel {
        ///----------------------------------------------------------------------------------
        //
        // delegate
        //
        //----------------------------------------------------------------------------------
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
        /*
         * スクロール周りの変数
         */
        private Point _oldMousePoint;
        private Point _oldAutoScrollPosition;

        /// <summary>
        /// 
        /// </summary>
        public Board() {
            /* 
             * ダブルバッファリングを有効にする
             */
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * InitializeControls
             */
            AllowDrop = true;
            AutoScroll = true;
            AutoSize = true;
            Dock = DockStyle.Fill;
            Margin = new Padding(16, 8, 16, 8);
            Name = "Board";
            Padding = new Padding(0);
            /*
             * Column追加(SetControlを配置出来るColumn数)
             */
            ColumnCount = _columnNumber;
            for(int i = 0; i < _columnNumber; i++)
                ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _columnWidth));
            /*
             * Row追加(SetControlを配置出来るRow数)
             */
            RowCount = _rowNumber;
            for(int i = 0; i < _rowNumber; i++) {
                RowStyles.Add(new RowStyle(SizeType.Absolute, _rowHeight));
            }
        }

        /// <summary>
        /// SetControlをBoardに追加する(VehicleDispatchHead/VehicleDispatchBody)
        /// </summary>
        /// <param name="cellNumber">0～159</param>
        /// <param name="vehicleDispatchFlag">true:配車されている false:配車されていない</param>
        /// <param name="purposeFlag">SetControlの形状 true:２列 false:１列</param>
        /// <param name="numberOfPeople">配車される基本人数</param>
        /// <param name="setMaster"></param>
        /// <param name="carMaster"></param>
        /// <param name="listStaffMaster"></param>
        public void AddSetControl(int cellNumber,
                                  bool vehicleDispatchFlag,
                                  bool purposeFlag,
                                  int numberOfPeople,
                                  SetMaster? setMaster,
                                  CarMaster? carMaster,
                                  List<StaffMaster?> listStaffMaster) {
            /*
             * SetControlを作成してBoardに追加する
             */
            SetControl setControl = new();
            setControl.CreateSetControl(purposeFlag);
            setControl.SetControlIndex = cellNumber;
            setControl.VehicleDispatchFlag = vehicleDispatchFlag;
            setControl.PurposeFlag = purposeFlag;
            setControl.NumberOfPeople = numberOfPeople;
            /*
             * SetLabelを作成してSetControlに追加する
             */
            if(vehicleDispatchFlag && setMaster is not null)
                setControl.CreateSetLabel(setMaster);
            /*
             * CarLabelを作成してSetControlに追加する
             */
            if(vehicleDispatchFlag && carMaster is not null) {
                setControl.CreateCarLabel(carMaster);
            }
            /*
             * StaffLabelを作成してSetControlに追加する
             */
            if(vehicleDispatchFlag && listStaffMaster is not null) {
                setControl.CreateStaffLabel(listStaffMaster);
            }

            setControl.SetControl_ContextMenuStripOpened += this.ContextMenuStripOpened!;
            setControl.SetControl_ToolStripMenuItemClick += this.ToolStripMenuItemClick!;
            setControl.SetControl_MouseClick += this.OnMouseClick!;
            setControl.SetControl_MouseDoubleClick += this.OnMouseDoubleClick!;
            setControl.SetControl_MouseEnter += this.OnMouseEnter!;
            setControl.SetControl_MouseLeave += this.OnMouseLeave!;
            setControl.SetControl_MouseDown += this.OnMouseDown!;
            setControl.SetControl_MouseMove += this.OnMouseMove!;
            setControl.SetControl_MouseUp += this.OnMouseUp!;
            setControl.SetControl_DragEnter += this.OnDragEnter!;
            setControl.SetControl_DragOver += this.OnDragOver!;
            //setControl.SetControl_DragDrop += this.OnDragDrop!;
            setControl.SetControl_DragDrop += OnSetControlDragDrop;

            Controls.Add(setControl, GetCellPoint(cellNumber).X, GetCellPoint(cellNumber).Y);
            SetColumnSpan(setControl, purposeFlag ? 2 : 1);
        }

        /// <summary>
        /// SetControlをBoardに追加する(VehicleDispatchDetailHead/VehicleDispatchDetailBody)
        /// </summary>
        public void AddSetControl() {

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
            SendMessage(Handle, WM_SETREDRAW, false, 0);

            try {
                for(int i = Controls.Count - 1; 0 <= i; i--)
                    Controls[i].Dispose();
            } finally {
                // 描画再開
                SendMessage(Handle, WM_SETREDRAW, true, 0);
                Invalidate();
                Update();
            }
        }

        ///----------------------------------------------------------------------------------
        //
        // Event
        //
        //----------------------------------------------------------------------------------
        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {
            base.OnCellPaint(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpened(object sender, EventArgs e) {
            Board_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {
            Board_ToolStripMenuItem_Click.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            Board_MouseClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            Board_MouseDoubleClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, EventArgs e) {
            Board_MouseEnter.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            /*
             * SetControl
             * SetLabel
             * CarLabel
             * StaffLabel
             */
            switch(sender) {
                // 画面スクロールの準備
                case SetControl:
                    if(e.Button == MouseButtons.Left) {
                        _oldMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                        _oldAutoScrollPosition = AutoScrollPosition;                                    // スクロールバーで移動している場合、AutoScrollPositionの値が変化するので、MouseDown時のAutoScrollPositionを保持しておく
                        Cursor = Cursors.Hand;
                    }
                    break;
                case SetLabel:
                case CarLabel:
                case StaffLabel:
                    Board_MouseDown.Invoke(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {
            Board_MouseLeave.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {
            /*
             * SetControl
             * SetLabel
             * CarLabel
             * StaffLabel
             */
            switch(sender) {
                case SetControl:
                    if(e.Button == MouseButtons.Left) {
                        Point _newMousePoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                        int x = _oldAutoScrollPosition.X + (_newMousePoint.X - _oldMousePoint.X);
                        int y = _oldAutoScrollPosition.Y + (_newMousePoint.Y - _oldMousePoint.Y);
                        AutoScrollPosition = new Point(-x, -y);
                    }
                    break;
                case SetLabel:
                case CarLabel:
                case StaffLabel:
                    Board_MouseDown.Invoke(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {
            /*
             * SetControl
             * SetLabel
             * CarLabel
             * StaffLabel
             */
            switch(sender) {
                case SetControl:
                    _oldAutoScrollPosition = AutoScrollPosition;
                    Cursor = Cursors.Default;
                    break;
                case SetLabel:
                case CarLabel:
                case StaffLabel:
                    Board_MouseUp.Invoke(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Drag中のLabelが入っている</param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e) {
            Board_DragEnter.Invoke(sender, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender">Drag中のLabelが入っている</param>
        /// <param name="e"></param>
        private void OnDragOver(object sender, DragEventArgs e) {
            Board_DragOver.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">DropされたLabelが入っている</param>
        private void OnDragDrop(object sender, DragEventArgs e) {
            Board_DragDrop.Invoke(sender, e);
        }

        /// <summary>
        /// カスタムイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSetControlDragDrop(object? sender, SetControlDragDropEventArgs e) {
            if(sender is not SetControl targetSetControl)
                return;
            Control source = e.DragControl;
            if(source.Parent is not SetControl sourceSetControl)
                return;

            sourceSetControl.DetachLabelEvents(source);                                 // Eventを削除

            sourceSetControl.Controls.Remove(source);                                   // 
            sourceSetControl.MouseEnterFlag = false;
            sourceSetControl.Invalidate();

            targetSetControl.Controls.Add(source, e.CellPoint.X, e.CellPoint.Y);
            targetSetControl.MouseEnterFlag = false;
            targetSetControl.Invalidate();

            targetSetControl.AttachLabelEvents(source);                                 // Eventを作成
        }
    }
}
