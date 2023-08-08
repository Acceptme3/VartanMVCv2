using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VartanMVCv2.Models;

namespace VartanMVCv2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            _logger.LogInformation("Начинает выполнение Account/Login [тип запроса: GET]");
           
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]      
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            _logger.LogInformation("Начинает выполнение Account/Login [тип запроса: POST]");

            if (ModelState.IsValid) 
            {
                IdentityUser? user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null) 
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded) 
                    {
                        return Redirect("/Admin/Owner/Index" ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пaроль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
