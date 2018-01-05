using System.Collections.Generic;

namespace AnimaLost.Model
{
    public class Color
    {
        public Color()
        {
            Animal = new HashSet<Animal>();
        }

        public string Name { get; set; }

        public ICollection<Animal> Animal { get; set; }
    }
}