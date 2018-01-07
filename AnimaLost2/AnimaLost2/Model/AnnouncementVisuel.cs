﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Model
{
    public class AnnouncementVisuel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string AnimalName { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
    }
}
