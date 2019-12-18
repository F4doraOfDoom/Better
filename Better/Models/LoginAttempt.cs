using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Better.Models
{
    public class LoginAttempt
    {
        public bool? Success { get; set; }
        public User User { get; set; }
    }
}