using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using System.Collections.Generic;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    [Authorize()]
    [Area("Seller")]
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

        private List<string> messages; // This is the backing field
        public List<string> Messages   // This is your property
        {
            get => messages = messages ?? new List<string>();
            set => messages = messages ?? new List<string>();
        }

        #region Alert Tempdata
        //[TempData]
        //public static string Message { get; set; }

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