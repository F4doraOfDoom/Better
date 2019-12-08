using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Better.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Passowrd { get; set; }
        public string Email { get; set; }
    }
}