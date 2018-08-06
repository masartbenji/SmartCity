using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Statut
    {
        public Statut()
        {
            Announcement = new HashSet<Announcement>();
        }

        public int Id { get; set; }
        public string State { get; set; }

        public ICollection<Announcement> Announcement { get; set; }
    }
}
