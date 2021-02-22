using OnlineFood.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Business.Security
{
    public class UserPaswordValidator : PasswordValidator<User>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            
            return base.ValidateAsync(manager, user, password);
        }
        public override bool IsDigit(char c)
        {
            return true;
        }
        public override bool IsLower(char c)
        {
            return true;
        }
        public override bool IsLetterOrDigit(char c)
        {
            return true;
        }
        public override bool IsUpper(char c)
        {
            return true;
        }
    }
}
