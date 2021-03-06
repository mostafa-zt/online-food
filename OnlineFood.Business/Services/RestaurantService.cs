﻿using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class RestaurantService : BaseService<Restaurant>, IRestaurantService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<Restaurant> query;
        #endregion

        #region Ctor
        public RestaurantService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<Restaurant>();
            _dbContext = dbContext;
        }
        #endregion
    }
}
