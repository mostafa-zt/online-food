using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Common.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
        public static string GetUsername(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("Username");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
        public static string GetEmail(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("Email");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }

        public static string GetRoleName(this System.Security.Principal.IPrincipal usr)
        {
            var roleNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("RoleName");
            if (roleNameClaim != null)
                return roleNameClaim.Value;
            return "";
        }

        public static string GetRestaurantTitle(this System.Security.Principal.IPrincipal usr)
        {
            var roleNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("RestaurantTitle");
            if (roleNameClaim != null)
                return roleNameClaim.Value;
            return "";
        }

        //public static Claim GetClaimFullname(this System.Security.Principal.IPrincipal usr )
        //{
        //    var claim  = ((ClaimsIdentity)usr.Identity).FindFirst("Fullname");
        //    return claim;
        //}

        //public static Claim GetClaimEmail(this System.Security.Principal.IPrincipal usr)
        //{
        //    var claim = ((ClaimsIdentity)usr.Identity).FindFirst("Email");
        //    return claim;
        //}
    }
}
