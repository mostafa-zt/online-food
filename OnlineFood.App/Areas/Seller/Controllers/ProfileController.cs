using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Domain.Enum;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Seller.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public static class ContactOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };
        public static OperationAuthorizationRequirement Reject =
          new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };
    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";

        public static readonly string ContactAdministratorsRole =
                                                              "ContactAdministrators";
        public static readonly string ContactManagersRole = "ContactManagers";
    }
    public class ProfileController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _hostingEnvironment; //for post file
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        //private readonly IRestaurantCategoryService _restaurantCategoryService;
        //private readonly ISellerPositionService _sellerPositionService;
        //private readonly ISellerImageService _sellerImageService;
        private readonly ISellerService _sellerService;

        private readonly IUserService _userService;

        //private readonly IAuthorizationService _authorizationService;


        public ProfileController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
            IHostingEnvironment hostingEnvironment,
            IUnitOfWork unitOfWork,
            IViewRenderService viewRenderService,
            ICityService cityService,

            ISellerService sellerService,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _cityService = cityService;
            _viewRenderService = viewRenderService;
            //_restaurantCategoryService = restaurantCategoryService;

            _sellerService = sellerService;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IActionResult> Edit()
        {
            ViewBag.Cities = _cityService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();

            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var model = await _sellerService.AllInclude(x => x.User).GetAsync(x => x.Id == userId, asNoTracking: true);

            ProfileViewModel viewModel = new ProfileViewModel()
            {
                CityId = model.CityId,
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                FullAddress = model.FullAddress,
                PhoneNumber = model.PhoneNumber,
                ZipCode = model.ZipCode,
                Id = model.Id
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(_userManager.GetUserId(User));
                var seller = await _sellerService.AllInclude(x => x.User).GetAsync(x => x.Id == userId);
                var user = await _userManager.GetUserAsync(User);

                seller.CityId = viewModel.CityId;
                seller.Email = viewModel.Email;
                seller.Firstname = viewModel.Firstname;
                seller.Lastname = viewModel.Lastname;
                seller.FullAddress = viewModel.FullAddress;
                seller.ZipCode = viewModel.ZipCode;
                seller.PhoneNumber = viewModel.PhoneNumber;

                user.Firstname = viewModel.Firstname;
                user.Lastname = viewModel.Lastname;
                user.Email = viewModel.Email;
                user.PhoneNumber = viewModel.PhoneNumber;

                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "Your profile has been successfully updated");
                return RedirectToAction("edit", "profile");
            }
            return View(viewModel);
        }
    }
}