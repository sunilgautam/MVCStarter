using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBlogger.Infrastructure.Utility
{
    public class Notification
    {
        public Notification(string Message, MessageType Type)
        {
            this.Message = Message;
            this.Type = Type;
        }

        public string Message { get; set; }
        public MessageType Type { get; set; }
    }
}