using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetBlogger.Infrastructure.Collections;
using NetBlogger.Infrastructure.Helpers;
using System.Web.Routing;
using System.Text;

namespace NetBlogger.Infrastructure.Helpers.Paging
{
    public static class Pager
    {
        public static MvcHtmlString Paginate<T>(this HtmlHelper htmlHelper, PagedList<T> PagedItem, object htmlAttributes)
        {
            if (PagedItem != null)
            {
                UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                string area = htmlHelper.ViewContext.RouteData.GetRequiredString("area");
                string controller = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
                string action = htmlHelper.ViewContext.RouteData.GetRequiredString("action");

                TagBuilder listTag = new TagBuilder("ul");
                listTag.Attributes.Add("class", "pagination");
                var attributes = new RouteValueDictionary(htmlAttributes);
                listTag.MergeAttributes(attributes);

                StringBuilder builder = new StringBuilder();

                int prevPage = PagedItem.CurrentPage - 1;
                int nextPage = PagedItem.CurrentPage + 1;
                
                // PREVIOUS
                if (PagedItem.CurrentPage != 1 && PagedItem.ItemsTotal >= 10)
                {
                    builder.AppendFormat("<a class=\"paginate\" href=\"{0}\">« Previous</a>", htmlHelper.ViewContext.HttpContext.Request.Path + "?page=" + prevPage + "&ipp=" + (PagedItem.ItemsPerPage));
                }
                else
                {
                    builder.AppendFormat("<span class=\"inactive\" href=\"#\">« Previous</span> ");
                }

                // INNER PAGE LINKS e.g 1,2,3
                int startRange = PagedItem.CurrentPage - (int)Math.Floor(PagedItem.MidRange / 2.0);
                int endRange = PagedItem.CurrentPage + (int)Math.Floor(PagedItem.MidRange / 2.0);

                if (startRange <= 0)
                {
                    endRange += Math.Abs(startRange) + 1;
                    startRange = 1;
                }
                if (endRange > PagedItem.NumPages)
                {
                    startRange -= endRange - PagedItem.NumPages;
                    endRange = PagedItem.NumPages;
                }
                //$this->range = range(startRange,endRange);

                // NEXT
                if (PagedItem.CurrentPage != PagedItem.NumPages && PagedItem.ItemsTotal >= 10)
                {
                    builder.AppendFormat("<a class=\"paginate\" href=\"{0}\">Next »</a>", htmlHelper.ViewContext.HttpContext.Request.Path + "?page=" + nextPage + "&ipp=" + (PagedItem.ItemsPerPage));
                }
                else
                {
                    builder.AppendFormat("<span class=\"inactive\" href=\"#\">» Next</span>");
                }

                listTag.InnerHtml = builder.ToString();
                return MvcHtmlString.Create(listTag.ToString(TagRenderMode.Normal));
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}