using Books.Business;
using Books.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Web.Controllers
{
    public class BookController : Controller
    {
        IBookBusiness bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        // GET: All Book
        public JsonResult GetAllBook()
        {
            var list = bookBusiness.GetAll().ToList();
            List<Book> listBook = new List<Book>();
            
            for(int i = 0; i < list.Count(); i++)
            {
                Book book = new Book();
                book.BookID = list.ElementAt(i).BookID;
                book.Title = list.ElementAt(i).Title;
                book.PublisherDate = list.ElementAt(i).PublisherDate;

                string dateTime = Convert.ToString(list.ElementAt(i).PublisherDate);
                book.dateTime = dateTime;
                book.Publisher = list.ElementAt(i).Publisher;
                book.PageNumber = list.ElementAt(i).PageNumber;

                listBook.Add(book);
            }

            return Json(listBook, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNotificationBooks()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null 
                ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponents NC = new NotificationComponents();
            var list = NC.GetBooks(notificationRegisterTime);
            Session["LastUpdated"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 

        }
        [HttpPost]
        public JsonResult GetNotificationBooksCount()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null
                ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponents NC = new NotificationComponents();
            var list = NC.GetBooks(notificationRegisterTime);
            return Json (new
            {
                count = list.Count(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet });

        }
        // GET: Book With ID
        public JsonResult GetBook(int id)
        {
            var book = bookBusiness.GetAll().ToList().Find(n => n.BookID == id);

            Book book1 = new Book();
            book1.BookID = book.BookID;
            book1.Title = book.Title;
            book1.PublisherDate = book.PublisherDate;
            string dateTime = Convert.ToString(book.PublisherDate);
            book1.dateTime = dateTime;
            book1.Publisher = book.Publisher;
            book1.PageNumber = book.PageNumber;

            return Json(book1, JsonRequestBehavior.AllowGet);
        }

        // CREATE Book
        [HttpPost]
        public JsonResult Create([Bind(Exclude = "BookID")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookBusiness.Create(book);
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        // UPDATE Book
        [HttpPost]
        public JsonResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                bookBusiness.Update(book);
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }

        // DELETE Book
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var book = bookBusiness.GetAll().ToList().Find(n => n.BookID == id);

            if(book != null)
            {
                bookBusiness.Delete(book);
            }

            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}