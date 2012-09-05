using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.WebPresentation.Models;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;

namespace Biblioteka.WebPresentation.Controllers
{
    public class LibrariesController : Controller
    {
        private readonly IRepository<Library> libraryRepository;

        public LibrariesController(IRepository<Library> _libraryRepository)
        {
            libraryRepository = _libraryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public JsonResult Libraries()
        {
            var libraries = libraryRepository.GetAll();
            return Json(new GridModel<Library>
            {
                Data = libraries,
                Total = libraries.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var library = libraryRepository.GetByID(id);
            return View(library);
        }

        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var l = new Library();
            return View(l);
        } 

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection collection)
        {
            var b = new Library();
            if (TryUpdateModel(b))
            {
                libraryRepository.Insert(b);
                if (libraryRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", libraryRepository.Errors);
            }
            return RedirectToAction("Create", b);
        }
        
        [ImportModelStateFromTempData]
        public ActionResult Edit(int id)
        {
            var library = libraryRepository.GetByID(id);
            return View(library);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var b = new Library();
            if (TryUpdateModel(b))
            {
                libraryRepository.Update(b);
                if (libraryRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", libraryRepository.Errors);
            }
            return RedirectToAction("Edit", b);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var author = libraryRepository.GetByID(id);
            libraryRepository.Delete(author);
            if (libraryRepository.Commit())
            {
                return Json(new { Resule = "Ok", Message = "Library deleted" });
            }
            return Json(new { Resule = "Error", Message = "Library cant be deleted" });
        }
    }
}
