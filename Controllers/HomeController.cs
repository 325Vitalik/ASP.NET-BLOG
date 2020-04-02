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
                var vote = _repo.GetVote(User.Identity.Name, post.Id);
                lst.Add(post.Id, vote);
                var allPostVotes = _repo.GetAllVotesOfPost(post.Id).ToList();
                post.Rating = allPostVotes.Where(v => v.LikeDislike).Count() - allPostVotes.Where(v => !v.LikeDislike).Count();
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