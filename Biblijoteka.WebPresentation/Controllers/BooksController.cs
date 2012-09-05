using System;
using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.WebPresentation.Models;
using Biblioteka.WebPresentation.HtmlHelpers;
using Telerik.Web.Mvc;

namespace Biblioteka.WebPresentation.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly Func<IAuthorRepository> authorRepository;

        public BooksController(
            IBookRepository _bookRepository,
            Func<IAuthorRepository> _authorRepository)
        {
            bookRepository = _bookRepository;
            authorRepository = _authorRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public JsonResult Books(string page, string size)
        {
            var books = bookRepository.GetAll();
            int cnt = books.Count();
            return Json(new GridModel<Book>
            {
                Data = books,
                Total = cnt
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var book = bookRepository.GetByID(id);
            return View(book);
        }

        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var b = new Book();
            UpdateLists(b);
            return View(b);
        }

        private void UpdateLists(Book _book)
        {
            ViewBag.Authors = authorRepository().GetAll().ToDropDown(c => c.ID.ToString(), c => c.FullName, c => _book.AuthorID == c.ID);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection collection)
        {
            var b = new Book();
            if (TryUpdateModel(b))
            {
                bookRepository.Insert(b);
                if (bookRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", bookRepository.Errors);
            }
            return RedirectToAction("Create", b);
        }

        [ImportModelStateFromTempData]
        public ActionResult Edit(int id)
        {
            var book = bookRepository.GetByID(id);
            UpdateLists(book);
            return View(book);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var book = bookRepository.GetByID(id);
            if (TryUpdateModel(book))
            {
                bookRepository.Update(book);
                if (bookRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", bookRepository.Errors);
            }
            return RedirectToAction("Edit", book);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var book = bookRepository.GetByID(id);
            bookRepository.Delete(book);
            if (bookRepository.Commit())
            {
                return Json(new { Resule = "Ok", Message = "Book deleted" });
            }
            return Json(new { Resule = "Error", Message = "Book cant be deleted" });
        }
 
        
    }
}
