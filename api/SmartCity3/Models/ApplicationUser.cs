using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity3
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser():base()
        {
            Animal = new HashSet<Animal>();
        }
        public ICollection<Animal> Animal { get; set; }
    }
}
