using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Common.Utility
{
    public class HtmlGenerator
    {
        public static Microsoft.AspNetCore.Html.HtmlString GeneralCommand(int id, bool hasRemove = true, bool hasEdit = true, bool hasDetail = false, bool hasMenu = false, bool hasGallery = false)
        {
            string str = "";
            str += "<div class=\"ui teal buttons\">" +
                     "<div class=\"ui button btn-tbl-edit" + (!hasEdit ? " disabled" : null) + "\"" + "data-id=\"" + id + "\" ><i class=\"edit icon\"></i> Edit </div>" +
                     "<div class=\"ui dropdown icon button" + (!hasRemove && !hasDetail ? " disabled" : null) + "\">" +
                       "<i class=\"dropdown icon\"></i>" +
                       "<div class=\"menu\"> " +
                         //"<div class=\"item btn-tbl-edit\" data-id=\"" + id + "\"><i class=\"edit icon\"></i> ویرایش </div>" +
                         "<div class=\"item btn-tbl-delete" + (!hasRemove ? " disabled" : null) + "\"" + "data-id=\"" + id + "\"><i class=\"icon-table-dropdown im im-icon-Trash-withMen\"></i> Delete </div>" +
                         "<div class=\"item btn-tbl-detail" + (!hasDetail ? " disabled" : null) + "\"" + "data-id=\"" + id + "\"><i class=\"icon-table-dropdown im im-icon-Eye\"></i>  Details </div>" +
                         "<div class=\"item btn-tbl-menu" + (!hasMenu ? " disabled" : null) + "\"" + "data-id=\"" + id + "\"><i class=\"icon-table-dropdown im im-icon-Receipt-3\"></i>  Menu </div>" +
                         "<div class=\"item btn-tbl-gallery" + (!hasGallery ? " disabled" : null) + "\"" + "data-id=\"" + id + "\"><i class=\"icon-table-dropdown im im-icon-Folder-Pictures\"></i>  Gallery </div>" +
                       "</div>" +
                     "</div>" +
                   "</div>";

            str += "<script type=\"text/javascript\"> " +
                         "$(document).ready(function () {";

            str += "$('.ui.dropdown').dropdown();";

            str += "})" + "</script>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون اطلاعات حقیقی
        /// </summary>
        /// <param name="gender">جنسیت</param>
        /// <param name="firstname">نام</param>
        /// <param name="lastname">نام خانوادگی</param>
        /// <param name="birthCertificateNumber">شماره شناسنامه</param>
        /// <param name="nationalCode">کد ملی</param>
        /// <param name="birthdate">تاریخ تولد</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString PersonalColumn(string gender, string firstname, string lastname, string birthCertificateNumber, string nationalCode, string birthdate)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "نام" + "</span>" + "<span class=\"lbl-tbl-text\">" + TextGenerator.FullnameWithGender(firstname, lastname, gender) + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ش.ش" + "</span>" + "<span class=\"lbl-tbl-text\">" + birthCertificateNumber + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ک.م" + "</span>" + "<span class=\"lbl-tbl-text\">" + nationalCode + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ت.ت" + "</span>" + "<span class=\"lbl-tbl-text\">" + birthdate + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون اطلاعات حقوقی
        /// </summary>
        /// <param name="companyTitle">نا شرکت</param>
        /// <param name="companyType">وع شرکت</param>
        /// <param name="registrationNumber">شمار ثبت</param>
        /// <param name="nationalId">شناسه ملی</param>
        /// <param name="economicCode">کد اقتصادی</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString LegalColumn(string companyTitle, string companyType, string registrationNumber, string nationalId, string economicCode)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "نام" + "</span>" + "<span class=\"lbl-tbl-text\">" + companyTitle + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "نوع" + "</span>" + "<span class=\"lbl-tbl-text\">" + companyType + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ش.ث" + "</span>" + "<span class=\"lbl-tbl-text\">" + registrationNumber + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ش.م" + "</span>" + "<span class=\"lbl-tbl-text\">" + nationalId + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "ک.ا" + "</span>" + "<span class=\"lbl-tbl-text\">" + economicCode + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون اطلاعات کاربری
        /// </summary>
        /// <param name="phoneNumber">شماره موبایل</param>
        /// <param name="email">ایمیل</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString UserColumn(string phoneNumber, string email)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "موبایل" + "</span>" + "<span class=\"lbl-tbl-text\">" + phoneNumber + "</span>" +
                   "<span class=\"lbl-tbl-text-title\">" + "ایمیل" + "</span>" + "<span class=\"lbl-tbl-text\">" + email + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون رستوران
        /// </summary>
        /// <param name="restaurantTitle">نام رستوران</param>
        /// <param name="restaurantCategory">نوع فروش</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString RestaurantColumn(string restaurantTitle, string restaurantCategory)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text\">" + string.Format("{0}/{1}", restaurantCategory, restaurantTitle) + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }


        /// <summary>
        /// ستون مختصات جغرافیایی
        /// </summary>
        /// <param name="lat">عرض جغرافیایی</param>
        /// <param name="lng">طول جغرافیایی</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString GeographicalCoordinatesColumn(string lat, string lng)
        {
            var str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "عرض جغرافیایی" + "</span>" + "<span class=\"lbl-tbl-text\">" + lat + "</span>" +
                    "<span class=\"lbl-tbl-text-title\">" + "طول جغرافیایی" + "</span>" + "<span class=\"lbl-tbl-text\">" + lng + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون برچسب
        /// </summary>
        /// <param name="label">مقدار برچسب</param>
        /// <param name="color">رنگ برچسب</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString LabelColum(string label, Color color)
        {
            string str = "";
            str += "<span class=\"label bg-" + color.ToString().ToLower() + " shadow-style\">" + label + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }

        /// <summary>
        /// ستون رستوران
        /// </summary>
        /// <param name="title">نام رستوران به فارسی</param>
        /// <param name="tilteEng">نام رستوران به انگلیسی</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString RestaurantColumn(string title, string titleEnglish, string restaurantCategoryTitleEnglish, string cityTitleEnglish)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "نام فارسی" + "</span>" + "<span class=\"lbl-tbl-text\">" + title + "</span>" +
                   "<span class=\"lbl-tbl-text-title\">" + "نام انگلیسی" + "</span>" + "<span class=\"lbl-tbl-text\">" + (!string.IsNullOrEmpty(titleEnglish) ? titleEnglish : "-") + "</span>" +
                   "<span class=\"lbl-tbl-text-title\">" + "لینک" + "</span>" + "<span class=\"lbl-tbl-text\">" + (!string.IsNullOrEmpty(titleEnglish) ? "<a class=\"lnk-effect\" target=\"_blank\" href=\"" + TextGenerator.RestaurantLink(cityTitleEnglish, restaurantCategoryTitleEnglish, titleEnglish) + "\">" + "لینک رستوران" + "</a>" : "-") + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }


        /// <summary>
        /// ستون تایید نهایی رستوران و تغییر وضعیت به آماده سفارش گیری
        /// </summary>
        /// <param name="title">نام رستوران به فارسی</param>
        /// <param name="tilteEng">نام رستوران به انگلیسی</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString CheckReadyToOrder(bool isChecked, int restaurantId)
        {
            string str = "";
            str += "<label class=\"box\"><input class=\"restaurant-activity-check\" " + "data-id=\"" + restaurantId + "\" type=\"checkbox\" " + (isChecked ? "checked=\"checked\"" : "") + "><span> آماده سفارش گیری </span></label>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }


        /// <summary>
        /// ستون شهر و محله
        /// </summary>
        /// <param name="city">شهر</param>
        /// <param name="cityArea">محله</param>
        /// <returns></returns>
        public static Microsoft.AspNetCore.Html.HtmlString CityAreaColumn(string city, string cityArea)
        {
            string str = "";
            str += "<span class=\"lbl-tbl-text-title\">" + "شهر" + "</span>" + "<span class=\"lbl-tbl-text\">" + city + "</span>" +
                   "<span class=\"lbl-tbl-text-title\">" + "محله" + "</span>" + "<span class=\"lbl-tbl-text\">" + cityArea + "</span>";
            return new Microsoft.AspNetCore.Html.HtmlString(str);
        }
    }
}
