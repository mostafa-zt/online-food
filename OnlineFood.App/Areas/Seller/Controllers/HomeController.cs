using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class HomeController : BaseController
    {
   
        private readonly UserManager<User> _userManager;
       

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            return RedirectToAction("edit" , "profile"); ;
        }

        [Route("error/404")]
        [AllowAnonymous]
        public IActionResult Error404()
        {
            return View();
        }
    }
}