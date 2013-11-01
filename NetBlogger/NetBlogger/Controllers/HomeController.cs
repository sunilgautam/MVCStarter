using NetBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NetBlogger.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Date()
        {
            MyClass cls = new MyClass();
            cls.Name = "Sunil";
            cls.Date = DateTime.Now;

            return View();
        }

        public ActionResult Paging(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "login" ? "login_desc" : "login";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));





            List<NetBlogger.Models.User> users = new List<NetBlogger.Models.User>();
            for (int i = 1; i < 1001; i++)
            {
                users.Add(new NetBlogger.Models.User{
                            UserId = i,
                            Login = "User - " + i
                          });
            }

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;
            var usersAsIPagedList = new StaticPagedList<NetBlogger.Models.User>(users, pageIndex + 1, pageSize, users.Count);

            ViewBag.OnePageOfUsers = usersAsIPagedList;

            //return View();
            return View(users.Skip((pageIndex * pageSize)).Take(pageSize));
        }
    }
}
