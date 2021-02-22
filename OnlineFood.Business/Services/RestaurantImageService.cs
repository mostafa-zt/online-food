using OnlineFood.Business.Contracts;
using OnlineFood.Common.Utility;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineFood.Common.Model;

namespace OnlineFood.Business.Services
{
    public class RestaurantImageService : BaseService<RestaurantImage>, IRestaurantImageService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<RestaurantImage> query;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRestaurantService _restaurantService;

        #endregion

        #region Ctor
        public RestaurantImageService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext,
                                      IHostingEnvironment hostingEnvironment,
                                      IRestaurantService restaurantService) : base(unitOfWork, dbContext)

        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<RestaurantImage>();
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _restaurantService = restaurantService;
        }
        #endregion

        #region save restaurant image
        public async Task<RestaurantImage> SaveFile(IFormFile file, User user, Domain.Enum.ImageType imageType, CrudOperation crudOperation = CrudOperation.Create)
        {
            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = "";
            string extension = "";
            string imageUrl = "";
            string serverPath;
            var restaurant = await _restaurantService.GetAll(x => x.CreatorUserId == user.Id, asNoTracking: true)
                                                     .Select(s => new { Id = s.Id })
                                                     .FirstOrDefaultAsync();
            try
            {
                extension = Path.GetExtension(file.FileName);
                serverPath = ImageManager.GetImageLink(webRootPath, ImageManager.RestaurantFolder, user.UserName, extension, ref fileName); //get server path
                imageUrl = ImageManager.GetImageUrl(ImageManager.RestaurantFolder, user.UserName, fileName); //get url image for saving in database
                Stream stream = new MemoryStream(); //Copy contents to memory stream.
                file.CopyTo(stream);
                stream.Position = 0;
                //String serverPath = link;
                using (FileStream writerFileStream = System.IO.File.Create(serverPath))  // Save the file
                {
                    await stream.CopyToAsync(writerFileStream);
                    writerFileStream.Dispose();
                }
                
                var model = new RestaurantImage()
                {
                    ImageExtension = extension,
                    ImageFileName = file.FileName,
                    ImageSize = file.Length,
                    ImageUrl = imageUrl,
                    ImageType = imageType,
                    RestaurantId = restaurant.Id
                };
                if (crudOperation == CrudOperation.Create)
                    query.Add(model);
                
                return model;
            }
            catch (Exception)
            {
                throw new Exception("An Error Occurred ");
            }
        }
        #endregion
    }


}
