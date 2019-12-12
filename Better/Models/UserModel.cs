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
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}