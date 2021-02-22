using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class SellerImageController : BaseController
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _hostingEnvironment; //for post file
        private readonly IUnitOfWork _unitOfWork;

        private readonly ISellerImageService _sellerImageService;


        public SellerImageController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILogger<AccountController> logger,
          IHostingEnvironment hostingEnvironment,
          IUnitOfWork unitOfWork,
          IViewRenderService viewRenderService,
          ISellerImageService sellerImageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _viewRenderService = viewRenderService;
            _sellerImageService = sellerImageService;
        }

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _sellerImageService.GetAsync(x => x.Id == id, asNoTracking: true);
            var result = await _sellerImageService.DeleteAsync(x => x.Id == id);
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
                message = "کد تایید به شماره موبایل شما ارسال شد.لطفا کد جدید را جهت تایید وارد کنید.",
                title = "ثبت رستوران"
            });
        }
    }
}