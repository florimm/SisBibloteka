using System;
using System.Web;
using System.Web.Mvc;
using Biblioteka.DataAccess.Repositories;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;

namespace Biblioteka.WebPresentation.Models
{
    public class Current : ICurrent
    {
        public User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["User"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string usr = HttpContext.Current.User.Identity.Name;
                    if (string.IsNullOrEmpty(usr))
                        throw new UnauthorizedAccessException("Please login first");
                    var userRepository = DependencyResolver.Current.GetService<IRepository<User>>();
                    var model = userRepository.GetByID(usr);
                    HttpContext.Current.Session.Add("User", model);
                }
                var user = HttpContext.Current.Session["User"] as User;
                return user;
            }
            set { HttpContext.Current.Session.Add("User", value); }
        }
    }

    public class Alert
    {
        
    }
}