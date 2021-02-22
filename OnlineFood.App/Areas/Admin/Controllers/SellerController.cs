using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Extensions;
using OnlineFood.Common.Model;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using OnlineFood.Web.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.Controllers
{
    public class SellerController : BaseController
    {
        #region Private Properties and Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISellerService _sellerService;
        private readonly ICityService _cityService;
        private readonly IRestaurantCategoryService _restaurantCategoryService;
        private readonly ISellerPositionService _sellerPositionService;
        private readonly IRestaurantService _restaurantService;


        public SellerController(IUnitOfWork unitOfWork, ISellerService sellerService,
                                ICityService cityService, IRestaurantCategoryService restaurantCategoryService,
                                ISellerPositionService sellerPositionService,
                                IRestaurantService restaurantService)
        {
            _unitOfWork = unitOfWork;
            _sellerService = sellerService;
            _cityService = cityService;
            _restaurantCategoryService = restaurantCategoryService;
            _sellerPositionService = sellerPositionService;
            _restaurantService = restaurantService;
        }
        #endregion


        public IActionResult Index()
        {
            ViewBag.Cities = _cityService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            ViewBag.RestaurantCategories = _restaurantCategoryService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            ViewBag.SellerPositions = _sellerPositionService.GetAll(c => c.ActivityStatus == Domain.Enum.ActivityStatus.Available).Select(s => new SelectListItem() { Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();

            //var viewModel = new SellerViewModel()
            //{
            //    SellerRegistrationProcess = Enum.GetValues(typeof(Domain.Enum.SellerRegistrationProcess)).Cast<Domain.Enum.SellerRegistrationProcess>().Select(s => new SelectListItem()
            //    {
            //        Text = s.GetDescriptionString(),
            //        Value = ((int)s).ToString(),
            //    }).ToList()
            //};

            return View();
        }

        [HttpPost]
        public JsonResult GetData(jQueryDataTableParamModel param, SearchSellerViewModel filter)
        {
            //initialize the datatable from the HTTP request
            var searchString = Request.Form.FirstOrDefault(w => w.Key == "search[value]").Value.ToString();
            var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var sortDirection = Request.Form["order[0][dir]"]; // asc or desc

            var totalRecords = 0;
            var filteredRecords = 0;
            param.start = param.start.HasValue ? param.start / 10 : 0;

            var query = _sellerService.AllInclude(x => x.City, x => x.User, x => x.RestaurantCategory, x => x.SellerPosition).GetAll();

            // جستجو
            //اطلاعات حقیقی
            string nationalCode = !string.IsNullOrEmpty(filter.NationalCode) ? filter.NationalCode.Trim() : null;
            string birthCertificateNumber = !string.IsNullOrEmpty(filter.BirthCertificateNumber) ? filter.BirthCertificateNumber.Trim() : null;
            string firstname = !string.IsNullOrEmpty(filter.Firstname) ? filter.Firstname.Trim() : null;
            string lastname = !string.IsNullOrEmpty(filter.Lastname) ? filter.Lastname.Trim() : null;
            DateTime birthDate = !string.IsNullOrEmpty(filter.BirthDate) ? DateConverter.GetGregorianDate(filter.BirthDate.GetEnglishNumber2(), out bool birthDateSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            //حقوقی
            string companyTitle = !string.IsNullOrEmpty(filter.CompanyTitle) ? filter.CompanyTitle.Trim() : null;
            string registrationNumber = !string.IsNullOrEmpty(filter.RegistrationNumber) ? filter.RegistrationNumber.Trim() : null;
            string nationalId = !string.IsNullOrEmpty(filter.NationalId) ? filter.NationalId.Trim() : null;
            string economicCode = !string.IsNullOrEmpty(filter.EconomicCode) ? filter.EconomicCode.Trim() : null;
            //اطلاعات تماس
            string zipCode = !string.IsNullOrEmpty(filter.ZipCode) ? filter.ZipCode.Trim() : null;
            string telphoneNumber = !string.IsNullOrEmpty(filter.TelphoneNumber) ? filter.TelphoneNumber.Trim() : null;
            string phoneNumber = !string.IsNullOrEmpty(filter.PhoneNumber) ? filter.PhoneNumber.Trim() : null;
            //اطلاعات تجاری
            string shabaNumber = !string.IsNullOrEmpty(filter.ShabaNumber) ? filter.ShabaNumber.Trim() : null;
            string title = !string.IsNullOrEmpty(filter.Title) ? filter.Title.Trim() : null;
            //کاربری
            string username = !string.IsNullOrEmpty(filter.Username) ? filter.Username.Trim() : null;
            string email = !string.IsNullOrEmpty(filter.Email) ? filter.Email.Trim() : null;

            string trackingCode = !string.IsNullOrEmpty(filter.TrackingCode) ? filter.TrackingCode.Trim() : null;
            DateTime fromCreatorDateTime = !string.IsNullOrEmpty(filter.FromCreatorDateTime) ? DateConverter.GetGregorianDate(filter.FromCreatorDateTime.GetEnglishNumber2(), out bool sFromCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;
            DateTime toCreatorDateTime = !string.IsNullOrEmpty(filter.ToCreatorDateTime) ? DateConverter.GetGregorianDate(filter.ToCreatorDateTime.GetEnglishNumber2(), out bool sToCreatorDateTimeSuccess, "yyyy/MM/dd") : DateTime.MinValue;

            query = query.Where(w => (nationalCode == null || nationalCode.Equals(w.SellerPersonal.NationalCode)) &&
                                     (birthCertificateNumber == null || birthCertificateNumber.Equals(w.SellerPersonal.BirthCertificateNumber)) &&
                                     (firstname == null || firstname.Equals(w.SellerPersonal.Firstname)) &&
                                     (lastname == null || lastname.Equals(w.SellerPersonal.Lastname)) &&
                                     (filter.Gender == null || !filter.Gender.Any() || filter.Gender.Any(a => a == (int)w.SellerPersonal.Gender)) &&
                                     (birthDate == DateTime.MinValue || birthDate.Date <= w.SellerPersonal.BirthDate) && //حقیقی

                                     (companyTitle == null || companyTitle.Equals(w.SellerLegal.CompanyTitle)) &&
                                     (registrationNumber == null || registrationNumber.Equals(w.SellerLegal.RegistrationNumber)) &&
                                     (nationalId == null || nationalId.Equals(w.SellerLegal.NationalId)) &&
                                     (filter.CompanyType == null || !filter.CompanyType.Any() || filter.CompanyType.Any(a => a == (int)w.SellerLegal.CompanyType)) &&
                                     (economicCode == null || economicCode.Equals(w.SellerLegal.EconomicCode)) && //حقوقی

                                     (zipCode == null || zipCode.Equals(w.ZipCode)) &&
                                     (telphoneNumber == null || telphoneNumber.Equals(w.TelphoneNumber)) &&
                                     (filter.City == null || !filter.City.Any() || filter.City.Any(a => a == w.CityId)) &&
                                     (phoneNumber == null || phoneNumber.Equals(w.PhoneNumber)) && //تماس

                                     (filter.RestaurantCategory == null || !filter.RestaurantCategory.Any() || filter.RestaurantCategory.Any(a => a == w.RestaurantCategoryId)) &&
                                     (filter.SellerPosition == null || !filter.SellerPosition.Any() || filter.SellerPosition.Any(a => a == w.SellerPositionId)) &&
                                     (shabaNumber == null || shabaNumber.Equals(w.ShabaNumber)) &&
                                     (title == null || title.Equals(w.Title)) && //تجاری

                                     (username == null || username.Equals(w.User.UserName)) &&
                                     (email == null || email.Equals(w.User.Email)) &&
                                     //(filter.ActivityStatus == null || !filter.ActivityStatus.Any() || filter.ActivityStatus.Any(a => a == (int)w.ActivityStatus)) &&
                                     (filter.SellerType == null || !filter.SellerType.Any() || filter.SellerType.Any(a => a == (int)w.SellerType)) &&
                                     (trackingCode == null || trackingCode.Equals(w.TrackingCode)) &&
                                     (fromCreatorDateTime == DateTime.MinValue || fromCreatorDateTime.Date <= w.CreatorDateTime.Value.Date) &&
                                     (toCreatorDateTime == DateTime.MinValue || toCreatorDateTime.Date >= w.CreatorDateTime.Value.Date));


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
                        query = sortDirection == "asc" ? query.OrderBy(o => o.User.UserName) : query.OrderByDescending(o => o.User.UserName);
                        break;
                    }
                case 2:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.SellerPersonal.BirthDate).ThenBy(o => o.SellerPersonal.Gender) : query.OrderByDescending(o => o.SellerPersonal.BirthDate).ThenBy(o => o.SellerPersonal.Gender);
                        break;
                    }
                case 3:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.SellerLegal.CompanyType) : query.OrderByDescending(o => o.SellerLegal.CompanyType);
                        break;
                    }
                case 4:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.CityId) : query.OrderByDescending(o => o.CityId);
                        break;
                    }
                case 5:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.TelphoneNumber) : query.OrderByDescending(o => o.TelphoneNumber);
                        break;
                    }
                case 6:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.PhoneNumber) : query.OrderByDescending(o => o.PhoneNumber);
                        break;
                    }
                case 7:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.RestaurantCategoryId) : query.OrderByDescending(o => o.RestaurantCategoryId);
                        break;
                    }
                case 8:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.SellerPositionId) : query.OrderByDescending(o => o.SellerPositionId);
                        break;
                    }
                case 9:
                    {
                        query = sortDirection == "asc" ? query.OrderBy(o => o.TrackingCode) : query.OrderByDescending(o => o.TrackingCode);
                        break;
                    }
                case 10:
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
                                               HtmlGenerator.UserColumn(s.User.UserName , s.User.Email).ToString(),
                                               s.SellerType == Domain.Enum.SellerType.Legal ?  HtmlGenerator.LegalColumn(s.SellerLegal.CompanyTitle, s.SellerLegal.CompanyType.GetDescriptionString() , s.SellerLegal.RegistrationNumber , s.SellerLegal.NationalId , s.SellerLegal.EconomicCode).ToString() :
                                               HtmlGenerator.PersonalColumn(s.SellerPersonal.Gender.GetDescriptionString() , s.SellerPersonal.Firstname , s.SellerPersonal.Lastname , s.SellerPersonal.BirthCertificateNumber , s.SellerPersonal.NationalCode ,s.SellerPersonal.BirthDate.ToString()).ToString(),
                                               HtmlGenerator.RestaurantColumn(s.Title , s.RestaurantCategory.Title).ToString(),
                                               s.City.Title,
                                               s.TelphoneNumber,
                                               s.SellerPosition.Title,
                                               s.TrackingCode,
                                               HtmlGenerator.LabelColum(s.SellerRegistrationProcess.GetDescriptionString() , OnlineFood.Common.Utility.GeneralMethods.ChooseColor(s.SellerRegistrationProcess)).ToString(),
                                               s.CreatorDateTime.ToString(),
                                               HtmlGenerator.GeneralCommand(s.Id , hasRemove : true , hasEdit :false , hasDetail:true).ToString()
                                            });
            return Json(new
            {
                param.draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = records
            });
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _sellerService.AllInclude(x => x.SellerPosition, x => x.RestaurantCategory, x => x.City, x => x.User, x => x.SellerImages)
                                            .GetAsync(x => x.Id == id, asNoTracking: true);
            var restaurant = await _restaurantService.GetAsync(x => x.CreatorUserId == model.Id, asNoTracking: true);
            DetailSellerViewModel viewModel = new DetailSellerViewModel()
            {
                RestaurantId = restaurant != null ? restaurant.Id : 0,
                BirthDate = model.SellerPersonal.BirthDate,
                City = model.City.Title,
                CompanyTitle = model.SellerLegal.CompanyTitle,
                CompanyType = model.SellerLegal.CompanyType,
                EconomicCode = model.SellerLegal.EconomicCode,
                Title = model.Title,
                Email = model.User.Email,
                SellerType = model.SellerType,
                Firstname = model.SellerPersonal.Firstname,
                Lastname = model.SellerPersonal.Lastname,
                Gender = model.SellerPersonal.Gender,
                FullAddress = model.FullAddress,
                RegistrationNumber = model.SellerLegal.RegistrationNumber,
                NationalCode = model.SellerPersonal.NationalCode,
                NationalId = model.SellerLegal.NationalId,
                ShabaNumber = model.ShabaNumber,
                PhoneNumber = model.PhoneNumber,
                TelphoneNumber = model.TelphoneNumber,
                ZipCode = model.ZipCode,
                BirthCertificateNumber = model.SellerPersonal.BirthCertificateNumber,
                RestaurantCategory = model.RestaurantCategory.Title,
                SellerPosition = model.SellerPosition.Title,
                SellerRegistrationProcess = model.SellerRegistrationProcess,
                CreatorDateTime = model.CreatorDateTime,
                Id = model.Id,
                TrackingCode = model.TrackingCode,
                Username = model.User.UserName,
                SellerImageViewModels = model.SellerImages.Select(s => new SellerImageViewModel()
                {
                    Extension = s.ImageExtension,
                    Size = ImageManager.SizeSuffix(s.ImageSize),
                    Type = s.SellerImageType.GetDescriptionString(),
                    Url = s.ImageUrl
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(DetailSellerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = await _sellerService.GetAsync(x => x.Id == viewModel.Id);
                //مدارک تایید شد و تغییر وضعیت به تایید مدارک
                model.SellerRegistrationProcess = Domain.Enum.SellerRegistrationProcess.DocumentConfirmation;

                var foundRestaurant =await _restaurantService.GetAsync(x => x.CreatorUserId == model.Id, asNoTracking: true);
                if (foundRestaurant == null)
                {
                    //register restaurant
                    var restaurant = new Restaurant()
                    {
                        CreatorDateTime = DateTime.Now,
                        CreatorUserId = model.Id,
                        Title = model.Title,
                        FullAddress = model.FullAddress,
                        RestaurantActivityStatus = Domain.Enum.RestaurantActivityStatus.UnReadyToOrder
                    };
                    _restaurantService.Add(restaurant);
                }

                await _unitOfWork.SaveAllChangesAsync(updateCommonFields: false);
                Success(this, string.Format("این رستوران با موفقیت به وضعیت {0} تغییر پیدا کرد.", Domain.Enum.SellerRegistrationProcess.DocumentConfirmation.GetDescriptionString()));
                return RedirectToAction("index");
            }
            return View(viewModel);
        }

        #region private methods


        #endregion
    }
}