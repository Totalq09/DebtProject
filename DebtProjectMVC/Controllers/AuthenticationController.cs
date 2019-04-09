using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtProjectMVC.DebtDbContext;
using DebtProjectMVC.Entities;
using DebtProjectMVC.Helpers;
using DebtProjectMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DebtProjectMVC.Controllers
{
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly DebtDatabaseContext dbContext;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            DebtDatabaseContext dbContext
            )
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.dbContext = dbContext;
        }

        [Route("currentuserpesel")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<string> GetUserPESEL()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return string.Empty;

            return this.dbContext.Creditors.FirstOrDefault(c => c.UserId == user.Id)?.PESEL;
        }

        [HttpGet]
        [Route("Index")]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpGet]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthenticationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                if(model.RegisterViewModel.RegisterPassword != model.RegisterViewModel.ConfirmRegisterPassword)
                {
                    ModelState.AddModelError("RegisterValidationMessage_0", "Powtórzone hasło różni się");
                    return View("Index", model);
                }

                var user = new IdentityUser { UserName = $"{model.RegisterViewModel.RegisterEmail}", Email = model.RegisterViewModel.RegisterEmail };
                var result = await UserManager.CreateAsync(user, model.RegisterViewModel.RegisterPassword);

                if (result.Succeeded)
                {
                    var creditor = new Creditor()
                    {
                        Name = model.RegisterViewModel.RegisterName,
                        Surname = model.RegisterViewModel.RegisterSurname,
                        PESEL = model.RegisterViewModel.RegisterPESEL,
                        UserId = user.Id,
                    };

                    dbContext.Creditors.Add(creditor);
                    await dbContext.SaveChangesAsync();

                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                if(result.Errors?.Count() > 0)
                {
                    var i = 0;
                    foreach (var code in result.Errors.Take(3))
                    {
                        var message = SimpleTranslator.GetMessageBasedOnCode(code.Code);
                        if (!string.IsNullOrEmpty(message))
                        {
                            message = code.Description;
                        }
                        ModelState.AddModelError($"RegisterValidationMessage_{i++}", message);
                    }             
                }
                else
                {
                    ModelState.AddModelError("RegisterValidationMessage", "Rejestracja nie powiodła się.");
                }
            }

            return View("Index", model);
        }

        [HttpGet]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(
                    model.LoginViewModel.LoginEmail, model.LoginViewModel.LoginPassword, model.LoginViewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("LoginValidationMessage", "Logowanie nie powiodło się.");
                    return View("Index", model);
                }
            }

            return View("Index", model);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            await SignInManager.SignOutAsync();
            return RedirectToLocal(returnUrl);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}