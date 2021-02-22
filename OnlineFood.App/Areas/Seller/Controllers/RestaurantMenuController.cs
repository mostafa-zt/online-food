using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
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
    public class RestaurantMenuController : BaseController
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _hostingEnvironment; //for post file
        private readonly IUnitOfWork _unitOfWork;


        private readonly ISellerService _sellerService;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;
        private readonly IRestaurantTypeService _restaurantTypeService;

        private readonly IRestaurantService _restaurantService;

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

        private RestaurantMenuViewModel GetListRestaurantMenu(int? userId)
        {
            var restaurantmenus = _restaurantMenuService.GetQuery().OrderBy(o => o.CreatorDateTime)
                                                        .Where(x => x.CreatorUserId == userId)
                                                        .Include(x => x.RestaurantFoodCategory)
                                                        .Include(x => x.RestaurantImage);
            var viewModel = new RestaurantMenuViewModel()
            {
                ListRestaurantMenuViewModels = restaurantmenus.GroupBy(x => x.RestaurantFoodCategoryId, (key, selectors) => new ListRestaurantMenuViewModel()
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
                }).ToList()
            };
            return viewModel;
        }

        #region Manage
        public IActionResult Manage()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = _restaurantService.Get(x => x.CreatorUserId == userId);

            var restaurantImages = _restaurantImageService.GetAll(x => x.CreatorUserId == userId && x.ImageType == Domain.Enum.ImageType.Menu, asNoTracking: true);
            ManageRestaurantMenuViewModel viewModel = new ManageRestaurantMenuViewModel()
            {
                RestaurantId = restaurant.Id,
                RestaurantMenuViewModels = GetListRestaurantMenu(userId),
            };
            return View(viewModel);
        }
        #endregion


        public async Task<IActionResult> List()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            return new JsonResult(new
            {
                success = true,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                message = "",
                title = "",
            });
        }


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

        public async Task<IActionResult> Create()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = _restaurantService.Get(x => x.CreatorUserId == userId);
            CreateRestaurantMenuViewModel viewModel = new CreateRestaurantMenuViewModel()
            {
                RestaurantId = restaurant.Id,
                GalleryRestaurantImageViewModels = GetGalleryRestaurantImages(restaurant.Id)
            };
            return new JsonResult(new
            {
                success = true,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_Create.cshtml", viewModel),
                message = "",
                title = "",
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestaurantMenuViewModel viewModel)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));

            if (!viewModel.RestaurantFoodCategoryId.HasValue)
            {
                //AddErrors(this, "RestaurantFoodCategoryId", "گروه بندی غذا را وارد کنید");
                Messages.Add("گروه بندی غذا را وارد کنید");
                AddErrors(this, "", "");
            }
            if (!viewModel.RestaurantImageId.HasValue)
            {
                Messages.Add("تصویر غذا را انتخاب کنید");
                AddErrors(this, "", "");
            }
            if (ModelState.IsValid)
            {
                RestaurantMenu model = new RestaurantMenu()
                {
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    PriceDescription = viewModel.PriceDescription,
                    RestaurantFoodCategoryId = viewModel.RestaurantFoodCategoryId.Value,
                    RestaurantId = viewModel.RestaurantId,
                    Title = viewModel.Title,
                    RestaurantImageId = viewModel.RestaurantImageId
                };

                _restaurantMenuService.Add(model);
                await _unitOfWork.SaveAllChangesAsync();

                return new JsonResult(new
                {
                    success = true,
                    view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                    message = "با موفقیت ثبت شد",
                    title = "ثبت" + model.Title,
                });
            }
            viewModel.GalleryRestaurantImageViewModels = GetGalleryRestaurantImages(viewModel.RestaurantId);
            return new JsonResult(new
            {
                success = false,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_Create.cshtml", viewModel),
                messages = Messages,
                title = "خطا در ورود اطلاعات",
            });
        }


        public async Task<IActionResult> Edit(int id)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurantImages = _restaurantImageService.GetAll(x => x.CreatorUserId == userId && x.ImageType == Domain.Enum.ImageType.Menu, asNoTracking: true);
            var model = await _restaurantMenuService.AllInclude(x => x.RestaurantFoodCategory)
                                                    .GetAsync(x => x.Id == id, asNoTracking: true);
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
            return new JsonResult(new
            {
                success = true,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_Edit.cshtml", viewModel),
                message = "",
                title = "",
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRestaurantMenuViewModel viewModel)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            if (!viewModel.RestaurantFoodCategoryId.HasValue)
            {
                AddErrors(this, "RestaurantFoodCategoryId", "گروه بندی غذا را وارد کنید");
            }
            if (!viewModel.RestaurantImageId.HasValue)
            {
                AddErrors(this, "RestaurantFoodCategoryId", "تصویر غذا را انتخاب کنید");
            }
            if (ModelState.IsValid)
            {
                var model = await _restaurantMenuService.GetAsync(x => x.Id == viewModel.Id);
                model.Description = viewModel.Description;
                model.Price = viewModel.Price;
                model.PriceDescription = viewModel.PriceDescription;
                model.RestaurantFoodCategoryId = viewModel.RestaurantFoodCategoryId.Value;
                model.RestaurantId = viewModel.RestaurantId;
                model.Title = viewModel.Title;
                model.RestaurantImageId = viewModel.RestaurantImageId;

                await _unitOfWork.SaveAllChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                    message = "با موفقیت ثبت شد",
                    title = "ثبت " + viewModel.Title,
                });
            }
            viewModel.GalleryRestaurantImageViewModels = GetGalleryRestaurantImages(viewModel.RestaurantId);
            return new JsonResult(new
            {
                success = false,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_Edit.cshtml", viewModel),
                message = "لطفا مجددا سعی کنید",
                title = "خطا در ورود اطلاعات",
            });
        }

        public JsonResult GetRestaurantFoodCategory(string q, int restaurantId)
        {
            var result = _restaurantFoodCategoryService.GetAll(x => x.ActivityStatus == Domain.Enum.ActivityStatus.Available &&
                                                                    x.Title.Contains(q))
                                         .Select(s => new
                                         {
                                             //iconClassName = s.IconClassName,
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



        #region Delete
        public async Task<ActionResult> Delete(int id)
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            try
            {
                int result = await _restaurantMenuService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new
                    {
                        success = true,
                        view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
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
                            view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                            message = "این موجودی در جدول دیگری استفاده شده است!",
                            title = "خطا در حذف"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                            message = "لطفا مجددا سعی کنید",
                            title = "خطا در حذف"
                        });
                    }
                }
            }
            return Json(new
            {
                success = false,
                view = await _viewRenderService.RenderToStringAsync("/Areas/Seller/Views/RestaurantMenu/_List.cshtml", GetListRestaurantMenu(userId)),
                message = "لطفا مجددا سعی کنید",
                title = "خطا در حذف"
            });
        }
        #endregion
    }
}