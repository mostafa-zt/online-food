using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Common.Model
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";
        public AlertStyles AlertStyle { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public bool Dismissable { get; set; }
    }
    public enum AlertStyles
    {
        Success,
        Info,
        Warning,
        Danger
    }
}
