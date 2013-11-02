using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NetBlogger.Models;
using NetBlogger.Domain.Abstract;
using NetBlogger.ViewModels;
using NetBlogger.Infrastructure.Utility;
using PagedList;

namespace NetBlogger.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository postRepository;
        private IUserRepository userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        //
        // GET: /Post/

        public ActionResult Index(int? page)
        {
            int total = postRepository.Count();
            int pageIndex = (page ?? 1) - 1;
            int pageSize = 2;

            IEnumerable<Post> posts = postRepository.GetAll((pageIndex * pageSize), pageSize, "PostId DESC", null, null);
            var modelAsIPagedList = new StaticPagedList<Post>(posts, pageIndex + 1, pageSize, total);

            ViewBag.PagedModel = modelAsIPagedList;


            if (Request.IsAjaxRequest())
            {
                return PartialView("_AjaxIndex", posts);
            }
            else
            {
                return View(posts);
            }
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = postRepository.GetById(id);
            if (post == null)
            {
                return View("NotFound");
            }
            return View(post);
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            PostViewModel postVM = new PostViewModel();
            postVM.Post = new Post();
            postVM.Authors = userRepository.GetAll().ToList();

            return View(postVM);
        }

        //
        // POST: /Post/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDate = DateTime.Now;
                post.UpdateDate = DateTime.Now;

                if(postRepository.Save(post))
                {
                    this.AddMessage("Post successfully added", MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddMessage("Post not successfully added", MessageType.Error);
                }
            }

            PostViewModel postVM = new PostViewModel();
            postVM.Post = post;
            postVM.Authors = userRepository.GetAll().ToList();
            return View(postVM);
        }

        //
        // GET: /Post/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PostViewModel postVM = new PostViewModel();
            postVM.Post = postRepository.GetById(id);
            postVM.Authors = userRepository.GetAll().ToList();

            if (postVM.Post == null)
            {
                return View("NotFound");
            }
            return View(postVM);
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.UpdateDate = DateTime.Now;

                if(postRepository.Update(post))
                {
                    this.AddMessage("Post successfully updated", MessageType.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    this.AddMessage("Post not successfully updated", MessageType.Error);
                }
            }
            PostViewModel postVM = new PostViewModel();
            postVM.Post = post;
            postVM.Authors = userRepository.GetAll().ToList();
            return View(postVM);
        }

        //
        // GET: /Post/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Post post = postRepository.GetById(id);
            if (post == null)
            {
                return View("NotFound");
            }
            return View(post);
        }

        //
        // POST: /Post/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postRepository.GetById(id);
            if (post == null)
            {
                return View("NotFound");
            }
            else
            {
                if (postRepository.Delete(post))
                {
                    this.AddMessage("Post successfully deleted", MessageType.Success);
                }
                else
                {
                    this.AddMessage("Post not successfully deleted", MessageType.Error);
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Posts/?

        public ActionResult Display(int? id)
        {
            if (id != null)
            {
                Post post = postRepository.GetByIdWithAuthors(id.Value);
                if (post == null)
                {
                    return View("NotFound");
                }
                return View("Display", post);
            }
            else
            {
                return View("ListPosts", postRepository.GetAllWithAuthors());
            }
        }
    }
}