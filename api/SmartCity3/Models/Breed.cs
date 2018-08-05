using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Breed
    {
        public Breed()
        {
            Animal = new HashSet<Animal>();
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string IdSpecies { get; set; }

        public Species IdSpeciesNavigation { get; set; }
        public ICollection<Animal> Animal { get; set; }
    }
}
