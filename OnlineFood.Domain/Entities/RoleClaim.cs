using Microsoft.AspNetCore.Identity;

namespace OnlineFood.Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public Role Role { get; set; }
    }
}
