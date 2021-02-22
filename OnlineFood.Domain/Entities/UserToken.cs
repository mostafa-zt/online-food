using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public User User { get; set; }
    }
}
