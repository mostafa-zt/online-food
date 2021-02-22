using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineFood.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Common.Utility
{
    public class TimeManagerForWorkingHour
    {
        private static readonly Lazy<IDictionary<TimeSpan, string>> _mapping = new Lazy<IDictionary<TimeSpan, string>>(BuildMapping);
        //private static readonly Lazy<IDictionary<TimeSpan, string>> _mappingsWorkingHoursMM = new Lazy<IDictionary<TimeSpan, string>>(BuildMappingsWorkingHoursMM);

        private static IDictionary<TimeSpan, string> BuildMapping()
        {
            var mappings = new Dictionary<TimeSpan, string>() {
                {TimeSpan.Parse("01:00"), "1 AM"},
                {TimeSpan.Parse("01:30"), "1:20 AM"},
                {TimeSpan.Parse("02:00"), "2:00 AM"},
                {TimeSpan.Parse("02:30"), "2:30 AM"},
                {TimeSpan.Parse("03:00"), "3:00 AM"},
                {TimeSpan.Parse("03:30"), "3:30 AM"},
                {TimeSpan.Parse("04:00"), "4:00 AM"},
                {TimeSpan.Parse("04:30"), "4:30 AM"},
                {TimeSpan.Parse("05:00"), "5:00 AM"},
                {TimeSpan.Parse("05:30"), "5:30 AM"},
                {TimeSpan.Parse("06:00"), "6:00 AM"},
                {TimeSpan.Parse("06:30"), "6:30 AM"},
                {TimeSpan.Parse("07:00"), "7:00 AM"},
                {TimeSpan.Parse("07:30"), "7:30 AM"},
                {TimeSpan.Parse("08:00"), "8:00 AM"},
                {TimeSpan.Parse("08:30"), "8:30 AM"},
                {TimeSpan.Parse("09:00"), "9:00 AM"},
                {TimeSpan.Parse("09:30"), "9:30 AM"},
                {TimeSpan.Parse("10:00"), "10:00 AM"},
                {TimeSpan.Parse("10:30"), "10:30 AM"},
                {TimeSpan.Parse("11:00"), "11:00 AM"},
                {TimeSpan.Parse("11:30"), "11:30 AM"},
                {TimeSpan.Parse("12:00"), "12:00 PM"},
                {TimeSpan.Parse("12:30"), "12:30 PM"},
                {TimeSpan.Parse("13:00"), "1:00 PM"},
                {TimeSpan.Parse("13:30"), "1:30 PM"},
                {TimeSpan.Parse("14:00"), "2:00 PM"},
                {TimeSpan.Parse("14:30"), "2:30 PM"},
                {TimeSpan.Parse("15:00"), "3:00 PM"},
                {TimeSpan.Parse("15:30"), "3:30 PM"},
                {TimeSpan.Parse("16:00"), "4:00 PM"},
                {TimeSpan.Parse("16:30"), "4:30 PM"},
                {TimeSpan.Parse("17:00"), "5:00 PM"},
                {TimeSpan.Parse("17:30"), "5:30 PM"},
                {TimeSpan.Parse("18:00"), "6:00 PM"},
                {TimeSpan.Parse("18:30"), "6:30 PM"},
                {TimeSpan.Parse("19:00"), "7:00 PM"},
                {TimeSpan.Parse("19:30"), "7:30 PM"},
                {TimeSpan.Parse("20:00"), "8:00 PM"},
                {TimeSpan.Parse("20:30"), "8:30 PM"},
                {TimeSpan.Parse("21:00"), "9:00 PM"},
                {TimeSpan.Parse("21:30"), "9:30 PM"},
                {TimeSpan.Parse("22:00"), "10:00 PM"},
                {TimeSpan.Parse("22:30"), "10:30 PM"},
                {TimeSpan.Parse("23:00"), "11:00 PM"},
                {TimeSpan.Parse("23:30"), "11:30 PM"},
                {TimeSpan.Parse("00:00"), "12:00 PM"},
                {TimeSpan.Parse("00:30"), "12:30 AM"}
                };

            //var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating
            //foreach (var mapping in cache)
            //{
            //    if (!mappings.ContainsKey(mapping.Key))
            //    {
            //        mappings.Add(mapping.Key, mapping.Value);
            //    }
            //}

            return mappings;
        }
        //private static IDictionary<TimeSpan, string> BuildMappingsWorkingHoursMM()
        //{
        //    var mappings = new Dictionary<TimeSpan, string>() {
        //        {TimeSpan.Parse("00:05"),"5 دقیقه"},
        //        {TimeSpan.Parse("00:10"), "10 دقیقه"},
        //        {TimeSpan.Parse("00:15"), "15 دقیقه"},
        //        {TimeSpan.Parse("00:20"), "20 دقیقه"},
        //        {TimeSpan.Parse("00:25"), "25 دقیقه"},
        //        {TimeSpan.Parse("00:30"), "30 دقیقه"},
        //        {TimeSpan.Parse("00:35"), "35 دقیقه"},
        //        {TimeSpan.Parse("00:40"), "40 دقیقه"},
        //        {TimeSpan.Parse("00:45"), "45 دقیقه"},
        //        {TimeSpan.Parse("00:50"), "50 دقیقه"},
        //        {TimeSpan.Parse("00:55"), "55 دقیقه"},
        //    };
        //    return mappings;
        //}

        public static string GetTitle(TimeSpan key)
        {
            string hourTitle;
            return _mapping.Value.TryGetValue(key, out hourTitle) ? hourTitle : "";
        }
        public static string GetTitle(string key)
        {
            string hourTitle;
            return _mapping.Value.TryGetValue(ToTimeSpan(key), out hourTitle) ? hourTitle : "";
        }

        public static TimeSpan GetValue(string title)
        {
            var found = _mapping.Value.Where(w => w.Value == title).FirstOrDefault();
            return found.Key;
        }

        public static int GetIndex(TimeSpan key)
        {
            return _mapping.Value.ToList().FindIndex(x => x.Key == key);
        }

        public static IEnumerable<SelectListItem> GetTimes()
        {
            return _mapping.Value.Select(s => new SelectListItem() { Text = s.Value, Value = s.Key.ToString() }).ToList();
        }

        public static IEnumerable<SelectListItem> GetToTimes(TimeSpan startTime)
        {
            var selectedindex = GetIndex(startTime);
            selectedindex = _mapping.Value.ToList().Last().Key == startTime ? selectedindex : selectedindex + 1;
            return _mapping.Value.Skip(selectedindex).Select(s => new SelectListItem() { Text = s.Value, Value = s.Key.ToString() }).ToList();
        }

        public static TimeSpan GetDefaultTime()
        {
            return _mapping.Value.First().Key;
        }

        public static TimeSpan ToTimeSpan(string time)
        {
            TimeSpan timespan;
            return TimeSpan.TryParse(time, out timespan) ? timespan : TimeSpan.MinValue;
        }


        public static string ToWorkingHour(TimeSpan fromTime, TimeSpan toTime)
        {
            return string.Format("from {0} to {1}", GetTitle(fromTime), GetTitle(toTime));
        }
    }

    public class TimeManagerForDeliveryTime
    {
        private static readonly Lazy<IDictionary<TimeSpan, string>> _mapping = new Lazy<IDictionary<TimeSpan, string>>(BuildMappings);
        //private static readonly Lazy<IDictionary<TimeSpan, string>> _mappingsMinutes = new Lazy<IDictionary<TimeSpan, string>>(BuildMappingsMM);

        private static IDictionary<TimeSpan, string> BuildMappings()
        {
            var mappings = new Dictionary<TimeSpan, string>() {
             {TimeSpan.Parse("00:05"),"5 minutes"},
             {TimeSpan.Parse("00:10"), "10 minutes"},
             {TimeSpan.Parse("00:15"), "15 minutes"},
             {TimeSpan.Parse("00:20"), "20 minutes"},
             {TimeSpan.Parse("00:25"), "25 minutes"},
             {TimeSpan.Parse("00:30"), "30 minutes"},
             {TimeSpan.Parse("00:35"), "35 minutes"},
             {TimeSpan.Parse("00:40"), "40 minutes"},
             {TimeSpan.Parse("00:45"), "45 minutes"},
             {TimeSpan.Parse("00:50"), "50 minutes"},
             {TimeSpan.Parse("00:55"), "55 minutes"},
             {TimeSpan.Parse("01:00"), "1 hour"},
             {TimeSpan.Parse("01:30"), "1:30"},
             {TimeSpan.Parse("02:00"), "2 hours"},
             {TimeSpan.Parse("02:30"), "2:30"},
             {TimeSpan.Parse("03:00"), "3 hours"},
             {TimeSpan.Parse("03:30"), "3.30"},
             {TimeSpan.Parse("04:00"), "4 hours"},
             {TimeSpan.Parse("04:30"), "4:30"},
             {TimeSpan.Parse("05:00"), "5 hours"},
             {TimeSpan.Parse("05:30"), "5:30"},
             {TimeSpan.Parse("06:00"), "6 hours"},
            };
            return mappings;
        }
        public static string GetTitle(TimeSpan key)
        {
            string hourTitle;
            return _mapping.Value.TryGetValue(key, out hourTitle) ? hourTitle : "";
        }
        public static string GetTitle(string key)
        {
            string hourTitle;
            return _mapping.Value.TryGetValue(ToTimeSpan(key), out hourTitle) ? hourTitle : "";
        }

        public static TimeSpan GetValue(string title)
        {
            var found = _mapping.Value.Where(w => w.Value == title).FirstOrDefault();
            return found.Key;
        }

        public static int GetIndex(TimeSpan key)
        {
            return _mapping.Value.ToList().FindIndex(x => x.Key == key);
        }

        public static IEnumerable<SelectListItem> GetTimes()
        {
            return _mapping.Value.Select(s => new SelectListItem() { Text = s.Value, Value = s.Key.ToString() }).ToList();
        }

        public static IEnumerable<SelectListItem> GetToTimes(TimeSpan startTime)
        {
            var selectedindex = GetIndex(startTime);
            selectedindex = _mapping.Value.ToList().Last().Key == startTime ? selectedindex : selectedindex + 1;
            return _mapping.Value.Skip(selectedindex).Select(s => new SelectListItem() { Text = s.Value, Value = s.Key.ToString() }).ToList();
        }

        public static TimeSpan GetDefaultTime()
        {
            return _mapping.Value.First().Key;
        }

        public static TimeSpan ToTimeSpan(string time)
        {
            TimeSpan timespan;
            return TimeSpan.TryParse(time, out timespan) ? timespan : TimeSpan.MinValue;
        }

        public static string ToDeliveryTime(TimeSpan fromTime, TimeSpan toTime)
        {
            return string.Format("from {0} to {1}", GetTitle(fromTime), GetTitle(toTime));
        }

    }
}
