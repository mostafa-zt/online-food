using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class RestaurantMenuService : BaseService<RestaurantMenu>, IRestaurantMenuService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<RestaurantMenu> query;
        #endregion

        #region Ctor
        public RestaurantMenuService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<RestaurantMenu>();
            _dbContext = dbContext;
        }
        #endregion
    }
}