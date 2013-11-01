using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetBlogger.Infrastructure.Utility
{
    public static class Extensions
    {
        public static void AddMessage(this Controller _contoller, string _message, MessageType _type)
        {
            if (!string.IsNullOrEmpty(_message))
            {
                HashSet<Notification> notifications = null;
                if (_contoller.TempData[AppKeys.MESSAGE_KEY] == null)
                {
                    notifications = new HashSet<Notification>();
                    _contoller.TempData[AppKeys.MESSAGE_KEY] = notifications;
                }
                else
                {
                    notifications = (HashSet<Notification>)_contoller.TempData[AppKeys.MESSAGE_KEY];
                }

                notifications.Add(new Notification(_message, _type));
            }
        }
    }
}