using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Project.WebUI.HtmlHelpers
{
    public static class FilterHelper
    {
        public static MvcHtmlString DropDownFilter<TModel, TValue>(this HtmlHelper<TModel> html, string name, Expression<Func<TModel, TValue>> expr, int? id) where TValue: IEnumerable
        {
            var result = new TagBuilder("div");
            result.AddCssClass("form-group");

            var dropdown =
                html.DropDownList(name, new SelectList(expr.Compile()(html.ViewData.Model), "Id", "Name", id),
                    string.Empty, new {@class = "form-control"}).ToString();

            var dropdownDivWrap = new TagBuilder("div");
            dropdownDivWrap.AddCssClass("col-md-10");
            dropdownDivWrap.InnerHtml = dropdown;
            var innerhtml = new StringBuilder();
            innerhtml.Append(html.LabelFor(expr, new {@class = "col-md-2 control-label"}));
            innerhtml.Append(dropdownDivWrap);
            result.InnerHtml = innerhtml.ToString();

            return MvcHtmlString.Create(result.ToString());
        }
    }
}