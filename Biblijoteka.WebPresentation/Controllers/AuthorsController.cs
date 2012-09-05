using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.WebPresentation.Models;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;

namespace Biblioteka.WebPresentation.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorsController(IAuthorRepository _authorRepository)
        {
            authorRepository = _authorRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public JsonResult Authors()
        {
            var authors = authorRepository.GetAll();
            return Json(new GridModel<Author>
            {
                Data = authors,
                Total = authors.Count()
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Details(int id)
        {
            var author = authorRepository.GetByID(id);
            return View(author);
        }

        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var a = new Author();
            return View(a);
        } 

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection collection)
        {
            var a = new Author();
            if(TryUpdateModel(a))
            {
                authorRepository.Insert(a);
                if(authorRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db",authorRepository.Errors);
            }
            return RedirectToAction("Create", a);

        }

        [ImportModelStateFromTempData]
        public ActionResult Edit(int id)
        {
            var author = authorRepository.GetByID(id);
            return View(author);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var author = authorRepository.GetByID(id);
            if(TryUpdateModel(author))
            {
                authorRepository.Update(author);
                if (authorRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", authorRepository.Errors);
            }
            return RedirectToAction("Edit", author);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var author = authorRepository.GetByID(id);
            authorRepository.Delete(author);
            if(authorRepository.Commit())
            {
                return Json(new {Resule = "Ok", Message = "Author deleted"});
            }
            return Json(new {Resule = "Error", Message = "Author cant be deleted"});
        }
    }
}
