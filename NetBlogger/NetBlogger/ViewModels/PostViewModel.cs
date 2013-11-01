using NetBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBlogger.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public List<User> Authors { get; set; }
    }
}