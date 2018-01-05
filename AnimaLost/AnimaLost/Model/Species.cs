using System.Collections.Generic;

namespace AnimaLost.Model
{
    public class Species
    {
        public Species()
        {
            Breed = new HashSet<Breed>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Breed> Breed { get; set; }
    }
}