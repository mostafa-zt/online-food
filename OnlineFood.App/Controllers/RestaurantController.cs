using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Utility;
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
    //[Route("restaurants")]
    public class RestaurantController : BaseController
    {
        #region Private Properties and Constructor
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly ICityAreaService _cityAreaService;
        //private readonly ILogger<ExternalLoginModel> _logger;

        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantFoodCategoryService _restaurantFoodCategoryService;
        private readonly IRestaurantTypeService _restaurantTypeService;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;
        private readonly IRestaurantMenuService _restaurantMenuService;
        private readonly IOrderService _orderService;


        private readonly IViewRenderService _viewRenderService;

        private const string DefaultCity = "rasht";

        private const string FilterGroupRestaurantFoodCategory = "RestaurantFoodCategory";
        private const string FilterGroupRestaurantLevelEconomy = "RestaurantLevelEconomy";
        private const string FilterGroupRestaurantType = "RestaurantType";
        public RestaurantController(SignInManager<User> signInManager, UserManager<User> userManager,
                              ICityService cityService, IUnitOfWork unitOfWork/*, ILogger<ExternalLoginModel> logger*/,
                              ICityAreaService cityAreaService,
                              IRestaurantService restaurantService,
                              IRestaurantFoodCategoryService restaurantFoodCategoryService,
                              IRestaurantTypeService restaurantTypeService,
                              IRestaurantLevelEconomyService restaurantLevelEconomyService,
                              IViewRenderService viewRenderService,
                              IRestaurantMenuService restaurantMenuService,
                              IOrderService orderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _cityAreaService = cityAreaService;
            _restaurantService = restaurantService;
            _restaurantFoodCategoryService = restaurantFoodCategoryService;
            _restaurantTypeService = restaurantTypeService;
            _restaurantLevelEconomyService = restaurantLevelEconomyService;
            _viewRenderService = viewRenderService;
            _restaurantMenuService = restaurantMenuService;
            //_logger = logger;
            _orderService = orderService;
        }
        #endregion

        //[HttpPost("restaurants/{city}/{cityarea}", Name = "Destination_Route")]
        //[HttpPost]
        [Route("restaurants/{city}/{cityarea?}")]
        public async Task<IActionResult> Index(string city, string cityarea, string search)
        {
            //if (!string.IsNullOrEmpty(search) && search.HasWhiteSpace())
            //{
            //    var url = string.IsNullOrEmpty(cityarea) ? string.Format("/restaurants/{0}/?search={1}", city, search.ToUrlString()) : string.Format("/restaurants/{0}/{1}/?search={2}", city, cityarea, search.ToUrlString());
            //    //var uri = new Uri(url);
            //    return RedirectPermanent(url);
            //}
            var userCity = await _cityService.GetAsync(x => x.TitleEng == city, asNoTracking: true);
            var userCityArea = await _cityAreaService.GetAsync(x => x.TitleEng == cityarea, asNoTracking: true);
            userCityArea = userCityArea ?? new CityArea();
            RestaurantViewModel viewModel = new RestaurantViewModel()
            {
                SearchLinkWithCityLimitation = TextGenerator.SearchLink(city, search: search),
                SearchLinkWithCityAreaLimitation = TextGenerator.SearchLink(city, cityarea, search: search),
                TotalOrderCartNumber = _orderService.TotalNumberOfOrderCart(_orderService.GetItemsOfCookie()),
                SearchViewModel = new SearchViewModel()
                {
                    ListCityAreaViewModels = _cityService.AllInclude(x => x.CityAreas).GetAll(x => x.TitleEng == city, asNoTracking: true)
                                                  .SelectMany(s => s.CityAreas)
                                                  .Select(s => new ListCityAreaViewModel()
                                                  {
                                                      Value = s.TitleEng,
                                                      Title = s.Title,
                                                      Selected = userCityArea != null ? s.Id == userCityArea.Id : false
                                                  }).ToList(),
                    CityAreaTitleEng = cityarea,
                    CityTitleEng = city,
                    CityId = userCity.Id,
                    CityAreaTitle = userCityArea != null ? userCityArea.Title : string.Empty,
                    CityTitle = userCity.Title,
                    Location = TextGenerator.Location(userCity.Title, userCityArea != null ? userCityArea.Title : string.Empty)
                },
                ListRestaurantViewModels = _restaurantService.GetAll()
                                                             .Include(x => x.RestaurantImages)
                                                             .Include(x => x.CityArea)
                                                             .Where(w => (w.CityArea.City.TitleEng == city &&
                                                                         string.IsNullOrEmpty(cityarea) || w.CityArea.TitleEng == cityarea) &&
                                                                         w.RestaurantActivityStatus == Domain.Enum.RestaurantActivityStatus.ReadyToOrder
                                                                         /*&&  w.CreatorUser.Seller.SellerRegistrationProcess == Domain.Enum.SellerRegistrationProcess.RestaurantRegistration*/)
                                                                         .Select(s => new ListRestaurantViewModel()
                                                                         {
                                                                             CityArea = s.CityArea.Title,
                                                                             HeaderUrl = s.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Header).ImageUrl,
                                                                             LogoUrl = s.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Logo).ImageUrl,
                                                                             Title = s.Title,
                                                                             UserComments = s.UserComments.Count(),
                                                                             RateCount = s.RateCount,
                                                                             Rate = s.Rate,
                                                                             DeliveryTime = TimeManagerForDeliveryTime.ToDeliveryTime(s.FromDeliveryTime.Value, s.ToDeliveryTime.Value),
                                                                             MinOrder = s.AcceptableMinimumOrder.HasValue ? s.AcceptableMinimumOrder.Value.GetPersianNumber(true) : string.Empty,
                                                                             RestaurantTypes = s.RestaurantStyles.Select(type => type.RestaurantType.Title).ToList(),
                                                                             Link = TextGenerator.RestaurantLink(s.CityArea.City.TitleEng, s.CreatorUser.Seller.RestaurantCategory.TitleEng, s.TitleEng)
                                                                         }).ToList(),
                ListRestaurantFoodCategoryViewModels = _restaurantFoodCategoryService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                                                     .Select(s => new ListRestaurantFoodCategoryViewModel()
                                                                                     {
                                                                                         Id = s.Id,
                                                                                         Title = s.Title,
                                                                                         FilterGroup = FilterGroupRestaurantFoodCategory
                                                                                     }).ToList(),
                ListRestaurantLevelEconomyViewModels = _restaurantLevelEconomyService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                                                     .Select(s => new ListRestaurantLevelEconomyViewModel()
                                                                                     {
                                                                                         Id = s.Id,
                                                                                         Title = s.Title,
                                                                                         FilterGroup = FilterGroupRestaurantLevelEconomy
                                                                                     }).ToList(),
                ListRestaurantTypeViewModels = _restaurantTypeService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                                                     .Select(s => new ListRestaurantTypeViewModel()
                                                                                     {
                                                                                         Id = s.Id,
                                                                                         Title = s.Title,
                                                                                         FilterGroup = FilterGroupRestaurantType
                                                                                     }).ToList()
            };
            ViewBag.Cities = _cityService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return View(viewModel);
        }

        public async Task<IActionResult> Filter(string city, string cityarea, List<Filter> filter)
        {
            var query = _restaurantService.GetAll()
                                              .Include(x => x.RestaurantImages)
                                              .Include(x => x.CityArea)
                                              //.ThenInclude(x => x.City)
                                              .AsQueryable();
            List<ListRestaurantViewModel> viewModel = new List<ListRestaurantViewModel>();

            viewModel = query.Where(w => (w.CityArea.City.TitleEng == city &&
                                                       string.IsNullOrEmpty(cityarea) || w.CityArea.TitleEng == cityarea) &&
                                                       w.RestaurantActivityStatus == Domain.Enum.RestaurantActivityStatus.ReadyToOrder &&
                                                       (!filter.Any(a => a.Group == FilterGroupRestaurantFoodCategory) || filter.Any(a => a.Group == FilterGroupRestaurantFoodCategory && w.RestaurantMenus.Any(menu => menu.RestaurantFoodCategoryId == a.Value))) &&
                                                       (!filter.Any(a => a.Group == FilterGroupRestaurantLevelEconomy) || filter.Any(a => a.Group == FilterGroupRestaurantLevelEconomy && w.RestaurantLevelEconomyId == a.Value)) &&
                                                       (!filter.Any(a => a.Group == FilterGroupRestaurantType) || filter.Any(a => a.Group == FilterGroupRestaurantType && w.RestaurantStyles.Any(type => type.RestaurantTypeId == a.Value)))
                                                       )
                                                       .Select(s => new ListRestaurantViewModel()
                                                       {
                                                           CityArea = s.CityArea.Title,
                                                           HeaderUrl = s.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Header).ImageUrl,
                                                           LogoUrl = s.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Logo).ImageUrl,
                                                           Title = s.Title,
                                                           UserComments = s.UserComments.Count(),
                                                           RateCount = s.RateCount,
                                                           Rate = s.Rate,
                                                           DeliveryTime = TimeManagerForDeliveryTime.ToDeliveryTime(s.FromDeliveryTime.Value, s.ToDeliveryTime.Value),
                                                           MinOrder = s.AcceptableMinimumOrder.HasValue ? s.AcceptableMinimumOrder.Value.GetPersianNumber(true) : string.Empty,
                                                           RestaurantTypes = s.RestaurantStyles.Select(type => type.RestaurantType.Title).ToList(),
                                                           Link = TextGenerator.RestaurantLink(s.CityArea.City.TitleEng, s.CreatorUser.Seller.RestaurantCategory.TitleEng, s.TitleEng)
                                                       }).ToList();
            return new JsonResult(new
            {
                success = true,
                view = await _viewRenderService.RenderToStringAsync("/Views/Restaurant/_List.cshtml", viewModel),
                //message = "لطفا مجددا سعی کنید",
                //title = "خطا در ورود اطلاعات",
            });
        }


        public JsonResult GetRestaurants(string q)
        {
            var restaurantSearch = _restaurantService.GetAll(x => x.Title.Contains(q) || x.TitleEng.Contains(q))
                                                        .Select(s => new
                                                        {
                                                            value = s.Id.ToString(),
                                                            text = s.Title,
                                                            url = TextGenerator.RestaurantLink(s.CityArea.City.TitleEng, s.CreatorUser.Seller.RestaurantCategory.TitleEng, s.TitleEng),
                                                            friendlytext = "",
                                                        }).ToList();
            var restaurantFoodcategorySearch = _restaurantMenuService.GetAll(x => x.Title.Contains(q) || x.TitleEng.Contains(q))
                                                       .Select(s => new
                                                       {
                                                           value = s.Id.ToString(),
                                                           text = s.Title,
                                                           url = "",
                                                           friendlytext = s.Title.ToUrlString(),
                                                       }).ToList();
            var result = restaurantSearch.Concat(restaurantFoodcategorySearch);
            return new JsonResult(new
            {
                success = true,
                results = result.ToList(),
                //message = "",
                //title = ""
            });
        }
    }

    public class Filter
    {
        public string Group { get; set; }
        public int Value { get; set; }
    }
}