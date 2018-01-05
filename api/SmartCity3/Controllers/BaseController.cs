using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace SmartCity3.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<ApplicationUser> userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            if (this.HttpContext.User == null)
                throw new Exception("L'utilisateur n'est pas identifié");
            Claim userNameClaim = this.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userNameClaim == null)
                throw new Exception("Le token JWT semble ne pas avoir été interprété correctement");
            return await userManager.FindByNameAsync(userNameClaim.Value);
        }
        public bool IsInRole(string roleName)
        {
            var view = this.HttpContext.User.Claims;
            Claim roleClaim = view.FirstOrDefault(claim => claim.Type == "Role" && claim.Value == roleName);
            
            return roleClaim != null;
        }
        public async Task<IList<String>> GetUserRoles()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return await userManager.GetRolesAsync(user);
        }
    }
}
