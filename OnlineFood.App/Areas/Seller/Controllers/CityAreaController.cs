using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class CityAreaController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ICityAreaService _cityAreaService;
        //private readonly ISellerService _sellerService;

        public CityAreaController(
         UserManager<User> userManager,
         SignInManager<User> signInManager,
         ILogger<AccountController> logger,
         IUnitOfWork unitOfWork,
         IViewRenderService viewRenderService
         )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _viewRenderService = viewRenderService;
        }

        public async Task<JsonResult> CheckLocation(string q, int cityId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _cityAreaService.GetAsync(x => x.CityId == cityId && x.Title.Contains(q));
            if (result != null)
            {
                return new JsonResult(new
                {
                    success = true,
                    address = result.Title,
                    id = result.Id,
                    message = "",
                    title = ""
                });
            }
            return new JsonResult(new
            {
                success = false,
                address = q,
                message = "آدرس انتخاب شده در سیستم وجود ندارد! <br/> لطفا مکان دیگری را روی نقشه انتخاب کنید.",
                title = "خطا"
            });
        }

        public JsonResult GetLocation(string q, int cityId)
        {
            var result = _cityAreaService.GetAll(x => x.CityId == cityId && x.Title.Contains(q))
                                         .Select(s => new
                                         {
                                             classname = "city_area_icon",
                                             name = s.Title,
                                             value = s.Id.ToString(),
                                             text = s.Title
                                         });
            return new JsonResult(new
            {
                success = true,
                results = result.ToList(),
                //message = "",
                //title = ""
            });
        }
    }
}