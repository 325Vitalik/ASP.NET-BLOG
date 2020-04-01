using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstBlog.Data.Repository;
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
                bool isLiked = likeCheck((int)id);
                bool isDesliked = dislikeCheck((int)id);
                int changeRating;

                if (!isLiked && !isDesliked)
                {
                    _repo.Like((int)id, User.Identity.Name);
                    changeRating = 1;
                }
                else if(isLiked)
                {
                    _repo.DeleteLike((int)id, User.Identity.Name);
                    changeRating = -1;
                }
                else
                {
                    _repo.DeleteDislike((int)id, User.Identity.Name);
                    _repo.Like((int)id, User.Identity.Name);
                    changeRating = 2;
                }
                ratingChanges((int)id, changeRating);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RatingDown(int? id)
        {
            if (id != null)
            {
                bool isLiked = likeCheck((int)id);
                bool isDesliked = dislikeCheck((int)id);
                int changeRating;

                if (!isLiked && !isDesliked)
                {
                    _repo.Dislike((int)id, User.Identity.Name);
                    changeRating = -1;
                }
                else if (isDesliked)
                {
                    _repo.DeleteDislike((int)id, User.Identity.Name);
                    changeRating = 1;
                }
                else
                {
                    _repo.DeleteLike((int)id, User.Identity.Name);
                    _repo.Dislike((int)id, User.Identity.Name);
                    changeRating = -2;
                }
                ratingChanges((int)id, changeRating);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool likeCheck(int userId)
        {
            if (_repo.GetLike(userId, User.Identity.Name) == null)
                return false;
            return true;
        }

        private bool dislikeCheck(int userId)
        {
            if (_repo.GetDislike(userId, User.Identity.Name) == null)
                return false;
            return true;
        }

        private void ratingChanges(int id, int change)
        {
            var post = _repo.GetPost(id);
            post.Rating += change;
            _repo.UpdatePost(post);
            _repo.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}