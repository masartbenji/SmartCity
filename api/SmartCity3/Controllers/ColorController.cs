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
    [Route("api/Color")]
    public class ColorController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public  ColorController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext ctx) : base(userManager)
        {
            this.ctx = ctx;
        }

        //GET :api/Color
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Color> GetColor()
        {
            return ctx.Color;
        }

        //Get: api/Color/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetColor([FromRoute]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var color = await ctx.Color.SingleOrDefaultAsync(m => m.Name == id);

            if (color == null) return NotFound();
            return Ok(color);
        }

        //PUT: api/Color/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor([FromRoute]string id, [FromBody] Color color)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != color.Name) return BadRequest();

            ctx.Entry(color).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        //POST: api/Color
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody]Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            ctx.Color.Add(color);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColorExists(color.Name))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColor", new { id = color.Name }, color);
        }

        //DELETE: api/Color/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            var color = await ctx.Color.SingleOrDefaultAsync(m => m.Name == id);

            if (color == null) return NotFound();

            ctx.Color.Remove(color);
            await ctx.SaveChangesAsync();
            return Ok(color);
        }

        private bool ColorExists(string id)
        {
            return ctx.Color.Any(e => e.Name == id);
        }
    }
}
