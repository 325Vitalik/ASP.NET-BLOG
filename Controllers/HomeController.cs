using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstBlog.Data;
using FirstBlog.Data.Repository;
using FirstBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstBlog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            var lst = new Dictionary<int, bool?>();
            foreach (var post in posts)
            {
                if(_repo.GetLike(post.Id, User.Identity.Name) == null && _repo.GetDislike(post.Id, User.Identity.Name) == null)
                {
                    lst.Add(post.Id, null);
                }
                else if (_repo.GetLike(post.Id, User.Identity.Name) == null)
                {
                    lst.Add(post.Id, false);
                }
                else
                {
                    lst.Add(post.Id, true);
                }
            }
            ViewBag.Vote = lst;
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
    }
}