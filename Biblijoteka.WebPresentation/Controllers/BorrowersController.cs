using System;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.WebPresentation.HtmlHelpers;
using Biblioteka.WebPresentation.Models;

namespace Biblioteka.WebPresentation.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly Func<IAuthorRepository> authorRepository;
        private readonly Func<IBookCopysRepository> bookCopysRepository;
        private readonly Func<IBookRepository> bookRepository;
        private readonly IBorrowerRepository borrowerRepository;
        private readonly Func<IStudentRepository> studentRepository;

        public BorrowersController(
            Func<IBookRepository> _bookRepository,
            Func<IAuthorRepository> _authorRepository,
            Func<IBookCopysRepository> _bookCopysRepository,
            IBorrowerRepository _borrowerRepository,
            Func<IStudentRepository> _studentRepository)
        {
            bookRepository = _bookRepository;
            authorRepository = _authorRepository;
            bookCopysRepository = _bookCopysRepository;
            borrowerRepository = _borrowerRepository;
            studentRepository = _studentRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Borrower borrower = borrowerRepository.GetByID(id);
            return View(borrower);
        }

        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var b = new Borrower();
            UpdateLists(b);
            return View(b);
        }

        private void UpdateLists(Borrower _borrower)
        {
            ViewBag.Books = bookCopysRepository().GetAll().ToDropDown(c => c.ID.ToString(), c => c.Book.Title,
                                                                      c => _borrower.BookCopyID == c.ID);
            ViewBag.Students = studentRepository().GetAll().ToDropDown(c => c.ID.ToString(), c => c.FullName,
                                                                       c => _borrower.StudentID == c.ID);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection collection)
        {
            var b = new Borrower();
            if (TryUpdateModel(b))
            {
                borrowerRepository.Insert(b);
                if (borrowerRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", borrowerRepository.Errors);
            }
            return RedirectToAction("Create", b);
        }

        [ImportModelStateFromTempData]
        public ActionResult Edit(int id)
        {
            Borrower borrower = borrowerRepository.GetByID(id);
            UpdateLists(borrower);
            return View(borrower);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Borrower b = borrowerRepository.GetByID(id);
            if (TryUpdateModel(b))
            {
                borrowerRepository.Update(b);
                if (borrowerRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", borrowerRepository.Errors);
            }
            return RedirectToAction("Edit", b);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Borrower book = borrowerRepository.GetByID(id);
            borrowerRepository.Delete(book);
            if (borrowerRepository.Commit())
            {
                return Json(new {Resule = "Ok", Message = "Borrower deleted"});
            }
            return Json(new {Resule = "Error", Message = "Borrower cant be deleted"});
        }
    }
}