using OnlineFood.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Business.Contracts
{
    public interface ISellerImageService : IBaseService<SellerImage>
    {
        /// <summary>
        /// save front of national card
        /// </summary>
        /// <param name="file"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> SaveFrontOfNationalCardFile(IFormFile file, User user);

        /// <summary>
        /// save behind of national card
        /// </summary>
        /// <param name="file"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> SaveBehindOfNationalCardFile(IFormFile file, User user);

        /// <summary>
        /// validate for national card
        /// </summary>
        /// <param name="frontOfNationalCardFile"></param>
        /// <param name="behindOfNationalCardFile"></param>
        /// <returns></returns>
        bool ValidateNationalCard(IFormFile frontOfNationalCardFile, IFormFile behindOfNationalCardFile);
    }
}
