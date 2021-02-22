using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
   
    //[Route("admin")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            TempData.Clear();
            return View();
        }

        public IActionResult test()
        {
            TempData.Clear();
            return View();
        }
    }
}