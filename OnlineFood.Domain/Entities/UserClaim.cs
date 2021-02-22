using Microsoft.AspNetCore.Identity;

namespace OnlineFood.Domain.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}
