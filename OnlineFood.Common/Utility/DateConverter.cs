using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Common.Utility
{
    /// <summary>
    /// تبدبل تاریخ با استفاده از  
    /// PersianCalendar
    /// </summary>
    public class DateConverter
    {
        public static DateTime GetGregorianDate(string date, out bool success, string format = null)
        {
            DateTime persianIssueDate;
            format = format ?? "dd/MM/yyyy";
            success = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out persianIssueDate);
            PersianCalendar pc = new PersianCalendar();
            DateTime gregorianIssueDate = pc.ToDateTime(persianIssueDate.Year, persianIssueDate.Month, persianIssueDate.Day, persianIssueDate.Hour, persianIssueDate.Minute, persianIssueDate.Second, persianIssueDate.Millisecond);
            return gregorianIssueDate;
        }

        public static DateTime? GetGregorianDate(string date, string format = null)
        {
            DateTime? gregorianIssueDate = null;
            if (!string.IsNullOrEmpty(date))
            {
                DateTime persianIssueDate;
                format = format ?? "dd/MM/yyyy";
                bool success = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out persianIssueDate);
                PersianCalendar pc = new PersianCalendar();
                gregorianIssueDate = success ? pc.ToDateTime(persianIssueDate.Year, persianIssueDate.Month, persianIssueDate.Day, persianIssueDate.Hour, persianIssueDate.Minute, persianIssueDate.Second, persianIssueDate.Millisecond) : gregorianIssueDate;
                return gregorianIssueDate;
            }
            return gregorianIssueDate;
        }

        //public static DateTime GetGregorianDate(string date, string format = null)
        //{
        //    DateTime persianIssueDate;
        //    format = format ?? "dd/MM/yyyy";
        //    persianIssueDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
        //    PersianCalendar pc = new PersianCalendar();
        //    DateTime gregorianIssueDate = pc.ToDateTime(persianIssueDate.Year, persianIssueDate.Month, persianIssueDate.Day, persianIssueDate.Hour, persianIssueDate.Minute, persianIssueDate.Second, persianIssueDate.Millisecond);
        //    return gregorianIssueDate;
        //}

        public static string GetPersianDate(DateTime date, string format = null)
        {
            var calendar = new PersianCalendar();
            format = format ?? "dd/MM/yyyy";
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
            var persianIssueDateFormated = persianDate.ToString(format, CultureInfo.InvariantCulture);
            return persianIssueDateFormated;
        }

        public static int GetPersianDay(DateTime date, string format = null)
        {
            var calendar = new PersianCalendar();
            format = format ?? "dd/MM/yyyy";
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
            return persianDate.Day;
        }

        public static int GetPersianMonth(DateTime date, string format = null)
        {
            var calendar = new PersianCalendar();
            format = format ?? "dd/MM/yyyy";
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
            return persianDate.Month;
        }

        public static int GetPersianYear(DateTime date, string format = null)
        {
            var calendar = new PersianCalendar();
            format = format ?? "dd/MM/yyyy";
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
            return persianDate.Year;
        }

        //public static DateTime GetDateFormat(int year, int month, int day, string format = null)
        //{
        //    var _year = year.ToString();
        //    var _month = month.ToString();
        //    var _day = day.ToString();
        //    string _birthdate = string.Format("{0}/{1}/{2}", year, month, day);
        //    DateTime birthdate = !string.IsNullOrEmpty(_birthdate) ? DateConverter.GetGregorianDate(_birthdate, out bool dateSuccess, "yyyy/M/d") : DateTime.MinValue;


        //    DateTime persianIssueDate;
        //    format = format ?? "dd/MM/yyyy";
        //    success = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out persianIssueDate);
        //    PersianCalendar pc = new PersianCalendar();
        //    DateTime gregorianIssueDate = pc.ToDateTime(persianIssueDate.Year, persianIssueDate.Month, persianIssueDate.Day, persianIssueDate.Hour, persianIssueDate.Minute, persianIssueDate.Second, persianIssueDate.Millisecond);
        //    return gregorianIssueDate;
        //}
    }


}
