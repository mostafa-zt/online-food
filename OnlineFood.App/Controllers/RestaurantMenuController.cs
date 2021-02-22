using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Domain.Enum;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Controllers
{
    public class RestaurantMenuController : BaseController
    {
        #region Private Properties and Constructor
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly ICityAreaService _cityAreaService;

        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantFoodCategoryService _restaurantFoodCategoryService;
        private readonly IOrderService _orderService;
        private readonly IViewRenderService _viewRenderService;


        public RestaurantMenuController(SignInManager<User> signInManager, UserManager<User> userManager,
                                        ICityService cityService, IUnitOfWork unitOfWork/*, ILogger<ExternalLoginModel> logger*/,
                                        ICityAreaService cityAreaService,
                                        IRestaurantService restaurantService,
                                        IRestaurantFoodCategoryService restaurantFoodCategoryService,
                                        IOrderService orderService, IViewRenderService viewRenderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _cityAreaService = cityAreaService;

            _restaurantService = restaurantService;
            _restaurantFoodCategoryService = restaurantFoodCategoryService;

            _orderService = orderService;
            //_logger = logger;

            _viewRenderService = viewRenderService;
        }
        #endregion

        //[Route("{city}/{category}/{title}")]
        public async Task<IActionResult> Index(string city, string category, string title)
        {
            string titleEnglish = title.ToRecoverUrlString();
            var restaurant = await _restaurantService.GetAll()
                                                     .Include(x => x.RestaurantImages)
                                                     .Include(x => x.RestaurantMenus)
                                                     .ThenInclude(x => (x as RestaurantMenu).RestaurantImage)
                                                     .Include(x => x.RestaurantMenus)
                                                     .ThenInclude(x => (x as RestaurantMenu).RestaurantFoodCategory)
                                                     .Include(x => x.CityArea)
                                                     .ThenInclude(x => x.City)
                                                     .Include(x => x.RestaurantWorkingHours)
                                                     .Include(x => x.RestaurantStyles)
                                                     .ThenInclude(x => (x as RestaurantStyle).RestaurantType)
                                                     .Include(x => x.RestaurantCityAreas)
                                                     .ThenInclude(x => (x as RestaurantCityArea).CityArea)
                                                     .FirstOrDefaultAsync(x => x.TitleEng == titleEnglish);

            //var isSWorkDay = restaurant.RestaurantWorkingHours.Any(a => (DayOfWeek)a.DaysOfWeek == DateTime.Today.DayOfWeek && DateTime.Now.TimeOfDay > a.StartTime && DateTime.Now.TimeOfDay < a.EndTime);
            //var tests = restaurant.RestaurantWorkingHours.Any(a => (DayOfWeek)a.DaysOfWeek == DateTime.Today.DayOfWeek);
            //var testsss = restaurant.RestaurantWorkingHours.Any(a => (DateTime.Now.TimeOfDay > a.StartTime));
            //var testsssss = restaurant.RestaurantWorkingHours.Any(a => DateTime.Now.TimeOfDay < a.EndTime);

            var days = Enum.GetValues(typeof(DaysOfWeek)).Cast<DaysOfWeek>().OrderBy(o => o.GetDisplayString(DisplayAttributeProperties.Order))
              .Select(s => new ListDaysViewModel()
              {
                  Day = s,
                  IsToday = (DayOfWeek)s == DateTime.Now.DayOfWeek
              });
            var viewModel = new RestaurantMenuViewModel()
            {
                RestaurantId = restaurant.Id,
                LogoUrl = restaurant.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Logo).ImageUrl,
                HeaderUrl = restaurant.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Header).ImageUrl,
                FromDeliveryTime = restaurant.FromDeliveryTime,
                ToDeliveryTime = restaurant.ToDeliveryTime,
                DeliveryTime = Common.Utility.TimeManagerForDeliveryTime.ToDeliveryTime(restaurant.FromDeliveryTime.Value, restaurant.ToDeliveryTime.Value),
                RemainingHours = "",
                IsOpened = restaurant.RestaurantWorkingHours.Any(a => (DayOfWeek)a.DaysOfWeek == DateTime.Today.DayOfWeek && DateTime.Now.TimeOfDay > a.StartTime && DateTime.Now.TimeOfDay < a.EndTime),
                RestaurantFullAddress = restaurant.FullAddress,
                RestaurantCity = restaurant.CityArea.City.Title,
                RestaurantCityArea = restaurant.CityArea.Title,
                RestaurantTitle = restaurant.Title,
                RestaurantLink = TextGenerator.RestaurantLink(city, category, title),
                ReturnLink = TextGenerator.SearchLink(city, restaurant.CityArea.TitleEng),
                SearchLinkWithCityLimitation = TextGenerator.SearchLink(city),
                SearchLinkWithCityAreaLimitation = TextGenerator.SearchLink(city, restaurant.CityArea.TitleEng),
                ListRestaurantMenuViewModels = restaurant.RestaurantMenus.GroupBy(x => x.RestaurantFoodCategoryId, (key, selectors) => new ListRestaurantMenuViewModel()
                {
                    RestaurantFoodCategoryId = key,
                    RestaurantFoodCategory = selectors.Any() ? selectors.FirstOrDefault().RestaurantFoodCategory.Title : null,
                    //RestaurantFoodCategoryClassname = selectors.Any() ? selectors.FirstOrDefault().RestaurantFoodCategory.IconClassName : null,
                    SubListRestaurantMenuViewModels = selectors.Select(s => new SubListRestaurantMenuViewModel()
                    {
                        Id = s.Id,
                        ImageUrl = s.RestaurantImage.ImageUrl,
                        Price = s.Price,
                        Title = s.Title,
                        Description = s.Description,
                        UserComments = 0,
                        IsOrdered = _orderService.HasItemInCookie(s.Id),
                        NumberOfOrder = _orderService.HasItemInCookie(s.Id) ? _orderService.GetItemOfCookie(s.Id).Number : 0
                    }).ToList()
                }).ToList(),
                OrderCartViewModel = new OrderCartViewModel()
                {
                    RestaurantTitle = restaurant.Title,
                    TotalPrice = _orderService.TotalPriceOfOrderCart(_orderService.GetItemsOfCookie()),
                    TotalNumber = _orderService.TotalNumberOfOrderCart(_orderService.GetItemsOfCookie()),
                    IsEmpty = !_orderService.GetItemsOfCookie().Any(),
                    IsOpened = restaurant.RestaurantWorkingHours.Any(a => (DayOfWeek)a.DaysOfWeek == DateTime.Today.DayOfWeek && DateTime.Now.TimeOfDay > a.StartTime && DateTime.Now.TimeOfDay < a.EndTime),
                    RestaurantLogo = restaurant.RestaurantImages.FirstOrDefault(w => w.ImageType == Domain.Enum.ImageType.Logo).ImageUrl,
                    ListOrderCartViewModels = _orderService.GetItemsOfCookie().Select(s => new ListOrderCartViewModel()
                    {
                        MenuId = s.MenuId,
                        Number = s.Number,
                        Price = s.Price,
                        Title = s.Title
                    }).ToList()
                },
                RestaurantInfoViewModel = new RestaurantInfoViewModel()
                {
                    AcceptableMinimumOrder = restaurant.AcceptableMinimumOrder,
                    RestaurantTilte = restaurant.Title,
                    RestaurantCourierCost = restaurant.RestaurantCourierCost,
                    DeliveryTime = TimeManagerForDeliveryTime.ToDeliveryTime(restaurant.FromDeliveryTime.Value, restaurant.ToDeliveryTime.Value),
                    RestaurantAddress = restaurant.FullAddress,
                    SelectedAllRestaurantCityAreas = restaurant.SelectedAllRestaurantCityAreas,
                    RestaurantCityAreas = restaurant.RestaurantCityAreas.Select(s => s.CityArea.Title).ToList(),
                    RestaurantFoodCategories = restaurant.RestaurantMenus.Select(s => s.RestaurantFoodCategory.Title).ToList(),
                    RestaurantTypes = restaurant.RestaurantStyles.Select(s => s.RestaurantType.Title).ToList(),
                    //ListDaysViewModels = Enum.GetValues(typeof(DaysOfWeek)).Cast<DaysOfWeek>().OrderBy(o => o.GetDisplayString(DisplayAttributeProperties.Order)).Select(s => new ListDaysViewModel() { Day = s.GetDescriptionString(), IsToday = (DayOfWeek)s == DateTime.Now.DayOfWeek }).ToList(),
                    ListRestaurantWorkingHourViewModels = (from day in days
                                                           join workinghour in restaurant.RestaurantWorkingHours.OrderBy(o => o.DaysOfWeek.GetDisplayString(DisplayAttributeProperties.Order)).ToList()
                                                           on day.Day equals workinghour.DaysOfWeek into tabJoin
                                                           from tj in tabJoin.DefaultIfEmpty()
                                                           select new ListRestaurantWorkingHourViewModel()
                                                           {
                                                               Day = Enum.GetValues(typeof(DaysOfWeek)).Cast<DaysOfWeek>().FirstOrDefault(w => w == day.Day).GetDescriptionString(),
                                                               IsOpened = tj != null ? DateTime.Now.TimeOfDay > tj.StartTime && DateTime.Now.TimeOfDay < tj.EndTime : false,
                                                               IsToday = tj != null ? (DayOfWeek)tj.DaysOfWeek == DateTime.Today.DayOfWeek : (DayOfWeek)day.Day == DateTime.Today.DayOfWeek,
                                                               WorkingHour = tj != null ? TimeManagerForWorkingHour.ToWorkingHour(tj.StartTime, tj.EndTime) : "رستوران در این روز بسته است!"
                                                           }).ToList()
                }
            };
            var eee = restaurant.RestaurantWorkingHours.OrderBy(o => o.DaysOfWeek.GetDisplayString(DisplayAttributeProperties.Order))
                                                            .Select(s => new ListRestaurantWorkingHourViewModel()
                                                            {
                                                                Day = s.DaysOfWeek.GetDescriptionString(),
                                                                IsOpened = DateTime.Now.TimeOfDay > s.StartTime && DateTime.Now.TimeOfDay < s.EndTime,
                                                                WorkingHour = TimeManagerForWorkingHour.ToWorkingHour(s.StartTime, s.EndTime),
                                                                IsToday = (DayOfWeek)s.DaysOfWeek == DateTime.Today.DayOfWeek
                                                            }).ToList();
            return View(viewModel);
        }


        public async Task<IActionResult> GetComment(int restaurantId)
        {

            UserCommentViewModel viewModel = new UserCommentViewModel()
            {
                ListUserCommentViewModels = _restaurantService.GetAll()
                                             .Where(w => w.Id == restaurantId).SelectMany(s => s.UserComments)
                                              .Select(s => new ListUserCommentViewModel()
                                              {
                                                  CreatorDateTime = s.CreatorDateTime,
                                                  Firstname = s.CreatorUser.Firstname,
                                                  Text = s.Comment,
                                                  SubListUserCommentViewModels = s.Children.Select(ss => new SubListUserCommentViewModel()
                                                  {
                                                      CreatorDateTime = ss.CreatorDateTime,
                                                      Firstname = ss.CreatorUser.Firstname,
                                                      Text = ss.Comment,
                                                      //RestaurantLogo = _restaurantService.Get(x => x.CreatorUserId == ss.CreatorUser.Seller.Id).LogoUrl
                                                  }).ToList()
                                              }).ToList(),
                RestaurantRateGroupViewModels = Enum.GetValues(typeof(RestaurantRateType)).Cast<RestaurantRateType>().Select(s => new RestaurantRateGroupViewModel()
                {
                    Percent = "5%",
                    Rate = null,
                    RestaurantRateType = s.GetDescriptionString()
                }).ToList(),
                RestaurantPercentRate = "5%",
                RestauarntRate = null,
                Bad = _restaurantService.GetAll().Where(w => w.Id == restaurantId).SelectMany(s => s.RestaurantRates).Where(x => x.Rate == SatisfactionStatus.Excellent).Count(),
                Normal = _restaurantService.GetAll().Where(w => w.Id == restaurantId).SelectMany(s => s.RestaurantRates).Where(x => x.Rate == SatisfactionStatus.Normal).Count(),
                Excellent = _restaurantService.GetAll().Where(w => w.Id == restaurantId).SelectMany(s => s.RestaurantRates).Where(x => x.Rate == SatisfactionStatus.Bad).Count()
            };


            return new JsonResult(new
            {
                success = true,
                view = await _viewRenderService.RenderToStringAsync("/Views/RestaurantMenu/_UserComment.cshtml", viewModel),
                //message = "لطفا مجددا سعی کنید",
                //title = "خطا در ورود اطلاعات",
            });
        }
    }
}
