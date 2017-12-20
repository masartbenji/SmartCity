using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Species
    {
        public Species()
        {
            Breed = new HashSet<Breed>();
        }
        
        public string Name { get; set; }

        public ICollection<Breed> Breed { get; set; }
    }
}
