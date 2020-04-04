using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstBlog.Data.Repository;
using FirstBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstBlog.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private IRepository _repo;

        public RatingController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult RatingUp(int? id)
        {  
            if (id != null)
            {
                changeVote((int)id, true);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RatingDown(int? id)
        {
            if (id != null)
            {
                changeVote((int)id, false);
            }
            return RedirectToAction("Index", "Home");
        }

        private void changeVote(int postId, bool like)
        {
            var check = _repo.GetVote(User.Identity.Name, postId) == like;

            _repo.RemoveVote(User.Identity.Name, postId);

            if (!check)
            {
                vote(postId, like);
            }
            _repo.SaveChangesAsync().GetAwaiter().GetResult();
        }

        private void vote(int postId, bool like)
        {
            _repo.AddVote(new PostLikeDislike()
            {
                PostId = postId,
                UserId = _repo.GetUserId(User.Identity.Name),
                LikeDislike = like
            });
        }
    }
}