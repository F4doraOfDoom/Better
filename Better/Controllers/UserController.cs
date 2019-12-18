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

        public ActionResult Login(LoginAttempt login)
        {
            if (Request.HttpMethod == "POST")
            {
                User dbUser = _context.Users.SingleOrDefault(m => m.Username == login.User.Username);

                if (dbUser.Password == login.User.Password)
                {
                    SessionData.Put("CurUser", dbUser);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //SessionData.Put("LoginAttempt", new LoginAttempt { Success = false });
                    return View(new LoginAttempt { Success = false });
                }
            }

            return View();
        }

        public ActionResult Signup(User newUser)
        {
            if (Request.HttpMethod == "POST")
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
                SessionData.Put("CurUser", newUser);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Signoff()
        {
            SessionData.Put("CurUser", null);

            return RedirectToAction("Index", "Home");
        }
    }
}