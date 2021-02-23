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
    public class ProfileController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly ISellerService _sellerService;
        
        public ProfileController(
            UserManager<User> userManager,
            ILogger<AccountController> logger,
            IUnitOfWork unitOfWork,
            ICityService cityService,
            ISellerService sellerService)
        {
            _userManager = userManager;
            _logger = logger;
            _cityService = cityService;
            _sellerService = sellerService;
            _unitOfWork = unitOfWork;
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