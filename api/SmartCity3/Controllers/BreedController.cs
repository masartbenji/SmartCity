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
    [Route("api/Breed")]
    public class BreedController : BaseController
    {
        private readonly _1718_etu32294_DB_SmartContext ctx;

        public BreedController(UserManager<ApplicationUser> userManager, _1718_etu32294_DB_SmartContext ctx)
            :base(userManager)
        {
            this.ctx = ctx;
        }

        //GET :api/Breed
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Breed> GetBreed()
        {
            return ctx.Breed;
        }

        //Get: api/Breed/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBreed([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var breed = await ctx.Breed.SingleOrDefaultAsync(m => m.id == id);

            if (breed == null) return NotFound();
            return Ok(breed);
        }
        [HttpGet("Android/{id}")]
        [AllowAnonymous]
        public BreedAndroid GetBreedWithId([FromRoute] int id)
        {
            Breed breed = ctx.Breed.Single(m => m.id == id);
            return new BreedAndroid()
            {
                Id = breed.id,
                IdSpecies = breed.IdSpecies,
                Name = breed.Name
            };
        }

        //PUT: api/Breed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreed([FromRoute]string id, [FromBody] Breed breed)
        {
            ApplicationUser user = await GetCurrentUserAsync();
             
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await userManager.IsInRoleAsync(user, "Admin")) return Unauthorized();

            if (id != breed.Name) return BadRequest();

            ctx.Entry(breed).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreedExists(id))
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

        //POST: api/Species
        [HttpPost]
        public async Task<IActionResult> PostBreed([FromBody] Breed breed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user = await GetCurrentUserAsync();
            if (await userManager.IsInRoleAsync(user, "Admin")) return Unauthorized();

            ctx.Breed.Add(breed);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BreedExists(breed.Name))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetSpecies", new { id = breed.Name }, breed);
        }

        //DELETE: api/Breed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreed([FromRoute]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            if (await userManager.IsInRoleAsync(user, "Admin")) return Unauthorized();

            var breed = await ctx.Species.SingleOrDefaultAsync(m => m.Name == id);

            if (breed == null) return NotFound();

            ctx.Species.Remove(breed);
            await ctx.SaveChangesAsync();
            return Ok(breed);
        }
        private bool BreedExists(string id)
        {
            return ctx.Breed.Any(e => e.Name == id);
        }

    }

    public class BreedAndroid
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String IdSpecies { get; set; }
    }
}

