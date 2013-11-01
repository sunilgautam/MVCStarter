using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBlogger.Infrastructure.Utility
{
    public class Functions
    {
        public string HumanReadableDateTimeDiff(DateTime from, DateTime? to = null)
        {
            if (to == null)
            {
                to = DateTime.Now;
            }

            double diff = (to.Value - from).TotalSeconds;
            double day_diff = Math.Floor(diff / 86400);

            if (day_diff < 0 || day_diff >= 31)
            {
                // MORE THAN A MONTH AGO
                return string.Empty;
            }

            if (day_diff == 0)
            {
                if (diff < 60)
                {
                    return "Just now";
                }
                else if (diff < 120)
                {
                    return "1 minute ago";
                }
                else if (diff < 3600)
                {
                    return Math.Floor(diff / 60) + " minutes ago";
                }
                else if (diff < 7200)
                {
                    return "1 hour ago";
                }
                else if (diff < 86400)
                {
                    return Math.Floor(diff / 3600) + " hours ago";
                }
            }
            else
            {
                if (day_diff == 1)
                {
                    return "Yesterday";
                }
                else if (day_diff < 7)
                {
                    return day_diff + " days ago";
                }
                else if (day_diff < 31)
                {
                    return Math.Ceiling(day_diff / 7) + " weeks ago";
                }
            }

            return string.Empty;
        }
    }
}