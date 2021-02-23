using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Seller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class RestaurantFoodCategoryController : BaseController
    {
        #region Private Properties and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantFoodCategoryService _restaurantFoodTypeService;
        private readonly UserManager<User> _userManager;
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantImageService _restaurantImageService;

        public RestaurantFoodCategoryController(IUnitOfWork unitOfWork, IRestaurantFoodCategoryService restaurantFoodTypeService, UserManager<User> userManager
            , IRestaurantImageService restaurantImageService, IRestaurantService restaurantService)
        {
            _unitOfWork = unitOfWork;
            _restaurantFoodTypeService = restaurantFoodTypeService;
            _userManager = userManager;
            _restaurantImageService = restaurantImageService;
            _restaurantService = restaurantService;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchRestaurantFoodCategoryViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _restaurantFoodTypeService.GetAll();

            // جستجو
            string sTitle = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            //DateTime sFromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            //DateTime sToCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            query = query.Where(w => (sTitle == null || sTitle.Equals(w.Title)) &&
                                     (!filter.FromCreatorDateTime.HasValue || filter.FromCreatorDateTime <= w.CreatorDateTime.Value) &&
                                     (!filter.ToCreatorDateTime.HasValue || filter.ToCreatorDateTime >= w.CreatorDateTime.Value) &&
                                     (filter.ActivityStatus == null || !filter.ActivityStatus.Any() || filter.ActivityStatus.Any(a => a == (int)w.ActivityStatus)));


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
                        query = sortDirection == "asc" ? query.OrderBy(o => o.ActivityStatus) : query.OrderByDescending(o => o.ActivityStatus);
                        break;
                    }
                case 3:
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
                                               ((param.start.Value * param.length.Value)+ (index + 1)).ToString() ,
                                               s.Title,
                                               //s.IconClassName,
                                               HtmlGenerator.LabelColum(s.ActivityStatus.GetDescriptionString() , GeneralMethods.ChooseColor(s.ActivityStatus)).ToString(),
                                               s.CreatorDateTime.HasValue ? s.CreatorDateTime.Value.ToString() :string.Empty,
                                               HtmlGenerator.GeneralCommand(s.Id , hasDetail:false).ToString()
                                            });
            return Json(new
            {
                param.draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = records
            });
        }
        #endregion

        #region Create
        [HttpGet()]
        public async Task<ActionResult> Create()
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = await _restaurantService.GetAsync(x => x.CreatorUserId == userId);
            CreateRestaurantFoodCategoryViewModel viewModel = new CreateRestaurantFoodCategoryViewModel()
            {
                GalleryRestaurantImageViewModels = GetGalleryRestaurantImages(restaurant.Id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRestaurantFoodCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new RestaurantFoodCategory()
                {
                    Title = viewModel.Title,
                    ActivityStatus = viewModel.ActivityStatus,
                    RestaurantImageId = viewModel.RestaurantImageId,
                    Price = viewModel.Price,
                    Description = viewModel.Description
                };
                _restaurantFoodTypeService.Add(model);
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "This item has been successfully saved");
                return RedirectToAction("index");
            }
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _restaurantFoodTypeService.GetAsync(x => x.Id == id, asNoTracking: true);
            var userId = Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = await _restaurantService.GetAsync(x => x.CreatorUserId == userId);
            var viewModel = new EditRestaurantFoodCategoryViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ActivityStatus = model.ActivityStatus,
                Price = model.Price,
                RestaurantImageId = model.RestaurantImageId,
                Description = model.Description,
                GalleryRestaurantImageViewModels = GetGalleryRestaurantImages(restaurant.Id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRestaurantFoodCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _restaurantFoodTypeService.GetAsync(x => x.Id == viewModel.Id);
                model.Title = viewModel.Title;
                model.ActivityStatus = viewModel.ActivityStatus;
                model.Description = viewModel.Description;
                model.Price = viewModel.Price;
                model.RestaurantImageId = viewModel.RestaurantImageId;

                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "This item has been successfully updated");
                return RedirectToAction("index");
            }
            return View(viewModel);
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int id)
        {
            //var model = await dbContext.Set<Gender>().FirstOrDefaultAsync(x => x.Id == id);
            //model.IsDeleted = true;
            //try
            //{
            //    var logicalDelete = await dbContext.SaveChangesAsync();
            //    return Json(new { succcess = true, Msg = "این موجودی با موفقیت حذف شد ", title = "جنسیت " }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception)
            //{
            //    return Json(new { succcess = false, Msg = "خطا در حذف،مجددا سعی نمایید", title = "جنسیت" }, JsonRequestBehavior.AllowGet);
            //}
            try
            {
                int result = await _restaurantFoodTypeService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new { succcess = true, message = "This item has been successfully deleted" });
                }
            }
            catch (Exception ex)
            {
                var sqlException = ex.GetBaseException() as System.Data.SqlClient.SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        return Json(new { succcess = false, message = "It is not possible to remove!" });
                    }
                    else
                    {
                        return Json(new { succcess = false, message = "Error occurred!" });
                    }
                }
            }
            return Json(new { succcess = false, message = "Error occurred!" });
        }
        #endregion


        private List<GalleryRestaurantImageViewModel> GetGalleryRestaurantImages(int restaurantId)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurantImages = _restaurantImageService.GetAll(img => img.RestaurantId == restaurantId && img.ImageType == Domain.Enum.ImageType.Menu, asNoTracking: true);
            var gallery = restaurantImages.Select(s => new GalleryRestaurantImageViewModel()
            {
                Id = s.Id,
                Extension = s.ImageExtension,
                Size = ImageManager.SizeSuffix(s.ImageSize),
                Type = s.ImageType.GetDescriptionString(),
                Url = s.ImageUrl
            }).ToList();
            return gallery;
        }
    }
}