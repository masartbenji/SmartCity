using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/UserRole")]
    public class UserRoleController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public UserRoleController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext ctx) : base(userManager)
        {
            this.ctx = ctx;
        }
        
    }
}
