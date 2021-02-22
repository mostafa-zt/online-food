using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OnlineFood.Common.Extensions;

namespace OnlineFood.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Private Properties and Constructor
        public HomeController()
        {

        }
        #endregion
        
        public ActionResult Index()
        {
            return Redirect("/seller/home/");
        }
    }
}
