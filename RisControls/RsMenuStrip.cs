using System.ComponentModel;
/*
 * 2026/10/04
 */
namespace RisControls {
    public partial class RsMenuStrip : MenuStrip {
        /// <summary>
        /// ToolStripMenuItemDataBaseLocalのチェック状態を保持するフラグ
        /// true:Local false:Network
        /// </summary>
        private bool _toolStripMenuItemDataBaseLocalFlag = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public RsMenuStrip() {
            InitializeComponent();
            CreateItems();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        private void CreateItems() {
            /*
             * ファイル
             */
            ToolStripMenuItem toolStripMenuItemFile = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemFile", "ファイル", null);
            toolStripMenuItemFile.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemFile.DropDownItems, "ToolStripMenuItemInitialize", "配車ボードを初期化", ToolStripMenuItem_Click!));
            toolStripMenuItemFile.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemFile.DropDownItems, "ToolStripMenuItemExit", "終了", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemFile);
            /*
             * 編集
             */
            ToolStripMenuItem toolStripMenuItemEdit = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemEdit", "編集", null);
            toolStripMenuItemEdit.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemEdit.DropDownItems, "ToolStripMenuItemInsertNewRecord", "新規レコード作成", ToolStripMenuItem_Click!));
            toolStripMenuItemEdit.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemEdit.DropDownItems, "ToolStripMenuItemUpdateTaitou", "台東資源収集量入力", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemEdit);
            /*
             * データベース
             */
            ToolStripMenuItem toolStripMenuItemDatabase = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemDataBase", "データベース", null);
            ToolStripMenuItem toolStripMenuItemDataBaseLocal = CreateToolStripMenuItem(toolStripMenuItemDatabase.DropDownItems, "ToolStripMenuItemDataBaseLocal", "ローカルデータベースへ接続", ToolStripMenuItem_Click!);
            toolStripMenuItemDatabase.DropDownItems.Add(toolStripMenuItemDataBaseLocal);
            toolStripMenuItemDataBaseLocal.Checked = ToolStripMenuItemDataBaseLocalFlag;
            toolStripMenuItemDataBaseLocal.CheckOnClick = true;
            this.Items.Add(toolStripMenuItemDatabase);
            /*
             * Export
             */
            ToolStripMenuItem toolStripMenuItemExport = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemExport", "エクスポート", null);
            toolStripMenuItemExport.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemExport.DropDownItems, "ToolStripMenuItemExportCsv", "CSV出力", ToolStripMenuItem_Click!));
            toolStripMenuItemExport.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemExport.DropDownItems, "ToolStripMenuItemExportExcel", "Excel出力", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemExport);
            /*
             * PDF
             */
            ToolStripMenuItem toolStripMenuItemPdf = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemPdf", "PDF", null);
            toolStripMenuItemPdf.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemPdf.DropDownItems, "ToolStripMenuItemOpenPdf", "PDFを開く", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemPdf);
            /*
             * 印刷
             */
            ToolStripMenuItem toolStripMenuItemPrint = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemPrint", "印刷", null);
            toolStripMenuItemPrint.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemPrint.DropDownItems, "ToolStripMenuItemPrintA4", "A4で印刷する", ToolStripMenuItem_Click!));
            toolStripMenuItemPrint.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemPrint.DropDownItems, "ToolStripMenuItemPrintA4CarDetail", "A4で印刷する(車検証＋記録事項)", ToolStripMenuItem_Click!));
            toolStripMenuItemPrint.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemPrint.DropDownItems, "ToolStripMenuItemPrintB4", "B4で印刷する", ToolStripMenuItem_Click!));
            toolStripMenuItemPrint.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemPrint.DropDownItems, "ToolStripMenuItemPrintB5", "B5で印刷する", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemPrint);
            /*
             * ヘルプ
             */
            ToolStripMenuItem toolStripMenuItemHelp = CreateToolStripMenuItem(this.Items, "ToolStripMenuItemHelp", "ヘルプ", null);
            toolStripMenuItemHelp.DropDownItems.Add(CreateToolStripMenuItem(toolStripMenuItemHelp.DropDownItems, "ToolStripMenuItemAbout", "バージョン情報", ToolStripMenuItem_Click!));
            this.Items.Add(toolStripMenuItemHelp);
        }

        /// <summary>
        /// ToolStripMenuItemを作成して親に追加
        /// </summary>
        /// <param name="parent">追加する親</param>
        /// <param name="name">Name</param>
        /// <param name="text">Text</param>
        /// <param name="handler">イベントハンドラ</param>
        /// <returns>作成されたToolStripMenuItem</returns>
        private ToolStripMenuItem CreateToolStripMenuItem(ToolStripItemCollection parent, string name, string text, EventHandler? handler = null) {
            ToolStripMenuItem item = new();
            item.Name = name;
            item.Text = text;
            if(handler is not null)
                item.Click += handler;
            parent.Add(item);
            return item;
        }

        /// <summary>
        /// ToolStripMenuItemのEnableを設定
        /// 2026-02-11 修正
        /// </summary>
        /// <param name="listString">有効化するメニューの名前リスト</param>
        public void ChangeEnable(List<string> listString) {
            foreach(ToolStripItem parentItem in this.Items) {
                if(parentItem is not ToolStripMenuItem parentMenu)
                    continue;
                /*
                 * 親メニューの表示・活性制御
                 */
                bool parentVisible = listString.Contains(parentMenu.Name!);
                parentMenu.Visible = parentVisible;

                foreach(ToolStripItem childItem in parentMenu.DropDownItems) {
                    if(childItem is not ToolStripMenuItem childMenu)
                        continue;
                    /*
                     * 子メニューの表示・活性制御
                     */
                    bool childEnabled = listString.Contains(childMenu.Name!);
                    childMenu.Visible = childEnabled;
                    childMenu.Enabled = childEnabled;
                }
            }
        }

        /*
         * Eventを外部に公開する
         */
        public event EventHandler? Event_RsMenuStrip_ToolStripMenuItem_Click;
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            Event_RsMenuStrip_ToolStripMenuItem_Click?.Invoke(sender, e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ToolStripMenuItemDataBaseLocalFlag {
            get => _toolStripMenuItemDataBaseLocalFlag;
            set => _toolStripMenuItemDataBaseLocalFlag = value;
        }
    }
}
