using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class Breed
    {
        public Breed()
        {
            Animal = new HashSet<Animal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdSpecies { get; set; }

        public Species IdSpeciesNavigation { get; set; }
        public ICollection<Animal> Animal { get; set; }
    }
}
