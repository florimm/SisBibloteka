using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.WebPresentation.Areas.Data.Controllers
{
    public class DistributionAPIController : Controller
    {
        private readonly IBookCopysRepository bookcopyRepository;
        private readonly ICurrent current;

        public DistributionAPIController(IBookCopysRepository _bookcopyRepository, ICurrent _current)
        {
            bookcopyRepository = _bookcopyRepository;
            current = _current;
        }

        public JsonResult Distributions(int libraryID, int page, int size)
        {
            if (libraryID == -1)
                libraryID = current.CurrentUser.LibraryID;
            var books = bookcopyRepository.GetAll(c=> c.LibraryID == libraryID).Skip(page * size).Take(size);
            return Json(books.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Details(int id)
        {
            var book = bookcopyRepository.GetByID(id);
            return Json(book);
        }

        [HttpPut]
        public JsonResult Create(FormCollection collection)
        {
            var b = new BookCopy();
            if (TryUpdateModel(b))
            {
                bookcopyRepository.Insert(b);
                if (bookcopyRepository.Commit())
                {
                    return Json(b);
                }
            }
            return Json(b);
        }

        [HttpPost]
        public JsonResult Edit(int id, FormCollection collection)
        {
            var book = bookcopyRepository.GetByID(id);
            if (TryUpdateModel(book))
            {
                bookcopyRepository.Update(book);
                if (bookcopyRepository.Commit())
                {
                    return Json(book);
                }
            }
            return Json(book);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var book = bookcopyRepository.GetByID(id);
            bookcopyRepository.Delete(book);
            if (bookcopyRepository.Commit())
            {
                return Json(new { Resule = "Ok", Message = "Book deleted" });
            }
            return Json(new { Resule = "Error", Message = "Book cant be deleted" });
        }
    }
}