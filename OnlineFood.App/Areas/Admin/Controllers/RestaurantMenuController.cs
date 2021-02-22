using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
    public class RestaurantMenuController : BaseController
    {
        #region Private Properties and Constructor

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _hostingEnvironment; //for post file
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISellerImageService _sellerImageService;
        private readonly ICityAreaService _cityAreaService;
        private readonly ISellerService _sellerService;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;
        private readonly IRestaurantTypeService _restaurantTypeService;

        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantCityAreaService _restaurantCityAreaService;

        private readonly IRestaurantImageService _restaurantImageService;

        private readonly IRestaurantMenuService _restaurantMenuService;

        private readonly IRestaurantFoodCategoryService _restaurantFoodCategoryService;

        public RestaurantMenuController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILogger<AccountController> logger,
          IHostingEnvironment hostingEnvironment,
          IUnitOfWork unitOfWork,
          IViewRenderService viewRenderService,
          ISellerImageService sellerImageService,
          ICityAreaService cityAreaService,
          ISellerService sellerService,
          IRestaurantLevelEconomyService restaurantLevelEconomyService,
          IRestaurantTypeService restaurantTypeService,
          IRestaurantService restaurantService,
          IRestaurantCityAreaService restaurantCityAreaService,
          IRestaurantImageService restaurantImageService,
          IRestaurantMenuService restaurantMenuService,
          IRestaurantFoodCategoryService restaurantFoodCategoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _viewRenderService = viewRenderService;
            _sellerImageService = sellerImageService;
            _cityAreaService = cityAreaService;
            _sellerService = sellerService;
            _restaurantLevelEconomyService = restaurantLevelEconomyService;
            _restaurantTypeService = restaurantTypeService;

            _restaurantService = restaurantService;
            _restaurantCityAreaService = restaurantCityAreaService;

            _restaurantImageService = restaurantImageService;

            _restaurantMenuService = restaurantMenuService;

            _restaurantFoodCategoryService = restaurantFoodCategoryService;
        }
        #endregion
        public IActionResult Index(int restaurantId)
        {
            var restaurantmenus = _restaurantMenuService.GetAll()
                                                        .AsNoTracking()
                                                        .Include(x => x.Restaurant)
                                                        .Include(x => x.RestaurantFoodCategory)
                                                        .Include(x => x.RestaurantImage)
                                                        .Where(w => w.RestaurantId == restaurantId);


            var query = restaurantmenus.GroupBy(x => x.RestaurantFoodCategoryId, (key, selectors) => new ListRestaurantMenuViewModel()
            {
                RestaurantFoodCategoryId = key,
                RestaurantFoodCategory = selectors.Any() ? selectors.FirstOrDefault().RestaurantFoodCategory.Title : null,
                //RestaurantFoodCategoryClassname = selectors.Any() ? selectors.FirstOrDefault().RestaurantFoodCategory.IconClassName : null,
                SubListRestaurantMenuViewModels = selectors.Select(s => new SubListRestaurantMenuViewModel()
                {
                    Id = s.Id,
                    ImageUrl = s.RestaurantImage.ImageUrl,
                    ImageId = s.RestaurantImageId,
                    Price = s.Price,
                    Title = s.Title,
                    PriceDescription = s.PriceDescription,
                    TitleDescription = s.Description
                }).ToList()
            }).ToList();

            var viewModel = new RestaurantMenuViewModel()
            {
                ListRestaurantMenuViewModels = query
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));

            var model = await _restaurantMenuService.AllInclude(x => x.RestaurantFoodCategory)
                                                    .GetAsync(x => x.Id == id, asNoTracking: true);

            var restaurantImages = _restaurantImageService.GetAll(x => x.RestaurantId == model.RestaurantId && x.ImageType == Domain.Enum.ImageType.Menu, asNoTracking: true);

            EditRestaurantMenuViewModel viewModel = new EditRestaurantMenuViewModel()
            {
                RestaurantId = model.RestaurantId,
                Id = model.Id,
                Description = model.Description,
                Price = model.Price,
                PriceDescription = model.PriceDescription,
                RestaurantFoodCategory = model.RestaurantFoodCategory.Title,
                RestaurantFoodCategoryId = model.RestaurantFoodCategoryId,
                RestaurantImageId = model.RestaurantImageId,
                Title = model.Title,
                //ListRestaurantMenuViewModels = GetListRestaurantMenu(userId),
                GalleryRestaurantImageViewModels = restaurantImages.Select(s => new GalleryRestaurantImageViewModel()
                {
                    Id = s.Id,
                    Extension = s.ImageExtension,
                    Size = ImageManager.SizeSuffix(s.ImageSize),
                    Type = s.ImageType.GetDescriptionString(),
                    Url = s.ImageUrl
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRestaurantMenuViewModel viewModel)
        {
            //var userId = System.Convert.ToInt32(_userManager.GetUserId(User));

            if (ModelState.IsValid)
            {
                var model = await _restaurantMenuService.GetAsync(x => x.Id == viewModel.Id);
                model.Description = viewModel.Description;
                model.Price = viewModel.Price;
                model.PriceDescription = viewModel.PriceDescription;
                model.RestaurantFoodCategoryId = viewModel.RestaurantFoodCategoryId;
                //model.RestaurantId = viewModel.RestaurantId;
                model.Title = viewModel.Title;
                model.RestaurantImageId = viewModel.RestaurantImageId;

                await _unitOfWork.SaveAllChangesAsync();
                return RedirectToAction("index", new { restaurantId = viewModel.RestaurantId });
            }
            return View(viewModel);
        }




        #region Delete
        public async Task<ActionResult> Delete(int id)
        {
            //var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            try
            {
                int result = await _restaurantMenuService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new
                    {
                        success = true,
                        message = "با موفقیت حذف شد",
                        title = "حذف",
                    });
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
                        return Json(new
                        {
                            success = false,
                            message = "این موجودی در جدول دیگری استفاده شده است!",
                            title = "خطا در حذف"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "لطفا مجددا سعی کنید",
                            title = "خطا در حذف"
                        });
                    }
                }
            }
            return Json(new
            {
                success = false,
                message = "لطفا مجددا سعی کنید",
                title = "خطا در حذف"
            });
        }
        #endregion


        public JsonResult GetRestaurantFoodCategory(string q, int restaurantId)
        {
            var result = _restaurantFoodCategoryService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available &&
                                                                    x.Title.Contains(q))
                                         .Select(s => new
                                         {
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