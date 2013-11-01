using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetBlogger.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public string Slug { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "Please enter post content")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Date")]
        public DateTime UpdateDate { get; set; }

        [Required(ErrorMessage = "Please select author")]
        [Display(Name = "Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}