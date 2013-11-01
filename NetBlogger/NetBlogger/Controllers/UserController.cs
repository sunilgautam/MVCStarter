using NetBlogger.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetBlogger.Models;
using NetBlogger.Infrastructure.Utility;

namespace NetBlogger.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(userRepository.GetAll());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = userRepository.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (userRepository.Save(user))
                {
                    this.AddMessage("User successfully added", MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddMessage("User not successfully added", MessageType.Error);
                }
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = userRepository.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdit user)
        {
            if (ModelState.IsValid)
            {
                if (userRepository.Update(user))
                {
                    this.AddMessage("User successfully updated", MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddMessage("User not successfully updated", MessageType.Error);
                }
            }
            return View(new User { UserId = user.UserId, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, Website = user.Website, Info = user.Info });
        }

        public ActionResult ChangePassword(int id = 0)
        {
            User user = userRepository.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(new UserChangePassword { UserId = user.UserId, Login = user.Login, Password = user.Password });
        }

        [HttpPost]
        public ActionResult ChangePassword(UserChangePassword user)
        {
            if (ModelState.IsValid)
            {
                if (userRepository.ChangePassword(user))
                {
                    this.AddMessage("Password changed successfully", MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddMessage("Password not changed", MessageType.Error);
                }
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = userRepository.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userRepository.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            else
            {
                if (userRepository.Delete(user))
                {
                    this.AddMessage("User successfully deleted", MessageType.Success);
                }
                else
                {
                    this.AddMessage("User not successfully deleted", MessageType.Error);
                }
            }
            return RedirectToAction("Index");
        }

    }
}
