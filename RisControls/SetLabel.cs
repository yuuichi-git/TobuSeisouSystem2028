/*
 * 2026-10-24
 */
using System.Drawing.Drawing2D;

using Entity;

namespace RisControls {
    public partial class SetLabel : Label {
        /*
         * デリゲート
         */
        public event EventHandler SetLabel_ContextMenuStrip_Opened = delegate { };
        public event EventHandler SetLabel_ToolStripMenuItem_Click = delegate { };
        public event EventHandler SetLabel_MouseEnter = delegate { };
        public event EventHandler SetLabel_MouseLeave = delegate { };
        public event MouseEventHandler SetLabel_MouseClick = delegate { };
        public event MouseEventHandler SetLabel_MouseDoubleClick = delegate { };
        public event MouseEventHandler SetLabel_MouseDown = delegate { };
        public event MouseEventHandler SetLabel_MouseMove = delegate { };
        public event MouseEventHandler SetLabel_MouseUp = delegate { };
        /*
         * Labelのサイズ
         */
        private const int _labelWidth = 72;
        private const int _labelHeight = 116;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMaster"></param>
        public SetLabel(SetMaster setMaster) {
            /* 
             * ダブルバッファリングを有効にする
             */
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * InitializeControl
             */
            this.AllowDrop = false;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.Height = _labelHeight;
            this.Margin = new Padding(2);
            this.Name = "SetLabel";
            this.Padding = new(0);
            this.Width = _labelWidth;
        }

        protected override void OnPaint(PaintEventArgs pe) {
            //base.OnPaint(pe);
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
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
                using SolidBrush fillBrush = new(Color.White);
                using Pen borderPen = new(Color.Black, 1);
                pe.Graphics.FillPath(fillBrush, path);
                pe.Graphics.DrawPath(borderPen, path);
            }
        }
    }
}
