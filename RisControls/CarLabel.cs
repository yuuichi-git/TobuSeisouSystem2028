/*
 * 2026-10-25
 */
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using Entity;

namespace RisControls {
    public partial class CarLabel : Label {
        ///----------------------------------------------------------------------------------
        //
        // delegate
        //
        //----------------------------------------------------------------------------------
        public event EventHandler CarLabel_ContextMenuStripOpened = delegate { };
        public event EventHandler CarLabel_ToolStripMenuItemClick = delegate { };
        public event EventHandler CarLabel_MouseEnter = delegate { };
        public event EventHandler CarLabel_MouseLeave = delegate { };
        public event MouseEventHandler CarLabel_MouseClick = delegate { };
        public event MouseEventHandler CarLabel_MouseDoubleClick = delegate { };
        public event MouseEventHandler CarLabel_MouseDown = delegate { };
        public event MouseEventHandler CarLabel_MouseMove = delegate { };
        public event MouseEventHandler CarLabel_MouseUp = delegate { };
        /*
         * 共通で使用するフォントと文字列描画の設定
         */
        private static readonly Font _font = new("Yu Gothic UI", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        private static readonly StringFormat _stringFormat = new(){
            Alignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.FitBlackBox,
            LineAlignment = StringAlignment.Center
        };
        /*
         * Labelのサイズ
         */
        private const int _labelWidth = 72;
        private const int _labelHeight = 116;
        /*
         * Entity
         */
        private CarMaster _carMaster;
        /*
         * プロパティ
         */
        private bool _mouseEnterFlag = false;            // マウスカーソルがLabel上にあるかどうかのフラグ
        /*
         * Dictionary
         */
        private static Dictionary<int, Color> _dictionaryOtherCode = new Dictionary<int, Color>(){{10, Color.DarkGreen},                                    // 事業用
                                                                                                  {11, Color.WhiteSmoke}};                                  // 自家用
        private static Dictionary<int, SolidBrush> _dictionaryManagedSpaceCode = new Dictionary<int, SolidBrush>(){{0, new SolidBrush(Color.White)},        // 該当なし
                                                                                                                   {1, new SolidBrush(Color.White)},        // 足立車庫
                                                                                                                   {2, new SolidBrush(Color.LightSkyBlue)}, // 三郷車庫
                                                                                                                   {3, new SolidBrush(Color.White)},        // ２丁目事務所
                                                                                                                   {4, new SolidBrush(Color.White)},        // リサイクルセンター
                                                                                                                   {5, new SolidBrush(Color.White)}};       // 産廃車庫
        /*
         * Timer
         * ClickイベントとDoubleClickイベントの両方を使用するために、Clickイベントを遅延させるためのTimer
         */
        private System.Windows.Forms.Timer _clickTimer = new();
        private MouseEventArgs? _clickArgs;
        /*
         * ドラッグ開始ポイント
         */
        private Point _dragStartPoint;
        private bool _dragStarted;
        private Size dragSize = SystemInformation.DragSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carMaster"></param>
        public CarLabel(CarMaster carMaster) {
            /*
             * Entity
             */
            _carMaster = carMaster;
            /* 
             * ダブルバッファリングを有効にする
             */
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * InitializeControl
             */
            AllowDrop = false;
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            Height = _labelHeight;
            Margin = new Padding(2);
            Name = "CarLabel";
            Padding = new(0);
            Width = _labelWidth;
            /*
             * ContextMenuStripを作成
             */
            CreateContextMenuStrip();
            /*
             * Timer
             */
            _clickTimer.Interval = SystemInformation.DoubleClickTime;
            _clickTimer.Tick += ClickTimer_Tick;
        }

        //----------------------------------------------------------------------------------
        //
        // Event
        //
        //----------------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe) {
            //base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            /*
             * Labelサイズ
             */
            Rectangle rect = new(2, 2, Width - 8, Height - 8);      // Labelの描画領域を取得し、影を考慮して少し小さくする
            /*
             * 影を描画
             */
            Rectangle shadowRect = rect;                            // 影の位置をずらす
            shadowRect.Offset(3, 3);
            using(GraphicsPath shadowPath = new()) {                // 影のパスを作成
                shadowPath.AddRoundedRectangle(shadowRect, new Size(4, 4));
                using SolidBrush shadowBrush = new(Color.FromArgb(80, 0, 0, 0));
                pe.Graphics.FillPath(shadowBrush, shadowPath);
            }
            /*
             * Labelを描画
             */
            using(GraphicsPath path = new()) {                      // Labelのパスを作成
                path.AddRoundedRectangle(rect, new Size(8, 8));
                Pen borderPen = new(_dictionaryOtherCode[CarMaster.OtherCode], 2);
                SolidBrush fillBrush = _mouseEnterFlag ? new(Color.LightBlue) : _dictionaryManagedSpaceCode[CarMaster.GarageCode];
                pe.Graphics.FillPath(fillBrush, path);
                pe.Graphics.DrawPath(borderPen, path);
            }
            /*
             * 文字(車両)を描画
             */
            Rectangle rectangle = new(0, 0, Width - 4, Height - 4);
            using SolidBrush brush = new(Color.Black);
            string text = $"{CarMaster.RegistrationNumber1}{CarMaster.RegistrationNumber2}\r\n" +
                          $"{CarMaster.RegistrationNumber3}{CarMaster.RegistrationNumber4}\r\n" +
                          $"{CarMaster.DisguiseKind1}{(CarMaster.DoorNumber != 0 ? CarMaster.DoorNumber : " ")}\r\n ";
            pe.Graphics.DrawString(text, _font, brush, rectangle, _stringFormat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpened(object sender, EventArgs e) {
            CarLabel_ContextMenuStripOpened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {
            CarLabel_ToolStripMenuItemClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            /*
             * DragDrop
             */
            if(e.Button == MouseButtons.Left) {
                _dragStartPoint = e.Location;
                _dragStarted = false;
            }

            CarLabel_MouseDown.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e) {
            _clickArgs = e;
            _clickTimer.Stop();
            _clickTimer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            _clickTimer.Stop();

            CarLabel_MouseDoubleClick.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            _mouseEnterFlag = true;                                     // マウスカーソルがLabel上にある
            Invalidate();                                               // 再描画
            CarLabel_MouseEnter.Invoke(this, e);                        // デリゲートを呼び出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            _mouseEnterFlag = false;                                    // マウスカーソルがLabel上にない
            Invalidate();                                               // 再描画
            CarLabel_MouseLeave.Invoke(this, e);                        // デリゲートを呼び出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {
            /*
             * DragDrop
             * 左ボタン以外は処理しない
             */
            if(e.Button != MouseButtons.Left)
                return;
            /*
             * 既にドラッグ中なら処理しない
             */
            if(_dragStarted)
                return;
            /*
             * MouseDown時の位置からどれだけ移動したか取得する
             */
            int dx = Math.Abs(e.X - _dragStartPoint.X);
            int dy = Math.Abs(e.Y - _dragStartPoint.Y);
            /*
             * Windows標準のドラッグ開始距離を超えた場合
             */
            if(dx > dragSize.Width || dy > dragSize.Height) {
                /*
                 * ドラッグ開始フラグを立てる
                 */
                _dragStarted = true;
                /*
                 * Drag&Dropを開始する
                 * this をドラッグデータとして渡す
                 * Move は移動操作を表す
                 */
                DoDragDrop(this, DragDropEffects.Move);
            }

            CarLabel_MouseMove.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e) {
            /*
             * DragDrop
             */
            _dragStarted = false;

            CarLabel_MouseUp.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickTimer_Tick(object? sender, EventArgs e) {
            _clickTimer.Stop();
            if(_clickArgs != null) {
                CarLabel_MouseClick.Invoke(this, _clickArgs);
            }
        }

        //----------------------------------------------------------------------------------
        //
        // ContextMenuStrip
        //
        //----------------------------------------------------------------------------------
        ContextMenuStrip contextMenuStrip = new();
        ToolStripMenuItem toolStripMenuItem00 = new("車両台帳を表示");
        ToolStripMenuItem toolStripMenuItem01 = new("出庫地"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem01_0 = new("本社から出庫"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem01_1 = new("三郷から出庫"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem02 = new("代車処理"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem02_0 = new("代車として記録する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem02_1 = new("代車を解除する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem03 = new("メモを作成・編集する");
        ToolStripMenuItem toolStripMenuItem04 = new("プロパティ");

        private void CreateContextMenuStrip() {
            contextMenuStrip.Name = "ContextMenuStripCarLabel";
            contextMenuStrip.Opened += ContextMenuStripOpened!;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 車両台帳を表示する
             */
            toolStripMenuItem00.Name = "ToolStripMenuItemCarVerification";
            toolStripMenuItem00.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 車庫地コード
             */
            toolStripMenuItem01.Name = "ToolStripMenuItemCarWarehouse";
            toolStripMenuItem01_0.Name = "ToolStripMenuItemCarWarehouseAdachi";
            toolStripMenuItem01_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem01.DropDownItems.Add(toolStripMenuItem01_0);
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            toolStripMenuItem01_1.Name = "ToolStripMenuItemCarWarehouseMisato";
            toolStripMenuItem01_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem01.DropDownItems.Add(toolStripMenuItem01_1);
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * 代番処理
             */
            toolStripMenuItem02.Name = "ToolStripMenuItemCarProxy";
            toolStripMenuItem02_0.Name = "ToolStripMenuItemCarProxyTrue";
            toolStripMenuItem02_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            toolStripMenuItem02_1.Name = "ToolStripMenuItemCarProxyFalse";
            toolStripMenuItem02_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * メモを作成・編集する
             */
            toolStripMenuItem03.Name = "ToolStripMenuItemCarMemo";
            toolStripMenuItem03.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * プロパティ
             */
            toolStripMenuItem04.Name = "ToolStripMenuItemCarProperty";
            toolStripMenuItem04.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem04);
        }

        //----------------------------------------------------------------------------------
        //
        // アクセサー
        //
        //----------------------------------------------------------------------------------

        /// <summary>
        /// CarMasterを保持するプロパティ
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public CarMaster CarMaster {
            get => _carMaster;
            set => _carMaster = value;
        }
        /// <summary>
        /// 車庫地コード
        /// 0:該当なし 1:足立車庫 2:三郷車庫 3:２丁目事務所 4:リサイクルセンター 5:産廃車庫
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int CarGarageCode {
            get; 
            set {
                Refresh();
            }
        } = 0;
        /// <summary>
        /// 代車フラグ
        /// true:代車 false:本番車
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool CarProxyFlag {
            get; 
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// メモフラグ
        /// true:メモあり false:メモなし
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool CarMemoFlag {
            get; 
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// メモ
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public string CarMemo {
            get; set;
        } = string.Empty;
    }
}
