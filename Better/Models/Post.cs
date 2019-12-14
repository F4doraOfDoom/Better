using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Better.Models;

namespace Better.Models
{
    public class Post
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public string Content { get; set; }
    }
}