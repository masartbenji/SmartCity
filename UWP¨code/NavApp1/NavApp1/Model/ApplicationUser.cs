using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class ApplicationUser
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string Email {  get; set; }
        public int Phone {  get; set; }
        public string RoleName {  get; set; }
    }
}
