using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Business.Security
{
    public class StandardClaims
    {
        #region Claims
        public const string FullName = "FullName";
        public const string Email = "Email";
        public const string Username = "Username";
        public const string RoleName = "RoleName";
        #endregion
    }

    public class SellerClaims
    {
        #region Claims
        public const string RestaurantTitle = "RestaurantTitle";
        #endregion
    }
}
