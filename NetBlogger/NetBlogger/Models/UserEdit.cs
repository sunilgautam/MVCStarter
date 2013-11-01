using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace NetBlogger.Models
{
    public class UserEdit
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Email(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Invalid website")]
        public string Website { get; set; }

        public string Info { get; set; }
    }
}