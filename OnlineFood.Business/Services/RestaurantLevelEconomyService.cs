using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
   public class RestaurantLevelEconomyService : BaseService<RestaurantLevelEconomy>, IRestaurantLevelEconomyService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<RestaurantLevelEconomy> query;
        #endregion

        #region Ctor
        public RestaurantLevelEconomyService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<RestaurantLevelEconomy>();
            _dbContext = dbContext;
        }
        #endregion
    }
}
