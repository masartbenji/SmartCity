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
        [HttpGet("Name/{nameUser}")]
        public async Task<ApplicationUserAndroid> getUserWithHisName(String nameUser)
        {
            ApplicationUser user = await _context.User.FirstAsync(u => u.UserName == nameUser);
            return new ApplicationUserAndroid()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserDTO dto)
        {
            var newUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.Phone.ToString()
            };
            bool RoleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            Console.WriteLine(dto.RoleName);
            IdentityResult roleResult;
            if (!RoleExists)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole(dto.RoleName));
            }
            IdentityResult result = await _userManager.CreateAsync(newUser, dto.Password);
            if (result.Succeeded)
            {
                ApplicationUser current = await _userManager.FindByNameAsync(dto.UserName);
                result = await _userManager.AddToRoleAsync(current, dto.RoleName);
            }
            return (result.Succeeded ) ? Ok() : (IActionResult)Unauthorized();
        }
        [HttpPost("Admin")]
        public IActionResult Admin([FromBody] NewUserDTO dto)
        {
            if (!IsInRole("Admin"))
            {
                return Unauthorized();
            }
            else
            {
                return Ok();
            }
        }
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser uti = await _context.User.SingleOrDefaultAsync(m => m.UserName == username);
            if (uti == null)
            {
                return NotFound();
            }
            var role =  await _userManager.GetRolesAsync(uti);
            var animalList = _context.Animal.Where(ani => ani.IdUser == uti.Id).ToList();
            foreach(Animal animal in animalList)
            {
                List<Announcement> announcements = await _context.Announcement.Where(anounc => anounc.IdAnimal == animal.Id).ToListAsync();
                foreach (Announcement announc in announcements)
                {
                    _context.Announcement.Remove(announc);
                    _context.SaveChanges();
                }
                _context.Animal.Remove(animal);
                _context.SaveChanges();
            }
            await _userManager.RemoveFromRolesAsync(uti, role);
            _context.User.Remove(uti);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [AllowAnonymous]
        [HttpPut("{userName}")]
        public async Task<IActionResult> PutUser([FromRoute] string userName,[FromBody]NewUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uti = await _context.User.SingleOrDefaultAsync(m => m.UserName == user.UserName);
            if (uti == null) return NotFound();
            uti.UserName = user.UserName;
            uti.Email = user.Email;
            uti.PhoneNumber = user.Phone.ToString();
            await _userManager.RemovePasswordAsync(uti);
            await _userManager.AddPasswordAsync(uti, user.Password);
            _context.Entry(uti).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }
            return NoContent();
        }
        [HttpGet]
        [Route("Admin")]
        public async Task<IActionResult> GetUserRolesAsync()
        {
            IList<String> roles = await GetUserRoles();
            if (roles.Contains("Admin"))
            {
                return Ok();
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("Role/{userName}")]
        public async Task<IActionResult> GetRoleAsync([FromRoute]string userName)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.UserName == userName);
            if(user != null)
            {
                return Ok(_userManager.GetRolesAsync(user));
            }
            return BadRequest();
        }
        //pas important
        [AllowAnonymous]
        [HttpGet("Announcement/{userName}")]
        public async Task<IEnumerable<AnnouncementVisu>> GetAnnouncementForUser(string userName)
        {
            List<AnnouncementVisu> announcements = new List<AnnouncementVisu>();
            ApplicationUser user = await _context.User.SingleOrDefaultAsync(m => m.UserName == userName);
            var animalList = _context.Animal.Where(a => a.IdUser == user.Id).ToList();
            foreach(Animal animal in animalList)
            {
                var announcementList = _context.Announcement.Where(announ => announ.IdAnimal == animal.Id).ToList();
                foreach(Announcement announc in announcementList)
                {
                    AnnouncementVisu announcementVisu = new AnnouncementVisu()
                    {
                        idAnnoun = announc.Id,
                        DateAnnoun = announc.Date,
                        NameAnimal = animal.Name,
                        Breed = _context.Breed.Where(e => e.id == animal.IdBreed).Select(e => e.Name).First(),
                        Species = _context.Breed.Where(e => e.id == animal.IdBreed).Select(e => e.IdSpecies).First(),
                        Description = announc.Description,
                        Status = _context.Statut.Where(e => e.Id == announc.IdStatut).Select(e => e.State).First()
                };
                    
                    announcements.Add(announcementVisu);
                }
            }
            return announcements;
        }
    }
    public class AnnouncementVisu
    {
        public int idAnnoun { get; set; }
        public DateTime DateAnnoun{get;set;}
        public string NameAnimal { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class ApplicationUserAndroid
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}

