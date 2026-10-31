/*
 * 2026-10-26
 */
using Microsoft.EntityFrameworkCore;

namespace Entity {
    /// <summary>
    /// EntityFramework用のキー定義
    /// </summary>
    [PrimaryKey(nameof(Id))]

    public class StaffHistory {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private string _id;
        private int _staffCode;
        private DateTime _historyDate;
        private string _companyName;
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
        public StaffHistory() {
            _id = string.Empty;
            _staffCode = 0;
            _historyDate = _defaultDateTime;
            _companyName = string.Empty;
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
        /// 入社日
        /// </summary>
        public DateTime HistoryDate {
            get => _historyDate;
            set => _historyDate = value;
        }
        /// <summary>
        /// 在籍会社名
        /// </summary>
        public string CompanyName {
            get => _companyName;
            set => _companyName = value;
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
