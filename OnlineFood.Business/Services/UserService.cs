using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Business.Contracts;
using OnlineFood.Common.Model;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ISellerService _sellerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor
        public UserService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IHttpContextAccessor httpContextAccessor,
                           ISellerService sellerService) : base(unitOfWork, dbContext)

        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;

            _httpContextAccessor = httpContextAccessor;
            _sellerService = sellerService;
        }
        #endregion

        public async Task<ResultOperation> SignInClaimAsync(string authenticationSchema, string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
           
            // create claims
            var claims = await _userManager.GetClaimsAsync(user);
            var _roles = await _userManager.GetRolesAsync(user);
            claims = claims ?? new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (var item in _roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            // create identity
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            // sign-in
            var props = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddYears(1)  };
            await _httpContextAccessor.HttpContext.SignInAsync(scheme: authenticationSchema, principal: principal, properties: props);
            return ResultOperation.Succeeded;
        }

    }
}
