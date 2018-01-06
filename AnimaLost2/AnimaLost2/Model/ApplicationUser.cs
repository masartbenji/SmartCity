using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Model
{
    public class ApplicationUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string RoleName { get; set; }

        public static ApplicationUser Deserialize(string json)
        {
            var split = json.Split(',','"','{','}');

            return new ApplicationUser()
            {
                UserName = split[12],
                Email = split[22],
                Phone = Convert.ToInt32(split[50])
            };
        }   
    }
}
