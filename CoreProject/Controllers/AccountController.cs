using CoreProject.Models;
using CoreProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl});

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {

            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/");

                }
                ModelState.AddModelError("", "Invalid username or password");

            }
            return View(loginVM);
        }

        public async Task<IActionResult> Details()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                return View(new AuthDetailsViewModel { Cookie = Request.Cookies[".AspNetCore.Identity.Application"], User = user });
            }

            return View(new AuthDetailsViewModel());
        }        
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [Authorize]
        public string AllRoles() => "All Roles";
            
        [Authorize(Roles ="Admin")]
        public string AdminOnly() => "Admin Only";

        [Authorize(Roles = "Manager")]
        public string ManagerOnly() => "Manager Only";

        public string AccessDenied() => "AccessDenied";

    }
}
