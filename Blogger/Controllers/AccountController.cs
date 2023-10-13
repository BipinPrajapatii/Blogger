using Blogger.Data.Entities;
using Blogger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult LogIn()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return Redirect("/");

            return View(new LogInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInViewModel model)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return Redirect("/");

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                ModelState.AddModelError("", "Invalid name or password");
            }
            return View(model);
        }
        public IActionResult SignUp()
        {
            return View(new SignUpUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            return NotFound();
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
    }
}
