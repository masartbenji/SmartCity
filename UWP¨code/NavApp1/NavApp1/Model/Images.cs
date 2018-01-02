using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace NavApp1.Model
{
        class Images
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int IdAnimal { get; set; }

            public Animal IdAnimalNavigation { get; set; }
        }
}
    
