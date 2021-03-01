using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Domain.Entities;
using OnlineFood.Web.Areas.Seller.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IRestaurantService _restaurantService;

        public HomeController(UserManager<User> userManager, IRestaurantService restaurantService)
        {
            _userManager = userManager;
            _restaurantService = restaurantService;
        }

        public async Task<ActionResult> Index()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));

            var restaurant = await _restaurantService.GetAll()
                                                        .Include(x => x.RestaurantImages)
                                                        .Include(x => x.RestaurantLevelEconomy)
                                                        .Include(x => x.CreatorUser).ThenInclude(x => x.Seller).ThenInclude(x => x.City)
                                                        .Include(x => x.RestaurantStyles).ThenInclude(x => (x as RestaurantStyle).RestaurantType)
                                                        .Include(x => x.RestaurantMenus).ThenInclude(x => (x as RestaurantMenu).RestaurantFoodCategory).ThenInclude(x => x.RestaurantImage)
                                                        .Include(x => x.RestaurantWorkingHours)
                                                        .FirstOrDefaultAsync(x => x.CreatorUserId == userId);
            RestaurantViewModel viewModel = null;
            if (restaurant.Title != null)
            {
                viewModel = new RestaurantViewModel()
                {
                    IsExist = true,
                    City = restaurant.CreatorUser.Seller.CityId.HasValue ? restaurant.CreatorUser.Seller.City.Title : null,
                    DeliveryTime = restaurant.FromDeliveryTime.HasValue ? string.Format("Average delivery time is between {0} to {1} minutes", restaurant.FromDeliveryTime.Value.TotalMinutes, restaurant.ToDeliveryTime.Value.TotalMinutes) : null,
                    FullAddress = restaurant.FullAddress,
                    PhoneNumber = restaurant.PhoneNumber,
                    PostalCode = restaurant.PostalCode,
                    RestaurantCourierCost = restaurant.RestaurantCourierCost,
                    RestaurantLevelEconomy = restaurant.RestaurantLevelEconomyId.HasValue ? restaurant.RestaurantLevelEconomy.Title : null,
                    Title = restaurant.Title,
                    RestaurantTypes = restaurant.RestaurantStyles.Any() ? string.Join(", ", restaurant.RestaurantStyles.Select(s => s.RestaurantType.Title).ToList()) : null,
                    WorkingHoursViewModels = restaurant.RestaurantWorkingHours.Select(s => new WorkingHoursViewModel()
                    {
                        Day = s.DaysOfWeek.ToString(),
                        End = (DateTime.Today + s.EndTime).ToString("hh:mm tt", CultureInfo.InvariantCulture),
                        Start = (DateTime.Today + s.StartTime).ToString("hh:mm tt", CultureInfo.InvariantCulture),
                        Id = s.Id,
                        Title = s.DaysOfWeek.GetDescriptionString()
                    }).ToList(),
                    RestaurantFoodTypeViewMdels = restaurant.RestaurantMenus.Select(s => new RestaurantFoodTypeViewMdel()
                    {
                        Description = s.RestaurantFoodCategory.Description,
                        FoodName = s.RestaurantFoodCategory.Title,
                        ImageUrl = s.RestaurantFoodCategory.RestaurantImageId.HasValue ? s.RestaurantFoodCategory.RestaurantImage.ImageUrl : null,
                        Price = s.RestaurantFoodCategory.Price.HasValue ? s.RestaurantFoodCategory.Price.Value.ToString() : null
                    }).ToList()
                };
            }
            else
            {
                viewModel = new RestaurantViewModel()
                {
                    IsExist = false
                };
            }

            return View(viewModel); ;
        }

        [Route("error/404")]
        [AllowAnonymous]
        public IActionResult Error404()
        {
            return View();
        }
    }
}