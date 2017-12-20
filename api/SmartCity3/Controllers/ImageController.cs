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
    [Route("api/Images")]
    public class ImagesController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public ImagesController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext ctx) 
            : base(userManager)
        {
            this.ctx = ctx;
        }

        //GET :api/Images
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Images> GetImages()
        {
            return ctx.Images;
        }

        //Get: api/Images/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImages([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var image = await ctx.Images.SingleOrDefaultAsync(m => m.Id == id);

            if (image == null) return NotFound();
            return Ok(image);
        }

        //PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages([FromRoute]int id, [FromBody] Images image)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != image.Id) return BadRequest();

            ctx.Entry(image).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesExists(id))
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

        //POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody]Images image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            ctx.Images.Add(image);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImagesExists(image.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImages", new { id = image.Id }, image);
        }

        //DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            var image = await ctx.Images.SingleOrDefaultAsync(m => m.Id == id);

            if (image == null) return NotFound();

            ctx.Images.Remove(image);
            await ctx.SaveChangesAsync();
            return Ok(image);
        }

        private bool ImagesExists(int id)
        {
            return ctx.Images.Any(e => e.Id == id);
        }
    }
}
