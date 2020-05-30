using Better.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Better.Controllers;
using Better.Models.Constants;
using System.IO;

namespace Better.Controllers
{
    public class UserController : Controller
    {
        public static CoolDB dbContext { get; } = new CoolDB();
        private static UserController _call_constructor = new UserController(); 

        public UserController()
        {
            foreach (var user in dbContext.Users)
            {
                var posts = dbContext.Posts.ToList().FindAll(p => p.UserId == user.Id);

                user.Posts = posts;
            }
        }

        public ActionResult ShowUsers(string userName)
        {
            User user = dbContext.Users.SingleOrDefault(u => u.Username == userName);
            var posts = dbContext.Posts.ToList().FindAll(p => p.UserId == user.Id);

            user.Posts = posts;

            return View(user);
        }

        public ActionResult Login(LoginAttempt login)
        {
            if (Request.HttpMethod == "POST")
            {
                User dbUser = dbContext.Users.SingleOrDefault(m => m.Username == login.User.Username);

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

        public ActionResult DeletePost(int postId)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);

            //User creator = _context.Users.Single(u => u.Username == creatorInSession.Username);
            foreach (var post in dbContext.Posts)
            {
                if (post.PostId == postId)
                {
                    dbContext.Posts.Remove(post);
                    break;
                }
            }

            //TryUpdateModel(creator);
            _SaveModel();

            return RedirectToAction("ShowUsers", "User", new { userName = creatorInSession.Username });
        }

        public ActionResult DeleteFriend(int userId)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);

            User creator = dbContext.Users.Single(u => u.Username == creatorInSession.Username);
            if (creator.Friends != null)
            {
                foreach (var friend in creator.Friends)
                {
                    if (friend.Id == userId)
                    {
                        creator.Friends.Remove(friend);
                        break;
                    }
                }
            }

            TryUpdateModel(creator);
            _SaveModel();

            return RedirectToAction("ShowUsers", "User", new { userName = creatorInSession.Username });
        }
        public ActionResult DeleteUser(int userId)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);

            //User creator = _context.Users.Single(u => u.Username == creatorInSession.Username);
            foreach (var user in dbContext.Users)
            {
                if (user.Id == userId)
                {
                    dbContext.Users.Remove(user);
                    break;
                }
            }

            //TryUpdateModel(creator);
            _SaveModel();

            return Redirect(Url.Content("~/"));
        }

        public ActionResult AddFriend(int userId)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);

            User creator = dbContext.Users.Single(u => u.Username == creatorInSession.Username);
            User friend = dbContext.Users.Single(u => u.Id == userId);

            if (creator.Friends == null)
            {
                creator.Friends = new List<User>();
            }

            creator.Friends.Add(friend);

            //SessionData.Put(Models.Constants.Session.CurrentUser, creator);

            TryUpdateModel(creator);
            _SaveModel();

            return RedirectToAction("ShowUsers", "User", new { userName = creatorInSession.Username });

        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);
                string images_root = System.IO.Path.Combine(Server.MapPath("~/Content"), creatorInSession.Id.ToString());
                if (!System.IO.File.Exists(images_root))
                {
                    System.IO.Directory.CreateDirectory(images_root);
                }

                string path = System.IO.Path.Combine(
                                       images_root, pic);
                
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                User creator = dbContext.Users.Single(u => u.Username == creatorInSession.Username);
                Post post = new Post();


                post.UserId = creator.Id;
                post.Content = path;
                post.CreationDate = DateTime.Now;
                post.Type = System.IO.Path.GetExtension(file.FileName) == ".mp4" ? 2 :
                    (System.IO.Path.GetExtension(file.FileName) == ".mp3" ? 3 : 1);

                dbContext.Posts.Add(post);
                TryUpdateModel(creator);

                _SaveModel();
            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "Home");
        }



        private void _SaveModel()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception exception = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        //create a new exception inserting the current one
                        //as the InnerException
                        exception = new InvalidOperationException(message, exception);
                    }
                }
                throw exception;
            }
        }

        public ActionResult AddPost(Post post)
        {
            User creatorInSession = SessionData.Get<User>(Models.Constants.Session.CurrentUser);
            User creator = dbContext.Users.Single(u => u.Username == creatorInSession.Username);

            post.UserId = creator.Id;
            post.CreationDate = DateTime.Now;
            post.Type = 0;

            dbContext.Posts.Add(post);
            TryUpdateModel(creator);

            _SaveModel();

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
                var userInDb = dbContext.Users.Single(m => m.Id == curUser.Id);

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
                dbContext.Users.Add(user);
            }
            else
            {
                var userInDb = dbContext.Users.Single(m => m.Id == user.Id);

                //userInDb.Id = user.Id;
                userInDb.Username = user.Username;
                userInDb.Password = user.Password;
                userInDb.Email = user.Email;
            }

            dbContext.SaveChanges();
        }

        public ActionResult UserForm(User newUser)
        {
            if (Request.HttpMethod == "POST")
            {
                if (dbContext.Users.Count(m => m.Username == newUser.Username) == 0)
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