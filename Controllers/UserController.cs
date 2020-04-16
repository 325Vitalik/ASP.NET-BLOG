using FirstBlog.Data.Repository;
using FirstBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FirstBlog.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IRepository _repo;

        public UserController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index", "Home");
            else
                return View(post);
        }
    }
}