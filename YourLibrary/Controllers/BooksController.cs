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
    [Authorize(Roles = "U")]
    public class BooksController : Controller
    {
        private YourLibraryDBEntities db = new YourLibraryDBEntities();

        // GET: Books
        public ViewResult Index(string currentFilter, string searchString)
        {
            var books = db.Books.Include(b => b.Category1);

            if (searchString != null) { }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;            

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(p => p.Title.Contains(searchString) || p.Author.Contains(searchString));
            }
           
            return View(books.ToList());
        }

        // GET: Books/Details/5
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

        public ActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            string path = Path.Combine(@"C:\Users\aktas\source\repos\YourLibrary\YourLibrary\uploads\", book.Title + ".pdf");
            Add(id);
            return File(path, "application/pdf");
            
        }

        public void Add(int id)
        {
            var context = new YourLibraryDBEntities();
            User user = Session["mydata"] as User;
            var book = (from b in context.Books
                        select b).FirstOrDefault(i => i.Id == id);
            var library = (from u in context.Users
                           select u).FirstOrDefault(i => i.Id == user.Id);
            if (library.Books.Where(i => i.Id == id).Count() != 0)
            {                
            }
            else
            {
                library.Books.Add(book);
                context.SaveChanges();
            }
        }

        // POST: Libraries/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            User user = Session["mydata"] as User;
            Book book = db.Books.Find(id);
            var library = db.Users.Find(user.Id);
            library.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index", "Libraries");
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
