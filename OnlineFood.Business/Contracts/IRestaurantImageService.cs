using OnlineFood.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnlineFood.Common.Model;

namespace OnlineFood.Business.Contracts
{
    public interface IRestaurantImageService : IBaseService<RestaurantImage>
    {
        /// <summary>
        /// ذخیره تصویر رستوران
        /// </summary>
        /// <param name="file">image file</param>
        /// <returns></returns>
        Task<RestaurantImage> SaveFile(IFormFile file, User user, Domain.Enum.ImageType imageType, CrudOperation crudOperation = CrudOperation.Create);
    }


}
