using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NewsApp.CORE.DBModels;
using System.Security.Claims;

namespace NewsApp.MVC.ClaimProvider
{
    public class UserClaimProvider : IClaimsTransformation
    {
        private readonly UserManager<AppUser> _userManager;
        public UserClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}
