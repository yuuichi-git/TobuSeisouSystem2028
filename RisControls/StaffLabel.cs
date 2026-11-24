/*
 * 2026-10-26
 */
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using Entity;

namespace RisControls {
    public partial class StaffLabel : Label {
        ///----------------------------------------------------------------------------------
        //
        // delegate
        //
        //----------------------------------------------------------------------------------
        public event EventHandler StaffLabel_ContextMenuStripOpened = delegate { };
        public event EventHandler StaffLabel_ToolStripMenuItemClick = delegate { };
        public event MouseEventHandler StaffLabel_MouseDown = delegate { };
        public event MouseEventHandler StaffLabel_MouseClick = delegate { };
        public event MouseEventHandler StaffLabel_MouseDoubleClick = delegate { };
        public event EventHandler StaffLabel_MouseEnter = delegate { };
        public event EventHandler StaffLabel_MouseLeave = delegate { };
        public event MouseEventHandler StaffLabel_MouseMove = delegate { };
        public event MouseEventHandler StaffLabel_MouseUp = delegate { };
        /*
         * 共通で使用するフォントと文字列描画の設定
         */
        private static readonly Font _fontDisplayName = new("メイリオ", 13, FontStyle.Regular, GraphicsUnit.Pixel);
        private static readonly Font _fontOccupation = new("メイリオ", 8, FontStyle.Regular, GraphicsUnit.Point);
        private static readonly StringFormat _stringFormat = new(){
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.DirectionVertical
        };
        /*
         * Labelのサイズ
         */
        private const int _labelWidth = 72;
        private const int _labelHeight = 116;
        /*
         * Entity
         */
        private StaffMaster _staffMaster;
        /*
         * プロパティ
         */
        private bool _mouseEnterFlag = false;                   // マウスカーソルがLabel上にあるかどうかのフラグ
        /*
         * Dictionary
         */
        private static Dictionary<int, SolidBrush> _dictionaryBelongs = new Dictionary<int, SolidBrush>(){{10, new SolidBrush(Color.White)},        // 役員
                                                                                                          {11, new SolidBrush(Color.White)},        // 社員
                                                                                                          {12, new SolidBrush(Color.Gold)},         // アルバイト
                                                                                                          {13, new SolidBrush(Color.White)},        // 派遣
                                                                                                          {14, new SolidBrush(Color.White)},        // 嘱託雇用契約社員
                                                                                                          {15, new SolidBrush(Color.White)},        // パートタイマー
                                                                                                          {22, new SolidBrush(Color.White)},        // 労共
                                                                                                          {99, new SolidBrush(Color.White)}};       // 指定なし
        private static Dictionary<int, SolidBrush> _dictionaryJobForm = new Dictionary<int, SolidBrush>(){{20, new SolidBrush(Color.White)},        // 長期(新)
                                                                                                          {21, new SolidBrush(Color.LimeGreen)},    // 短期(新)
                                                                                                          {22, new SolidBrush(Color.White)},        // 長期(自)
                                                                                                          {23, new SolidBrush(Color.LimeGreen)},    // 短期(自)
                                                                                                          {99, new SolidBrush(Color.White)}};       // 指定なし
        private static Dictionary<int, SolidBrush> _dictionaryOccupation = new Dictionary<int, SolidBrush>(){{10, new SolidBrush(Color.White)},     // 運転手
                                                                                                             {11, new SolidBrush(Color.White)},     // 作業員
                                                                                                             {12, new SolidBrush(Color.White)},     // 自転車駐輪場
                                                                                                             {13, new SolidBrush(Color.White)},     // リサイクルセンター
                                                                                                             {14, new SolidBrush(Color.White)},     // 文京中継所
                                                                                                             {20, new SolidBrush(Color.White)},     // 事務職
                                                                                                             {99, new SolidBrush(Color.White)}};    // 指定なし
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
        /// <param name="staffMaster"></param>
        public StaffLabel(StaffMaster staffMaster) {
            /*
             * Entity
             */
            _staffMaster = staffMaster;
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
            Name = "StaffLabel";
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
            Rectangle rect = new(2, 2, Width - 8, Height - 8);                              // Labelの描画領域を取得し、影を考慮して少し小さくする
            /*
             * 影を描画
             */
            Rectangle shadowRect = rect;                                                    // 影の位置をずらす
            shadowRect.Offset(3, 3);
            using(GraphicsPath shadowPath = new()) {                                        // 影のパスを作成
                shadowPath.AddRoundedRectangle(shadowRect, new Size(4, 4));
                using SolidBrush brushShadow = new(Color.FromArgb(80, 0, 0, 0));
                pe.Graphics.FillPath(brushShadow, shadowPath);
            }
            /*
             * Labelを描画
             */
            using(GraphicsPath path = new()) {                                              // Labelのパスを作成
                path.AddRoundedRectangle(rect, new Size(8, 8));
                // Labelの枠線色を黒に設定
                Pen borderPen = new(Color.Black, 1);
                // Labelの塗りつぶし色を白に設定
                SolidBrush brushFill = _mouseEnterFlag ? new(Color.LightBlue) :
                                                         _staffMaster.Belongs == 22 && (_staffMaster.JobForm == 21 || _staffMaster.JobForm == 23) ? _dictionaryJobForm[_staffMaster.JobForm] : _dictionaryBelongs[_staffMaster.Belongs];
                // Labelを塗りつぶす
                pe.Graphics.FillPath(brushFill, path);
                // Labelの枠線を描画
                pe.Graphics.DrawPath(borderPen, path);
            }
            /*
             * 文字(DisplayName)を描画
             */
            Rectangle rectangle = new(0, 0, Width - 5, Height);
            using SolidBrush brushDisplayName = new(Color.Black);
            pe.Graphics.DrawString(StaffMaster.DisplayName, _fontDisplayName, brushDisplayName, rectangle, _stringFormat);
            /*
             * 左下に「作」を描画
             */
            if(StaffOccupationFlag) {
                const string mark = "作";
                SizeF markSize = pe.Graphics.MeasureString(mark, _fontOccupation);
                using SolidBrush brushOccupation = new(Color.Blue);
                pe.Graphics.DrawString(mark, _fontOccupation, brushOccupation, 6, Height - markSize.Height - 6);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpened(object sender, EventArgs e) {
            StaffLabel_ContextMenuStripOpened.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {
            StaffLabel_ToolStripMenuItemClick.Invoke(sender, e);
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

            StaffLabel_MouseDown.Invoke(this, e);
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

            StaffLabel_MouseDoubleClick.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            _mouseEnterFlag = true;                                     // マウスカーソルがLabel上にある
            Invalidate();                                               // 再描画
            StaffLabel_MouseEnter.Invoke(this, e);                      // デリゲートを呼び出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            _mouseEnterFlag = false;                                    // マウスカーソルがLabel上にない
            Invalidate();                                               // 再描画
            StaffLabel_MouseLeave.Invoke(this, e);                      // デリゲートを呼び出す
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

            StaffLabel_MouseMove.Invoke(this, e);
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

            StaffLabel_MouseUp.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickTimer_Tick(object? sender, EventArgs e) {
            _clickTimer.Stop();
            if(_clickArgs != null) {
                StaffLabel_MouseClick.Invoke(this, _clickArgs);
            }
        }

        //----------------------------------------------------------------------------------
        //
        // ContextMenuStrip
        //
        //----------------------------------------------------------------------------------
        ContextMenuStrip contextMenuStrip = new();
        public ToolStripMenuItem toolStripMenuItem00 = new("従事者台帳を表示");
        public ToolStripMenuItem toolStripMenuItem01 = new("免許証を表示");
        public ToolStripMenuItem toolStripMenuItem02 = new("代番処理"); // 親アイテム
        public ToolStripMenuItem toolStripMenuItem02_0 = new("代番として記録する"); // 子アイテム１
        public ToolStripMenuItem toolStripMenuItem02_1 = new("代番を解除する"); // 子アイテム２
        public ToolStripMenuItem toolStripMenuItem03 = new("職種設定"); // 親アイテム
        public ToolStripMenuItem toolStripMenuItem03_0 = new("運転手の料金設定にする(運賃コードに依存)"); // 子アイテム１
        public ToolStripMenuItem toolStripMenuItem03_1 = new("作業員の料金設定にする"); // 子アイテム２
        public ToolStripMenuItem toolStripMenuItem04 = new("出勤確認(電話確認)"); // 親アイテム
        public ToolStripMenuItem toolStripMenuItem04_0 = new("出勤を確認済"); // 子アイテム１
        public ToolStripMenuItem toolStripMenuItem04_1 = new("出勤を未確認"); // 子アイテム２
        public ToolStripMenuItem toolStripMenuItem05 = new("メモを作成・編集する('Ctrl + Click')");
        public ToolStripMenuItem toolStripMenuItem06 = new("有給詳細情報を表示");
        public ToolStripMenuItem toolStripMenuItem07 = new("プロパティ");

        private void CreateContextMenuStrip() {
            this.contextMenuStrip.Name = "ContextMenuStripStaffLabel";
            this.contextMenuStrip.Opened += this.ContextMenuStripOpened!;
            this.ContextMenuStrip = contextMenuStrip;
            /*
             * 従事者台帳を表示する
             */
            toolStripMenuItem00.Name = "ToolStripMenuItemStaffPaper";
            toolStripMenuItem00.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * 従事者免許証を表示する
             */
            toolStripMenuItem01.Name = "ToolStripMenuItemStaffLicense";
            toolStripMenuItem01.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * 代番処理
             */
            toolStripMenuItem02.Name = "ToolStripMenuItemStaffProxy";
            toolStripMenuItem02_0.Name = "ToolStripMenuItemStaffProxyTrue";
            toolStripMenuItem02_0.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_0);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            toolStripMenuItem02_1.Name = "ToolStripMenuItemStaffProxyFalse";
            toolStripMenuItem02_1.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem02.DropDownItems.Add(toolStripMenuItem02_1);
            contextMenuStrip.Items.Add(toolStripMenuItem02);
            /*
             * 料金設定
             */
            toolStripMenuItem03.Name = "ToolStripMenuItemStaffOccupation";
            toolStripMenuItem03_0.Name = "ToolStripMenuItemStaffOccupation10";
            toolStripMenuItem03_0.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_0);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            toolStripMenuItem03_1.Name = "ToolStripMenuItemStaffOccupation11";
            toolStripMenuItem03_1.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem03.DropDownItems.Add(toolStripMenuItem03_1);
            contextMenuStrip.Items.Add(toolStripMenuItem03);
            /*
             * 電話連絡・出勤確認
             */
            toolStripMenuItem04.Name = "ToolStripMenuItemStaffTelephoneMark";
            toolStripMenuItem04_0.Name = "ToolStripMenuItemTelephoneMarkTrue";
            toolStripMenuItem04_0.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_0);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            toolStripMenuItem04_1.Name = "ToolStripMenuItemStaffTelephoneMarkFalse";
            toolStripMenuItem04_1.Click += this.ToolStripMenuItemClick!;
            toolStripMenuItem04.DropDownItems.Add(toolStripMenuItem04_1);
            contextMenuStrip.Items.Add(toolStripMenuItem04);
            /*
             * メモを作成・編集する("Ctrl + Click")
             */
            toolStripMenuItem05.Name = "ToolStripMenuItemStaffMemo";
            toolStripMenuItem05.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem05);
            /*
             * 有給詳細情報を表示
             */
            toolStripMenuItem06.Name = "ToolStripMenuItemPaidLeaveDetail";
            toolStripMenuItem06.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem06);
            /*
             * プロパティ
             */
            toolStripMenuItem07.Name = "ToolStripMenuItemStaffProperty";
            toolStripMenuItem07.Click += this.ToolStripMenuItemClick!;
            contextMenuStrip.Items.Add(toolStripMenuItem07);
        }

        //----------------------------------------------------------------------------------
        //
        // アクセサー
        //
        //----------------------------------------------------------------------------------

        /// <summary>
        /// StaffMasterを保持するプロパティ
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public StaffMaster StaffMaster {
            get => _staffMaster;
            set => _staffMaster = value;
        }
        /// <summary>
        /// 職種を保持するフラグ
        /// true:作業員 false:運転手等
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool StaffOccupationFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 代番、フラグを保持するフラグ
        /// true:代番 false:本番
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool StaffProxyFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 点呼フラグを保持するフラグ
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool StaffRollCallFlag {
            get;
            set {
                Refresh();
            }
        } = false;
        /// <summary>
        /// 点呼日時を保持するプロパティ
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public DateTime StaffRollCallYmdHms {
            get; set;
        } = new(1900, 1, 1);
        /// <summary>
        /// メモフラグ
        /// true:メモあり false:メモなし
        /// </summary>
        [Browsable(false)]                                                              // プロパティウィンドウに表示しない
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]       // デザイナーでのシリアル化を無効にする
        public bool StaffMemoFlag {
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
        public string StaffMemo {
            get; set;
        } = string.Empty;
    }
}
