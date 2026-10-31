/*
 * 2026-10-07
 */
using Microsoft.EntityFrameworkCore;

namespace Entity {
    /// <summary>
    /// EntityFramework用のキー定義
    /// </summary>
    [PrimaryKey(nameof(Id))]

    public class LoginUser {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private string _id;
        private string _userId;
        private string _userPassword;
        private string _displayName;
        private int _roleCode;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public LoginUser() {
            _id = string.Empty;
            _userId = string.Empty;
            _userPassword = string.Empty;
            _displayName = string.Empty;
            _roleCode = 0;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        public string Id {
            get => _id;
            set => _id = value;
        }
        public string UserId {
            get => _userId;
            set => _userId = value;
        }
        public string UserPassword {
            get => _userPassword;
            set => _userPassword = value;
        }
        public string DisplayName {
            get => _displayName;
            set => _displayName = value;
        }
        public int RoleCode {
            get => _roleCode;
            set => _roleCode = value;
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
