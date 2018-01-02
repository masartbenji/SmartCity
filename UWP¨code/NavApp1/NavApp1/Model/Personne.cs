using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class Personne
    {
        public Personne()
        {
            Animal = new HashSet<Animal>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string NameRole { get; set; }

        public Role NameRoleNavigation { get; set; }
        public ICollection<Animal> Animal { get; set; }

    }
}
