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
                if (_repo.GetLike((int)id, User.Identity.Name) == null && _repo.GetDislike((int)id, User.Identity.Name) == null)
                {
                    _repo.Like((int)id, User.Identity.Name);
                    ratingChanges((int)id, 1);
                }
                else if(_repo.GetDislike((int)id, User.Identity.Name) == null)
                {
                    _repo.DeleteLike((int)id, User.Identity.Name);
                    ratingChanges((int)id, -1);
                    HttpContext.Response.Cookies.Delete(id.ToString());
                }
                else if(_repo.GetLike((int)id, User.Identity.Name) == null)
                {
                    _repo.DeleteDislike((int)id, User.Identity.Name);
                    _repo.Like((int)id, User.Identity.Name);
                    ratingChanges((int)id, 2);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RatingDown(int? id)
        {
            if (id != null)
            {
                if (_repo.GetLike((int)id, User.Identity.Name) == null && _repo.GetDislike((int)id, User.Identity.Name) == null)
                {
                    _repo.Dislike((int)id, User.Identity.Name);
                    ratingChanges((int)id, -1);
                }
                else if (_repo.GetLike((int)id, User.Identity.Name) == null)
                {
                    _repo.DeleteDislike((int)id, User.Identity.Name);
                    ratingChanges((int)id, 1);
                    HttpContext.Response.Cookies.Delete(id.ToString());
                }
                else if (_repo.GetDislike((int)id, User.Identity.Name) == null)
                {
                    _repo.DeleteLike((int)id, User.Identity.Name);
                    _repo.Dislike((int)id, User.Identity.Name);
                    ratingChanges((int)id, -2);
                }
            }
            return RedirectToAction("Index", "Home");
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