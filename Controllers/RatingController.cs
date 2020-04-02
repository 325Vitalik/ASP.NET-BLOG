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
                var likeCheck = _repo.GetVote(User.Identity.Name, (int)id) == true;

                _repo.RemoveVote(User.Identity.Name, (int)id);

                if (!likeCheck) {
                    vote((int)id, true);
                }
                _repo.SaveChangesAsync().GetAwaiter().GetResult();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RatingDown(int? id)
        {
            if (id != null)
            {
                var dislikeCheck = _repo.GetVote(User.Identity.Name, (int)id) == false;

                _repo.RemoveVote(User.Identity.Name, (int)id);

                if (!dislikeCheck)
                {
                    vote((int)id, false);
                }
                _repo.SaveChangesAsync().GetAwaiter().GetResult();
            }
            return RedirectToAction("Index", "Home");
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