﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Model
{
    public class Status
    {
        public Status()
        {
            Announcement = new HashSet<Announcement>();
        }

        public int Id { get; set; }
        public string State { get; set; }

        public ICollection<Announcement> Announcement { get; set; }
    }
}
