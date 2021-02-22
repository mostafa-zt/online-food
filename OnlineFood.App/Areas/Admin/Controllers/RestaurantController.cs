using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
    public class RestaurantController : BaseController
    {
        #region Private Properties and Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISellerService _sellerService;
        private readonly ICityService _cityService;
        private readonly IRestaurantCategoryService _restaurantCategoryService;
        private readonly ISellerPositionService _sellerPositionService;
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;


        public RestaurantController(IUnitOfWork unitOfWork, ISellerService sellerService,
                                    ICityService cityService, IRestaurantCategoryService restaurantCategoryService,
                                    ISellerPositionService sellerPositionService,
                                    IRestaurantService restaurantService,
                                    IRestaurantLevelEconomyService restaurantLevelEconomyService)
        {
            _unitOfWork = unitOfWork;
            _sellerService = sellerService;
            _cityService = cityService;
            _restaurantCategoryService = restaurantCategoryService;
            _sellerPositionService = sellerPositionService;
            _restaurantService = restaurantService;
            _restaurantLevelEconomyService = restaurantLevelEconomyService;
        }
        #endregion

        public IActionResult Index()
        {
            ViewBag.RestaurantLevelEconomies = _restaurantLevelEconomyService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();

            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchRestaurantViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _restaurantService.GetAll().Include(x => x.CreatorUser)
                                                   .ThenInclude(x => x.Seller)
                                                   .ThenInclude(x => x.RestaurantCategory)
                                                   .Include(x => x.CityArea).ThenInclude(x => x.City)
                                                   .Include(x => x.RestaurantLevelEconomy)
                                                   .AsNoTracking();

            // جستجو
            //اطلاعات حقیقی
            string title = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            string titleEng = !string.IsNullOrEmpty(filter.TitleEng) ? filter.TitleEng.Trim() : null;
            DateTime fromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            DateTime toCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;

            query = query.Where(w => (title == null || title.Equals(w.Title)) &&
                                     (titleEng == null || titleEng.Equals(w.TitleEng)) &&
                                     (filter.AcceptableMinimumOrder == null || filter.AcceptableMinimumOrder.Equals(w.AcceptableMinimumOrder)) &&
                                     (filter.CityArea == null || !filter.CityArea.Any() || filter.CityArea.Any(a => a == w.CityAreaId)) &&
                                     (filter.RestaurantLevelEconomy == null || !filter.RestaurantLevelEconomy.Any() || filter.RestaurantLevelEconomy.Any(a => a == w.RestaurantLevelEconomyId)) &&
                                     (filter.DeliveryTime == null || filter.DeliveryTime.Equals(w.FromDeliveryTime)) &&
                                     (filter.RestaurantCourierCost == null || filter.RestaurantCourierCost.Equals(w.RestaurantCourierCost)) &&
                                     (!filter.SelectedAllRestaurantCityAreas || w.SelectedAllRestaurantCityAreas) &&
                                     (filter.RestaurantActivityStatus == null || !filter.RestaurantActivityStatus.Any() || filter.RestaurantActivityStatus.Any(a => a == (int)w.RestaurantActivityStatus)) &&
                                     (fromCreatorDateTime == DateTime.MinValue || fromCreatorDateTime.Date <= w.CreatorDateTime.Value.Date) &&
                                     (toCreatorDateTime == DateTime.MinValue || toCreatorDateTime.Date >= w.CreatorDateTime.Value.Date));


            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(w => w.Title.Contains(searchString));
            }

            totalRecords = query.Count();

            switch (sortColumnIndex)
            {
                case 0:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id);
                        break;
                    }
                case 1:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                        break;
                    }
                case 2:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.CityArea.CityId).ThenBy(o => o.CityAreaId) : query.OrderByDescending(o => o.CityArea.CityId).ThenBy(o => o.CityAreaId);
                        break;
                    }
                case 3:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.AcceptableMinimumOrder) : query.OrderByDescending(o => o.AcceptableMinimumOrder);
                        break;
                    }
                case 4:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.RestaurantLevelEconomyId) : query.OrderByDescending(o => o.RestaurantLevelEconomyId);
                        break;
                    }
                //case 5:
                //    {
                //        query = sortDirection == "asc" ? query.OrderBy(o => o.DeliveryTime) : query.OrderByDescending(o => o.DeliveryTime);
                //        break;
                //    }
                case 6:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.RestaurantCourierCost) : query.OrderByDescending(o => o.RestaurantCourierCost);
                        break;
                    }
                case 7:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.RestaurantActivityStatus) : query.OrderByDescending(o => o.RestaurantActivityStatus);
                        break;
                    }
                case 8:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.CreatorDateTime) : query.OrderByDescending(o => o.CreatorDateTime);
                        break;
                    }
                default:
                    query = query.OrderByDescending(o => o.CreatorDateTime);
                    break;
            }
            query = query.Skip(param.start.Value * param.length.Value).Take(param.length.Value);
            filteredRecords = query.Count();

            var records = query.ToList().Select((s, index) => new[]
                                             {
                                               ((param.start.Value * param.length.Value)+ (index + 1)).GetPersianNumber() ,
                                               HtmlGenerator.RestaurantColumn(s.Title , s.TitleEng , s.CreatorUser.Seller.RestaurantCategory.TitleEng , s.CityAreaId.HasValue ? s.CityArea.City.TitleEng : "").ToString(),
                                               s.CityAreaId.HasValue?  HtmlGenerator.CityAreaColumn(s.CityArea.City.Title, s.CityArea.Title).ToString() : "",
                                               s.AcceptableMinimumOrder.HasValue ? s.AcceptableMinimumOrder.Value.GetPersianNumber() :"",
                                               s.RestaurantLevelEconomy!=null ? s.RestaurantLevelEconomy.Title : "",
                                               s.FromDeliveryTime.HasValue && s.ToDeliveryTime.HasValue ? TimeManagerForDeliveryTime.ToDeliveryTime(s.FromDeliveryTime.Value , s.ToDeliveryTime.Value) :string.Empty,
                                               s.RestaurantCourierCost.HasValue  ?  s.RestaurantCourierCost.Value.GetPersianNumber():"",
                                               HtmlGenerator.LabelColum(s.CreatorUser.Seller.SellerRegistrationProcess.GetDescriptionString() , OnlineFood.Common.Utility.GeneralMethods.ChooseColor(s.CreatorUser.Seller.SellerRegistrationProcess)).ToString(),
                                               HtmlGenerator.LabelColum(s.RestaurantActivityStatus.GetDescriptionString() , GeneralMethods.ChooseColor(s.RestaurantActivityStatus)).ToString(),
                                               HtmlGenerator.CheckReadyToOrder(s.RestaurantActivityStatus ==  Domain.Enum.RestaurantActivityStatus.ReadyToOrder , s.Id).ToString(),
                                               s.CreatorDateTime.ToString(),
                                               HtmlGenerator.GeneralCommand(s.Id , hasRemove : true , hasEdit :false , hasDetail:true,hasMenu:true, hasGallery:true).ToString()
                                            });
            return Json(new
            {
                param.draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = records
            });
        }


        public async Task<IActionResult> Detail(int id)
        {
            var model = await _restaurantService.GetAll().Include(x => x.CreatorUser)
                                                   .ThenInclude(x => x.Seller)
                                                   .ThenInclude(x => x.RestaurantCategory)
                                                   .Include(x => x.CityArea).ThenInclude(x => x.City)
                                                   .Include(x => x.RestaurantLevelEconomy)
                                                   .Include(x => x.RestaurantImages)
                                                   .Include(x => x.RestaurantWorkingHours)
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(x => x.Id == id);

            DetailRestaurantViewModel viewModel = new DetailRestaurantViewModel()
            {
                Id = model.Id,
                SellerId = model.CreatorUserId,
                AcceptableMinimumOrder = model.AcceptableMinimumOrder,
                CityArea = model.CityArea != null ? model.CityArea.Title : null,
                DeliveryTime = model.FromDeliveryTime,
                FullAddress = model.FullAddress,
                CreatorDateTime = model.CreatorDateTime,
                City = model.CityArea != null ? model.CityArea.City.Title : null,
                RestaurantCourierCost = model.RestaurantCourierCost,
                Title = model.Title,
                RestaurantLevelEconomy = model.RestaurantLevelEconomy != null ? model.RestaurantLevelEconomy.Title : null,
                TitleEng = model.TitleEng,
                RestaurantActivityStatus = model.RestaurantActivityStatus,
                WorkingHoursViewModels = model.RestaurantWorkingHours.OrderBy(o => o.DaysOfWeek).Select(s => new WorkingHoursViewModel()
                {
                    Day = s.DaysOfWeek.ToString(),
                    End = s.EndTime.ToString(),
                    Start = s.StartTime.ToString(),
                    Id = s.Id,
                    Title = s.DaysOfWeek.GetDescriptionString()
                }).ToList(),
                RestaurantImagesViewModels = model.RestaurantImages.Where(w => w.ImageType == Domain.Enum.ImageType.Header || w.ImageType == Domain.Enum.ImageType.Logo)
                                                                  .Select(s => new RestaurantImagesViewModel()
                                                                  {
                                                                      Extension = s.ImageExtension,
                                                                      Size = ImageManager.SizeSuffix(s.ImageSize),
                                                                      Type = s.ImageType.GetDescriptionString(),
                                                                      Url = s.ImageUrl
                                                                  }).ToList(),
                SellerRegistrationProcess = model.CreatorUser.Seller.SellerRegistrationProcess
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(DetailRestaurantViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var restaurant = await _restaurantService.GetAsync(x => x.CreatorUserId == viewModel.SellerId);
                //restaurant.ActivityStatus = viewModel.ActivityStatus;

                var seller = await _sellerService.GetAsync(x => x.Id == viewModel.SellerId);
                //مدارک تایید شد و تغییر وضعیت به ثبت رستوران
                seller.SellerRegistrationProcess = Domain.Enum.SellerRegistrationProcess.RestaurantRegistration;

                await _unitOfWork.SaveAllChangesAsync(updateCommonFields: false);
                Success(this, string.Format("این رستوران با موفقیت به وضعیت {0} تغییر پیدا کرد.", seller.SellerRegistrationProcess.GetDescriptionString()));
                //Success(this, string.Format("وضعیت رستوران {0} است.", restaurant.ActivityStatus.GetDescriptionString()));
                return RedirectToAction("index");
            }
            return View(viewModel);
        }

        public async Task<JsonResult> ActivitiStatus(int id, bool ischecked)
        {
            var model = await _restaurantService.GetAsync(x => x.Id == id);
            if (ischecked)
            {
                model.RestaurantActivityStatus = Domain.Enum.RestaurantActivityStatus.ReadyToOrder;
            }
            else if (ischecked == false)
            {
                model.RestaurantActivityStatus = Domain.Enum.RestaurantActivityStatus.UnReadyToOrder;
            }
            await _unitOfWork.SaveAllChangesAsync();
            return new JsonResult(new
            {
                success = true,
                message = string.Format("رستوران {0}، {1} است. ", model.Title, model.RestaurantActivityStatus.GetDescriptionString())
            });
        }

    }
}


