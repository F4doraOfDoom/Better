using Better.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Better.Controllers;

namespace Better.Controllers
{
    public class UserController : Controller
    {
        private CoolDB _context = new CoolDB();

        public ActionResult ShowUsers(string userName)
        {
            User user = _context.Users.SingleOrDefault(u => u.Username == userName);

            return View(user);
        }

        public ActionResult Signup(User newUser)
        {
            if (Request.HttpMethod == "POST")
            {

            }

            return View();
        }

    }
}