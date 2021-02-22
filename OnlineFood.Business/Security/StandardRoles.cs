using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OnlineFood.Business.Security
{
    public class StandardRoles
    {
        #region DefaultRoles
        public const string Administrator = "Admin";
        public const string User = "User";
        public const string Seller = "Seller";
        #endregion

        #region GetSysmteRoles
        public static List<RoleRecord> GetSysmteRoles()
        {
            return new List<RoleRecord>()
            {
                new RoleRecord() { Description ="System Admin" , Name = Administrator },
                new RoleRecord() { Description  =  "User", Name = User },
                new RoleRecord() { Description = "Seller Admin" , Name = Seller}
            };
        }
        #endregion

        //public static IEnumerable<PermissionRecord> SystemRolesWithPermissions
        //{
        //    get
        //    {
        //        if (_rolesWithPermissionsLazy.IsValueCreated)
        //            return _rolesWithPermissionsLazy.Value;
        //        _rolesWithPermissionsLazy = new Lazy<IEnumerable<PermissionRecord>>(GetDefaultRolesWithPermissions);
        //        return _rolesWithPermissionsLazy.Value;
        //    }
        //}
        private static Lazy<IEnumerable<RoleRecord>> _permissionsLazy = new Lazy<IEnumerable<RoleRecord>>(GetSysmteRoles, LazyThreadSafetyMode.ExecutionAndPublication);
        public static IEnumerable<RoleRecord> Permissions => _permissionsLazy.Value;
    }

    public class RoleRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
