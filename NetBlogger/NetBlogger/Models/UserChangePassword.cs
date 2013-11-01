using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetBlogger.Models
{
    public class UserChangePassword
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [Display(Name = "Username")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}