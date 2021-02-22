using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineFood.Common.HtmlHelpers.AlertNotificationHelper;

namespace OnlineFood.Web.Controllers
{
    //[Authorize(Roles = "Admin", AuthenticationSchemes = "AdminAuth")]
    public class BaseController : Controller
    {
        //Put in BaseController
        public string RenderPartialViewToString(Microsoft.AspNetCore.Mvc.ViewEngines.ICompositeViewEngine viewEngine, string viewName, object model)
        {
            viewName = viewName ?? ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = model;
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Microsoft.AspNetCore.Mvc.ViewEngines.IView view = viewEngine.FindView(ControllerContext, viewName, false).View;
                Microsoft.AspNetCore.Mvc.Rendering.ViewContext viewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext(ControllerContext, view, ViewData, TempData, sw, new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions());
                view.RenderAsync(viewContext).Wait();
                return sw.GetStringBuilder().ToString();
            }
        }

        #region cookie order cart
        public const string OnlineFoodOrderCart = "OnlineFoodOrderCart";

        ///// <summary>  
        ///// Get the cookie  
        ///// </summary>  
        ///// <param name="key">Key </param>  
        ///// <returns>string value</returns>  
        //public string GetCookie(string key/*, bool isContext = false*/)
        //{
        //    //if (isContext)
        //    //    return _httpContextAccessor.HttpContext.Request.Cookies[key];
        //    return Request.Cookies[key];
        //}

        //public void SetCookie(string key, string value, int? expireTime)
        //{
        //    CookieOptions option = new CookieOptions();
        //    if (expireTime.HasValue)
        //        option.Expires = DateTime.Now.AddDays(expireTime.Value);
        //    else
        //        option.Expires = DateTime.Now.AddMilliseconds(10);
        //    Response.Cookies.Append(key, value, option);
        //}

        ///// <summary>  
        ///// Delete the key  
        ///// </summary>  
        ///// <param name="key">Key</param>  
        //public void RemoveCookie(string key)
        //{
        //    Response.Cookies.Delete(key);
        //}

        //public int TotalNumberOfOrderCart()
        //{
        //    int totalnumber = 0;
        //    if (IsExistOrderCart())
        //    {
        //        var carts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
        //        foreach (var item in carts)
        //        {
        //            totalnumber += item.Number;
        //        }
        //    }
        //    return totalnumber;
        //}
        //public decimal TotalPriceOfOrderCart()
        //{
        //    decimal totalprice = 0;
        //    if (IsExistOrderCart())
        //    {
        //        var carts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderBasket>>(GetCookie(OnlineFoodOrderCart));
        //        foreach (var item in carts)
        //        {
        //            totalprice = totalprice + (item.Price * item.Number);
        //        }
        //    }
        //    return totalprice;
        //}
        //public bool IsExistOrderCart()
        //{
        //    return Request.Cookies[OnlineFoodOrderCart] != null;
        //}


        #endregion

        #region Alert Tempdata
        [TempData]
        public static string Message { get; set; }

        public static void AddErrors(Controller controller, Microsoft.AspNetCore.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError("", error.Description);
            }
        }

        public static void AddErrors(Controller controller, string property, string error)
        {
            controller.ModelState.AddModelError(property, error);
        }

        public static void Success(Controller controller, string message, string title = null, bool dismissable = true)
        {
            AddAlert(controller, AlertStyles.Success, message, title, dismissable);
        }

        public static void Information(Controller controller, string message, string title = null, bool dismissable = true)
        {
            AddAlert(controller, AlertStyles.Info, message, title, dismissable);
        }

        public static void Warning(Controller controller, string message, string title = null, bool dismissable = true)
        {
            AddAlert(controller, AlertStyles.Warning, message, title, dismissable);
        }

        public static void Danger(Controller controller, string message, string title = null, bool dismissable = true)
        {
            AddAlert(controller, AlertStyles.Danger, message, title, dismissable);
        }

        private static void AddAlert(Controller controler, AlertStyles alertStyle, string message, string title, bool dismissable)
        {
            var alerts = GetAlerts(controler) ?? new List<Alert>();
            alerts.Add(new Alert()
            {
                AlertStyle = alertStyle,
                Message = message,
                Title = title,
                Dismissable = dismissable
            });

            controler.TempData.Put<List<Alert>>(Alert.TempDataKey, alerts);
        }

        private static List<Alert> GetAlerts(Controller controller)
        {
            return controller.TempData.Get<List<Alert>>(Alert.TempDataKey) ?? new List<Alert>();
        }
        #endregion
    }
}