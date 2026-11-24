/*
 * 2026-10-17
 */
using System.ComponentModel;

using Entity;

namespace RisControls {
    public partial class SetControl : TableLayoutPanel {
        ///----------------------------------------------------------------------------------
        //
        // delegate
        //
        //----------------------------------------------------------------------------------
        public event EventHandler SetControl_ContextMenuStripOpened = delegate { };
        public event EventHandler SetControl_ToolStripMenuItemClick = delegate { };
        public event MouseEventHandler SetControl_MouseClick = delegate { };
        public event MouseEventHandler SetControl_MouseDoubleClick = delegate { };
        public event MouseEventHandler SetControl_MouseDown = delegate { };
        public event EventHandler SetControl_MouseEnter = delegate { };
        public event EventHandler SetControl_MouseLeave = delegate { };
        public event MouseEventHandler SetControl_MouseMove = delegate { };
        public event MouseEventHandler SetControl_MouseUp = delegate { };
        public event DragEventHandler SetControl_DragEnter = delegate { };
        public event DragEventHandler SetControl_DragOver = delegate { };

        //public event DragEventHandler SetControl_DragDrop = delegate { };
        public event EventHandler<SetControlDragDropEventArgs> SetControl_DragDrop = delegate { };
        /*
         * Cellのサイズ
         */
        private const int _cellDummyHeight = 20;
        private const int _cellWidth = 74;
        private const int _cellHeight = 118;
        /*
         * Cellの数
         */
        private const int _columnCount = 1;             // Columnの数
        private const int _rowCount = 5;                // Rowの数(Detail + 4行)
        /*
         * プロパティ
         */
        private int _setControlIndex = 0;               // SetControlのIndex(0～159) 配置されるSetControlのMaxIndexは159まで
        private bool _vehicleDispatchFlag =  false;     // True:配車割当あり False:配車割当なし
        private bool _purposeFlag = false;              // True: Purpose, False:Single
        private int _numberOfPeople = 0;                // 配車割当人数
        private bool _mouseEnterFlag = false;           // マウスがSetControl上にあるかどうかのフラグ

        /// <summary>
        /// コンストラクター
        /// 最低限のプロパティをセットする
        /// </summary>
        public SetControl() {
            /* 
             * ダブルバッファリングを有効にする
             */
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * SetControlのプロパティをセットする
             */
            AllowDrop = true;
            Dock = DockStyle.None;
            Margin = new Padding(0);
            Name = "SetControl";
            Padding = new Padding(0);
            /*
             * ContextMenuStripを作成
             */
            CreateContextMenuStrip();
        }

        /// <summary>
        /// SetControlを作成する
        /// </summary>
        /// /// <param name="purposeFlag">true:2列 false:1列</param>
        public void CreateSetControl(bool purposeFlag = false) {
            PurposeFlag = purposeFlag;
            /*
             * Column作成
             */
            switch(PurposeFlag) {
                case false: // １列
                    Size = new Size(_cellWidth * _columnCount, _cellDummyHeight + (_cellHeight * 4));
                    ColumnCount = _columnCount;
                    ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
                case true: // ２列
                    Size = new Size(_cellWidth * _columnCount * 2, _cellDummyHeight + (_cellHeight * 4));
                    ColumnCount = _columnCount + 1;
                    ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
            }
            /*
             * Row作成
             */
            RowCount = _rowCount;
            RowStyles.Add(new RowStyle(SizeType.Absolute, _cellDummyHeight));  // Detail
            RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 1行目
            RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 2行目
            RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 3行目
            RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 4行目
            /*
             * イベントハンドラを登録する
             * オーバーライドは使わない。SetControlの配下にある各LabelのEventを受け入れるため
             */
            MouseDown += OnMouseDown!;
            //MouseClick += OnMouseClick!;                      // SetControlではMouseClickは使わない。
            //MouseDoubleClick += OnMouseDoubleClick!;          // SetControlではMouseDoubleClickは使わない。
            MouseEnter += OnMouseEnter!;
            MouseLeave += OnMouseLeave!;
            MouseMove += OnMouseMove!;
            MouseUp += OnMouseUp!;

            DragEnter += OnDragEnter!;
            DragOver += OnDragOver!;
            DragDrop += OnDragDrop!;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMaster"></param>
        public void CreateSetLabel(SetMaster setMaster) {
            SetLabel setLabel = new(setMaster);
            setLabel.SetLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
            setLabel.SetLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
            setLabel.SetLabel_MouseDown += OnMouseDown!;
            setLabel.SetLabel_MouseClick += OnMouseClick!;
            setLabel.SetLabel_MouseDoubleClick += OnMouseDoubleClick!;
            setLabel.SetLabel_MouseEnter += OnMouseEnter!;
            setLabel.SetLabel_MouseLeave += OnMouseLeave!;
            setLabel.SetLabel_MouseMove += OnMouseMove!;
            setLabel.SetLabel_MouseUp += OnMouseUp!;
            this.Controls.Add(setLabel, 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carMaster"></param>
        public void CreateCarLabel(CarMaster carMaster) {
            CarLabel carLabel = new(carMaster);
            carLabel.CarLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
            carLabel.CarLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
            carLabel.CarLabel_MouseDown += OnMouseDown!;
            carLabel.CarLabel_MouseClick += OnMouseClick!;
            carLabel.CarLabel_MouseDoubleClick += OnMouseDoubleClick!;
            carLabel.CarLabel_MouseEnter += OnMouseEnter!;
            carLabel.CarLabel_MouseLeave += OnMouseLeave!;
            carLabel.CarLabel_MouseMove += OnMouseMove!;
            carLabel.CarLabel_MouseUp += OnMouseUp!;
            this.Controls.Add(carLabel, 0, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMaster"></param>
        public void CreateStaffLabel(List<StaffMaster?> listStaffMaster) {
            int i = 0;                                                                          // iはStaffLabelのIndex(0～3) 配置されるStaffLabelのMaxIndexは3まで
            foreach(StaffMaster? staffMaster in listStaffMaster) {
                if(staffMaster is not null) {
                    StaffLabel staffLabel = new(staffMaster);
                    staffLabel.StaffOccupationFlag = staffMaster.Occupation == 11;                   // Occupationが作業員の場合、true
                    staffLabel.StaffLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
                    staffLabel.StaffLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
                    staffLabel.StaffLabel_MouseDown += OnMouseDown!;
                    staffLabel.StaffLabel_MouseClick += OnMouseClick!;
                    staffLabel.StaffLabel_MouseDoubleClick += OnMouseDoubleClick!;
                    staffLabel.StaffLabel_MouseEnter += OnMouseEnter!;
                    staffLabel.StaffLabel_MouseLeave += OnMouseLeave!;
                    staffLabel.StaffLabel_MouseMove += OnMouseMove!;
                    staffLabel.StaffLabel_MouseUp += OnMouseUp!;
                    this.Controls.Add(staffLabel, i / 2, (i % 2) + 3);
                }
                if(PurposeFlag == false && !(i < 2))
                    break;
                i++;
            }
        }

        //----------------------------------------------------------------------------------
        //
        // Event
        //
        //----------------------------------------------------------------------------------

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {
            // セルの矩形を取得
            Rectangle rectangle = e.CellBounds;
            // 少し内側に縮める（線が太く見えないように）
            rectangle.Inflate(-1, -1);
            if(VehicleDispatchFlag) {
                switch(e.Column) {
                    case 0: // １列目
                        switch(e.Row) {
                            case 3: // StaffLabel(1人目)
                                if(NumberOfPeople >= 1)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Black, ButtonBorderStyle.Dotted);          // StaffLabel1の枠線
                                break;
                            case 4: // StaffLabel(2人目)
                                if(NumberOfPeople >= 2)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted);           // StaffLabel2の枠線
                                break;
                        }
                        break;
                    case 1: // ２列目
                        switch(e.Row) {
                            case 3: // StaffLabel(3人目)
                                if(NumberOfPeople >= 3)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted);           // StaffLabel3の枠線
                                break;
                            case 4: // StaffLabel(4人目)
                                if(NumberOfPeople >= 4)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted);           // StaffLabel4の枠線
                                break;
                        }
                        break;
                }
            }
            /*
             * H_SetControlの外枠を描画する
             */
            if(MouseEnterFlag) {
                if(PurposeFlag) {
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(1, _cellDummyHeight + 1, (_cellWidth * 2) - 2, (_cellHeight * 4) - 2));
                } else {
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(1, _cellDummyHeight + 1, _cellWidth - 2, (_cellHeight * 4) - 2));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip_Opened(object sender, EventArgs e) {
            SetControl_ContextMenuStripOpened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {
            SetControl_ToolStripMenuItemClick.Invoke(sender, e);
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            SetControl_MouseDown.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            SetControl_MouseClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            SetControl_MouseDoubleClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, EventArgs e) {
            MouseEnterFlag = true;
            Invalidate();                                       // コントロールの再描画を要求する
            SetControl_MouseEnter.Invoke(this, e);              // デリゲートを呼び出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {
            MouseEnterFlag = false;
            Invalidate();                                       // コントロールの再描画を要求する
            SetControl_MouseLeave.Invoke(this, e);              // デリゲートを呼び出す
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {
            SetControl_MouseMove.Invoke(sender, e);
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {
            SetControl_MouseUp.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragEnter(object? sender, DragEventArgs e) {
            Control? dragControl = GetDragControl(e);

            if(e.Data?.GetDataPresent(typeof(SetLabel)) == true || e.Data?.GetDataPresent(typeof(CarLabel)) == true || e.Data?.GetDataPresent(typeof(StaffLabel)) == true) {
                e.Effect = DragDropEffects.Move;
            } else {
                e.Effect = DragDropEffects.None;
            }

            SetControl_DragEnter.Invoke(dragControl, e);
        }

        /// <summary>
        /// ドラッグ アンド ドロップ操作中にマウス カーソルがコントロールの境界内を移動したときに発生します。
        /// Copy  :データがドロップ先にコピーされようとしている状態
        /// Move  :データがドロップ先に移動されようとしている状態
        /// Scroll:データによってドロップ先でスクロールが開始されようとしている状態、あるいは現在スクロール中である状態
        /// All   :上の3つを組み合わせたもの
        /// Link  :データのリンクがドロップ先に作成されようとしている状態
        /// None  :いかなるデータもドロップ先が受け付けようとしない状態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragOver(object? sender, DragEventArgs e) {
            Control? source = GetDragControl(e);

            Point clientPoint = PointToClient(new Point(e.X, e.Y));
            Point cellPoint = GetCellPoint(clientPoint);
            bool isEmpty = IsEmptyCell(cellPoint);
            bool canDrop = source switch{
                SetLabel => cellPoint.X == 0 && cellPoint.Y == 1 && isEmpty,
                CarLabel => cellPoint.X == 0 && cellPoint.Y == 2 && isEmpty,
                StaffLabel => (cellPoint.Y == 3 || cellPoint.Y == 4) && cellPoint.X >= 0 && cellPoint.X < ColumnCount && isEmpty,
                _ => false
            };
            e.Effect = canDrop ? DragDropEffects.Move : DragDropEffects.None;
            /*
             * BoardのDragOverイベントでDragLabelのイメージを表示するためにdragControlを渡す
             */
            SetControl_DragOver.Invoke(source, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragDrop(object? sender, DragEventArgs e) {
            Control? source = GetDragControl(e);
            if(source is null)
                return;
            Point clientPoint = PointToClient(new Point(e.X, e.Y));
            Point cellPoint = GetCellPoint(clientPoint);
            bool isEmpty = IsEmptyCell(cellPoint);
            bool canDrop = source switch{
                SetLabel => cellPoint.X == 0 && cellPoint.Y == 1 && isEmpty,
                CarLabel => cellPoint.X == 0 && cellPoint.Y == 2 && isEmpty,
                StaffLabel => (cellPoint.Y == 3 || cellPoint.Y == 4) && cellPoint.X >= 0 && cellPoint.X < ColumnCount && isEmpty,
                _ => false
            };
            if(!canDrop)
                return;
            SetControl_DragDrop.Invoke(this, new SetControlDragDropEventArgs(source, cellPoint));
        }

        /// <summary>
        /// SetControl上のマウス座標から、SetControlのCell座標を取得する
        /// </summary>
        /// <param name="clientPoint"></param>
        /// <returns></returns>
        private Point GetCellPoint(Point clientPoint) {
            int column = clientPoint.X / _cellWidth;
            int row = 0;
            if(clientPoint.Y >= _cellDummyHeight) {
                row = ((clientPoint.Y - _cellDummyHeight) / _cellHeight) + 1;
                row = Math.Min(row, RowCount - 1);
            }
            return new Point(column, row);
        }

        /// <summary>
        /// cellPointにコントロールが配置されていないかどうかを判定する
        /// true:配置されていない false:配置されている
        /// </summary>
        /// <param name="cellPoint"></param>
        /// <returns></returns>
        private bool IsEmptyCell(Point cellPoint) {
            return GetControlFromPosition(cellPoint.X, cellPoint.Y) is null;
        }

        /// <summary>
        /// DragされているLabelControlを取得する
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Control? GetDragControl(DragEventArgs e) {
            return e.Data?.GetData(typeof(SetLabel)) as Control
                ?? e.Data?.GetData(typeof(CarLabel)) as Control
                ?? e.Data?.GetData(typeof(StaffLabel)) as Control;
        }

        /// <summary>
        /// SetControlを移動した場合のEventの付け替え処理
        /// </summary>
        /// <param name="control">SetLabel CarLabel StaffLabel</param>
        public void AttachLabelEvents(Control control) {
            switch(control) {
                case SetLabel setLabel:
                    setLabel.SetLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
                    setLabel.SetLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
                    setLabel.SetLabel_MouseDown += OnMouseDown!;
                    setLabel.SetLabel_MouseClick += OnMouseClick!;
                    setLabel.SetLabel_MouseDoubleClick += OnMouseDoubleClick!;
                    setLabel.SetLabel_MouseEnter += OnMouseEnter!;
                    setLabel.SetLabel_MouseLeave += OnMouseLeave!;
                    setLabel.SetLabel_MouseMove += OnMouseMove!;
                    setLabel.SetLabel_MouseUp += OnMouseUp!;
                    break;

                case CarLabel carLabel:
                    carLabel.CarLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
                    carLabel.CarLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
                    carLabel.CarLabel_MouseDown += OnMouseDown!;
                    carLabel.CarLabel_MouseClick += OnMouseClick!;
                    carLabel.CarLabel_MouseDoubleClick += OnMouseDoubleClick!;
                    carLabel.CarLabel_MouseEnter += OnMouseEnter!;
                    carLabel.CarLabel_MouseLeave += OnMouseLeave!;
                    carLabel.CarLabel_MouseMove += OnMouseMove!;
                    carLabel.CarLabel_MouseUp += OnMouseUp!;
                    break;

                case StaffLabel staffLabel:
                    staffLabel.StaffLabel_ContextMenuStripOpened += ContextMenuStrip_Opened!;
                    staffLabel.StaffLabel_ToolStripMenuItemClick += ToolStripMenuItemClick!;
                    staffLabel.StaffLabel_MouseDown += OnMouseDown!;
                    staffLabel.StaffLabel_MouseClick += OnMouseClick!;
                    staffLabel.StaffLabel_MouseDoubleClick += OnMouseDoubleClick!;
                    staffLabel.StaffLabel_MouseEnter += OnMouseEnter!;
                    staffLabel.StaffLabel_MouseLeave += OnMouseLeave!;
                    staffLabel.StaffLabel_MouseMove += OnMouseMove!;
                    staffLabel.StaffLabel_MouseUp += OnMouseUp!;
                    break;
            }
        }

        /// <summary>
        /// SetControlを移動した場合のEventの付け替え処理
        /// </summary>
        /// <param name="control">SetLabel CarLabel StaffLabel</param>
        public void DetachLabelEvents(Control control) {
            switch(control) {
                case SetLabel setLabel:
                    setLabel.SetLabel_ContextMenuStripOpened -= ContextMenuStrip_Opened!;
                    setLabel.SetLabel_ToolStripMenuItemClick -= ToolStripMenuItemClick!;
                    setLabel.SetLabel_MouseDown -= OnMouseDown!;
                    setLabel.SetLabel_MouseClick -= OnMouseClick!;
                    setLabel.SetLabel_MouseDoubleClick -= OnMouseDoubleClick!;
                    setLabel.SetLabel_MouseEnter -= OnMouseEnter!;
                    setLabel.SetLabel_MouseLeave -= OnMouseLeave!;
                    setLabel.SetLabel_MouseMove -= OnMouseMove!;
                    setLabel.SetLabel_MouseUp -= OnMouseUp!;
                    break;

                case CarLabel carLabel:
                    carLabel.CarLabel_ContextMenuStripOpened -= ContextMenuStrip_Opened!;
                    carLabel.CarLabel_ToolStripMenuItemClick -= ToolStripMenuItemClick!;
                    carLabel.CarLabel_MouseDown -= OnMouseDown!;
                    carLabel.CarLabel_MouseClick -= OnMouseClick!;
                    carLabel.CarLabel_MouseDoubleClick -= OnMouseDoubleClick!;
                    carLabel.CarLabel_MouseEnter -= OnMouseEnter!;
                    carLabel.CarLabel_MouseLeave -= OnMouseLeave!;
                    carLabel.CarLabel_MouseMove -= OnMouseMove!;
                    carLabel.CarLabel_MouseUp -= OnMouseUp!;
                    break;

                case StaffLabel staffLabel:
                    staffLabel.StaffLabel_ContextMenuStripOpened -= ContextMenuStrip_Opened!;
                    staffLabel.StaffLabel_ToolStripMenuItemClick -= ToolStripMenuItemClick!;
                    staffLabel.StaffLabel_MouseDown -= OnMouseDown!;
                    staffLabel.StaffLabel_MouseClick -= OnMouseClick!;
                    staffLabel.StaffLabel_MouseDoubleClick -= OnMouseDoubleClick!;
                    staffLabel.StaffLabel_MouseEnter -= OnMouseEnter!;
                    staffLabel.StaffLabel_MouseLeave -= OnMouseLeave!;
                    staffLabel.StaffLabel_MouseMove -= OnMouseMove!;
                    staffLabel.StaffLabel_MouseUp -= OnMouseUp!;
                    break;
            }
        }

        //----------------------------------------------------------------------------------
        //
        // ContextMenuStrip
        //
        //----------------------------------------------------------------------------------
        ContextMenuStrip contextMenuStrip = new();
        ToolStripMenuItem toolStripMenuItem00 = new("SetControlの形状");
        ToolStripMenuItem toolStripMenuItem00_0 = new("Single-SetControl");         // 子アイテム１
        ToolStripMenuItem toolStripMenuItem00_1 = new("Double-SetControl");         // 子アイテム２
        /// <summary>
        /// CreateContextMenuStrip
        /// </summary>
        private void CreateContextMenuStrip() {
            //ContextMenuStrip contextMenuStrip = new();
            contextMenuStrip.Name = "ContextMenuStripSetControl";
            contextMenuStrip.Opened += this.ContextMenuStrip_Opened!;
            this.ContextMenuStrip = contextMenuStrip;

            //ToolStripMenuItem toolStripMenuItem00 = new("SetControlの形状");
            toolStripMenuItem00.Name = "ToolStripMenuItemCarVerification";
            toolStripMenuItem00.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            //ToolStripMenuItem toolStripMenuItem00_0 = new("Single-SetControl");   // 子アイテム１
            toolStripMenuItem00_0.Name = "ToolStripMenuItemSetControlSingle";
            toolStripMenuItem00_0.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem00.DropDownItems.Add(toolStripMenuItem00_0);
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            //ToolStripMenuItem toolStripMenuItem00_1 = new("Double-SetControl");   // 子アイテム２
            toolStripMenuItem00_1.Name = "ToolStripMenuItemSetControlDouble";
            toolStripMenuItem00_1.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem00.DropDownItems.Add(toolStripMenuItem00_1);
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * プロパティ
             */
            ToolStripMenuItem toolStripMenuItem01 = new("プロパティ");
            toolStripMenuItem01.Name = "ToolStripMenuItemSetControlProperty";
            toolStripMenuItem01.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
        }

        //----------------------------------------------------------------------------------
        //
        // プロパティ
        //
        //----------------------------------------------------------------------------------

        /// <summary>
        /// コントロールのインデックス値を取得または設定します。
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int SetControlIndex {
            get => _setControlIndex;
            set => _setControlIndex = value;
        }
        /// <summary>
        /// true:配車割当あり false:配車割当なし
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool VehicleDispatchFlag {
            get => _vehicleDispatchFlag;
            set => _vehicleDispatchFlag = value;
        }
        /// <summary>
        /// SetControlの形状を取得または設定します。
        /// True:Double False:Single
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool PurposeFlag {
            get => _purposeFlag;
            set => _purposeFlag = value;
        }
        /// <summary>
        /// SetControlに割り当てられた人数を取得または設定します。
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int NumberOfPeople {
            get => _numberOfPeople;
            set => _numberOfPeople = value;
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool MouseEnterFlag {
            get => _mouseEnterFlag;
            set {
                _mouseEnterFlag = value;
                Invalidate();                                                           // 再描画
            }
        }
    }

    //----------------------------------------------------------------------------------
    //
    // インナークラス
    // BoardでSetControlの情報を渡すためのカスタムイベント
    //
    //----------------------------------------------------------------------------------

    public sealed class SetControlDragDropEventArgs : EventArgs {
        public Control DragControl {
            get;
        }
        public Point CellPoint {
            get;
        }

        public SetControlDragDropEventArgs(Control dragControl, Point cellPoint) {
            DragControl = dragControl;
            CellPoint = cellPoint;
        }
    }
}
