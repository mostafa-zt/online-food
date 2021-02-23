using OnlineFood.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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