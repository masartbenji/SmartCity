using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SmartCity3.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Announcement")]
    public class AnnouncementController : BaseController
    {
        _1718_etu32294_DB_SmartContext ctx;
        public AnnouncementController(UserManager<ApplicationUser> userManager,_1718_etu32294_DB_SmartContext ctx) : base(userManager)
        {
            this.ctx = ctx;
        }

        //GET :api/Announcement
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Announcement> GetAnnouncement()
        {
            return ctx.Announcement;
        }

        //Get: api/Announcement/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncement([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var announcement = await ctx.Announcement.SingleOrDefaultAsync(m => m.Id == id);

            if (announcement == null) return NotFound();
            return Ok(announcement);
        }

        
        [HttpGet("Person")]
        public async Task<IActionResult> GetAnnouncementOfPerson()
        {
            var animal = ctx.Animal;
            var anounc = ctx.Announcement;
            ApplicationUser user = await GetCurrentUserAsync();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var announcement = ctx.User.Join(animal,u=>u.Id,an=>an.IdUser,(u,an)=>new { idUser = u.Id, idAnimal = an.Id })
                .Join(anounc,an => an.idAnimal, announc =>announc.Id, (an,a) => new { idUser = an.idUser, idAnnouncement = a.Id })
                .Where(a => a.idUser == user.Id);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }

        [AllowAnonymous]
        [HttpGet("Status")]
        public IActionResult GetAnnouncementOfStatus([FromRoute]string statusName)
        {
            var status = ctx.Statut;
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var announcement = await ctx.User.SingleOrDefaultAsync(u => u.UserName == pseudo);
            var announcement = ctx.Announcement.Join(status, a => a.IdStatut, s => s.Id, (a, s) => new { idAnnouncement = a.Id, nameStatus = s.State })
                .Where(a => a.nameStatus == statusName);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }
        //PUT: api/Announcement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute]int id, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != announcement.Id) return BadRequest();

            ctx.Entry(announcement).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
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

        //POST: api/Announcement
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody]Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            ctx.Announcement.Add(announcement);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnnouncementExists(announcement.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnnouncement", new { id = announcement.Id }, announcement);
        }

        //DELETE: api/Announcement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var roleSearch = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (roleSearch == null) return Unauthorized();

            var announcement = await ctx.Announcement.SingleOrDefaultAsync(m => m.Id == id);

            if (announcement == null) return NotFound();

            ctx.Announcement.Remove(announcement);
            await ctx.SaveChangesAsync();
            return Ok(announcement);
        }

        private bool AnnouncementExists(int id)
        {
            return ctx.Announcement.Any(e => e.Id == id);
        }
    }
}
