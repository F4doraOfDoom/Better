using Better.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Better.Controllers
{
    public class UserController : Controller
    {
        public static List<User> Users { get; set; } = new List<User>();

        public ActionResult Login()
        {
            
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public static void AddNewUser(User user)
        {
            Users.Add(user);
        }
    }
}