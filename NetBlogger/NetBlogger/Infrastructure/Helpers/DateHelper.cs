using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetBlogger.Infrastructure.Helpers
{
    public static class DateHelper
    {
        public static MvcHtmlString HumanReadableDateTimeDiff(this HtmlHelper htmlHelper, string tag, object htmlAttributes, DateTime from, DateTime? to = null)
        {
            string output = null;

            if (to == null)
            {
                to = DateTime.Now;
            }

            double diff = (to.Value - from).TotalSeconds;
            double day_diff = Math.Floor(diff / 86400);

            if (day_diff < 0 || day_diff >= 31)
            {
                // MORE THAN A MONTH AGO
                output = string.Empty;
            }

            if (day_diff == 0)
            {
                if (diff < 60)
                {
                    output = "Just now";
                }
                else if (diff < 120)
                {
                    output = "1 minute ago";
                }
                else if (diff < 3600)
                {
                    output = Math.Floor(diff / 60) + " minutes ago";
                }
                else if (diff < 7200)
                {
                    output = "1 hour ago";
                }
                else if (diff < 86400)
                {
                    output = Math.Floor(diff / 3600) + " hours ago";
                }
                else
                {
                    output = string.Empty;
                }
            }
            else
            {
                if (day_diff == 1)
                {
                    output = "Yesterday";
                }
                else if (day_diff < 7)
                {
                    output = day_diff + " days ago";
                }
                else if (day_diff < 31)
                {
                    output = Math.Ceiling(day_diff / 7) + " weeks ago";
                }
                else
                {
                    output = string.Empty;
                }
            }

            if (!string.IsNullOrEmpty(output))
            {
                TagBuilder outputTag = new TagBuilder(tag);
                var attributes = new RouteValueDictionary(htmlAttributes);
                outputTag.MergeAttributes(attributes);
                outputTag.Attributes["title"] = from.ToString();
                outputTag.InnerHtml = output;
                return MvcHtmlString.Create(outputTag.ToString(TagRenderMode.Normal));
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}