﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.Model
{
    public abstract class Token
    {
        public static string Id { get; set; }
        public static DateTime ExpirationTime { get; set; }
    }
}
