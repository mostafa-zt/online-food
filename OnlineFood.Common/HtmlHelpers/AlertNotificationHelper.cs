using OnlineFood.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineFood.Common.Model;

namespace OnlineFood.Common.HtmlHelpers
{
    public static class AlertNotificationHelper
    {
        /// <summary>
        /// generate alert notification for admin layout
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static HtmlString AlertNotification(this IHtmlHelper htmlHelper, ITempDataDictionary tempData)
        {
            var alerts = tempData.Get<List<Alert>>(Alert.TempDataKey) ?? new List<Alert>();
            string str = "";
            if (alerts.Any())
            {
                str += "<script type=\"text/javascript\"> " +
                                               "$(document).ready(function () {";
                foreach (var alert in alerts)
                {
                    str += "showNotification('alert-" + alert.AlertStyle.ToString().ToLower() + "', " + "'" + alert.Message + "'" + ", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');";
                }
                str += "})" + "</script>";
            }
            tempData.Clear();
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        public static HtmlString AlertNotify(this IHtmlHelper htmlHelper, ITempDataDictionary tempData)
        {
            var alerts = tempData.Get<List<Alert>>(Alert.TempDataKey) ?? new List<Alert>();
            string str = "";
            if (alerts.Any())
            {
                str += "<script type=\"text/javascript\"> " +
                                               "$(document).ready(function () {";
                foreach (var alert in alerts)
                {
                    str += "ShowNotification('" + alert.AlertStyle.ToString().ToLower() + "', " + "'" + alert.Message + "'" + ",'" + alert.Title + "'" + ", 'top', 'center', 'animated zoomInRight', 'animated zoomOutRight');";
                }
                str += "})" + "</script>";
            }
            tempData.Clear();
            return new HtmlString(str);
        }

        /// <summary>
        /// alert panel which generate a list of meesage
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="tempData"></param>
        /// <param name="title"></param>
        /// <param name="hasClose"></param>
        /// <returns></returns>
        public static HtmlString AlertPanel(this IHtmlHelper htmlHelper, ITempDataDictionary tempData, string title, bool hasClose = false)
        {
            var alerts = tempData.Get<List<Alert>>(Alert.TempDataKey) ?? new List<Alert>();
            var dangerALerts = alerts.Where(w => w.AlertStyle == AlertStyles.Danger).ToList();
            var succesAlerts = alerts.Where(w => w.AlertStyle == AlertStyles.Success).ToList();
            string str = "";
            if (succesAlerts.Any())
            {
                str += "<div class=\"ui success message\">" +
                             (hasClose ? "<i class=\"close icon\"></i>" : "") +
                              "<div class=\"header list-info\">" +
                                            title +
                                "</div>" +
                                "<ul class=\"list-info\"> ";
                foreach (var item in succesAlerts)
                {
                    str += "<li>" + item.Message + "</li>";
                }
                str += "</ul></div>";
            }
            if (dangerALerts.Any())
            {
                str += "<div class=\"ui danger message\">" +
                             (hasClose ? "<i class=\"close icon\"></i>" : "") +
                              "<div class=\"header list-info\">" +
                                            title +
                                "</div>" +
                                "<ul class=\"list-info\"> ";
                foreach (var item in dangerALerts)
                {
                    str += "<li>" + item.Message + "</li>";
                }
                str += "</ul></div>";
            }
            tempData.Clear();
            return new HtmlString(str);
        }
    }
}
