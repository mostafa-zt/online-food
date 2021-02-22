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
    public class UserAddressTitleController : BaseController
    {
        #region Private Properties and Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAddressTitleService _userAddressTitleService;

        public UserAddressTitleController(IUnitOfWork unitOfWork, IUserAddressTitleService userAddressTitleService)
        {
            _unitOfWork = unitOfWork;
            _userAddressTitleService = userAddressTitleService;
        }
        #endregion

        #region List
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchUserAddressTitleViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _userAddressTitleService.GetAll();

            // جستجو
            string sTitle = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            DateTime sFromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            DateTime sToCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            query = query.Where(w => (sTitle == null || sTitle.Equals(w.Title)) &&
                                     (sFromCreatorDateTime == DateTime.MinValue || sFromCreatorDateTime.Date <= w.CreatorDateTime.Value.Date) &&
                                     (sToCreatorDateTime == DateTime.MinValue || sToCreatorDateTime.Date >= w.CreatorDateTime.Value.Date) &&
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
                                               ((param.start.Value * param.length.Value)+ (index + 1)).GetPersianNumber() ,
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
        public async Task<ActionResult> Create(CreateUserAddressTitleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new UserAddressTitle()
                {
                    Title = viewModel.Title,
                    ActivityStatus = viewModel.ActivityStatus,
                };
                _userAddressTitleService.Add(model);
                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "با موفقیت ثبت شد");
                return RedirectToAction("index");
            }
            return View(viewModel);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _userAddressTitleService.GetAsync(x => x.Id == id, asNoTracking: true);
            var viewModel = new EditUserAddressTitleViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ActivityStatus = model.ActivityStatus,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserAddressTitleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _userAddressTitleService.GetAsync(x => x.Id == viewModel.Id);
                model.Title = viewModel.Title;
                model.ActivityStatus = viewModel.ActivityStatus;

                await _unitOfWork.SaveAllChangesAsync();
                Success(this, "با موفقیت ویرایش شد");
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
                int result = await _userAddressTitleService.DeleteAsync(w => w.Id == id);
                if (result >= 1)
                {
                    return Json(new { succcess = true, message = "این موجودی با موفقیت حذف شد" });
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
                        return Json(new { succcess = false, message = "این موجودی در جدول دیگری استفاده شده است!" });
                    }
                    else
                    {
                        return Json(new { succcess = false, message = "خطا در حذف! لطفا مجددا سعی کنید" });
                    }
                }
            }
            return Json(new { succcess = false, message = "خطا در حذف! لطفا مجددا سعی کنید" });
        }
        #endregion
    }
}