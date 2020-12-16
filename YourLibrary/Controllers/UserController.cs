using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YourLibrary.Models;

namespace YourLibrary.Controllers
{
    public class UserController : Controller
    {
        //GET Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //POST Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            string message = "";
            using (YourLibraryDBEntities dc = new YourLibraryDBEntities())
            {
                var v = dc.User.Where(a => a.Email == user.Email).FirstOrDefault(); //select * from User where Email = user.Email

                if (v != null)
                {
                    if (v.Password == user.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        message = "Wrong password!";
                    }
                }
                else
                {
                    message = "Wrong email address!";
                }

            }

            ViewBag.Message = message;
            return View();
        }

        //GET Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //POST Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            string message = "";
            bool status = false;

            if (ModelState.IsValid)
            {
                bool isExist = EmailChecker(user.Email);
                if (isExist == true)
                {
                    ModelState.AddModelError("EmailExist", "This email has already used!");
                    return View(user);
                }

                using (YourLibraryDBEntities dc = new YourLibraryDBEntities())
                {
                    dc.User.Add(user);
                    dc.SaveChanges();
                    message = "Registration is succesfully done!";
                    status = true;
                }
            }
            else
            {
                message = "Invalid entries!";
            }

            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
        }

        [NonAction]
        public bool EmailChecker(string email)
        {
            using (YourLibraryDBEntities dc = new YourLibraryDBEntities())
            {
                var v = dc.User.Where(a => a.Email == email).FirstOrDefault();  //select * from User where Email = email
                if (v != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
