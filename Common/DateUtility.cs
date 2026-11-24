/*
 * 2023-11-08
 */
using System.Globalization;

namespace Common {

    public static class DateUtility {

        private static readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private static readonly CultureInfo _cultureInfo = new("ja-JP");

        /// <summary>
        /// 静的コンストラクター
        /// </summary>
        static DateUtility() {
            _cultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
        }

        /// <summary>
        /// 年齢を計算する
        /// </summary>
        /// <param name="birthDate">生年月日（null または default(DateTime) は未設定扱い）</param>
        /// <param name="today">計算基準日（省略時は DateTime.Today）</param>
        /// <param name="defaultDate">未設定を示す既定日（省略時は default(DateTime)）</param>
        /// <returns>年齢（未設定や未来日付の場合は 0）</returns>
        public static int GetAge(DateTime? birthDate, DateTime? today = null, DateTime? defaultDate = null) {
            DateTime todayDate = (today ?? DateTime.Today).Date;
            DateTime defaultDt = defaultDate ?? default(DateTime);

            if(!birthDate.HasValue || birthDate.Value.Date == defaultDt)
                return 0;

            DateTime b = birthDate.Value.Date;

            if(b > todayDate)
                return 0;

            int age = todayDate.Year - b.Year;

            if(todayDate.Month < b.Month ||
               (todayDate.Month == b.Month && todayDate.Day < b.Day))
                age--;

            return Math.Max(age, 0);
        }

        /// <summary>
        /// 誕生日
        /// 1900/01/01の場合はstring.Emptyを返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetBirthday(DateTime dateTime) {
            return dateTime.Date != _defaultDateTime.Date
                ? dateTime.Date.ToString("yyyy/MM/dd")
                : string.Empty;
        }

        /// <summary>
        /// 雇用年月日
        /// 1900/01/01の場合はstring.Emptyを返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetEmploymentDate(DateTime dateTime) {
            return dateTime.Date != _defaultDateTime.Date
                ? dateTime.Date.ToString("yyyy/MM/dd")
                : string.Empty;
        }

        /// <summary>
        /// 勤続年数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetEmploymenteYear(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays / 365;
        }

        /// <summary>
        /// 勤続月数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetEmploymenteMonth(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays % 365 / 30;
        }

        /// <summary>
        /// 勤続日数を計算
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetEmploymenteDay(DateTime dateTime) {
            DateTime nowDate = DateTime.Now.Date;
            TimeSpan elapsedSpan = nowDate - dateTime;
            return (int)elapsedSpan.TotalDays;
        }

        /// <summary>
        /// 瞬間の日時を取得する
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeNow() {
            return DateTime.Now.ToString("yyyy年MM月dd日(ddd)　HH時mm分ss秒");
        }

        /// <summary>
        /// 和暦を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDateTimeNowJp(DateTime dateTime) {
            return dateTime.ToString("ggy年M月d日", _cultureInfo);
        }

        /// <summary>
        /// 月初日を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetBeginOfMonth(DateTime dateTime) {
            return dateTime.AddDays((dateTime.Day - 1) * -1);
        }

        /// <summary>
        /// 月末日を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetEndOfMonth(DateTime dateTime) {
            return new DateTime(dateTime.Year,
                                dateTime.Month,
                                GetDaysInMonth(dateTime));
        }

        /// <summary>
        /// 該当月の日数を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetDaysInMonth(DateTime dateTime) {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        /// <summary>
        /// 会計年度を返す
        /// </summary>
        /// <returns></returns>
        public static int GetFiscalYear() {
            return DateTime.Today.AddMonths(-3).Year;
        }

        /// <summary>
        /// 会計年度を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetFiscalYear(DateTime dateTime) {
            return dateTime.AddMonths(-3).Year;
        }

        /// <summary>
        /// 会計年度の始めの日付を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetFiscalYearStartDate(DateTime dateTime) {
            int fiscalYear = dateTime.AddMonths(-3).Year;
            return new DateTime(fiscalYear, 4, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 会計年度の始めの日付を返す
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public static DateTime GetFiscalYearStartDate(int fiscalYear) {
            return new DateTime(fiscalYear, 4, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 会計年度の最後の日付を返す
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetFiscalYearEndDate(DateTime dateTime) {
            int fiscalYear = dateTime.AddMonths(-3).Year;
            return new DateTime(fiscalYear + 1, 3, 31);
        }

        /// <summary>
        /// 会計年度の最後の日付を返す
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        public static DateTime GetFiscalYearEndDate(int fiscalYear) {
            return new DateTime(fiscalYear + 1, 3, 31);
        }

        /// <summary>
        /// GetWorkingDays
        /// 稼働日(曜日配車・第五週)かどうかを判定する
        /// true:稼働 false:休車
        /// </summary>
        /// <param name="dateTime">判定日</param>
        /// <param name="workingDays">稼働曜日</param>
        /// <param name="fiveLap">第五週フラグ</param>
        /// <returns></returns>
        public static bool GetWorkingDays(DateTime dateTime,
                                          string workingDays,
                                          bool fiveLap) {
            if(workingDays.Contains(dateTime.ToString("ddd"))) {
                if(!fiveLap && dateTime.Day > 28) {
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// string型の時刻(HH:mm)をDateTime(operationDateを元に)に変換する
        /// </summary>
        /// <param name="operationDate"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetStringTimeToDateTime(DateTime operationDate,
                                                       string time) {
            return new DateTime(
                operationDate.Year,
                operationDate.Month,
                operationDate.Day,
                DateTime.ParseExact(time, "HH:mm", null).Hour,
                DateTime.ParseExact(time, "HH:mm", null).Minute,
                DateTime.Now.Second);
        }

        /// <summary>
        /// 直近の有給起算日を取得する
        /// </summary>
        /// <param name="date">起算日</param>
        /// <returns></returns>
        public static DateTime GetPaidLeaveCommencementDate(DateTime date) {
            DateTime recentCommencementDate =
                new(DateTime.Today.Year,
                    date.Month,
                    date.Day);

            if(recentCommencementDate.Date >= DateTime.Today.Date)
                recentCommencementDate = recentCommencementDate.AddYears(-1);

            return recentCommencementDate;
        }
    }
}