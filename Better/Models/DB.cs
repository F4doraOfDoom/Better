using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Better.Models;

namespace Better.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {

        }

        public DbSet<User> Users { get; set; }
    }
}