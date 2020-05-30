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

        [Required(ErrorMessage = "Every user needs to have a username!")]
        //[StringLength(64)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[ValidatePasswordLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<User> Friends { get; set; }
    }
}