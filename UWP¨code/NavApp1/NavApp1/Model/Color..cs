using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class Color
    {
        public Color()
        {
            Animal = new HashSet<Animal>();
        }

        public string Name { get; set; }

        public ICollection<Animal> Animal { get; set; }

    }
}
