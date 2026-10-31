/*
 * 2026-10-26
 */
using Microsoft.EntityFrameworkCore;

namespace Entity {
    /// <summary>
    /// EntityFramework用のキー定義
    /// </summary>
    [PrimaryKey(nameof(Id))]

    public class StaffMedicalExamination {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private string _id;
        private int _staffCode;
        private DateTime _medicalExaminationDate;
        private string _medicalInstitutionName;
        private string _medicalExaminationNote;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StaffMedicalExamination() {
            _id = string.Empty;
            _staffCode = 0;
            _medicalExaminationDate = _defaultDateTime;
            _medicalInstitutionName = string.Empty;
            _medicalExaminationNote = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }
        /// <summary>
        /// 一意に識別する文字列を取得または設定します。
        /// </summary>
        public string Id {
            get => _id;
            set => _id = value;
        }
        /// <summary>
        /// 従業員コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 健診実施日
        /// </summary>
        public DateTime MedicalExaminationDate {
            get => _medicalExaminationDate;
            set => _medicalExaminationDate = value;
        }
        /// <summary>
        /// 受診機関名
        /// </summary>
        public string MedicalInstitutionName {
            get => _medicalInstitutionName;
            set => _medicalInstitutionName = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string MedicalExaminationNote {
            get => _medicalExaminationNote;
            set => _medicalExaminationNote = value;
        }
        public string InsertPcName {
            get => _insertPcName;
            set => _insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => _insertYmdHms;
            set => _insertYmdHms = value;
        }
        public string UpdatePcName {
            get => _updatePcName;
            set => _updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => _updateYmdHms;
            set => _updateYmdHms = value;
        }
        public string DeletePcName {
            get => _deletePcName;
            set => _deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => _deleteYmdHms;
            set => _deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => _deleteFlag;
            set => _deleteFlag = value;
        }
    }
}
