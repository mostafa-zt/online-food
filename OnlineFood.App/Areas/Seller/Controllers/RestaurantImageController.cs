using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Seller.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class RestaurantImageController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _hostingEnvironment; //for post file
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantLevelEconomyService _restaurantLevelEconomyService;
        private readonly IRestaurantTypeService _restaurantTypeService;
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantImageService _restaurantImageService;

        public RestaurantImageController(
          UserManager<User> userManager,
          ILogger<AccountController> logger,
          IHostingEnvironment hostingEnvironment,
          IUnitOfWork unitOfWork,
          IViewRenderService viewRenderService,
          IRestaurantLevelEconomyService restaurantLevelEconomyService,
          IRestaurantTypeService restaurantTypeService,
          IRestaurantImageService restaurantImageService,
          IRestaurantService restaurantService)
        {
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _viewRenderService = viewRenderService;
            _restaurantLevelEconomyService = restaurantLevelEconomyService;
            _restaurantTypeService = restaurantTypeService;
            _restaurantImageService = restaurantImageService;
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Manage()
        {
            var userId = System.Convert.ToInt32(_userManager.GetUserId(User));
            var restaurant = await _restaurantService.AllInclude(x => x.RestaurantImages).GetAsync(x => x.CreatorUserId == userId);
            UploadRestaurantImageViewModel viewModel = new UploadRestaurantImageViewModel()
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

        public async Task<JsonResult> Upload(int id, IFormFile ImageFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ImageFile != null && ImageFile.Length > 0)
            {
                try
                {
                    var result = await _restaurantImageService.SaveFile(ImageFile, user, Domain.Enum.ImageType.Menu);
                    await _unitOfWork.SaveAllChangesAsync();

                    return new JsonResult(new
                    {
                        success = true,
                        message = "Your image has been successfully uploaded",
                        title = "Uploading File",
                        url = result.ImageUrl,
                        type = result.ImageType.GetDescriptionString(),
                        extension = result.ImageExtension,
                        size= ImageManager.SizeSuffix(result.ImageSize),
                        id = result.Id
                    });
                }
                catch (Exception ex)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = ex.Message,
                        title = "An error occurred"
                    });
                }

            }
            return new JsonResult(new
            {
                success = false,
                message = "There is no file!",
                title = "An error occurred"
            });
        }

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
                message = "Your image has been successfully deleted",
                title = "Removing File"
            });
        }
    }
}