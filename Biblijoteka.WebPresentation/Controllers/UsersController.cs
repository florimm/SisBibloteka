using System;
using System.Linq;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.WebPresentation.HtmlHelpers;
using Biblioteka.WebPresentation.Models;
using Telerik.Web.Mvc;
using Utilities;

namespace Biblioteka.WebPresentation.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IRepository<User> userRepository;
        private readonly Func<IRepository<Library>> libraryRepositpry;

        public UsersController(IRepository<User> _userRepository, Func<IRepository<Library>> _libraryRepositpry )
        {
            userRepository = _userRepository;
            libraryRepositpry = _libraryRepositpry;
        }

        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public JsonResult Users()
        {
            var users = userRepository.GetAll();
            return Json(new GridModel<User>
            {
                Data = users,
                Total = users.Count()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var user = userRepository.GetByID(id);
            return View(user);
        }

        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var user = new User();
            FillList(user);
            return View(user);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection collection)
        {
            var b = new User();
            if (TryUpdateModel(b))
            {
                userRepository.Insert(b);
                if (userRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", userRepository.Errors);
            }
            return RedirectToAction("Create", b);
        }

        [ImportModelStateFromTempData]
        public ActionResult Edit(int id)
        {
            var user = userRepository.GetByID(id);
            FillList(user);
            return View(user);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var b = new User();
            if (TryUpdateModel(b))
            {
                userRepository.Update(b);
                if (userRepository.Commit())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("db", userRepository.Errors);
            }
            return RedirectToAction("Edit", b);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var user = userRepository.GetByID(id);
            userRepository.Delete(user);
            if (userRepository.Commit())
            {
                return Json(new { Resule = "Ok", Message = "User deleted" });
            }
            return Json(new { Resule = "Error", Message = "User cant be deleted" });
        }

        private void FillList(User _user)
        {
            ViewBag.Librarys = libraryRepositpry().GetAll().ToDropDown(c => c.ID.ToString(), c => c.Name, c=> c.ID == _user.LibraryID);
            ViewBag.Positions = typeof(UserPosition).ToDistionary().ToDropDown(c => c.Value.ToString(), c => c.Value, c=> _user.Position == c.Value.ToString());
        }
    }
}