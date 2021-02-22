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

namespace OnlineFood.Business.Services
{
    public class SellerImageService : BaseService<SellerImage>, ISellerImageService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<SellerImage> query;

        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Ctor
        public SellerImageService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext,
                                  IHostingEnvironment hostingEnvironment) : base(unitOfWork, dbContext)

        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<SellerImage>();
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region save front of national card
        /// <summary>
        /// ذخیره تصویر روی کارت ملی
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> SaveFrontOfNationalCardFile(IFormFile file, User user)
        {
            bool isValid = false;
            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = "";
            string extension = "";
            string imageUrl = "";
            string serverPath;
            try
            {
                /// ذخیره تصویر روی کارت ملی
                extension = Path.GetExtension(file.FileName);
                serverPath = ImageManager.GetImageLink(webRootPath, ImageManager.DoumentFolder, user.UserName, extension, ref fileName); //get server path
                imageUrl = ImageManager.GetImageUrl(ImageManager.DoumentFolder, user.UserName, fileName); //get url image for saving in database
                Stream stream = new MemoryStream(); //Copy contents to memory stream.
                file.CopyTo(stream);
                stream.Position = 0;
                //String serverPath = link;
                using (FileStream writerFileStream = System.IO.File.Create(serverPath))  // Save the file
                {
                    await stream.CopyToAsync(writerFileStream);
                    writerFileStream.Dispose();
                }

                query.Add(new SellerImage()
                {
                    ImageExtension = extension,
                    ImageFileName = file.FileName,
                    ImageSize = file.Length,
                    ImageUrl = imageUrl,
                    SellerImageType = Domain.Enum.SellerImageType.FrontTheCard,
                    SellerId = user.Id
                });
                isValid = true;
            }
            catch (Exception)
            {
                throw new Exception("خطایی در ذخیره فایل رخ داده است ");
            }
            return isValid;
        }
        #endregion

        #region save behind of national card
        /// <summary>
        /// ذخیره تصویر پشت کارت ملی
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> SaveBehindOfNationalCardFile(IFormFile file, User user)
        {
            bool isValid = false;
            /// Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = "";
            string extension = "";
            string imageUrl = "";
            string serverPath;
            try
            {
                /// ذخیره تصویر پشت کارت ملی
                extension = Path.GetExtension(file.FileName);
                serverPath = ImageManager.GetImageLink(webRootPath, ImageManager.DoumentFolder, user.UserName, extension, ref fileName); //get server path
                imageUrl = ImageManager.GetImageUrl(ImageManager.DoumentFolder, user.UserName, fileName); //get url image for saving in database
                Stream stream = new MemoryStream(); //Copy contents to memory stream.
                file.CopyTo(stream);
                stream.Position = 0;
                //String serverPath = link;
                using (FileStream writerFileStream = System.IO.File.Create(serverPath))  // Save the file
                {
                    await stream.CopyToAsync(writerFileStream);
                    writerFileStream.Dispose();
                }
                query.Add(new SellerImage()
                {
                    ImageExtension = extension,
                    ImageFileName = file.FileName,
                    ImageSize = file.Length,
                    ImageUrl = imageUrl,
                    SellerImageType = Domain.Enum.SellerImageType.BehindTheCard,
                    SellerId = user.Id
                });
                isValid = true;
            }
            catch (Exception)
            {
                throw new Exception("خطایی در ذخیره فایل رخ داده است ");
            }
            return isValid;
        }
        #endregion

        public bool ValidateNationalCard(IFormFile frontOfNationalCardFile, IFormFile behindOfNationalCardFile)
        {
            bool isValid = false;
            string extensionFrontOfNationalCardFile = Path.GetExtension(frontOfNationalCardFile.FileName);
            string extensionBehindOfNationalCardFile = Path.GetExtension(behindOfNationalCardFile.FileName);
            // Get the mime type
            var mimeTypeFrontOfNationalCardFile = frontOfNationalCardFile.ContentType;
            var mimeTypeBehindOfNationalCardFile = behindOfNationalCardFile.ContentType;
            if (ImageManager.IsValidMimeType(mimeTypeFrontOfNationalCardFile) || ImageManager.IsValidExtension(extensionFrontOfNationalCardFile) ||
                ImageManager.IsValidMimeType(mimeTypeBehindOfNationalCardFile) || ImageManager.IsValidExtension(extensionBehindOfNationalCardFile))
                isValid = true;
            return isValid;
        }

        public bool ValidateImageFiles(List<IFormFile> files)
        {
            bool isValid = false;
            if (files.Any())
            {
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string extension = System.IO.Path.GetExtension(file.FileName);
                        var mimType = file.ContentType;
                        if (ImageManager.IsValidMimeType(mimType) && ImageManager.IsValidExtension(extension))
                            isValid = true;
                    }
                }
            }
            return isValid;
        }
    }
}
