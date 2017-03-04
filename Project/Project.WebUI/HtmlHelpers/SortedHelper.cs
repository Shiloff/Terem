using System;
using Project.WebUI.Models;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
namespace Project.WebUI.HtmlHelpers
{
    public static class SortedHelpers
    {

        public static MvcHtmlString SortedTH(this HtmlHelper html,string name ,string asc,string desc,string sort, Func<string, string> pageUrl,Dictionary<string, object> attributes=null)
        {
            StringBuilder result = new StringBuilder();
            
            TagBuilder tag = new TagBuilder("a");
            TagBuilder tagi = new TagBuilder("i");
            string Sort = desc == sort ? asc : desc;            
            tag.MergeAttribute("href", pageUrl(Sort));
            
            //tag.Attributes.Add("style", "display:block;width:100%;height: 100%;");

            TagBuilder tagth = new TagBuilder("th");
            string mode = desc == sort ? "fa fa-chevron-down pull-right" : asc == sort ? "fa fa-chevron-up pull-right" : "pull-right";
            tagi.AddCssClass(mode);
            tag.InnerHtml = name + tagi.ToString();
            //tag.AddCssClass(mode);
            tagth.InnerHtml = tag.ToString();
            foreach (var attr in attributes)
            {
                tagth.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            result.Append(tagth.ToString());
            
            return MvcHtmlString.Create(result.ToString());
        }
    }
}