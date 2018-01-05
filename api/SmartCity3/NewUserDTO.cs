using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartCity3.DTO
{
    public class NewUserDTO
    {
        private string email;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                Regex r = new Regex(@"[\w1-9]{1,}@(\w){1,}.(\w){2,3}", RegexOptions.IgnoreCase);
                if (r.IsMatch(value))
                {
                    email = value;
                }
            }
        }
        public int Phone { get; set; }
        public string RoleName { get; set; }
    }
}
