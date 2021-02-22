using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public User User { get; set; }
    }
}
