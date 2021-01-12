using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourLibrary.Models;

namespace YourLibrary.Controllers
{
    public class LibrariesController : Controller
    {
        private YourLibraryDBEntities db = new YourLibraryDBEntities();

        // GET: Libraries
        public ActionResult Index(string currentFilter, string searchString)
        {
            var user = Session["mydata"] as User;
            var library = db.Users.SelectMany(i => i.Books).Include(b => b.Category1);

            if (searchString != null) { }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                library = library.Where(p => p.Title.Contains(searchString) || p.Author.Contains(searchString));
            }

            return View(library.ToList());
        }

        public ActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            string path = Path.Combine(@"C:\Users\aktas\source\repos\YourLibrary\YourLibrary\uploads\", book.Title + ".pdf");
            return File(path, "application/pdf");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            User user = Session["mydata"] as User;
            Book book = db.Books.Find(id) ;
            var library = db.Users.Find(user.Id);
            library.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        }
    }
