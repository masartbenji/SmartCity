using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class Role
    {
            public Role()
            {
                Personne = new HashSet<Personne>();
            }

            public string Name { get; set; }

            public ICollection<Personne> Personne { get; set; }
        }
}
