using Microsoft.AspNetCore.Mvc;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Seller.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.Controllers
{
    public class RestaurantTypeController : BaseController
    {
        #region Private Properties and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantTypeService _restaurantTypeService;

        public RestaurantTypeController(IUnitOfWork unitOfWork, IRestaurantTypeService restaurantTypeService)
        {
            _unitOfWork = unitOfWork;
            _restaurantTypeService = restaurantTypeService;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchRestaurantTypeViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _restaurantTypeService.GetAll();

            // جستجو
            string sTitle = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            //DateTime sFromCreatorDateTime = filter.FromCreatorDateTime.HasValue ? filter.FromCreatorDateTime.Value: DateTime.MinValue;
            //DateTime sToCreatorDateTime = filter.ToCreatorDateTime.HasValue ? filter.ToCreatorDateTime.Value : DateTime.MinValue;
            query = query.Where(w => (sTitle == null || sTitle.Equals(w.Title)) &&
                                     (!filter.FromCreatorDateTime.HasValue || filter.FromCreatorDateTime <= w.CreatorDateTime.Value) &&
                                     (!filter.ToCreatorDateTime.HasValue || filter.ToCreatorDateTime >= w.CreatorDateTime.Value) &&
                                     (filter.ActivityStatus == null || !filter.ActivityStatus.Any() || filter.ActivityStatus.Any(a => a == (int)w.ActivityStatus)));


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
                                               ((param.start.Value * param.length.Value)+ (index + 1)).ToString(),
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
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRestaurantTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new RestaurantType()
                {
                    Title = viewModel.Title,
                    ActivityStatus = viewModel.ActivityStatus,
                };
                _restaurantTypeService.Add(model);
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
            var model = await _restaurantTypeService.GetAsync(x => x.Id == id, asNoTracking: true);
            var viewModel = new EditRestaurantTypeViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ActivityStatus = model.ActivityStatus,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRestaurantTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _restaurantTypeService.GetAsync(x => x.Id == viewModel.Id);
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
                int result = await _restaurantTypeService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new { succcess = true, message = "This item has been successfully deleted" });
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
                        return Json(new { succcess = false, message = "It is not possible to remove!" });
                    }
                    else
                    {
                        return Json(new { succcess = false, message = "Error occurred!" });
                    }
                }
            }
            return Json(new { succcess = false, message = "Error occurred!" });
        }
        #endregion
    }
}