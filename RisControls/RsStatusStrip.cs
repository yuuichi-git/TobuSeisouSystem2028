/*
 * 2026-10-10
 */
namespace RisControls {
    public partial class RsStatusStrip : StatusStrip {
        public RsStatusStrip() {
            InitializeComponent();
            CreateItems();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        private void CreateItems() {
            /*
             * Status Label
             */
            ToolStripStatusLabel toolStripStatusLabelStatus;
            toolStripStatusLabelStatus = CreateStatusLabel("ToolStripStatusLabelStatus", "Status：");
            this.Items.Add(toolStripStatusLabelStatus);
            /*
             * Detail Label
             */
            ToolStripStatusLabel toolStripStatusLabelDetail;
            toolStripStatusLabelDetail = CreateStatusLabel("ToolStripStatusLabelDetail", "Detail", ToolStripStatusLabel_Click);
            this.Items.Add(toolStripStatusLabelDetail);
        }

        private ToolStripStatusLabel CreateStatusLabel(string name, string text, EventHandler? handler = null) {
            ToolStripStatusLabel item = new();
            item.Name = name;
            item.Text = text;
            if(handler is not null)
                item.Click += handler;
            return item;
        }

        /// <summary>
        /// ステータスバーの詳細テキストを設定する
        /// </summary>
        /// <param name="text"></param>
        public void SetDetailText(string text) {
            foreach(ToolStripItem item in this.Items) {
                if(item is ToolStripStatusLabel statusLabel && statusLabel.Name == "ToolStripStatusLabelDetail") {
                    statusLabel.Text = text;
                    break;
                }
            }
        }

        /// <summary>
        /// ステータスバーの詳細テキストをクリアする
        /// </summary>
        public void ClearDetailText() {
            foreach(ToolStripItem item in this.Items) {
                if(item is ToolStripStatusLabel statusLabel && statusLabel.Name == "ToolStripStatusLabelDetail") {
                    statusLabel.Text = string.Empty;
                    break;
                }
            }
        }

        /*
         * Eventを外部に公開する
         */
        public event EventHandler? Event_RsStatusStrip_ToolStripStatusLabel_Click;
        private void ToolStripStatusLabel_Click(object? sender, EventArgs e) {
            Event_RsStatusStrip_ToolStripStatusLabel_Click?.Invoke(sender, e);
        }
    }
}
