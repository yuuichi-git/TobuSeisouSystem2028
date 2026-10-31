/*
 * 2026-10-14
 */
using System.Data.Common;
using System.Net;
using System.Net.Sockets;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Dao {
    /// <summary>
    /// SQL Server 接続情報診断クラス
    /// </summary>
    public static class ConnectionDiagnosticService {

        /// <summary>
        /// 実際に接続している SQL Server のサーバー名（インスタンス名）を取得
        /// </summary>
        public static async Task<string> GetServerNameAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>("SELECT @@SERVERNAME AS Value")
                .FirstAsync();
        }

        /// <summary>
        /// SQL Server のホスト名（物理サーバー名）を取得
        /// </summary>
        public static async Task<string> GetHostNameAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>("SELECT HOST_NAME() AS Value")
                .FirstAsync();
        }

        /// <summary>
        /// 接続しているデータベース名を取得
        /// </summary>
        public static async Task<string> GetDatabaseNameAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>("SELECT DB_NAME() AS Value")
                .FirstAsync();
        }

        /// <summary>
        /// 接続している SQL Server のバージョン情報を取得
        /// </summary>
        public static async Task<string> GetServerVersionAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>("SELECT @@VERSION AS Value")
                .FirstAsync();
        }

        /// <summary>
        /// 接続しているログイン名を取得
        /// </summary>
        public static async Task<string> GetLoginNameAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>("SELECT ORIGINAL_LOGIN() AS Value")
                .FirstAsync();
        }

        /// <summary>
        /// DbContext が保持している接続文字列を取得
        /// </summary>
        public static async Task<string> GetConnectionStringAsync() {
            using var db = new AppDbContext();
            return db.Database.GetDbConnection().ConnectionString;
        }

        /// <summary>
        /// 接続文字列からサーバー名を解析して取得（問い合わせ不要）
        /// </summary>
        public static async Task<string> GetServerNameFromConnectionStringAsync() {
            using var db = new AppDbContext();
            DbConnection conn = db.Database.GetDbConnection();
            var builder = new SqlConnectionStringBuilder(conn.ConnectionString);
            return builder.DataSource;
        }

        /// <summary>
        /// 接続している SQL Server の IP アドレスを取得
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetServerIpAddressAsync() {
            using var db = new AppDbContext();
            return await db.Database
                .SqlQueryRaw<string>(
                    "SELECT local_net_address AS Value " +
                    "FROM sys.dm_exec_connections " +
                    "WHERE session_id = @@SPID"
                )
                .FirstAsync();
        }

        /// <summary>
        /// 接続診断結果をまとめて取得する
        /// </summary>
        public static async Task<Dictionary<string, string>> DiagnoseAsync() {
            using var db = new AppDbContext();
            var conn = db.Database.GetDbConnection();

            Dictionary<string, string> result = new();

            // 接続文字列情報（EF から取得）
            var builder = new SqlConnectionStringBuilder(conn.ConnectionString);
            result["DataSource (ConnectionString)"] = builder.DataSource;
            result["InitialCatalog"] = builder.InitialCatalog;
            result["UserID"] = builder.UserID;

            // SQL Server 問い合わせ情報（全部 AS Value に統一）
            result["@@SERVERNAME"] = await db.Database.SqlQueryRaw<string>("SELECT @@SERVERNAME AS Value").FirstAsync();
            result["HOST_NAME()"] = await db.Database.SqlQueryRaw<string>("SELECT HOST_NAME() AS Value").FirstAsync();
            result["DB_NAME()"] = await db.Database.SqlQueryRaw<string>("SELECT DB_NAME() AS Value").FirstAsync();
            result["ORIGINAL_LOGIN()"] = await db.Database.SqlQueryRaw<string>("SELECT ORIGINAL_LOGIN() AS Value").FirstAsync();
            result["@@VERSION"] = await db.Database.SqlQueryRaw<string>("SELECT @@VERSION AS Value").FirstAsync();
            result["SQL Server IP"] = await db.Database.SqlQueryRaw<string>("SELECT local_net_address AS Value " +
                                                                            "FROM sys.dm_exec_connections " +
                                                                            "WHERE session_id = @@SPID").FirstAsync();
            return result;
        }

        /// <summary>
        /// クライアントIPの第三オクテッドから営業所コードを取得
        /// </summary>
        public static async Task<int> GetOfficeCodeByClientIpAsync() {
            // クライアントIP取得
            string clientIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "0.0.0.0";

            // 第三オクテッド抽出
            var parts = clientIp.Split('.');
            if(parts.Length != 4)
                return 0; // 該当なし
            if(!int.TryParse(parts[2], out int thirdOctet))
                return 0;

            using var db = new AppDbContext();

            // ManagedSpaceMaster から第三オクテッド一致を検索
            var office = await db.ManagedSpaceMaster.Where(x => x.IpAddress.StartsWith($"192.168.{thirdOctet}.")).Select(x => new { x.Code }).FirstOrDefaultAsync();

            return office?.Code ?? 0; // 該当なしは Code=0
        }
    }
}
