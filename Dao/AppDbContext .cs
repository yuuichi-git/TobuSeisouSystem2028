/*
 * 2026-10-07
 */
using System.Security.Cryptography;
using System.Text;

using Dao.Properties;

using Entity;

using Microsoft.EntityFrameworkCore;

namespace Dao {

    public class AppDbContext : DbContext {
        /*
         * 参照するテーブルを定義する
         */
        public DbSet<LoginUser> LoginUser => Set<LoginUser>();
        public DbSet<RoleMaster> RoleMaster => Set<RoleMaster>();
        public DbSet<ManagedSpaceMaster> ManagedSpaceMaster => Set<ManagedSpaceMaster>();
        public DbSet<SetMaster> SetMaster => Set<SetMaster>();
        public DbSet<CarMaster> CarMaster => Set<CarMaster>();
        public DbSet<StaffMaster> StaffMaster => Set<StaffMaster>();
        public DbSet<LicenseMaster> LicenseMaster => Set<LicenseMaster>();
        public DbSet<StaffHistory> StaffHistory => Set<StaffHistory>();
        public DbSet<StaffExperience> StaffExperience => Set<StaffExperience>();
        public DbSet<StaffFamily> StaffFamily => Set<StaffFamily>();
        public DbSet<StaffMedicalExamination> StaffMedicalExamination => Set<StaffMedicalExamination>();
        public DbSet<StaffCarViolate> StaffCarViolate => Set<StaffCarViolate>();
        public DbSet<StaffEducate> StaffEducate => Set<StaffEducate>();
        public DbSet<StaffProper> StaffProper => Set<StaffProper>();
        public DbSet<StaffPunishment> StaffPunishment => Set<StaffPunishment>();
        public DbSet<VehicleDispatchHead> VehicleDispatchHead => Set<VehicleDispatchHead>();
        public DbSet<VehicleDispatchBody> VehicleDispatchBody => Set<VehicleDispatchBody>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer("Server=" + Resources.DataBaseServer + ";" +
                                 "Database=" + Resources.DataBaseName + ";" +
                                 "User ID=" + Resources.UserName + ";" +
                                 "Password=" + Resources.UserPassword + ";" +
                                 "MultipleActiveResultSets=True;" +
                                 "Encrypt=False;" +
                                 "TrustServerCertificate=False;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<LoginUser>().HasKey(u => u.Id);
            modelBuilder.Entity<LoginUser>().Property(u => u.UserId).HasMaxLength(50);
            modelBuilder.Entity<LoginUser>().Property(u => u.UserPassword).HasMaxLength(64); // SHA256
            modelBuilder.Entity<LoginUser>().Property(u => u.DisplayName).HasMaxLength(100);

        }
    }

    public static class LoginService {
        /// <summary>
        /// ユーザー認証を行う
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<LoginUser?> AuthenticateAsync(string userId, string password) {
            string hash = ComputeSha256(password);
            using var db = new AppDbContext();
            var user = await db.LoginUser.Where(u => u.UserId == userId && u.UserPassword == hash && u.DeleteFlag == false).FirstOrDefaultAsync();
            return user;   // 成功なら LoginUser、失敗なら null
        }
        /// <summary>
        /// パスワードをSHA256でハッシュ化する
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        private static string ComputeSha256(string raw) {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));

            StringBuilder sb = new();
            foreach(byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// ユーザーを登録する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="displayName"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static async Task RegisterUserAsync(string userId, string password, string displayName, int roleCode) {
            string hash = ComputeSha256(password);
            using var db = new AppDbContext();
            LoginUser loginUser = new LoginUser{
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                UserPassword = hash,
                DisplayName = displayName,
                RoleCode = roleCode,
                InsertPcName = Environment.MachineName,
                InsertYmdHms = DateTime.Now,
                DeleteFlag = false
            };
            db.LoginUser.Add(loginUser);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// ユーザー情報を更新する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="displayName"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task UpdateUserAsync(string userId, string? password, string displayName, int roleCode) {
            using var db = new AppDbContext();
            var user = await db.LoginUser.Where(x => x.UserId == userId && !x.DeleteFlag).FirstOrDefaultAsync();
            if(user == null)
                throw new Exception("指定されたユーザーが存在しません。");
            /*
             * パスワードは空なら変更しない
             */
            if(!string.IsNullOrWhiteSpace(password))
                user.UserPassword = ComputeSha256(password);
            user.DisplayName = displayName;
            user.RoleCode = roleCode;
            user.UpdatePcName = Environment.MachineName;
            user.UpdateYmdHms = DateTime.Now;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// ユーザーを削除する（論理削除）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task DeleteUserAsync(string userId) {
            using var db = new AppDbContext();
            var user = await db.LoginUser.Where(x => x.UserId == userId && !x.DeleteFlag).FirstOrDefaultAsync();
            if(user == null)
                throw new Exception("指定されたユーザーが存在しません。");
            user.DeletePcName = Environment.MachineName;
            user.DeleteYmdHms = DateTime.Now;
            user.DeleteFlag = true;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// パスワードの強度をチェックする
        /// </summary>
        /// <param name="password"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsStrongPassword(string password, out string message) {
            if(password.Length < 8) {
                message = "パスワードは8文字以上で入力してください。";
                return false;
            }

            if(!password.Any(char.IsLetter)) {
                message = "パスワードに英字を含めてください。";
                return false;
            }

            if(!password.Any(char.IsDigit)) {
                message = "パスワードに数字を含めてください。";
                return false;
            }

            if(!password.Any(ch => !char.IsLetterOrDigit(ch))) {
                message = "パスワードに記号を含めてください。";
                return false;
            }

            message = "";
            return true;
        }

        /// <summary>
        /// パスワードの強度を判定する
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetPasswordStrength(string password) {
            if(string.IsNullOrEmpty(password))
                return "未入力";

            int score = 0;

            if(password.Length >= 8)
                score++;
            if(password.Any(char.IsLetter))
                score++;
            if(password.Any(char.IsDigit))
                score++;
            if(password.Any(ch => !char.IsLetterOrDigit(ch)))
                score++;

            return score switch {
                <= 1 => "弱い",
                2 => "普通",
                >= 3 => "強い"
            };
        }
    }
}
