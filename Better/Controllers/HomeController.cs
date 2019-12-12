using Better.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Better.Controllers
{
    public class HomeController : Controller
    {
        private CoolDB _context;

        public HomeController()
        {
            _context = new CoolDB();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

    }
}