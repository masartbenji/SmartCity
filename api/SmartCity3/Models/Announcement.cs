using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Decimal CoordX { get; set; }
        public Decimal CoordY { get; set; }
        public int IdStatut { get; set; }
        public int IdAnimal { get; set; }

        public Animal IdAnimalNavigation { get; set; }
        public Statut IdStatusNavigation { get; set; }
    }
}
