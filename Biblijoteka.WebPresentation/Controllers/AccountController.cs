using System;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.WebPresentation.Models;

namespace Biblioteka.WebPresentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly Func<IFormsAuthenticationService> formsService;
        private readonly Func<IMembershipService> membershipService;
        private readonly ICurrent current;
        private readonly Func<ILogger> log;
        private readonly Func<IRepository<User>> userRepository;

        public AccountController(
            Func<IFormsAuthenticationService> _authenticationService,
            Func<IMembershipService> _membershipService,
            ICurrent _current,
            Func<ILogger> _log, Func<IRepository<User>> _userRepository)
        {
            formsService = _authenticationService;
            membershipService = _membershipService;
            current = _current;
            log = _log;
            userRepository = _userRepository;
        }

        public ActionResult LogOn()
        {
            var u = new User();
            return View(u);
        }

        [HttpPost]
        public ActionResult LogOn(User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (membershipService().ValidateUser(model.UserName, model.Password))
                {
                    formsService().SignIn(model.UserName, false);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Members");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please try again");
                }
            }
            return View(model);

        }
        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            formsService().SignOut();
            DependencyResolver.Current.GetService<ICurrent>().CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var repo = userRepository();
                repo.Insert(model);
                if(repo.Commit())
                {
                    formsService().SignIn(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [System.Web.Mvc.Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = 3;
            return View();
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
