using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;

namespace Biblioteka.WebPresentation.Areas.Data.Controllers
{
    public class BorrowerAPIController : Controller
    {
        private readonly IBorrowerRepository borrowerRepository;

        public BorrowerAPIController(IBorrowerRepository _borrowerRepository)
        {
            borrowerRepository = _borrowerRepository;
        }

        public JsonResult Borrowers(int page, int size)
        {
            var b = borrowerRepository.GetAll().Skip(page * size).Take(size);
            return Json(b.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Details(int id)
        {
            var b = borrowerRepository.GetByID(id);
            return Json(b);
        }

        [HttpPut]
        public JsonResult Create(FormCollection collection)
        {
            var b = new Borrower();
            if (TryUpdateModel(b))
            {
                borrowerRepository.Insert(b);
                if (borrowerRepository.Commit())
                {
                    return Json(b);
                }
            }
            return Json(b);
        }

        [HttpPost]
        public JsonResult Edit(int id, FormCollection collection)
        {
            var b = borrowerRepository.GetByID(id);
            if (TryUpdateModel(b))
            {
                borrowerRepository.Update(b);
                if (borrowerRepository.Commit())
                {
                    return Json(b);
                }
            }
            return Json(b);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var b = borrowerRepository.GetByID(id);
            borrowerRepository.Delete(b);
            if (borrowerRepository.Commit())
            {
                return Json(new { Resule = "Ok", Message = "Book deleted" });
            }
            return Json(new { Resule = "Error", Message = "Book cant be deleted" });
        }
    }
}