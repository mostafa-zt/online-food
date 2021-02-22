using OnlineFood.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Common.HtmlHelpers
{
    public static class ValidationSummaryHelper
    {
        public static HtmlString ValidationSummary(this IHtmlHelper htmlHelper, ModelStateDictionary modelStateDictionary, string validationSummary, object htmlAttr = null)
        {

            var str = modelStateDictionary.Any(a => a.Value.Errors.Any()) ? "<div class=\"ui danger message\" " + htmlAttr.SetStringHtmlAttribute() + "> " +
                                                                                 "<div class=\"header list-info\">" +
                                                                                         "لطفا یه موارد زیر توجه فرمایید :" +
                                                                                  "</div>" +
                                                                                  "<div class=\"message-body\">" +
                                                                                        validationSummary +
                                                                                  "</div>" +
                                                                             "</div>" : null;

            return new HtmlString(str);
        }
    }

    [HtmlTargetElement("input", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InvariantDecimalTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";

        private IHtmlGenerator _generator;

        [HtmlAttributeName("asp-is-invariant")]
        public bool IsInvariant { set; get; }

        public InvariantDecimalTagHelper(IHtmlGenerator generator) : base(generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (IsInvariant && output.TagName == "input" && For.Model != null && For.Model.GetType() == typeof(decimal))
            {
                decimal value = (decimal)(For.Model);
                var invariantValue = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                output.Attributes.SetAttribute(new TagHelperAttribute("value", invariantValue));
            }
        }
    }
}
