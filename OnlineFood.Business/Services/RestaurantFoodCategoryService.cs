using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class RestaurantFoodCategoryService : BaseService<RestaurantFoodCategory>, IRestaurantFoodCategoryService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<RestaurantFoodCategory> query;
        #endregion

        #region Ctor
        public RestaurantFoodCategoryService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<RestaurantFoodCategory>();
            _dbContext = dbContext;
        }
        #endregion
    }
}
