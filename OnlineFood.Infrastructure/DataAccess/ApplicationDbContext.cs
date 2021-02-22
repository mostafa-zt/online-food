using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Domain.Config;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OnlineFood.Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<
                                        User, Role, int,
                                        UserClaim, UserRole, UserLogin,
                                        RoleClaim, UserToken>, IUnitOfWork
    {

        //private readonly IUserResolveService _userResolveService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, /*IUserResolveService userResolveService,*/
                                    IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            //_userResolveService = userResolveService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new RoleClaimConfig());
            builder.ApplyConfiguration(new UserClaimConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
            builder.ApplyConfiguration(new UserTokenConfig());
            builder.ApplyConfiguration(new UserLoginConfig());
            builder.ApplyConfiguration(new UserWalletConfig());
            //builder.ApplyConfiguration(new CityAreaConfig());
            builder.ApplyConfiguration(new CityConfig());
            //builder.ApplyConfiguration(new OrderCartConfig());
            builder.ApplyConfiguration(new OrderConfig());
            builder.ApplyConfiguration(new OrderDetailConfig());
            builder.ApplyConfiguration(new OrderReservationConfig());
            //builder.ApplyConfiguration(new SellerPositionConfig());
            //builder.ApplyConfiguration(new RestaurantCityAreaConfig());
            builder.ApplyConfiguration(new RestaurantConfig());
            //builder.ApplyConfiguration(new RestaurantCategoryConfig());
            builder.ApplyConfiguration(new RestaurantFoodCategoryConfig());
            builder.ApplyConfiguration(new RestaurantLevelEconomyConfig());
            //builder.ApplyConfiguration(new RestaurantMenuCategoryConfig());
            builder.ApplyConfiguration(new RestaurantMenuConfig());
            //builder.ApplyConfiguration(new RestaurantMenuDetailConfig());
            builder.ApplyConfiguration(new RestaurantRateConfig());
            builder.ApplyConfiguration(new RestaurantStyleConfig());
            builder.ApplyConfiguration(new RestaurantTypeConfig());
            builder.ApplyConfiguration(new RestaurantWorkingHourConfig());
            builder.ApplyConfiguration(new RestaurantImageConfig());
            //builder.ApplyConfiguration(new SellerLegalTypeConfig());
            builder.ApplyConfiguration(new SellerConfig());
            //builder.ApplyConfiguration(new SellerPositionConfig());
            builder.ApplyConfiguration(new TicketConfig());
            //builder.ApplyConfiguration(new UserAddressConfig());
            //builder.ApplyConfiguration(new UserAddressTitleConfig());
            builder.ApplyConfiguration(new UserCommentConfig());
            builder.ApplyConfiguration(new UserRestaurantFavoriteConfig());
            builder.ApplyConfiguration(new UserRestaurantMenuFavoriteConfig());
        }
        #endregion

        #region IUnitOfWork
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public int SaveAllChanges(bool updateCommonFields = true)
        {
            if (updateCommonFields) UpdateCommonFields();
            return base.SaveChanges();
        }

        public Task<int> SaveAllChangesAsync(bool updateCommonFields = true)
        {
            if (updateCommonFields) UpdateCommonFields();
            return base.SaveChangesAsync();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }
        #endregion

        //private static string GetUserId(this System.Security.Claims.ClaimsPrincipal principal)
        //{
        //    if (principal == null)
        //        throw new ArgumentNullException(nameof(principal));

        //    return principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        //}

        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        #region Private Methods
        private void UpdateCommonFields()
        {
            DateTime dateTime = System.DateTime.Now;
            int? authenticatedUserId = null;
            string authenticatedUserName = "";
            //var currentSessionUserId = await _userResolveService.GetCurrentSessionUserId(this);



            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    authenticatedUserName = httpContext.User.Identity.Name;
                    // If it returns null, even when the user was authenticated, you may try to get the value of a specific claim 
                    authenticatedUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                }
                // var authenticatedUserId = _httpContextAccessor.HttpContext.User.FindFirst("sub").Value
                // TODO use name to set the shadow property value like in the following post: https://www.meziantou.net/2017/07/03/entity-framework-core-generate-tracking-columns
            }
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorDateTime = dateTime;
                        entry.Entity.CreatorUserId = authenticatedUserId;
                        //entry.Entity.CreatorUserIpAddress = UserIp;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifierDateTime = dateTime;
                        entry.Entity.ModifierUserId = authenticatedUserId;
                        //entry.Entity.ModifierUserIpAddress = UserIp;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        //entry.Entity.IsDeleted = true;
                        //entry.Entity.DeletedDateTime = DateTime;
                        //entry.State = EntityState.Modified;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        #region OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Microsoft.Extensions.Configuration.IConfigurationRoot configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //if (!optionsBuilder.IsConfigured)
            //{
            //    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-CEODST7;Database=OnlineFood;User Id=sa;Password=123;MultipleActiveResultSets=true;Trusted_Connection=True;");
            //}
            //optionsBuilder.UseLazyLoadingProxies().UseSqlServer("DefaultConnection");
        }
        #endregion
    }
}
