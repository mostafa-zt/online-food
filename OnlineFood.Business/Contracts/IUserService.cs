using OnlineFood.Common.Model;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Business.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        Task<ResultOperation> SignInClaimAsync(string authenticationSchema, string username, string password);
    }
}
