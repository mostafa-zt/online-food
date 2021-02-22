using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
    public class CityAreaController : BaseController
    {
        #region Private Properties and Constructor
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly ICityAreaService _cityAreaService;
        //private readonly ILogger<ExternalLoginModel> _logger;

        public CityAreaController(SignInManager<User> signInManager, UserManager<User> userManager,
                              ICityService cityService, IUnitOfWork unitOfWork/*, ILogger<ExternalLoginModel> logger*/,
                              ICityAreaService cityAreaService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _cityAreaService = cityAreaService;
            //_logger = logger;
        }
        #endregion

        #region List
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin", AuthenticationSchemes = "AdminAuth")]
        public ActionResult Index()
        {
            ViewBag.Cities = _cityService.GetAll().Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchCityAreaViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _cityAreaService.AllInclude(city => city.City).GetAll();

            // جستجو
            string sTitle = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            string sTitleEng = !string.IsNullOrEmpty(filter.TitleEng) ? filter.TitleEng.Trim() : null;
            DateTime sFromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            DateTime sToCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            query = query.Where(w => (sTitle == null || w.Title.Contains(sTitle)) &&
                                     (sTitleEng == null || w.TitleEng.Contains(sTitleEng)) &&
                                     (sFromCreatorDateTime == DateTime.MinValue || sFromCreatorDateTime.Date <= w.CreatorDateTime.Value.Date) &&
                                     (sToCreatorDateTime == DateTime.MinValue || sToCreatorDateTime.Date >= w.CreatorDateTime.Value.Date) &&
                                     (filter.ActivityStatus == null || !filter.ActivityStatus.Any() || filter.ActivityStatus.Any(a => a == (int)w.ActivityStatus)) &&
                                     (filter.Cities == null || !filter.Cities.Any() || filter.Cities.Any(a => a == w.CityId)));


            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(w => w.Title.Contains(searchString) || w.TitleEng.Contains(searchString));
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
                        query = sortDirection == "asc" ? query.OrderBy(o => o.CityId) : query.OrderByDescending(o => o.CityId);
                        break;
                    }
                case 2:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                        break;
                    }
                case 3:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.ActivityStatus) : query.OrderByDescending(o => o.ActivityStatus);
                        break;
                    }
                case 4:
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
                                               ((param.start.Value * param.length.Value)+ (index + 1)).GetPersianNumber() ,
                                               s.City.Title,
                                               s.Title,
                                               s.TitleEng,
                                               HtmlGenerator.GeographicalCoordinatesColumn(s.Latitude , s.Longitude).ToString(),
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
        public ActionResult Create()
        {
            ViewBag.Cities = _cityService.GetAll().Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCityAreaViewModel viewModel)
        {
            string titleEng = viewModel.TitleEng.Trim();
            string title = viewModel.Title.Trim();
            var foundCityArea = await _cityAreaService.GetAsync(x => x.TitleEng == titleEng || x.Title == title, asNoTracking: true);

            if (foundCityArea != null)
                ModelState.AddModelError("", "نام فارسی یا انگلیسی محله تکراری است");

            if (ModelState.IsValid)
            {
                var model = new CityArea()
                {
                    Title = viewModel.Title.Trim(),
                    TitleEng = viewModel.TitleEng.GetFriendlyTitle(),
                    ActivityStatus = viewModel.ActivityStatus,
                    CityId = viewModel.CityId,
                    Latitude = viewModel.Latitude.Trim(),
                    Longitude = viewModel.Longitude.Trim()
                };
                var userId = _userManager.GetUserId(User);
                _cityAreaService.Add(model);
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "با موفقیت ثبت شد");
                return RedirectToAction("create");
            }
            ViewBag.Cities = _cityService.GetAll().Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Cities = _cityService.GetAll().Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            var model = await _cityAreaService.GetAsync(x => x.Id == id, asNoTracking: true);
            var viewModel = new EditCityAreaViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                TitleEng = model.TitleEng,
                ActivityStatus = model.ActivityStatus,
                CityId = model.CityId,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCityAreaViewModel viewModel)
        {
            string titleEng = viewModel.TitleEng.Trim();
            string title = viewModel.Title.Trim();
            var foundCityArea = await _cityAreaService.GetAsync(x => (x.TitleEng == titleEng || x.Title == title) &&
                                                                      x.Id != viewModel.Id, asNoTracking: true);

            if (foundCityArea != null)
                ModelState.AddModelError("", "نام فارسی یا انگلیسی محله تکراری است");

            if (ModelState.IsValid)
            {
                var model = await _cityAreaService.GetAsync(x => x.Id == viewModel.Id);
                model.Title = viewModel.Title.Trim();
                model.TitleEng = viewModel.TitleEng.GetFriendlyTitle();
                model.ActivityStatus = viewModel.ActivityStatus;
                model.CityId = viewModel.CityId;
                model.Latitude = viewModel.Latitude.Trim();
                model.Longitude = viewModel.Longitude.Trim();

                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "با موفقیت ویرایش شد");
                return RedirectToAction("index");
            }
            ViewBag.Cities = _cityService.GetAll().Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
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
                int result = await _cityAreaService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new { success = true, message = "این موجودی با موفقیت حذف شد" });
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
                        return Json(new { success = false, message = "این موجودی در جدول دیگری استفاده شده است!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "خطا در حذف! لطفا مجددا سعی کنید" });
                    }
                }
            }
            return Json(new { success = false, message = "خطا در حذف! لطفا مجددا سعی کنید" });
        }
        #endregion
    }
}