﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Model
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
