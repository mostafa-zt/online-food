using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        #region fields &  constructor 
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserService _userService;

        public AccountController(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ILogger<AccountController> logger,
                IUnitOfWork unitOfWork,
                IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        #endregion

        #region Logout
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            //await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(OnlineFood.Business.Security.AuthenticationSchema.AdminArea);

            _logger.LogInformation("User logged out.");

            // Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(string.Empty), null);

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/admin/home/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //IList<AuthenticationScheme> ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // We do not want to use any existing identity information
            EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            var viewModel = new LoginViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                //To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    await _userService.SignInClaimAsync(OnlineFood.Business.Security.AuthenticationSchema.AdminArea, viewModel.UserName, viewModel.Password);
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.IsNotAllowed)
                {
                    //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    //_logger.LogWarning("User does not have permission.");
                    //return RedirectToPage("./NotAllowed");
                    Danger(this, "شما مجاز به ورود به سیستم نیستید!");
                    return View();
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    //return RedirectToPage("./Lockout");
                    Danger(this, "حساب کاربری شما تا 24 ساعت مسدود است.لطفا بعد از 24 ساعت مجددا سعی کنید.");
                    return View();
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    //return Page();
                    Danger(this, "نام کاربری یا رمز عبور اشتباه است");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            //Danger(this, "ورود به مرکز فروشندگان امکانپذیر نیست!");
            return View(viewModel);
        }
        #endregion


        #region EnsureLoggedOut Method
        [AllowAnonymous]
        private async void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (User.Identity.IsAuthenticated)
                //await Logout();
                await HttpContext.SignOutAsync(OnlineFood.Business.Security.AuthenticationSchema.AdminArea);
        }
        #endregion
        //[Route("Identity/Account/Login")]
        //public IActionResult LoginRedirect(string ReturnUrl)
        //{
        //    return Redirect("/Admin/Account/Login?ReturnUrl=" + ReturnUrl);
        //}


        #region AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            EnsureLoggedOut();
            //Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(string.Empty), null);
            AccessDeniedViewModel viewModel = new AccessDeniedViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }
        #endregion

    }
}