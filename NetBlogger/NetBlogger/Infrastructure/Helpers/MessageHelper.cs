using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using NetBlogger.Infrastructure.Utility;

namespace NetBlogger.Infrastructure.Helpers
{
    public static class MessageHelper
    {
        public static MvcHtmlString ShowMessage(this HtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewContext.TempData[AppKeys.MESSAGE_KEY] != null)
            {
                HashSet<Notification> notifications = (HashSet<Notification>)htmlHelper.ViewContext.TempData[AppKeys.MESSAGE_KEY];

                if (notifications != null)
                {
                    TagBuilder notificationContainer = new TagBuilder("div");
                    notificationContainer.AddCssClass("notification-box");

                    StringBuilder builder = new StringBuilder();
                    HashSet<Notification>.Enumerator iterator = notifications.GetEnumerator();
                    string alert_class = "info";
                    while (iterator.MoveNext())
                    {
                        Notification notification = iterator.Current;

                        switch (notification.Type)
                        {
                            case MessageType.Error:
                                alert_class = "danger";
                                break;
                            case MessageType.Information:
                                alert_class = "info";
                                break;
                            case MessageType.Success:
                                alert_class = "success";
                                break;
                            case MessageType.Warning:
                                alert_class = "warning";
                                break;
                            default:
                                alert_class = "info";
                                break;
                        }

                        builder.AppendFormat("<div class='alert alert-{1}'>{0}</div>", notification.Message, alert_class);
                    }

                    notificationContainer.InnerHtml = builder.ToString();
                    return MvcHtmlString.Create(notificationContainer.ToString(TagRenderMode.Normal));
                }
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}