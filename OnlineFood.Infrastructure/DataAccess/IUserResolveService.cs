using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Infrastructure.DataAccess
{
    public interface IUserResolveService
    {
        Task<string> GetCurrentSessionUserId(ApplicationDbContext dbContext);
    }
}
