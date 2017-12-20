using System;
using System.Collections.Generic;

namespace SmartCity3
{
    public partial class Color
    {
        public Color()
        {
            Animal = new HashSet<Animal>();
        }

        public string Name { get; set; }

        public ICollection<Animal> Animal { get; set; }
    }
}
