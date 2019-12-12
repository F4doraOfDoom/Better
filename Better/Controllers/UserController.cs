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

        public ActionResult ShowUsers(string username)
        {
            return View();
        }

    }
}