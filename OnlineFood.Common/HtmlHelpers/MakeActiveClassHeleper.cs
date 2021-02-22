using OnlineFood.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Common.HtmlHelpers
{
    public static class MakeActiveClassHeleper
    {

        public static bool HasActiveClass(this IHtmlHelper htmlHelper, List<string> controllers)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
            return controllers.Any(w => w == currentController.ToLower()) ? true : false;
        }

        #region MakeActiveClass
        public static string MakeActiveClass(this IHtmlHelper htmlHelper, string controller, string action, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (action ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controller ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction.ToLower()) && acceptedControllers.Contains(currentController.ToLower()) ? cssClass : String.Empty;
        }

        public static string MakeActiveClass(this IHtmlHelper htmlHelper, string controller, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedControllers = (controller ?? currentController).Split(',');

            return acceptedControllers.Contains(currentController.ToLower()) ? cssClass : String.Empty;
        }

        public static string MakeActiveClass(this IHtmlHelper htmlHelper, Dictionary<string, string> routes, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
            return routes.Any(w => w.Key == currentController.ToLower() && w.Value == currentAction.ToLower()) ? cssClass : String.Empty;
        }

        public static string MakeActiveClass(this IHtmlHelper htmlHelper, List<string> controllers, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
            return controllers.Any(w => w == currentController.ToLower()) ? cssClass : String.Empty;
        } 
        #endregion

        #region RouteIf
        /// <summary>
        ///     Compares the requested route with the given <paramref name="value" /> value, if a match is found the
        ///     <paramref name="attribute" /> value is returned.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value">The action value to compare to the requested route action.</param>
        /// <param name="attribute">The attribute value to return in the current action matches the given action value.</param>
        /// <returns>A HtmlString containing the given attribute value; otherwise an empty string.</returns>
        public static HtmlString RouteIf(this HtmlHelper helper, string controller, string action, string attribute)
        {
            var currentController =
                (helper.ViewContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();
            var currentAction =
                (helper.ViewContext.RouteData.Values["action"] ?? string.Empty).ToString().UnDash();

            var hasController = controller.Equals(currentController, StringComparison.InvariantCultureIgnoreCase);
            var hasAction = action.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase);

            return hasController && hasAction ? new HtmlString(attribute) : new HtmlString(string.Empty);
        }

        public static HtmlString RouteIf(this HtmlHelper helper, string controller, string attribute)
        {
            var currentController =
                (helper.ViewContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();
            //var currentAction =
            //    (helper.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString().UnDash();

            var hasController = controller.Equals(currentController, StringComparison.InvariantCultureIgnoreCase);
            //var hasAction = action.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase);

            return hasController ? new HtmlString(attribute) : new HtmlString(string.Empty);
        }
        #endregion

        #region RenderPartialIf
        /// <summary>
        ///     Renders the specified partial view with the parent's view data and model if the given setting entry is found and
        ///     represents the equivalent of true.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName">The name of the partial view.</param>
        /// <param name="condition">The boolean value that determines if the partial view should be rendered.</param>
        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, bool condition)
        {
            if (!condition)
                return;
            htmlHelper.RenderPartial(partialViewName);
        }

        public static void RenderPartialIf(this HtmlHelper htmlHelper, string partialViewName, bool condition, object model)
        {
            if (!condition)
                return;
            htmlHelper.RenderPartial(partialViewName, model);
        }
        #endregion

        #region ValidationBootstrap Html
        /// <summary>
        ///     Returns an unordered list (ul element) of validation messages that utilizes bootstrap markup and styling.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="alertType">The alert type styling rule to apply to the summary element.</param>
        /// <param name="heading">The optional value for the heading of the summary element.</param>
        /// <returns></returns>
        public static HtmlString ValidationBootstrap(this HtmlHelper htmlHelper, string alertType = "danger", string heading = "")
        {
            if (htmlHelper.ViewData.ModelState.IsValid)
                return new HtmlString("")/*(string.Empty)*/;

            var sb = new StringBuilder();

            sb.AppendFormat("<div class=\"alert alert-{0} alert-block\">", alertType);
            sb.Append("<button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");

            if (!string.IsNullOrWhiteSpace(heading))
            {
                sb.AppendFormat("<h4 class=\"alert-heading\">{0}</h4>", heading);
            }

            sb.Append(htmlHelper.ValidationSummary());
            sb.Append("</div>");

            return new HtmlString(sb.ToString());

        }
        #endregion
    }
}
