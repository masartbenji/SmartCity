using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    class Token
    {
        private string id;
        private DateTime expirationTime;

        public string Id { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
