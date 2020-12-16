using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Index()
        {
            var user = Session["mydata"] as User;
            var library = db.Library.Where(l => l.UserId == user.Id);
            var books = library.Select(l => l.Book);
            return View(books.ToList());
        }

        public ActionResult Add(int id)
        {
            var context = new YourLibraryDBEntities();
            var user = Session["mydata"] as User;
            var book = (from b in context.Book
                        select b).FirstOrDefault(i => i.Id == id);
            var library = (from l in context.Library
                           select l).FirstOrDefault(i => i.UserId == user.Id);
            library.Book.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index", "Libraries");
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
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
