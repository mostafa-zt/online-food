using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
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
    public class CityController : BaseController
    {
        #region Private Properties and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService, IUnitOfWork unitOfWork, ILogger<CityController> logger)
        {
            _cityService = cityService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region List
        //[Area("AdminArea")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[Area("AdminArea")]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchCityViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();//["search[value]"];
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var dateNow = DateTime.Now;
            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _cityService.GetAll();


            string sTitle = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            //bool? sActivityStatus = filter.ActivityStatus != null && filter.ActivityStatus.Any() ? Enum.GetValues(typeof(Domain.Enum.ActivityStatus)).Cast<Domain.Enum.ActivityStatus>().Where(w=> filter.ActivityStatus.Any(a=> (int)w == a))
            //Domain.Enum.ActivityStatus. filter.ActivityStatus. .Value == 1 ? true : false : true;

            //DateTime sFromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            //DateTime sToCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;


            query = query.Where(w => (sTitle == null || sTitle.Equals(w.Title)) &&
                                     (!filter.FromCreatorDateTime.HasValue || filter.FromCreatorDateTime <= w.CreatorDateTime.Value) &&
                                     (!filter.ToCreatorDateTime.HasValue || filter.ToCreatorDateTime >= w.CreatorDateTime.Value) &&
                                     (filter.ActivityStatus == null || !filter.ActivityStatus.Any() || filter.ActivityStatus.Any(a => a == (int)w.ActivityStatus))); //Enum.GetValues(typeof(Domain.Enum.ActivityStatus)).Cast<Domain.Enum.ActivityStatus>().Any(a => filter.ActivityStatus.Any(activity => (int)a == activity))));


            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(w => w.Title.Contains(searchString));
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
                        query = sortDirection == "asc" ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                        break;
                    }
                case 2:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.ActivityStatus) : query.OrderByDescending(o => o.ActivityStatus);
                        break;
                    }
                case 3:
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
                                               ((param.start.Value * param.length.Value)+ (index + 1)).ToString() ,
                                               s.Title,
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
            ViewBag.ListGneder = new List<City>() { new City() { Id = 1, Title = "رشت" }, new City() { Id = 2, Title = "شیراز" } }.Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() });
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new City()
                {
                    Title = viewModel.Title,
                    ActivityStatus = viewModel.ActivityStatus
                };
                _cityService.Add(model);
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "This item has been successfully saved");
                return RedirectToAction("index");
            }
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _cityService.GetAsync(x => x.Id == id, asNoTracking: true); // .Set<Gender>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var viewModel = new EditCityViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ActivityStatus = model.ActivityStatus,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _cityService.GetAsync(x => x.Id == viewModel.Id);
                model.Title = viewModel.Title;
                model.ActivityStatus = viewModel.ActivityStatus;

                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "This item has been successfully updated");
                return RedirectToAction("index");
            }
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
                int result = await _cityService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new { success = true, message = "This item has been successfully deleted" });
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
                        return Json(new { success = false, message = "It is not possible to remove!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error occurred!" });
                    }
                }
            }
            return Json(new { success = false, message = "Error occurred!" });
        }
        #endregion
    }
}