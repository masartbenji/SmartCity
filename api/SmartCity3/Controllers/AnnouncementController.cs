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
        [AllowAnonymous]
        [HttpGet("android/{nameStatus}")]
        public IEnumerable<AnnouncementAndroid> GetAnnouncementsAndroid(String nameStatus)
        {
            List<AnnouncementAndroid> announcements = new List<AnnouncementAndroid>();
            int id = ctx.Statut.Where(s => s.State == nameStatus).Select(s => s.Id).Single();

            foreach (Announcement announc in ctx.Announcement.Where(a => a.IdStatut == id))
            {
                Animal animal = ctx.Animal.Where(a => a.Id == announc.IdAnimal).First();
                Breed breed = ctx.Breed.Where(b => b.id == animal.IdBreed).First();
                var announcement = new AnnouncementAndroid()
                {
                    Id = announc.Id,
                    Name = animal.Name,
                    Species = breed.IdSpecies,
                    Breed = breed.Name,
                    Color = animal.IdColor,
                    Date = announc.Date,
                    Description = announc.Description,
                    IdStatut = announc.IdStatut,
                    IdAnimal = announc.IdAnimal
                };
                announcements.Add(announcement);
            }
            return announcements;
        } 
        [HttpGet("MaxId")]
        public async Task<AnnouncementAndroid> GetAnnouncementWithMaxId()
        {
            int maxId = 0;
            Announcement announcement = new Announcement();
            foreach (Announcement anounc in ctx.Announcement)
            {
                if (anounc.Id > maxId)
                {
                    maxId = anounc.Id;
                    announcement = anounc;
                }
            }
            Animal animal = await ctx.Animal.Where(a => a.Id == announcement.IdAnimal).FirstAsync();
            Breed breed = await ctx.Breed.Where(b => b.id == animal.IdBreed).FirstAsync();
            return new AnnouncementAndroid()
            {
                Id = announcement.Id,
                Name = animal.Name,
                Species = breed.IdSpecies,
                Breed = breed.Name,
                Color = animal.IdColor,
                Date = announcement.Date,
                Description = announcement.Description,
                IdStatut = announcement.IdStatut,
                IdAnimal = announcement.IdAnimal
            };
        }
    
        //Get: api/Announcement/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IEnumerable<Announcement>> GetAnnouncement([FromRoute]int id)
        {
            List<Announcement> announcement = new List<Announcement>();
            announcement.Add(await ctx.Announcement.SingleOrDefaultAsync(m => m.Id == id));
            return announcement;
        }

        [AllowAnonymous]
        [HttpGet("android/id/{id}")]
        public IEnumerable<AnnouncementAndroid> GetAnnouncementAndroid([FromRoute] int id)
        {
            List<AnnouncementAndroid> announcements = new List<AnnouncementAndroid>();
            var announcement = ctx.Announcement.SingleOrDefault(m => m.Id == id);
            if (announcement != null)
            {
                Animal animal = ctx.Animal.Where(a => a.Id == announcement.IdAnimal).First();
                Breed breed = ctx.Breed.Where(b => b.id == animal.IdBreed).First();
                var announcementAndroid = new AnnouncementAndroid()
                {
                    Id = announcement.Id,
                    Name = animal.Name,
                    Species = breed.IdSpecies,
                    Breed = breed.Name,
                    Color = animal.IdColor,
                    Date = announcement.Date,
                    Description = announcement.Description,
                    IdStatut = announcement.IdStatut,
                    IdAnimal = announcement.IdAnimal
                };
                announcements.Add(announcementAndroid);
            }
            return announcements;
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
        [HttpGet("Status/{statusName}")]
        public IActionResult GetAnnouncementOfStatus([FromRoute]string statusName)
        {
            var status = ctx.Statut;
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var announcement = ctx.Announcement.Join(status, a => a.IdStatut, s => s.Id, (a, s) => new { idAnnouncement = a.Id, nameStatus = s.State })
                .Where(a => a.nameStatus == statusName);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }
        [AllowAnonymous]
        [HttpGet("{nameUser}/{nameStatut}")]
        public async Task<IEnumerable<AnnouncementAndroid>> GetAnnouncementForAUserWithStatus([FromRoute]string nameUser,[FromRoute]string nameStatut)
        {
            ApplicationUser user = await ctx.User.FirstAsync(u => u.UserName == nameUser);
            List<AnnouncementAndroid> announcements = new List<AnnouncementAndroid>();
            Statut statut = await ctx.Statut.FirstAsync(s => s.State == nameStatut);
            var announcementsStatus = ctx.Announcement.Where(a => a.IdStatut == statut.Id);
            foreach(Announcement announcement in announcementsStatus)
            {
                Animal animal = await ctx.Animal.FirstAsync(a => a.Id == announcement.IdAnimal);
                if(animal.IdUser == user.Id)
                {
                    Breed breed = ctx.Breed.Where(b => b.id == animal.IdBreed).First();
                    announcements.Add(new AnnouncementAndroid()
                    {
                        Id = announcement.Id,
                        IdAnimal = announcement.IdAnimal,
                        Breed = breed.Name,
                        Species = breed.IdSpecies,
                        Name = animal.Name,
                        Color = animal.IdColor,
                        Date = announcement.Date,
                        Description = announcement.Description,
                        IdStatut = announcement.IdStatut
                    });
                }
            }
            return announcements;
        }
        //PUT: api/Announcement/5
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement([FromRoute]int id, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser user = await GetCurrentUserAsync();
            var searchRole = ctx.UserRoles.Where(e => e.UserId == user.Id && e.RoleId == "Admin");
            if (searchRole == null) return Unauthorized();

            if (id != announcement.Id) return BadRequest();
            using(var transaction = ctx.Database.BeginTransaction())
            {
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
            return Unauthorized();
        }

        //POST: api/Announcement
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement([FromBody]Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

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
        public class AnnouncementAndroid
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Species { get; set; }
            public String Breed { get; set; }
            public String Color { get; set; }
            public DateTime Date { get; set; }
            public String Description { get; set; }
            public int IdStatut { get; set; }
            public int IdAnimal { get; set; }
        }
    }
    
}
