/*
 * 2026-10-14
 */
using Common;

using Dao;

using Entity;

using Microsoft.EntityFrameworkCore;

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
         * Entity
         */
        private List<SetMaster> _listSetMaster;
        private List<CarMaster> _listCarMaster;
        private List<StaffMaster> _listStaffMaster;
        /*
         * Vo
         */
        private InitializeVo _initializeVo;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="initializeVo"></param>
        /// <param name="parentForm"></param>
        public VehicleDispatchBoard(InitializeVo initializeVo, Form parentForm) {
            /*
             * Entity
             */
            _listSetMaster = new();
            _listCarMaster = new();
            _listStaffMaster = new();

            _initializeVo = initializeVo;
            _parentForm = parentForm;
            /*
             * Event
             * 配下のSetControlのイベントをBoardで受け取るため、Boardのイベントを登録する
             */
            _board = new();
            _board.Board_ContextMenuStrip_Opened += ContextMenuStripOpened!;
            _board.Board_ToolStripMenuItem_Click += ToolStripMenuItemClick!;
            _board.Board_MouseClick += OnMouseClick!;
            _board.Board_MouseDoubleClick += OnMouseDoubleClick!;
            _board.Board_MouseDown += OnMouseDown!;
            _board.Board_MouseEnter += OnMouseEnter!;
            _board.Board_MouseLeave += OnMouseLeave!;
            _board.Board_MouseMove += OnMouseMove!;
            _board.Board_MouseUp += OnMouseUp!;

            _board.Board_DragEnter += OnDragEnter!;
            _board.Board_DragOver += OnDragOver!;
            _board.Board_DragDrop += OnDragDrop!;
            /*
             * InitializeControls
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",           // ファイル
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemView",           // 表示
                                             "ToolStripMenuItemViewProper",
                                             "ToolStripMenuItemEdit",           // 編集
                                             "ToolStripMenuItemUpdateTaitou",
                                             "ToolStripMenuItemHelp"};          // ヘルプ
            RsMenuStrip1.ChangeEnable(listString);
            RsMenuStrip1.Event_RsMenuStrip_ToolStripMenuItem_Click += ToolStripMenuItemClick!;

            RsDateTimeOperationDate.SetToday();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleDispatchBoard_Load(object sender, EventArgs e) {
            try {
                using AppDbContext db = new();
                /*
                 * SetMaster
                 */
                _listSetMaster = db.SetMaster.AsNoTracking().ToList();
                /*
                 * CarMaster
                 * Entityと同一にすること
                 */
                _listCarMaster = db.CarMaster.Select(x => new CarMaster {
                    CarCode = x.CarCode,
                    ClassificationCode = x.ClassificationCode,
                    RegistrationNumber = x.RegistrationNumber,
                    RegistrationNumber1 = x.RegistrationNumber1,
                    RegistrationNumber2 = x.RegistrationNumber2,
                    RegistrationNumber3 = x.RegistrationNumber3,
                    RegistrationNumber4 = x.RegistrationNumber4,
                    GarageCode = x.GarageCode,
                    DoorNumber = x.DoorNumber,
                    RegistrationDate = x.RegistrationDate,
                    FirstRegistrationDate = x.FirstRegistrationDate,
                    CarKindCode = x.CarKindCode,
                    DisguiseKind1 = x.DisguiseKind1,
                    DisguiseKind2 = x.DisguiseKind2,
                    DisguiseKind3 = x.DisguiseKind3,
                    CarUse = x.CarUse,
                    OtherCode = x.OtherCode,
                    ShapeCode = x.ShapeCode,
                    ManufacturerCode = x.ManufacturerCode,
                    Capacity = x.Capacity,
                    MaximumLoadCapacity = x.MaximumLoadCapacity,
                    VehicleWeight = x.VehicleWeight,
                    TotalVehicleWeight = x.TotalVehicleWeight,
                    VehicleNumber = x.VehicleNumber,
                    Length = x.Length,
                    Width = x.Width,
                    Height = x.Height,
                    FfAxisWeight = x.FfAxisWeight,
                    FrAxisWeight = x.FrAxisWeight,
                    RfAxisWeight = x.RfAxisWeight,
                    RrAxisWeight = x.RrAxisWeight,
                    Version = x.Version,
                    MotorVersion = x.MotorVersion,
                    TotalDisplacement = x.TotalDisplacement,
                    TypesOfFuel = x.TypesOfFuel,
                    VersionDesignateNumber = x.VersionDesignateNumber,
                    CategoryDistinguishNumber = x.CategoryDistinguishNumber,
                    OwnerName = x.OwnerName,
                    OwnerAddress = x.OwnerAddress,
                    UserName = x.UserName,
                    UserAddress = x.UserAddress,
                    BaseAddress = x.BaseAddress,
                    ExpirationDate = x.ExpirationDate,
                    Remarks = x.Remarks,
                    //CompulsoryAutomobileLiabilityInsuranceOld = x.CompulsoryAutomobileLiabilityInsuranceOld,
                    //CompulsoryAutomobileLiabilityInsuranceNew = x.CompulsoryAutomobileLiabilityInsuranceNew,
                    //VehicleInspectionCertificatePicture = x.VehicleInspectionCertificatePicture,
                    //RecordDetailsPicture = x.RecordDetailsPicture,
                    EmergencyVehicleFlag = x.EmergencyVehicleFlag,
                    EmergencyVehicleDate = x.EmergencyVehicleDate,
                    DigitalTachographFlag = x.DigitalTachographFlag,
                    DigitalTachographType = x.DigitalTachographType,
                    CameraFront = x.CameraFront,
                    CameraBack = x.CameraBack,
                    CameraLeftBack = x.CameraLeftBack,
                    CameraRightBack = x.CameraRightBack,
                    CameraLeftUnder = x.CameraLeftUnder,
                    CameraRoomMic = x.CameraRoomMic,
                    CameraCanopy = x.CameraCanopy,
                    //InsertPcName = x.InsertPcName,
                    //InsertYmdHms = x.InsertYmdHms,
                    //UpdatePcName = x.UpdatePcName,
                    //UpdateYmdHms = x.UpdateYmdHms,
                    //DeletePcName = x.DeletePcName,
                    //DeleteYmdHms = x.DeleteYmdHms,
                    DeleteFlag = x.DeleteFlag
                }).ToList();
                /*
                 * StaffMaster
                 * Entityと同一にすること
                 */
                _listStaffMaster = db.StaffMaster.Select(x => new StaffMaster {
                    StaffCode = x.StaffCode,
                    UnionCode = x.UnionCode,
                    Belongs = x.Belongs,
                    VehicleDispatchTarget = x.VehicleDispatchTarget,
                    JobForm = x.JobForm,
                    Occupation = x.Occupation,
                    NameKana = x.NameKana,
                    Name = x.Name,
                    OtherNameKana = x.OtherNameKana,
                    OtherName = x.OtherName,
                    DisplayName = x.DisplayName,
                    Gender = x.Gender,
                    BirthDate = x.BirthDate,
                    EmploymentDate = x.EmploymentDate,
                    CurrentAddress = x.CurrentAddress,
                    BeforeChangeAddress = x.BeforeChangeAddress,
                    Remarks = x.Remarks,
                    TelephoneNumber = x.TelephoneNumber,
                    CellphoneNumber = x.CellphoneNumber,
                    //Picture = x.Picture,
                    //StampPicture = x.StampPicture,
                    BloodType = x.BloodType,
                    SelectionDate = x.SelectionDate,
                    NotSelectionDate = x.NotSelectionDate,
                    NotSelectionReason = x.NotSelectionReason,
                    ContractFlag = x.ContractFlag,
                    ContractDate = x.ContractDate,
                    PaidLeaveFlag = x.PaidLeaveFlag,
                    PaidLeaveReferenceDate = x.PaidLeaveReferenceDate,
                    PaidLeaveCommencementDate = x.PaidLeaveCommencementDate,
                    RetirementFlag = x.RetirementFlag,
                    RetirementDate = x.RetirementDate,
                    RetirementNote = x.RetirementNote,
                    DeathDate = x.DeathDate,
                    DeathNote = x.DeathNote,
                    LegalTwelveItemFlag = x.LegalTwelveItemFlag,
                    MedicalCheckupFlag = x.MedicalCheckupFlag,
                    ToukanpoFlag = x.ToukanpoFlag,
                    RiskAssessmentFlag = x.RiskAssessmentFlag,
                    UrgentTelephoneNumber = x.UrgentTelephoneNumber,
                    UrgentTelephoneMethod = x.UrgentTelephoneMethod,
                    HealthInsuranceDate = x.HealthInsuranceDate,
                    HealthInsuranceNumber = x.HealthInsuranceNumber,
                    HealthInsuranceNote = x.HealthInsuranceNote,
                    WelfarePensionDate = x.WelfarePensionDate,
                    WelfarePensionNumber = x.WelfarePensionNumber,
                    WelfarePensionNote = x.WelfarePensionNote,
                    EmploymentInsuranceDate = x.EmploymentInsuranceDate,
                    EmploymentInsuranceNumber = x.EmploymentInsuranceNumber,
                    EmploymentInsuranceNote = x.EmploymentInsuranceNote,
                    WorkerAccidentInsuranceDate = x.WorkerAccidentInsuranceDate,
                    WorkerAccidentInsuranceNumber = x.WorkerAccidentInsuranceNumber,
                    WorkerAccidentInsuranceNote = x.WorkerAccidentInsuranceNote,
                    //InsertPcName = x.InsertPcName,
                    //InsertYmdHms = x.InsertYmdHms,
                    //UpdatePcName = x.UpdatePcName,
                    //UpdateYmdHms = x.UpdateYmdHms,
                    //DeletePcName = x.DeletePcName,
                    //DeleteYmdHms = x.DeleteYmdHms,
                    DeleteFlag = x.DeleteFlag
                }).ToList();

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            AddBoard();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RsButton_Click(object sender, EventArgs e) {
            switch(((RsButton)sender).Name) {
                case "RsButtonUpdate": {
                    /*
                     * Board上のSetControlをDisposeする
                     */
                    _board.RemoveAllControls();
                    /*
                     * DBからVehicleDispatchHeadを取得する
                     */
                    using AppDbContext db = new();
                    var listVehicleDispatchHead = db.VehicleDispatchHead.AsNoTracking()
                                                                        .Include(x => x.ListVehicleDispatchBody)
                                                                        .Where(x => x.FinancialYear == DateUtility.GetFiscalYear(RsDateTimeOperationDate.GetDate()))
                                                                        .OrderBy(x => x.CellNumber)
                                                                        .ToList();
                    /*
                     * 曜日を作成する
                     */
                    string dayOfWeek = RsDateTimeOperationDate.GetDate().DayOfWeek switch{
                        DayOfWeek.Sunday    => "日",
                        DayOfWeek.Monday    => "月",
                        DayOfWeek.Tuesday   => "火",
                        DayOfWeek.Wednesday => "水",
                        DayOfWeek.Thursday  => "木",
                        DayOfWeek.Friday    => "金",
                        DayOfWeek.Saturday  => "土",
                        _ => throw new NotImplementedException(),
                    };
                    /*
                     * BoardにSetControlを追加する
                     */
                    for(int i = 0; i < _maxSetControls; i++) {
                        var vehicleDispatchHead = listVehicleDispatchHead.Single(x => x.CellNumber == i);
                        /*
                         * StaffMasterをList化する(StaffCode1～4の順番に格納)
                         */
                        var body = vehicleDispatchHead.ListVehicleDispatchBody.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
                        List<StaffMaster?> listStaffMaster = new();
                        if(body != null) {
                            int[] staffCodes = [body.StaffCode1,
                                                body.StaffCode2,
                                                body.StaffCode3,
                                                body.StaffCode4];
                            listStaffMaster = staffCodes.Select(code => _listStaffMaster.FirstOrDefault(x => x.StaffCode == code)).ToList();
                        }
                        /*
                         * Controlを追加する
                         */
                        _board.AddSetControl(i,
                                             vehicleDispatchHead.VehicleDispatchFlag,
                                             vehicleDispatchHead.Purpose,
                                             _listSetMaster.FirstOrDefault(x => x.SetCode == vehicleDispatchHead.SetCode)?.NumberOfPeople ?? 0,
                                             _listSetMaster.FirstOrDefault(x => x.SetCode == vehicleDispatchHead.SetCode),
                                             _listCarMaster.FirstOrDefault(car => vehicleDispatchHead.ListVehicleDispatchBody.Any(body => body.DayOfWeek == dayOfWeek && body.CarCode == car.CarCode)),
                                             listStaffMaster
                                             );
                        /*
                         * Purpose == Trueの場合、SetControlを2列分配置するため、iを1つ増やす
                         */
                        if(vehicleDispatchHead.Purpose)
                            i++;
                    }
                }
                break;
            }
        }

        private void AddBoard() {
            /*
             * BoardをTableLayoutPanelに追加
             */
            RsSplitContainer1.Panel1.Controls.Add(_board);
        }

        //----------------------------------------------------------------------------------
        //
        // Event
        //
        //----------------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripOpened(object sender, EventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemClick(object sender, EventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            switch(sender) {
                case SetControl setControl:
                    MessageBox.Show("SetControl-OnMouseClick");
                    break;
                case SetLabel setLabel:
                    MessageBox.Show("SetLabel-OnMouseClick");
                    break;
                case CarLabel carLabel:
                    MessageBox.Show("CarLabel-OnMouseClick");
                    break;
                case StaffLabel staffLabel:
                    MessageBox.Show("StaffLabel-OnMouseClick");
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            switch(sender) {
                case SetControl setControl:
                    MessageBox.Show("SetControl-OnMouseDoubleClick");
                    break;
                case SetLabel setLabel:
                    MessageBox.Show("SetLabel-OnMouseDoubleClick");
                    break;
                case CarLabel carLabel:
                    MessageBox.Show("CarLabel-OnMouseDoubleClick");
                    break;
                case StaffLabel staffLabel:
                    MessageBox.Show("StaffLabel-OnMouseDoubleClick");
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, EventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {

        }

        /// <summary>
        /// ドラッグ中のデータが、そのコントロールの領域に入った瞬間に呼ばれるイベント
        /// </summary>
        /// <param name="sender">Drag中のLabelが入っている</param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragOver(object sender, DragEventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleDispatchBoard_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
