using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class RestaurantCityAreaService : BaseService<RestaurantCityArea>, IRestaurantCityAreaService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<RestaurantCityArea> query;
        #endregion

        #region Ctor
        public RestaurantCityAreaService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<RestaurantCityArea>();
            _dbContext = dbContext;
        }
        #endregion
    }
}
