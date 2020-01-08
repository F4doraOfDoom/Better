using Better.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Better.Controllers;
using Better.Models.Constants;

namespace Better.Controllers
{
    public class UserController : Controller
    {
        private CoolDB _context = new CoolDB();

        public ActionResult ShowUsers(string userName)
        {
            User user = _context.Users.SingleOrDefault(u => u.Username == userName);
            var posts = _context.Posts.ToList().FindAll(p => p.UserId == user.Id);

            user.Posts = posts;

            return View(user);
        }

        public ActionResult Login(LoginAttempt login)
        {
            if (Request.HttpMethod == "POST")
            {
                User dbUser = _context.Users.SingleOrDefault(m => m.Username == login.User.Username);

                if (dbUser != null && dbUser.Password == login.User.Password)
                {
                    SessionData.Put(Models.Constants.Session.CurrentUser, dbUser);
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

        public ActionResult AddPost(Post post)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);
            User creator = _context.Users.Single(u => u.Username == creatorInSession.Username);


            post.UserId = creator.Id;
            post.CreationDate = DateTime.Now;

            _context.Posts.Add(post);

            TryUpdateModel(creator);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Edit(User user)
        {
            var curUser = SessionData.Get<User>(Models.Constants.Session.CurrentUser);
            if (curUser == null)
            {
                return HttpNotFound();
            }
            
            if (Request.HttpMethod == "POST")
            {
                var userInDb = _context.Users.Single(m => m.Id == curUser.Id);

                Save(user, false);
                SessionData.Put(Models.Constants.Session.CurrentUser, user);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(curUser);
            }
        }

        private void Save(User user, bool isNew)
        {
            if (isNew)
            {
                _context.Users.Add(user);
            }
            else
            {
                var userInDb = _context.Users.Single(m => m.Id == user.Id);

                //userInDb.Id = user.Id;
                userInDb.Username = user.Username;
                userInDb.Password = user.Password;
                userInDb.Email = user.Email;
            }

            _context.SaveChanges();
        }

        public ActionResult UserForm(User newUser)
        {
            if (Request.HttpMethod == "POST")
            {
                if (_context.Users.Count(m => m.Username == newUser.Username) == 0)
                {
                    Save(newUser, true);
                    SessionData.Put(Models.Constants.Session.CurrentUser, newUser);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Signoff()
        {
            SessionData.Put(Models.Constants.Session.CurrentUser, null);

            return RedirectToAction("Index", "Home");
        }
    }
}