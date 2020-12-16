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
    public class BooksController : Controller
    {
        private YourLibraryDBEntities db = new YourLibraryDBEntities();

        // GET: Books
        public ActionResult Index()
        {
            var book = db.Book.Include(b => b.Category1);
            return View(book.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult GetBook(int id)
        {
            Book book = db.Book.Find(id);
            string path = Path.Combine(@"C:\Users\aktas\source\repos\YourLibrary\YourLibrary\uploads\", book.Title + ".pdf");
            return File(path, "application/pdf");
            
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
