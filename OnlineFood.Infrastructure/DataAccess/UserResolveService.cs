using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OnlineFood.Infrastructure.DataAccess
{
    public class UserResolveService : IUserResolveService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserResolveService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetCurrentSessionUserId(ApplicationDbContext dbContext)
        {
            var currentSessionUsername = httpContextAccessor.HttpContext.User.Identity.Name;

            var user = await dbContext.Users.SingleAsync(u => u.UserName.Equals(currentSessionUsername));
            return user.Id.ToString();
        }
    }
}
