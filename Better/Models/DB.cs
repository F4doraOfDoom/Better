using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Better.Models;

namespace Better.Models
{
    public class CoolDB : DbContext
    {
        public CoolDB()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}