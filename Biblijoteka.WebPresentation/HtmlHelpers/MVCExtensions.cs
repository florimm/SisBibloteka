using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Biblioteka.DomainModel;
using Biblioteka.DomainModel.Interfaces;
using Biblioteka.WebPresentation.Models;

namespace Biblioteka.WebPresentation.HtmlHelpers
{
    public static class MVCExtensions
    {
        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IQueryable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector, Func<TSource, bool> selected)
        {
            return source.ToList().ToDropDown(keySelector, elementSelector, selected);
        }
        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IQueryable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector)
        {
            return source.ToList().ToDropDown(keySelector, elementSelector);
        }
        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector)
        {
            return source.Select(c => new SelectListItem { Value = keySelector(c), Text = elementSelector(c) }).ToList();
        }
        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector, Func<TSource, bool> selected)
        {
            return source.Select(c => new SelectListItem { Value = keySelector(c), Text = elementSelector(c), Selected = selected(c) }).ToList();
        }
        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector, Func<TSource, bool> selected, bool generateDummyData)
        {

            var data = source.Select(c => new SelectListItem { Value = keySelector(c), Text = elementSelector(c), Selected = selected(c) }).ToList();
            var ls = new List<SelectListItem>();
            if (generateDummyData)
            {
                var sl = new SelectListItem { Text = "Other", Value = "", Selected = !data.Any(c => c.Selected) };
                ls.Add(sl);
            }
            ls.AddRange(data);
            return ls;
        }

        public static IEnumerable<SelectListItem> ToDropDown<TSource>(this IEnumerable<TSource> source, Func<TSource, string> keySelector, Func<TSource, string> elementSelector, bool generateDummyData)
        {
            var data = source.Select(c => new SelectListItem { Value = keySelector(c), Text = elementSelector(c) }).ToList();
            var ls = new List<SelectListItem>();
            if (generateDummyData)
            {
                var sl = new SelectListItem { Text = "Other", Value = "", Selected = !data.Any(c => c.Selected) };
                ls.Add(sl);
            }
            ls.AddRange(data);
            return ls;
        }


        public static string ToConcatenated(this IEnumerable<string> source, string delimiter)
        {
            return source.Aggregate(string.Empty, (current, str) => current + (str + delimiter));
        }

        public static string ToConcatenated<TSource>(this IEnumerable<TSource> source, Func<TSource, string> selector, string delimiter)
        {
            return source.Aggregate("", (current, str) => current + (selector(str) + delimiter));
        }

        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
        public static bool HasRole(this User usr, params string[] str)
        {
            if (usr.Position.Contains("Admin"))
                return true;
            var userRoles = usr.Position.Split(',');
            var hasRole = from c in str
                          join u in userRoles on c equals u
                          select c;
            if (hasRole.Any())
                return true;
            return false;
        }
        public static bool IsInRole(this IPrincipal principal, string[] roles)
        {
            var userRoles = DependencyResolver.Current.GetService<ICurrent>().CurrentUser.Position.Split(',');
            if (userRoles.Contains("Admin"))
                return true;
            return userRoles.Any(roles.Contains);
        }
        public static bool IsRole(this IPrincipal principal, string role)
        {
            var userRoles = DependencyResolver.Current.GetService<ICurrent>().CurrentUser.Position.Split(',');
            if (userRoles.Contains("Admin"))
                return true;
            return userRoles.Contains(role);
        }
        public static bool IsNotInRole(this IPrincipal principal, string role)
        {
            var userRoles = DependencyResolver.Current.GetService<ICurrent>().CurrentUser.Position.Split(',');
            if (userRoles.Contains("Admin"))
                return false;
            return !userRoles.Contains(role);
        }

        public static string SubmitButton(this HtmlHelper helper, string buttonText)
        {
            return String.Format("<input type=\"submit\" value=\"{0}\" />", buttonText);
        }
        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, string trueString)
        {
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, string trueString, string falseString)
        {
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Create(falseString));
        }

        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> action)
        {
            return html.IfElse(condition, action, h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction, Func<HtmlHelper, MvcHtmlString> falseAction)
        {
            return (condition ? trueAction : falseAction).Invoke(html);
        }
        public static MvcHtmlString Alert(this HtmlHelper html)
        {
            var alert = html.ViewContext.TempData[typeof(Alert).FullName] as Alert;
            if (alert != null)
                return html.Partial("_Alert", alert);

            return MvcHtmlString.Empty;
        }
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }

    }
}