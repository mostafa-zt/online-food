using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Controllers
{
    public class OrderController : BaseController
    {
        #region Private Properties and Constructor
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        //private readonly ILogger<ExternalLoginModel> _logger;

        private readonly IRestaurantMenuService _restaurantMenuService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IViewRenderService _viewRenderService;

        private readonly IOrderService _orderService;

        private new const string OnlineFoodOrderCart = "OnlineFoodcart";

        public OrderController(SignInManager<User> signInManager, UserManager<User> userManager,
                                   ICityService cityService, IUnitOfWork unitOfWork/*, ILogger<ExternalLoginModel> logger*/,
                                   IRestaurantMenuService restaurantMenuService,
                                   IHttpContextAccessor httpContextAccessor,
                                   IViewRenderService viewRenderService,
                                   IOrderService orderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cityService = cityService;

            _httpContextAccessor = httpContextAccessor;
            _viewRenderService = viewRenderService;

            _orderService = orderService;
            //_logger = logger;
        }
        #endregion

        /// <summary>
        /// payment
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Insert(int menuId, decimal price, string title, string logo, string restaurantTitle)
        {
            var result = _orderService.InsertToCookie(menuId, price, title);
            var viewModel = result.Select(s => new ListOrderCartViewModel()
            {
                MenuId = s.MenuId,
                Number = s.Number,
                Price = s.Price,
                Title = s.Title
            }).ToList();
            return new JsonResult(new
            {
                success = true,
                totalprice = _orderService.TotalPriceOfOrderCart(result).GetPersianNumber(),
                totalnumber = _orderService.TotalNumberOfOrderCart(result).GetPersianNumber(),
                number = result.FirstOrDefault(x => x.MenuId == menuId) != null ? result.FirstOrDefault(x => x.MenuId == menuId).Number.GetPersianNumber() : "",
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_OrderDetail.cshtml", viewModel)
            });
        }

        public async Task<JsonResult> Increase(int menuId)
        {
            var result = _orderService.IncreaseToCookie(menuId);
            var viewModel = result.Select(s => new ListOrderCartViewModel()
            {
                MenuId = s.MenuId,
                Number = s.Number,
                Price = s.Price,
                Title = s.Title
            }).ToList();
            return new JsonResult(new
            {
                success = true,
                totalprice = _orderService.TotalPriceOfOrderCart(result).GetPersianNumber(),
                totalnumber = _orderService.TotalNumberOfOrderCart(result).GetPersianNumber(),
                number = result.FirstOrDefault(x => x.MenuId == menuId) != null ? result.FirstOrDefault(x => x.MenuId == menuId).Number.GetPersianNumber() : "",
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_OrderDetail.cshtml", viewModel)
            });
        }

        public async Task<JsonResult> Decrease(int menuId)
        {
            var result = _orderService.DecreaseToCookie(menuId);
            var viewModel = result.Select(s => new ListOrderCartViewModel()
            {
                MenuId = s.MenuId,
                Number = s.Number,
                Price = s.Price,
                Title = s.Title
            }).ToList();
            return new JsonResult(new
            {
                success = true,
                totalprice = _orderService.TotalPriceOfOrderCart(result).GetPersianNumber(),
                totalnumber = _orderService.TotalNumberOfOrderCart(result).GetPersianNumber(),
                number = result.FirstOrDefault(x => x.MenuId == menuId) != null ? result.FirstOrDefault(x => x.MenuId == menuId).Number.GetPersianNumber() : "0",
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_OrderDetail.cshtml", viewModel)
            });
        }

        public async Task<JsonResult> Clear()
        {
            if (_orderService.IsExistCookie(OnlineFoodOrderCart))
                _orderService.RemoveCookie(OnlineFoodOrderCart);
            return new JsonResult(new
            {
                success = true,
                totalprice = "0",
                totalnumber = "0",
                number = "0",
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_OrderDetail.cshtml", new List<ListOrderCartViewModel>())
            });
        }

        public async Task<JsonResult> Remove(int menuId)
        {
            var result = _orderService.RemoveItemInCookie(menuId);
            var viewModel = result.Select(s => new ListOrderCartViewModel()
            {
                MenuId = s.MenuId,
                Number = s.Number,
                Price = s.Price,
                Title = s.Title
            }).ToList();
            return new JsonResult(new
            {
                success = true,
                totalprice = _orderService.TotalPriceOfOrderCart(result).GetPersianNumber(),
                totalnumber = _orderService.TotalNumberOfOrderCart(result).GetPersianNumber(),
                number = result.FirstOrDefault(x => x.MenuId == menuId) != null ? result.FirstOrDefault(x => x.MenuId == menuId).Number.GetPersianNumber() : "0",
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_OrderDetail.cshtml", viewModel)
            });
        }
    }
}