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
        private YourLibraryDBEntities db = new YourLibraryDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            var users = db.Users;
            return View(users.ToList());
        }

        //GET Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //POST Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel user)
        {
            string message = "";
            using (db)
            {
                var v = db.Users.Where(a => a.Email == user.Email).FirstOrDefault(); //select * from User where Email = user.Email
                
                if (v != null)
                {
                    if (v.Password == user.Password)
                    {
                        FormsAuthentication.SetAuthCookie(v.Email, false);
                        var data = v;
                        Session["mydata"] = data;
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
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        //POST Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
                    dc.Users.Add(user);
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
                var v = dc.Users.Where(a => a.Email == email).FirstOrDefault();  //select * from User where Email = email
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

        //void RememberMe(LoginModel user)
        //{
        //    #region Remember Me section
        //    int timeout = user.RememberMe ? 525600 : 20; //If RememberMe == true then timeout = 525600 else timeout = 20
        //    var ticket = new FormsAuthenticationTicket(user.Email, user.RememberMe, timeout);
        //    string encrypted = FormsAuthentication.Encrypt(ticket);
        //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
        //    cookie.Expires = DateTime.Now.AddMinutes(timeout);
        //    cookie.HttpOnly = true;
        //    Response.Cookies.Add(cookie);
        //    #endregion
        //}
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            _ = db.Users.Remove(user);
            _ = db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

    }
}
