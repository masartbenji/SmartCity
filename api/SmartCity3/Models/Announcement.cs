﻿using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public int IdStatus { get; set; }
        public int IdAnimal { get; set; }

        public Animal IdAnimalNavigation { get; set; }
        public Status IdStatusNavigation { get; set; }
    }
}