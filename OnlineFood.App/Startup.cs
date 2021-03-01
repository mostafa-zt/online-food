using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineFood.Business.Contracts;
using OnlineFood.Business.Security;
using OnlineFood.Business.Services;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineFood.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options => options.Stores.MaxLengthForKeys = 128)
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                  .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "OnlineFoodApp";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Seller/Account/Login";
                options.LogoutPath = "/Seller/Account/Logout";
                // ReturnUrlParameter requires 
                options.ReturnUrlParameter = "RedirectUrl";
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                // Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            //Dependency injection
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddTransient<IUserResolveService, UserResolveService>();

            services.AddTransient<ICityService, CityService>();
            //services.AddTransient<ICityAreaService, CityAreaService>();
            services.AddTransient<IRestaurantFoodCategoryService, RestaurantFoodCategoryService>();
            services.AddTransient<IRestaurantLevelEconomyService, RestaurantLevelEconomyService>();
            services.AddTransient<IRestaurantTypeService, RestaurantTypeService>();
            //services.AddTransient<IUserAddressTitleService, UserAddressTitleService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            //services.AddTransient<IRestaurantCityAreaService, RestaurantCityAreaService>();
            services.AddTransient<IRestaurantImageService, RestaurantImageService>();
            services.AddTransient<IRestaurantMenuService, RestaurantMenuService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetService<IActionContextAccessor>().ActionContext;
                //var factory = x.GetRequiredService<IUrlHelperFactory>();
                return new UrlHelper(actionContext);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                // When the app runs in the Development environment:
                //   Use the Developer Exception Page to report app runtime errors.
                //   Use the Database Error Page to report database runtime errors.
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // When the app doesn't run in the Development environment:
                //   Enable the Exception Handler Middleware to catch exceptions
                //     thrown in the following middlewares.
                //   Use the HTTP Strict Transport Security Protocol (HSTS)
                //     Middleware.
                app.UseExceptionHandler("/Error");
                //app.UseHsts();
            }

            // Use HTTPS Redirection Middleware to redirect HTTP requests to HTTPS.
            //app.UseHttpsRedirection();
            // Return static files and end the pipeline.
            app.UseStaticFiles();
            // Use Cookie Policy Middleware to conform to EU General Data 
            // Protection Regulation (GDPR) regulations.
            app.UseCookiePolicy();
            // Authenticate before the user accesses secure resources.
            app.UseAuthentication();
            // If the app uses session state, call Session Middleware after Cookie 
            // Policy Middleware and before MVC Middleware.
            //app.UseSession();

            //automatic migration and seed database
            UpdateDatabase(app);
            //seed database
            //new DataBaseService().SeedDataBaseAsync();

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            }).UseMvcWithDefaultRoute();
            var routess = new Microsoft.AspNetCore.Routing.RouteBuilder(app);

            // Create route collection and add the middleware.
            app.UseRouter(routess.Build());
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.EnsureCreated();
                    context.Database.Migrate();

                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                    DataBaseService.SeedDataBaseAsync(userManager, roleManager, context).Wait();
                }
            }
        }

    }
}
