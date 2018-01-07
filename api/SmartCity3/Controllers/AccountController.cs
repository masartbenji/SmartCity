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
            roleResult = await _userManager.AddToRoleAsync(newUser, dto.RoleName);
            // TODO: retourner un Created à la place du Ok;
            return (result.Succeeded ) ? Ok() : (IActionResult)BadRequest();
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

            var uti = await _context.User.SingleOrDefaultAsync(m => m.UserName == username);
            if (uti == null)
            {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(uti);
            await _userManager.RemoveFromRolesAsync(uti,role);
            foreach(Animal animal in uti.Animal)
            {
                _context.Animal.Remove(animal);
            }
            _context.User.Remove(uti);
            await _context.SaveChangesAsync();

            return Ok(uti);
        }
        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody]NewUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uti = await _context.User.SingleOrDefaultAsync(m => m.UserName == user.UserName);
            if (uti == null) return NotFound();
            _context.Entry(user).State = EntityState.Modified;
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
        [HttpGet("{userName}")]
        [Route("Role")]
        public async Task<IActionResult> getRoleAsync([FromRoute]string userName)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.UserName == userName);
            if(user != null)
            {
                return Ok(_userManager.GetRolesAsync(user));
            }
            return BadRequest();
        }
        [HttpGet("Announcement/{userName}")]
        public async Task<IActionResult> GetAnnouncementForUser(string userName)
        {
            ApplicationUser user = await _context.User.SingleOrDefaultAsync(m => m.UserName == userName);
            List<Animal> animals = new List<Animal>();
            List<AnnouncementVisu> announcements = new List<AnnouncementVisu>();
            if(user != null)
            {
                animals = _context.Animal.Where(a => a.IdUser == user.Id).ToList<Animal>();
                foreach(Animal a in animals)
                {
                    Breed breedAnim = _context.Breed.SingleOrDefault( m => m.Name == a.IdBreed);
                    
                    foreach(Announcement announc in _context.Announcement.Where( annou => annou.IdAnimal == a.Id))
                    {
                        Status statusAnn = _context.Status.SingleOrDefault(m => m.Id == announc.IdStatus);
                        announcements.Add(new AnnouncementVisu()
                        {
                            idAnnoun = announc.Id,
                            DateAnnoun = announc.Date,
                            NameAnimal = a.Name,
                            Breed = breedAnim.Name,
                            Species = breedAnim.IdSpecies,
                            Description = announc.Description,
                            Status = statusAnn.State
                        });
                    }
                }
            }
            return Ok(announcements);
        }
    }
    class AnnouncementVisu
    {
        public int idAnnoun { get; set; }
        public DateTime DateAnnoun{get;set;}
        public string NameAnimal { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}

