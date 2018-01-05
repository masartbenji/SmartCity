using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost.Model
{
    public class Animal
    {
        public Animal()
        {
            Announcement = new HashSet<Announcement>();
            Images = new HashSet<Images>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int IdBreed { get; set; }
        public string IdColor { get; set; }
        public string PwPersonne { get; set; }
        public string LoginPersonne { get; set; }

        public Breed IdBreedNavigation { get; set; }
        public Color IdColorNavigation { get; set; }
        public ApplicationUser Personne { get; set; }
        public ICollection<Announcement> Announcement { get; set; }
        public ICollection<Images> Images { get; set; }
    }
}
