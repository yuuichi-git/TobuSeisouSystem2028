/*
 * 2026-10-05
 */
using Common;

using Dao;

using RisControls;

using VehicleDispatch;

using Vo;

namespace StartForm {
    public partial class StartFormUnkouKanri : Form {
        private readonly RsScreenForm _rsScreenForm = new();
        /// <summary>
        /// 親フォームの参照
        /// </summary>
        private Form _parentForm;
        /*
         * Vo
         */
        private InitializeVo _initializeVo = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parentForm"></param>
        public StartFormUnkouKanri(Form parentForm) {
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
                                             "ToolStripMenuItemHelp"};
            this.RsMenuStrip1.ChangeEnable(listString);
            this.RsMenuStrip1.Event_RsMenuStrip_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            /*
             * StatusStrip
             */
            RsStatusStrip1.SetDetailText("Initialize");
        }

        /// <summary>
        /// ツールストリップメニュー項目がクリックされたときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object? sender, EventArgs e) {
            if(sender is null)
                return;

            switch(((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
                case "ToolStripMenuItemDataBaseLocal":
                    RsMenuStrip1.ToolStripMenuItemDataBaseLocalFlag = !((ToolStripMenuItem)sender).Checked;
                    break;
            }
        }

        private VehicleDispatchBoard? _vehicleDispatchBoard = null;

        /// <summary>
        /// ラベルがクリックされたときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsLabel_Click(object sender, EventArgs e) {
            switch(((RsLabel)sender).Name) {
                case "RsLabelVehicleDispatchBoardAdachi":
                    if(_vehicleDispatchBoard == null || _vehicleDispatchBoard.IsDisposed) {
                        _vehicleDispatchBoard = new VehicleDispatchBoard(_initializeVo, this);
                        _rsScreenForm.SetPosition((Screen)RsComboBoxMonitor1.SelectedValue!, _vehicleDispatchBoard);
                        _vehicleDispatchBoard.Show();
                    } else {
                        MessageBox.Show("すでに起動しています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    RsStatusStrip1.ClearDetailText();
                    break;
                case "RsLabelVehicleDispatchBoardMisato":
                    if(_vehicleDispatchBoard == null || _vehicleDispatchBoard.IsDisposed) {
                        _vehicleDispatchBoard = new VehicleDispatchBoard(_initializeVo, this);
                        _rsScreenForm.SetPosition((Screen)RsComboBoxMonitor1.SelectedValue!, _vehicleDispatchBoard);
                        _vehicleDispatchBoard.Show();
                    } else {
                        MessageBox.Show("すでに起動しています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    RsStatusStrip1.ClearDetailText();
                    break;
                default:
                    RsStatusStrip1.SetDetailText("この機能は未実装です");
                    break;
            }
        }

        /// <summary>
        /// マウスがラベルに入ったときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsLabel_MouseEnter(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = Color.Red;
        }

        /// <summary>
        /// マウスがラベルから離れたときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsLabel_MouseLeave(object sender, EventArgs e) {
            ((RsLabel)sender).ForeColor = SystemColors.ControlText;
        }

        /// <summary>
        /// フォームがロードされたときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartFormUnkouKanri_LoadAsync(object sender, EventArgs e) {
            /*
             * InitializeVoを作成
             */
            _initializeVo.ManagedSpaceCode = await ConnectionDiagnosticService.GetOfficeCodeByClientIpAsync();// 管理対象スペースの識別コードを取得または設定
            _initializeVo.Screen = (Screen)RsComboBoxMonitor1.SelectedValue!;

            Dictionary<string, string> dictionary = await ConnectionDiagnosticService.DiagnoseAsync();
            /*
             * クライアント情報を表示
             */
            RsLabelClientName.Text = dictionary["HOST_NAME()"];
            RsLabelClientIpAddress.Text = RsNetwork.GetLocalIPv4();
            RsLabelDefaultGateway.Text = RsNetwork.GetDefaultGateway();
            /*
             * 接続診断結果を表示（例：サーバー名やDB名など）
             */
            RsLabelServerName.Text = dictionary["@@SERVERNAME"];
            if(dictionary["HOST_NAME()"] == dictionary["@@SERVERNAME"]) {
                RsLabelServerIpAddress.Text = "(local)";
            } else {
                RsLabelServerIpAddress.Text = dictionary["SQL Server IP"];
            }
            RsLabelDbName.Text = dictionary["DB_NAME()"];
            RsLabelDbStatus.Text = dictionary["@@VERSION"];
        }

        /// <summary>
        /// フォームが閉じられたときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartFormUnkouKanri_FormClosed(object sender, FormClosedEventArgs e) {
            _parentForm.Show();
        }
    }
}
