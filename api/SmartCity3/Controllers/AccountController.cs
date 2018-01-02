using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SmartCity3.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SmartCity3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly _1718_etu32294_DB_SmartContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext _context,RoleManager<IdentityRole> roleManager) : base(userManager)
        {
            this._userManager = userManager;
            this._context = _context;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            return _context.User.ToList();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserName == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {
            var newUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };
            bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");
            IdentityResult roleResult;
            if (!adminRoleExists)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);
            if (result.Succeeded)
            {
                ApplicationUser current = await _userManager.FindByNameAsync(dto.UserName);
                result = await _userManager.AddToRoleAsync(current, "Admin");
            }
            roleResult = await _userManager.AddToRoleAsync(newUser, "Admin");
            // TODO: retourner un Created à la place du Ok;
            return (result.Succeeded ) ? Ok() : (IActionResult)BadRequest();
        }
        [HttpPost("Admin")]
        public IActionResult Admin([FromBody] NewUserDTO dto)
        {
            if (IsInRole("Admin"))
            {
                return Unauthorized();
            }
            else
            {
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] String username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uti = await _context.User.SingleOrDefaultAsync(m => m.UserName == username);
            if (uti == null)
            {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(uti);
            await _userManager.RemoveFromRolesAsync(uti,role);
            _context.User.Remove(uti);
            await _context.SaveChangesAsync();

            return Ok(uti);
        }

    }
}

