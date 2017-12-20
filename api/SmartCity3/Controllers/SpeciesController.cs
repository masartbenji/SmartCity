using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/Species")]
    public class SpeciesController : Controller
    {
        private readonly _1718_etu32294_DB_SmartContext ctx;

        public SpeciesController(_1718_etu32294_DB_SmartContext ctx)
        {
            this.ctx = ctx;
        }

        //GET :api/Species
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Species> GetSpecies()
        {
            return ctx.Species;
        }

        //Get: api/Species/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecies([FromRoute]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var species = await ctx.Species.SingleOrDefaultAsync(m => m.Name == id);

            if (species == null) return NotFound();
            return Ok(species);
        }

        //PUT: api/Species/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecies([FromRoute]string id, [FromBody] Species species)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != species.Name) return BadRequest();

            ctx.Entry(species).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
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
        public async Task<IActionResult> PostSpecies([FromBody] Species species)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ctx.Species.Add(species);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpeciesExists(species.Name))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetSpecies", new { id = species.Name }, species);
        }

        //DELETE: api/Species/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies([FromRoute]string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var species = await ctx.Species.SingleOrDefaultAsync(m => m.Name == id);

            if (species == null) return NotFound();

            ctx.Species.Remove(species);
            await ctx.SaveChangesAsync();
            return Ok(species);
        }

        private bool SpeciesExists(string id)
        {
            return ctx.Species.Any(e => e.Name == id);
        }
    }
}
