using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;

namespace Biblioteka.WebPresentation.Areas.Data.Controllers
{
    public class BooksAPIController : Controller
    {
        private readonly IBookRepository bookRepository;

        public BooksAPIController(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }

        public JsonResult Books(int page, int size)
        {
            var books = bookRepository.GetAll().Skip(page * size).Take(size);
            return Json(books.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Details(int id)
        {
            var book = bookRepository.GetByID(id);
            return Json(book);
        }

        [HttpPut]
        public JsonResult Create(FormCollection collection)
        {
            var b = new Book();
            if (TryUpdateModel(b))
            {
                bookRepository.Insert(b);
                if (bookRepository.Commit())
                {
                    return Json(b);
                }
            }
            return Json(b);
        }

        [HttpPost]
        public JsonResult Edit(int id, FormCollection collection)
        {
            var book = bookRepository.GetByID(id);
            if (TryUpdateModel(book))
            {
                bookRepository.Update(book);
                if (bookRepository.Commit())
                {
                    return Json(book);
                }
            }
            return Json(book);
        }

        [HttpDelete]
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
