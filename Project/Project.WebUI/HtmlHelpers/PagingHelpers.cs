using System;
using Project.WebUI.Models;
using System.Text;
using System.Web.Mvc;

namespace Project.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl,string classes=null)
        {
            int offset = 2;
            StringBuilder result = new StringBuilder();
            StringBuilder resultul = new StringBuilder();
            //<ul class="pagination pull-right">
            TagBuilder tagUl = new TagBuilder("ul");
            if (classes != null)
            {
                tagUl.AddCssClass(classes);
            }
            tagUl.AddCssClass("pagination");
            
            //<li class="footable-page-arrow disabled"><a data-page="first" href="#first">«</a></li>
            TagBuilder tagAFirst = new TagBuilder("a");
            tagAFirst.MergeAttribute("href", pagingInfo.CurrentPage != 1 ? pageUrl(1) : "");
            tagAFirst.MergeAttribute("data-page", "first");
            tagAFirst.InnerHtml="«";
            TagBuilder tagLiFirst = new TagBuilder("li");
            tagLiFirst.InnerHtml = tagAFirst.ToString();
            tagLiFirst.AddCssClass("footable-page-arrow");
            if (pagingInfo.CurrentPage == 1)
            {
                tagLiFirst.AddCssClass("disabled");            
            }
            resultul.Append(tagLiFirst.ToString());

            //<li class="footable-page-arrow disabled"><a data-page="prev" href="#prev">‹</a></li>
            TagBuilder tagAPrev = new TagBuilder("a");
            tagAPrev.MergeAttribute("href", pagingInfo.CurrentPage != 1 ? pageUrl(pagingInfo.CurrentPage - 1) : "");
            tagAPrev.MergeAttribute("data-page", "first");
            tagAPrev.InnerHtml = "‹";
            TagBuilder tagLiPrev = new TagBuilder("li");
            tagLiPrev.InnerHtml = tagAPrev.ToString();
            tagLiPrev.AddCssClass("footable-page-arrow");
            if (pagingInfo.CurrentPage == 1)
            {
                tagLiPrev.AddCssClass("disabled");
            }
            resultul.Append(tagLiPrev.ToString());


            int leftBorder = pagingInfo.CurrentPage - offset >= 1 ? pagingInfo.CurrentPage - offset : 1;
            int rightBorder = pagingInfo.CurrentPage + offset <= pagingInfo.TotalPages ? pagingInfo.CurrentPage + offset : pagingInfo.TotalPages;

            for (int i = leftBorder; i <= rightBorder; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.MergeAttribute("data-page", "first");
                tag.InnerHtml = i.ToString();
                
                
                TagBuilder tagLi = new TagBuilder("li");
                tagLi.InnerHtml = tag.ToString();
                tagLi.AddCssClass("footable-page-arrow");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }
                resultul.Append(tagLi.ToString());

                //TagBuilder tag = new TagBuilder("a");
                //tag.MergeAttribute("href", pageUrl(i));
                //tag.InnerHtml = i.ToString();
                //if (i == pagingInfo.CurrentPage)
                //{
                //    tag.AddCssClass("selected");
                //    tag.AddCssClass("btn-primary");
                //}
                //else
                //{
                //    tag.AddCssClass("btn-default");
                //}
                //tag.AddCssClass("btn");
                //result.Append(tag.ToString());
            }
            //<li class="footable-page-arrow"><a data-page="next" href="#next">›</a></li>
            TagBuilder tagANext = new TagBuilder("a");
            tagANext.MergeAttribute("href", pagingInfo.CurrentPage != pagingInfo.TotalPages ? pageUrl(pagingInfo.CurrentPage + 1) : "");
            tagANext.MergeAttribute("data-page", "first");
            tagANext.InnerHtml = "›";
            TagBuilder tagLiNext = new TagBuilder("li");
            tagLiNext.InnerHtml = tagANext.ToString();
            tagLiNext.AddCssClass("footable-page-arrow");
            if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
            {
                tagLiNext.AddCssClass("disabled");
            }
            resultul.Append(tagLiNext.ToString());

            //<li class="footable-page-arrow"><a data-page="last" href="#last">»</a></li>
            TagBuilder tagALast = new TagBuilder("a");
            tagALast.MergeAttribute("href", pagingInfo.CurrentPage != pagingInfo.TotalPages ? pageUrl(pagingInfo.TotalPages) : "");
            tagALast.MergeAttribute("data-page", "first");
            tagALast.InnerHtml = "»";
            TagBuilder tagLiLast = new TagBuilder("li");
            tagLiLast.InnerHtml = tagALast.ToString();
            tagLiLast.AddCssClass("footable-page-arrow");
            if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
            {
                tagLiLast.AddCssClass("disabled");
            }
            resultul.Append(tagLiLast.ToString());

            tagUl.InnerHtml = resultul.ToString();

            return MvcHtmlString.Create(tagUl.ToString());
        }
    }
}