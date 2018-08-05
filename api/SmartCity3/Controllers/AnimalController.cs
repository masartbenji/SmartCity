using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SmartCity3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Animal")]
    public class AnimalController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public AnimalController(_1718_etu32294_DB_SmartContext ctx, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            this.ctx = ctx;
        }

        //GET :api/ApplicationUser
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Animal> GetAnimal()
        {
            return ctx.Animal.ToList();
        }

        //Get: api/ApplicationUser/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimal([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var animal = await ctx.Animal.SingleOrDefaultAsync(m => m.Id == id);

            if (animal == null) return NotFound();
            return Ok(animal);
        }

        //PUT: api/ApplicationUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal([FromRoute]int id, [FromBody] Animal animal)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != animal.Id) return BadRequest();

            ctx.Entry(animal).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        //POST: api/ApplicationUser
        [HttpPost]
        public async Task<IActionResult> PostAnimal([FromBody]Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await GetCurrentUserAsync();
            animal.IdUser = user.Id;
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            ctx.Animal.Add(animal);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnimalExists(animal.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        //DELETE: api/ApplicationUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            var animal = await ctx.Animal.SingleOrDefaultAsync(m => m.Id == id);

            if (animal == null) return NotFound();

            ctx.Animal.Remove(animal);
            await ctx.SaveChangesAsync();
            return Ok(animal);
        }

        private bool AnimalExists(int id)
        {
            return ctx.Animal.Any(e => e.Id == id);
        }
    }
}
