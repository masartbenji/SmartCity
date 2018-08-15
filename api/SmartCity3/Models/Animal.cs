using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Animal
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdBreed { get; set; }
        public string IdColor { get; set; }
        public string IdUser { get; set; }

        public Breed IdBreedNavigation { get; set; }
        public Color IdColorNavigation { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<Announcement> Announcement { get; internal set; }
        public IEnumerable<Images> Images { get; internal set; }
    }
}
