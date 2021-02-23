using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Seller.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class RestaurantController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IViewRenderService _viewRenderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;
        private readonly IRestaurantTypeService _restaurantTypeService;
        private readonly IRestaurantFoodCategoryService _restaurantFoodCategoryService;
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantImageService _restaurantImageService;
        private readonly ICityService _cityService;

        public RestaurantController(
          UserManager<User> userManager,
          IUnitOfWork unitOfWork,
          IViewRenderService viewRenderService,
          IRestaurantLevelEconomyService restaurantLevelEconomyService,
          IRestaurantTypeService restaurantTypeService,
          IRestaurantService restaurantService,
          IRestaurantImageService restaurantImageService,
          IRestaurantFoodCategoryService restaurantFoodCategoryService,
          ICityService cityService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _viewRenderService = viewRenderService;
            _restaurantLevelEconomyService = restaurantLevelEconomyService;
            _restaurantTypeService = restaurantTypeService;
            _restaurantFoodCategoryService = restaurantFoodCategoryService;
            _restaurantService = restaurantService;
            _restaurantImageService = restaurantImageService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Manage()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));

            var model = await _restaurantService.GetAll(asNoTracking: true)
                                                         .Include(x => x.CreatorUser)
                                                         .ThenInclude(x => x.Seller)
                                                         .Include(x => x.RestaurantStyles)
                                                         .Include(x => x.RestaurantWorkingHours)
                                                         .Include(x => x.RestaurantImages)
                                                         .Include(x=>x.RestaurantMenus)
                                                         .FirstOrDefaultAsync(x => x.CreatorUserId == userId);

            var logoFile = model.RestaurantImages.FirstOrDefault(x => x.ImageType == Domain.Enum.ImageType.Logo);
            EditRestaurantViewModel viewModel = new EditRestaurantViewModel()
            {
                Id = model.Id,
                CityId = model.CreatorUser.Seller.CityId,
                FullAddress = model.FullAddress,
                Title = model.Title,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber,
                //ValidLink = !string.IsNullOrEmpty(model.TitleEng) ? string.Format("www.foodbarg.com/{0}/{1}", model.CreatorUser.Seller.RestaurantCategory.TitleEng.ToLower(), model.TitleEng.ToLower().GetFriendlyTitle()) : null,
                RestaurantCourierCost = model.RestaurantCourierCost,
                RestaurantLevelEconomyId = model.RestaurantLevelEconomyId.HasValue ? model.RestaurantLevelEconomyId.Value : 0,
                RestaurantTypes = model.RestaurantStyles.Select(s => s.RestaurantTypeId).ToList(),
                RestaurantFoodTypes = model.RestaurantMenus.Select(s => s.RestaurantFoodCategoryId).ToList(),
                //FromDeliveryHour = model.FromDeliveryTime.HasValue ? DeliveryTimeManager.ToTimeSpan(model.FromDeliveryTime.Value.Hours, 0) : DeliveryTimeManager.GetHourDefault(),
                //FromDeliveryMinute = model.FromDeliveryTime.HasValue ? DeliveryTimeManager.ToTimeSpan(0, model.FromDeliveryTime.Value.Minutes) : DeliveryTimeManager.GetMinuteDefault(),
                //ToDeliveryHour = model.ToDeliveryTime.HasValue ? DeliveryTimeManager.ToTimeSpan(model.ToDeliveryTime.Value.Hours, 0) : DeliveryTimeManager.GetHourDefault(),
                //ToDeliveryMinute = model.ToDeliveryTime.HasValue ? DeliveryTimeManager.ToTimeSpan(0, model.ToDeliveryTime.Value.Minutes) : DeliveryTimeManager.GetMinuteDefault(),
                FromDeliveryTime = model.FromDeliveryTime,
                ToDeliveryTime = model.ToDeliveryTime,
                DaysOfWeekViewModel = Enum.GetValues(typeof(Domain.Enum.DaysOfWeek)).Cast<Domain.Enum.DaysOfWeek>()
                                           .Select(s => new DaysOfWeekViewModel()
                                           {
                                               Name = s.ToString(),
                                               Title = s.GetDescriptionString(),
                                               Value = (int)s,
                                               IsEnabled = !model.RestaurantWorkingHours.Any(a => a.DaysOfWeek.ToString() == s.ToString())
                                           }).ToList(),
                WorkingHoursViewModels = model.RestaurantWorkingHours.OrderBy(o => o.DaysOfWeek.GetDisplayString(DisplayAttributeProperties.Order)).Select(s => new WorkingHoursViewModel()
                {
                    Day = s.DaysOfWeek.ToString(),
                    End = s.EndTime.ToString(),
                    Start = s.StartTime.ToString(),
                    Id = s.Id,
                    Title = s.DaysOfWeek.GetDescriptionString()
                }).ToList(),

                LogoFileId = logoFile != null ? logoFile.Id : 0,
                LogoFileName = logoFile != null ? logoFile.ImageFileName : null,
                LogoFileUrl = logoFile != null ? logoFile.ImageUrl : null
            };

            ViewBag.RestaurantLevelEconomies = _restaurantLevelEconomyService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                                             .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                                             .AsEnumerable();

            ViewBag.Cities = _cityService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            ViewBag.RestaurantTypes = _restaurantTypeService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                             .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                             .AsEnumerable();

            ViewBag.RestaurantFoodTypes = _restaurantFoodCategoryService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                          .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                          .AsEnumerable();

            //ViewBag.RestaurantCityAreas = model.RestaurantCityAreas.Select(s => new SelectListItem() { Selected = true, Value = s.CityAreaId.ToString(), Text = s.CityArea.Title });

            ViewBag.FromDeliveryTimes = TimeManagerForDeliveryTime.GetTimes();
            ViewBag.ToDeliveryTimes = model.FromDeliveryTime.HasValue ? TimeManagerForDeliveryTime.GetToTimes(model.FromDeliveryTime.Value) : TimeManagerForDeliveryTime.GetToTimes(TimeManagerForDeliveryTime.GetDefaultTime());

            ViewBag.StartTimeWorkingHours = TimeManagerForWorkingHour.GetTimes();
            ViewBag.EndTimeWorkingHours = TimeManagerForWorkingHour.GetToTimes(TimeManagerForWorkingHour.GetDefaultTime());

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Manage(EditRestaurantViewModel viewModel)
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var user = await _userManager.GetUserAsync(User);
      
            if (ModelState.IsValid)
            {

               var model = await _restaurantService.GetAll(asNoTracking: false)
                                                    //.Include(x => x.RestaurantCityAreas).ThenInclude(x => (x as RestaurantCityArea).CityArea)
                                                    //.Include(x => x.CityArea)
                                                    .Include(x=>x.CreatorUser).ThenInclude(x=>x.Seller)
                                                    .Include(x => x.RestaurantStyles).ThenInclude(x => (x as RestaurantStyle).RestaurantType)
                                                    .Include(x=>x.RestaurantMenus).ThenInclude(x=>(x as RestaurantMenu).RestaurantFoodCategory)
                                                    .Include(x => x.RestaurantWorkingHours)
                                                    .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

                

                model.Title = viewModel.Title;


                model.FullAddress = viewModel.FullAddress;
                model.PhoneNumber = viewModel.PhoneNumber;
                model.PostalCode = viewModel.PostalCode;
                model.RestaurantCourierCost = viewModel.RestaurantCourierCost;
                model.RestaurantLevelEconomyId = viewModel.RestaurantLevelEconomyId;

                model.CreatorUser.Seller.CityId = viewModel.CityId;

                model.FromDeliveryTime = viewModel.FromDeliveryTime;  
                model.ToDeliveryTime = viewModel.ToDeliveryTime;  

                //manage restaurant types
                if (viewModel.RestaurantTypes != null && viewModel.RestaurantTypes.Any())
                {
                    var selected = _restaurantTypeService.GetAll(type => viewModel.RestaurantTypes.Any(a => a == type.Id)).ToList();
                    var deleted = model.RestaurantStyles.Select(s => s.RestaurantType).Except(selected, f => f.Id).ToList();
                    var added = selected.Except(model.RestaurantStyles.Select(s => s.RestaurantType), f => f.Id).ToList();
                    //remove collection
                    model.RestaurantStyles.Where(w => deleted.Any(d => d.Id == w.RestaurantTypeId)).ToList()
                         .ForEach(entity => model.RestaurantStyles.Remove(entity));
                    //add collection
                    added.ForEach(entity => model.RestaurantStyles.Add(new RestaurantStyle()
                    {
                        RestaurantTypeId = entity.Id,
                        RestaurantId = viewModel.Id
                    }));
                }
                else
                {
                    //remove all collection
                    model.RestaurantStyles.ToList().ForEach(entity => model.RestaurantStyles.Remove(entity));
                }
                
                //manage restaurant food types
                if (viewModel.RestaurantFoodTypes != null && viewModel.RestaurantFoodTypes.Any())
                {
                    var selected = _restaurantFoodCategoryService.GetAll(type => viewModel.RestaurantFoodTypes.Any(a => a == type.Id)).ToList();
                    var deleted = model.RestaurantMenus.Select(s => s.RestaurantFoodCategory).Except(selected, f => f.Id).ToList();
                    var added = selected.Except(model.RestaurantMenus.Select(s => s.RestaurantFoodCategory), f => f.Id).ToList();
                    //remove collection
                    model.RestaurantMenus.Where(w => deleted.Any(d => d.Id == w.RestaurantFoodCategoryId)).ToList()
                         .ForEach(entity => model.RestaurantMenus.Remove(entity));
                    //add collection
                    added.ForEach(entity => model.RestaurantMenus.Add(new RestaurantMenu()
                    {
                        RestaurantFoodCategoryId = entity.Id,
                        RestaurantId = viewModel.Id
                    }));
                }
                else
                {
                    //remove all collection
                    model.RestaurantMenus.ToList().ForEach(entity => model.RestaurantMenus.Remove(entity));
                }

                //manage working hours
                if (viewModel.WorkingHoursViewModels != null && viewModel.WorkingHoursViewModels.Any())
                {
                    var selected = viewModel.WorkingHoursViewModels.Select(s => new RestaurantWorkingHour()
                    {
                        Id = s.Id,
                        EndTime = TimeSpan.Parse(s.End),
                        StartTime = TimeSpan.Parse(s.Start),
                        DaysOfWeek = Enum.Parse<Domain.Enum.DaysOfWeek>(s.Day, ignoreCase: true),
                        RestaurantId = viewModel.Id
                    });
                    var deleted = model.RestaurantWorkingHours.Except(selected, f => f.Id).ToList();
                    var added = selected.Except(model.RestaurantWorkingHours, f => f.Id).ToList();
                    //remove collection
                    deleted.ForEach(entity => model.RestaurantWorkingHours.Remove(entity));
                    //add collection
                    added.ForEach(entity => model.RestaurantWorkingHours.Add(entity));
                }
                else
                {
                    //remove all collection
                    model.RestaurantWorkingHours.ToList().ForEach(entity => model.RestaurantWorkingHours.Remove(entity));
                }


                if (viewModel.LogoFile != null && viewModel.LogoFile.Length > 0)
                    await _restaurantImageService.SaveFile(viewModel.LogoFile, user, Domain.Enum.ImageType.Logo);
                
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "Your restaurant has been successfully updated");
                return RedirectToAction("manage");
            }

            ViewBag.RestaurantLevelEconomies = _restaurantLevelEconomyService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                                                       .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                                                       .AsEnumerable();

            ViewBag.Cities = _cityService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            ViewBag.RestaurantTypes = _restaurantTypeService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                             .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                             .AsEnumerable();

            ViewBag.RestaurantFoodTypes = _restaurantFoodCategoryService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available)
                                                          .Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() })
                                                          .AsEnumerable();

            //ViewBag.RestaurantCityAreas = model.RestaurantCityAreas.Select(s => new SelectListItem() { Selected = true, Value = s.CityAreaId.ToString(), Text = s.CityArea.Title });

            //ViewBag.FromDeliveryTimes = TimeManagerForDeliveryTime.GetTimes();
            //ViewBag.ToDeliveryTimes = model.FromDeliveryTime.HasValue ? TimeManagerForDeliveryTime.GetToTimes(model.FromDeliveryTime.Value) : TimeManagerForDeliveryTime.GetToTimes(TimeManagerForDeliveryTime.GetDefaultTime());

            //ViewBag.StartTimeWorkingHours = TimeManagerForWorkingHour.GetTimes();
            //ViewBag.EndTimeWorkingHours = TimeManagerForWorkingHour.GetToTimes(TimeManagerForWorkingHour.GetDefaultTime());

            return View(viewModel);
        }
        
        public JsonResult GetEndWorkingHour(TimeSpan time)
        {
            var times = TimeManagerForWorkingHour.GetToTimes(time);
            return new JsonResult(new
            {
                isvalid = true,
                results = times.ToList(),
                message = ""
            });
        }
        public JsonResult GetToDeliveryTime(TimeSpan time)
        {
            var times = TimeManagerForDeliveryTime.GetToTimes(time);
            return new JsonResult(new
            {
                isvalid = true,
                results = times.ToList(),
                message = ""
            });
        }
    }
}