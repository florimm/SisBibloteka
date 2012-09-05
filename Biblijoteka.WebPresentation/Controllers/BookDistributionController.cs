using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.WebPresentation.HtmlHelpers;
using Biblioteka.WebPresentation.ViewModels;
using Telerik.Web.Mvc;

namespace Biblioteka.WebPresentation.Controllers
{
    public class DistributionController : Controller
    {
        private readonly IBookRepository bookRepository;

        private readonly ICurrent current;
        private readonly IBookCopysRepository repo;
        private readonly ILibraryRepository libraryRepository;

        public DistributionController(
            IBookRepository _bookRepository,
            IBookCopysRepository _repo,
            ILibraryRepository _libraryRepository,
            ICurrent _current)
        {
            bookRepository = _bookRepository;
            repo = _repo;
            libraryRepository = _libraryRepository;
            current = _current;
        }

        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public JsonResult ManagedBooks()
        {
            var ls = repo.GetAll(c => c.Library.ID == current.CurrentUser.LibraryID);
            return Json(new GridModel<BookCopy>
                {
                    Data = ls,
                    Total = ls.Count()
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Libraries = libraryRepository.GetAll().ToDropDown(c => c.ID.ToString(), c => c.Name);
            ViewBag.Books = bookRepository.GetAll().ToDropDown(c => c.ID.ToString(), c => c.Title);
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }


        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            return View();
        }
    }
}