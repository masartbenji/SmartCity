using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public StatusController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext ctx)
            : base(userManager)
        {
            this.ctx = ctx;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Status> GetStatus()
        {
            return ctx.Status;
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStatus([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var status = await ctx.Status.SingleOrDefaultAsync(m => m.Id == id);

            if (status == null) return NotFound();
            return Ok(status);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus([FromRoute]int id, [FromBody] Status status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != status.Id) return BadRequest();

            ctx.Entry(status).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody]Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            ctx.Status.Add(status);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusExists(status.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            var status = await ctx.Status.SingleOrDefaultAsync(m => m.Id == id);

            if (status == null) return NotFound();

            ctx.Status.Remove(status);
            await ctx.SaveChangesAsync();
            return Ok(status);
        }

        private bool StatusExists(int id)
        {
            return ctx.Status.Any(e => e.Id == id);
        }
    }
}
