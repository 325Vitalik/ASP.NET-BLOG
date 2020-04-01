using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FirstBlog.ViewModels;

namespace FirstBlog.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userMgr;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userMgr)
        {
            this.signInManager = signInManager;
            this.userMgr = userMgr;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(vm.UserName);

                var result = await userMgr.CreateAsync(user, vm.Password);
                await userMgr.AddToRoleAsync(user, "user");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }
    }
}