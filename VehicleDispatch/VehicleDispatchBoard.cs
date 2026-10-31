/*
 * 2026-10-14
 */
using Dao;

using RisControls;

using Vo;

namespace VehicleDispatch {

    public partial class VehicleDispatchBoard : Form {
        private Board _board;
        private int _maxSetControls = 160;
        /*
         * 参照
         */
        private Form _parentForm;
        /*
         * Vo
         */
        private InitializeVo _initializeVo;

        public VehicleDispatchBoard(InitializeVo initializeVo, Form parentForm) {
            _board = new();
            _initializeVo = initializeVo;
            _parentForm = parentForm;
            /*
             * InitializeControls
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemEdit",
                                             "ToolStripMenuItemUpdateTaitou",
                                             "ToolStripMenuItemHelp"};
            this.RsMenuStrip1.ChangeEnable(listString);
            this.RsMenuStrip1.Event_RsMenuStrip_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleDispatchBoard_Load(object sender, EventArgs e) {
            AddBoard();

            for(int i = 0; i < _maxSetControls; i++) {
                _board.AddSetControl(i);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsButton_Click(object sender, EventArgs e) {
            switch(((RsButton)sender).Name) {
                case "RsButtonUpdate": {
                    using AppDbContext db = new();


                }
                break;
            }
        }

        private void AddBoard() {
            /*
             * BoardをTableLayoutPanelに追加
             */
            RsTableLayoutPanelBase.Controls.Add(_board, 0, 2);
        }

        private void ContextMenuStrip_Opened(object sender, EventArgs e) {
        }
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
        }
        private void OnMouseClick(object sender, MouseEventArgs e) {
        }
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
        }
        private void OnMouseDown(object sender, MouseEventArgs e) {
        }
        private void OnMouseEnter(object sender, EventArgs e) {
        }
        private void OnMouseLeave(object sender, EventArgs e) {
        }
        private void OnMouseMove(object sender, MouseEventArgs e) {
        }
        private void OnMouseUp(object sender, MouseEventArgs e) {
        }
        private void OnDragEnter(object sender, DragEventArgs e) {
        }
        private void OnDragOver(object sender, DragEventArgs e) {
        }
        private void OnDragDrop(object sender, DragEventArgs e) {
        }
        private void VehicleDispatchBoard_FormClosing(object sender, FormClosingEventArgs e) {
        }
    }
}
