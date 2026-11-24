/*
 * 2026-10-24
 */
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using Entity;

namespace RisControls {
    public partial class SetLabel : Label {
        ///----------------------------------------------------------------------------------
        //
        // delegate
        //
        //----------------------------------------------------------------------------------
        public event EventHandler SetLabel_ContextMenuStripOpened = delegate { };
        public event EventHandler SetLabel_ToolStripMenuItemClick = delegate { };
        public event EventHandler SetLabel_MouseEnter = delegate { };
        public event EventHandler SetLabel_MouseLeave = delegate { };
        public event MouseEventHandler SetLabel_MouseClick = delegate { };
        public event MouseEventHandler SetLabel_MouseDoubleClick = delegate { };
        public event MouseEventHandler SetLabel_MouseDown = delegate { };
        public event MouseEventHandler SetLabel_MouseMove = delegate { };
        public event MouseEventHandler SetLabel_MouseUp = delegate { };
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
         * プロパティ
         */
        private bool _mouseEnterFlag = false;            // マウスカーソルがLabel上にあるかどうかのフラグ
        /*
         * Dictionary
         */
        private static Dictionary<int, Color> _dictionaryClassificationCode = new Dictionary<int, Color>(){{10, Color.Black},                               // 雇上
                                                                                                           {11, Color.Chocolate},                           // 区契
                                                                                                           {12, Color.Black},                               // 臨時
                                                                                                           {20, Color.DarkGray},                            // 清掃工場
                                                                                                           {30, Color.Black},                               // 社内
                                                                                                           {50, Color.Black},                               // 一般
                                                                                                           {51, Color.Black},                               // 社用車
                                                                                                           {99, Color.Black}};                              // 指定なし
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
        /// <param name="setMaster"></param>
        public SetLabel(SetMaster setMaster) {
            /*
             * Entity
             */
            SetMaster = setMaster;
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
            Name = "SetLabel";
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
                Pen borderPen = new(_dictionaryClassificationCode[SetMaster.ClassificationCode], 2);
                SolidBrush fillBrush = _mouseEnterFlag ? new(Color.LightBlue) : _dictionaryManagedSpaceCode[SetMaster.ManagedSpaceCode];
                pe.Graphics.FillPath(fillBrush, path);
                pe.Graphics.DrawPath(borderPen, path);
            }
            /*
             * 文字(配車先)を描画
             */
            Rectangle rectangle = new(0, 0, Width -4, Height - 4);
            using SolidBrush brush = new(Color.Black);
            string text = $"{SetMaster?.SetName1}\r\n" + $"{SetMaster?.SetName2}\r\n" + $"{SetMaster?.SetCode}";
            pe.Graphics.DrawString(text, _font, brush, rectangle, _stringFormat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpened(object sender, EventArgs e) {
            SetLabel_ContextMenuStripOpened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {
            SetLabel_ToolStripMenuItemClick.Invoke(sender, e);
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

            SetLabel_MouseDown.Invoke(this, e);
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

            SetLabel_MouseDoubleClick.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            _mouseEnterFlag = true;                                      // マウスカーソルがLabel上にある
            Invalidate();                                                // 再描画

            SetLabel_MouseEnter.Invoke(this, e);                         // デリゲートを呼び出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            _mouseEnterFlag = false;                                     // マウスカーソルがLabel上にない
            Invalidate();                                                // 再描画

            SetLabel_MouseLeave.Invoke(this, e);                         // デリゲートを呼び出す
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

            SetLabel_MouseMove.Invoke(this, e);
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

            SetLabel_MouseUp.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickTimer_Tick(object? sender, EventArgs e) {
            _clickTimer.Stop();
            if(_clickArgs != null) {
                SetLabel_MouseClick.Invoke(this, _clickArgs);
            }
        }

        //----------------------------------------------------------------------------------
        //
        // ContextMenuStrip
        //
        //----------------------------------------------------------------------------------
        ContextMenuStrip contextMenuStrip = new();
        ToolStripMenuItem toolStripMenuItem00 = new("配車先の情報を表示する");
        ToolStripMenuItem toolStripMenuItem01 = new("運転日報を印刷する");
        ToolStripMenuItem toolStripMenuItem02 = new("配車の状態"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem02_0 = new("配車する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem02_1 = new("休車する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem03 = new("管理地"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem03_0 = new("本社管理"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem03_1 = new("三郷管理"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem04 = new("雇上・区契"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem04_0 = new("雇上契約に変更する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem04_1 = new("区契約に変更する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem05 = new("作業員付きの配置"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem05_0 = new("作業員付きに変更する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem05_1 = new("作業員なしに変更する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem06 = new("早番・遅番"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem06_0 = new("早番に変更する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem06_1 = new("遅番に変更する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem06_2 = new("解除する"); // 子アイテム３
        ToolStripMenuItem toolStripMenuItem07 = new("待機"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem07_0 = new("待機に変更する"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem07_1 = new("待機を解除する"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem08 = new("連絡事項"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem08_0 = new("連絡事項あり"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem08_1 = new("連絡事項なし"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem09 = new("メモを作成・編集する(点呼記録簿に転写)");
        ToolStripMenuItem toolStripMenuItem10 = new("代車・代番Fax作成確認"); // 親アイテム
        ToolStripMenuItem toolStripMenuItem10_0 = new("Fax送信をする"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem10_1 = new("Fax送信をしない"); // 子アイテム２
        ToolStripMenuItem toolStripMenuItem11 = new("代車・代番Faxを作成する");
        ToolStripMenuItem toolStripMenuItem12 = new("削除する");
        ToolStripMenuItem toolStripMenuItem13 = new("プロパティ");

        private void CreateContextMenuStrip() {
            contextMenuStrip.Name = "ContextMenuStripSetLabel";
            contextMenuStrip.Opened += ContextMenuStripOpened!;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 配車先の情報を表示する
             */
            toolStripMenuItem00.Name = "ToolStripMenuItemSetDetail";
            toolStripMenuItem00.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * 日報を作成する
             */
            toolStripMenuItem01.Name = "ToolStripMenuItemDriversReport";
            toolStripMenuItem01.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 配車の状態
             */
            toolStripMenuItem02.Name = "ToolStripMenuItemSetOperation";
            toolStripMenuItem02_0.Name = "ToolStripMenuItemSetOperationTrue";
            toolStripMenuItem02_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            toolStripMenuItem02_1.Name = "ToolStripMenuItemSetOperationFalse";
            toolStripMenuItem02_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * 管理地
             */
            toolStripMenuItem03.Name = "ToolStripMenuItemSetWarehouse";
            toolStripMenuItem03_0.Name = "ToolStripMenuItemSetWarehouseAdachi";
            toolStripMenuItem03_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_0);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            toolStripMenuItem03_1.Name = "ToolStripMenuItemSetWarehouseMisato";
            toolStripMenuItem03_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_1);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * 雇上・区契
             */
            toolStripMenuItem04.Name = "ToolStripMenuItemClassification";
            toolStripMenuItem04_0.Name = "ToolStripMenuItemClassificationYOUJYOU";
            toolStripMenuItem04_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_0);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            toolStripMenuItem04_1.Name = "ToolStripMenuItemClassificationKUKEI";
            toolStripMenuItem04_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_1);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            /*
             * 作業員付きの配置
             */
            toolStripMenuItem05.Name = "ToolStripMenuItemAddWorker";
            toolStripMenuItem05_0.Name = "ToolStripMenuItemAddWorkerTrue";
            toolStripMenuItem05_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem05.DropDownItems.Add(toolStripMenuItem05_0);
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            toolStripMenuItem05_1.Name = "ToolStripMenuItemAddWorkerFalse";
            toolStripMenuItem05_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem05.DropDownItems.Add(toolStripMenuItem05_1);
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            /*
             * 早番・遅番
             */
            toolStripMenuItem06.Name = "ToolStripMenuItemShift";
            toolStripMenuItem06_0.Name = "ToolStripMenuItemShiftFirst";
            toolStripMenuItem06_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_0);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            toolStripMenuItem06_1.Name = "ToolStripMenuItemShiftLater";
            toolStripMenuItem06_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_1);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            toolStripMenuItem06_2.Name = "ToolStripMenuItemShiftNone";
            toolStripMenuItem06_2.Click += ToolStripMenuItemClick!;
            toolStripMenuItem06.DropDownItems.Add(toolStripMenuItem06_2);
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            /*
             * 待機
             */
            toolStripMenuItem07.Name = "ToolStripMenuItemStandBy";
            toolStripMenuItem07_0.Name = "ToolStripMenuItemStandByTrue";
            toolStripMenuItem07_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem07.DropDownItems.Add(toolStripMenuItem07_0);
            contextMenuStrip.Items.Add(toolStripMenuItem07);
            toolStripMenuItem07_1.Name = "ToolStripMenuItemStandByFalse";
            toolStripMenuItem07_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem07.DropDownItems.Add(toolStripMenuItem07_1);
            contextMenuStrip.Items.Add(toolStripMenuItem07);
            /*
             * 連絡事項
             */
            toolStripMenuItem08.Name = "ToolStripMenuItemContactInformation";
            toolStripMenuItem08_0.Name = "ToolStripMenuItemContactInformationTrue";
            toolStripMenuItem08_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem08.DropDownItems.Add(toolStripMenuItem08_0);
            contextMenuStrip.Items.Add(toolStripMenuItem08);
            toolStripMenuItem08_1.Name = "ToolStripMenuItemContactInformationFalse";
            toolStripMenuItem08_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem08.DropDownItems.Add(toolStripMenuItem08_1);
            contextMenuStrip.Items.Add(toolStripMenuItem08);
            /*
             * メモを作成・編集する
             */
            toolStripMenuItem09.Name = "ToolStripMenuItemSetMemo";
            toolStripMenuItem09.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem09);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 代車・代番Fax作成確認
             */
            toolStripMenuItem10.Name = "ToolStripMenuItemFaxInformation";
            toolStripMenuItem10_0.Name = "ToolStripMenuItemFaxInformationTrue";
            toolStripMenuItem10_0.Click += ToolStripMenuItemClick!;
            toolStripMenuItem10.DropDownItems.Add(toolStripMenuItem10_0);
            contextMenuStrip.Items.Add(toolStripMenuItem10);
            toolStripMenuItem10_1.Name = "ToolStripMenuItemFaxInformationFalse";
            toolStripMenuItem10_1.Click += ToolStripMenuItemClick!;
            toolStripMenuItem10.DropDownItems.Add(toolStripMenuItem10_1);
            contextMenuStrip.Items.Add(toolStripMenuItem10);
            /*
             * 代車・代番Faxを作成する
             */
            toolStripMenuItem11.Name = "ToolStripMenuItemCreateFax";
            toolStripMenuItem11.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem11);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 削除する
             */
            toolStripMenuItem12.Name = "ToolStripMenuItemSetDelete";
            toolStripMenuItem12.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem12);
            /*
             * プロパティ
             */
            toolStripMenuItem13.Name = "ToolStripMenuItemSetProperty";
            toolStripMenuItem13.Click += ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem13);
        }

        //----------------------------------------------------------------------------------
        //
        // アクセサー
        //
        //----------------------------------------------------------------------------------

        /// <summary>
        /// SetMasterを保持する
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public SetMaster SetMaster {
            get; set;
        }
        /// <summary>
        /// 管理地コードを保持する
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int ManagedSpaceCode {
            get;
            set {
                Refresh();
            }
        } = 0;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int ClassificationCode {
            get;
            set {
                Refresh();
            }
        } = 0;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool LastRollCallFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public DateTime LastRollCallYmdHms {
            get; set;
        } = new(1900, 1, 1);
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool SetMemoFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public string SetMemo {
            get; set;
        } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public int ShiftCode {
            get;
            set {
                Refresh();
            }
        } = 0;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool StandByFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool AddWorkerFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool ContactInfomationFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool FaxTransmissionFlag {
            get;
            set {
                Refresh();
            }
        } = false;
    }
}
