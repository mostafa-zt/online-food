using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{

    public class RestaurantImageController : BaseController
    {
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

        public RestaurantImageController(
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
          IRestaurantImageService restaurantImageService)
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
        }
        public async Task<IActionResult> Index(int restaurantId)
        {
            //var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = await _restaurantService.AllInclude(x => x.RestaurantImages).GetAsync(x => x.Id == restaurantId);
            RestaurantImageViewModel viewModel = new RestaurantImageViewModel()
            {
                RestaurantId = restaurant.Id,
                GalleryRestaurantImageViewModels = restaurant.RestaurantImages.Select(s => new GalleryRestaurantImageViewModel()
                {
                    Id = s.Id,
                    Extension = s.ImageExtension,
                    Size = ImageManager.SizeSuffix(s.ImageSize),
                    Type = s.ImageType.GetDescriptionString(),
                    Url = s.ImageUrl
                }).ToList(),

            };
            return View(viewModel);
        }

        #region Create
        [HttpGet()]
        public ActionResult Create(int restaurantId, string returnUrl)
        {
            var viewModel = new CreateRestaurantImageViewModel() { RestaurantId = restaurantId, ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRestaurantImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _restaurantService.AllInclude(x => x.CreatorUser)
                                                    .GetAsync(x => x.Id == viewModel.RestaurantId, asNoTracking: false);
                var result = await _restaurantImageService.SaveFile(viewModel.ImageFile, model.CreatorUser, Domain.Enum.ImageType.Menu);
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "با موفقیت ثبت شد");
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                    return Redirect(viewModel.ReturnUrl);
                else
                    return RedirectToAction("index", new { restaurantId = viewModel.RestaurantId });
            }
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id , string returnUrl)
        {
            var model = await _restaurantImageService.GetAsync(x => x.Id == id, asNoTracking: true);
            EditRestaurantImageViewModel viewModel = new EditRestaurantImageViewModel()
            {
                RestaurantId = model.RestaurantId,
                ImageFileId = model.Id,
                ImageFileName = model.ImageFileName,
                ImageFileUrl = model.ImageUrl ,
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRestaurantImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _restaurantImageService.GetAll(asNoTracking: false)
                                                         .Include(x => x.Restaurant).ThenInclude(x => x.CreatorUser)
                                                         .FirstOrDefaultAsync(x => x.Id == viewModel.ImageFileId);

                //var removeFile = await _restaurantImageService.DeleteAsync(x => x.Id == viewModel.ImageFileId);

                var url = _hostingEnvironment.WebRootPath + model.ImageUrl;
                if (System.IO.File.Exists(url))
                    System.IO.File.Delete(url);

                var user = model.Restaurant.CreatorUser;

                var saveFielModel = await _restaurantImageService.SaveFile(viewModel.ImageFile, user, Domain.Enum.ImageType.Menu, Common.Model.CrudOperation.Update);
                model.ImageExtension = saveFielModel.ImageExtension;
                model.ImageFileName = saveFielModel.ImageFileName;
                model.ImageSize = saveFielModel.ImageSize;
                model.ImageUrl = saveFielModel.ImageUrl;

                await _unitOfWork.SaveAllChangesAsync();

                Success(this, "با موفقیت ویرایش شد");
                if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                    return Redirect(viewModel.ReturnUrl);
                else
                    return RedirectToAction("index", new { restaurantId = viewModel.RestaurantId });
            }
            return View(viewModel);
        }
        #endregion

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _restaurantImageService.GetAsync(x => x.Id == id, asNoTracking: true);
            var result = await _restaurantImageService.DeleteAsync(x => x.Id == id);
            var url = _hostingEnvironment.WebRootPath + model.ImageUrl;
            System.IO.File.Delete(url);
            //Delete User Files as well  
            //var dirPath = Path.Combine(
            //               Directory.GetCurrentDirectory(),
            //               "wwwroot" + "/UserFiles/" + Convert.ToString(id) + "/");
            //Directory.Delete(dirPath, true);
            return new JsonResult(new
            {
                success = true,
                //code = code,
                message = "با موفقیت حذف شد",
                title = "حذف فایل"
            });
        }


        /// <summary>
        /// remove only image file not record of database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> Remove(int id)
        {
            var model = await _restaurantImageService.GetAsync(x => x.Id == id, asNoTracking: true);
            //var result = await _restaurantImageService.DeleteAsync(x => x.Id == id);
            var url = _hostingEnvironment.WebRootPath + model.ImageUrl;
            System.IO.File.Delete(url);
            return new JsonResult(new
            {
                success = true,
                //code = code,
                message = "با موفقیت حذف شد",
                title = "حذف فایل"
            });
        }
    }
}