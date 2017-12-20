using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Images
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdAnimal { get; set; }

        public Animal IdAnimalNavigation { get; set; }
    }
}
