﻿using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Services
{
    public class UserAddressTitleService : BaseService<UserAddressTitle>, IUserAddressTitleService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private DbSet<UserAddressTitle> query;
        #endregion

        #region Ctor
        public UserAddressTitleService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext) : base(unitOfWork, dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<UserAddressTitle>();
            _dbContext = dbContext;
        }
        #endregion
    }
}
