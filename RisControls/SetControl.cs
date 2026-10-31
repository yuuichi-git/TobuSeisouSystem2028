/*
 * 2026-10-17
 */
using System.ComponentModel;

using Entity;

namespace RisControls {
    public partial class SetControl : TableLayoutPanel {
        /*
         * デリゲート
         */
        public event EventHandler SetControl_ContextMenuStrip_Opened = delegate { };
        public event EventHandler SetControl_ToolStripMenuItem_Click = delegate { };
        public event EventHandler SetControl_MouseEnter = delegate { };
        public event EventHandler SetControl_MouseLeave = delegate { };
        public event MouseEventHandler SetControl_MouseClick = delegate { };
        public event MouseEventHandler SetControl_MouseDoubleClick = delegate { };
        public event MouseEventHandler SetControl_MouseDown = delegate { };
        public event MouseEventHandler SetControl_MouseMove = delegate { };
        public event MouseEventHandler SetControl_MouseUp = delegate { };
        public event DragEventHandler SetControl_DragDrop = delegate { };
        public event DragEventHandler SetControl_DragEnter = delegate { };
        public event DragEventHandler SetControl_DragOver = delegate { };
        /*
         * Cellのサイズ
         */
        private const float _cellDummyHeight = 20;
        private const float _cellWidth = 74;
        private const float _cellHeight = 118;
        /*
         * Cellの数
         */
        private const int _columnCount = 1;             // Columnの数
        private const int _rowCount = 5;                // Rowの数
        /*
         * その他
         */
        private int _setControlIndex = 0;               // SetControlのIndex(0～159) 配置されるSetControlのMaxIndexは159まで
        private bool _vehicleDispatchFlag =  false;     // 
        private bool _purposeFlag = false;              // True: Purpose, False:Single
        

        /// <summary>
        /// 
        /// </summary>
        public SetControl() {
            /* 
             * ダブルバッファリングを有効にする
             */
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * SetControlのプロパティをセットする
             */
            this.AllowDrop = true;
            this.Dock = DockStyle.None;
            this.Margin = new Padding(0);
            this.Name = "SetControl";
            this.Padding = new Padding(0);
            /*
             * Column作成
             */
            switch(_purposeFlag) {
                case true: // ２列
                    this.Size = new Size((int)_cellWidth * _columnCount * 2, (int)(_cellDummyHeight + (_cellHeight * 4)));
                    this.ColumnCount = _columnCount + 1;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
                case false: // １列
                    this.Size = new Size((int)_cellWidth * _columnCount, (int)(_cellDummyHeight + (_cellHeight * 4)));
                    this.ColumnCount = _columnCount;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
            }
            /*
             * Row作成
             */
            this.RowCount = _rowCount;
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellDummyHeight));  // Detail
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 1行目
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 2行目
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 3行目
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));       // 4行目
        }

        public SetLabel CreateSetLabel(SetMaster setMaster) {

            return new SetLabel(setMaster);
        }
        public CarLabel CreateCarLabel(CarMaster carMaster) {

            return new CarLabel(carMaster);
        }
        public StaffLabel CreateStaffLabel(StaffMaster staffMaster) {

            return new StaffLabel(staffMaster);
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {
            //// セルの矩形を取得
            //Rectangle rect = e.CellBounds;

            //// 少し内側に縮める（線が太く見えないように）
            //rect.Inflate(-1, -1);

            //// 黒の実線で枠線を描く
            //ControlPaint.DrawBorder(e.Graphics, rect, Color.Gray, ButtonBorderStyle.Dotted);
        }

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
    }
}
