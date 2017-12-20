using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Animal
    {

        public Animal()
        {
            Announcement = new HashSet<Announcement>();
            Images = new HashSet<Images>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string IdBreed { get; set; }
        public string IdColor { get; set; }
        public string IdUser { get; set; }

        public Breed IdBreedNavigation { get; set; }
        public Color IdColorNavigation { get; set; }
        
        public ApplicationUser User { get; set; }
        public ICollection<Announcement> Announcement { get; set; }
        public ICollection<Images> Images { get; set; }
    }
}
