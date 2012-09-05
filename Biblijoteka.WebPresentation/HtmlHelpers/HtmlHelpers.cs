using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Biblioteka.WebPresentation.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString DeleteLink(this HtmlHelper html, string linkText, string id, string action, string controller, string typeofmodule)
        {
            return html.ActionLink(linkText, action, controller, new { id }, new { data_ajax_delete = typeofmodule, data_ajax_msg = "Delete " + typeofmodule + "!" });
        }

        public static HtmlString ToolTipFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var exp = (MemberExpression)expression.Body;
            foreach (Attribute attribute in exp.Expression.Type.GetProperty(exp.Member.Name).GetCustomAttributes(true))
            {
                //if (typeof(TooltipAttribute) == attribute.GetType())
                //{
                //    return MvcHtmlString.Create(((TooltipAttribute)attribute).Description);
                //}

            } return new HtmlString("");
        }
        public static HtmlString ToolTip(this HtmlHelper html, object obj)
        {
            foreach (Attribute attribute in obj.GetType().GetCustomAttributes(true))
            {
                //if (typeof(TooltipAttribute) == attribute.GetType())
                //{
                //    return MvcHtmlString.Create(((TooltipAttribute)attribute).Description);
                //}
            }
            return new HtmlString("");
        }

        /// <summary>
        /// Begins a conditional rendering statement
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="condition"></param>
        /// <param name="ifAction"></param>
        /// <returns></returns>
        public static ConditionalHtmlRender If(this HtmlHelper helper, bool condition, Func<HtmlHelper, string> ifAction)
        {
            return new ConditionalHtmlRender(helper, condition, ifAction);
        }

        public class ConditionalHtmlRender
        {
            private readonly HtmlHelper _helper;
            private readonly bool _ifCondition;
            private readonly Func<HtmlHelper, string> _ifAction;
            private readonly Dictionary<bool, Func<HtmlHelper, string>> _elseActions = new Dictionary<bool, Func<HtmlHelper, string>>();

            public ConditionalHtmlRender(HtmlHelper helper, bool ifCondition, Func<HtmlHelper, string> ifAction)
            {
                _helper = helper;
                _ifCondition = ifCondition;
                _ifAction = ifAction;
            }

            /// <summary>
            /// Ends the conditional block with an else branch
            /// </summary>
            /// <param name="renderAction"></param>
            /// <returns></returns>
            public ConditionalHtmlRender Else(Func<HtmlHelper, string> renderAction)
            {
                return ElseIf(true, renderAction);
            }

            /// <summary>
            /// Adds an else if branch to the conditional block
            /// </summary>
            /// <param name="condition"></param>
            /// <param name="renderAction"></param>
            /// <returns></returns>
            public ConditionalHtmlRender ElseIf(bool condition, Func<HtmlHelper, string> renderAction)
            {
                _elseActions.Add(condition, renderAction);
                return this;
            }

            public override string ToString()
            {
                if (_ifCondition) // if the IF condition is true, render IF action
                {
                    return _ifAction.Invoke(_helper);
                }

                // find the first ELSE condition that is true, and render it
                foreach (KeyValuePair<bool, Func<HtmlHelper, string>> elseAction in _elseActions)
                {
                    if (elseAction.Key)
                    {
                        return elseAction.Value.Invoke(_helper);
                    }
                }

                // no condition true; render nothing
                return String.Empty;
            }
        }


        public static MvcHtmlString LabelWithTooltip<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            if (!string.IsNullOrEmpty(metaData.Description))
                label.Attributes.Add("title", metaData.Description);

            label.SetInnerText(labelText);
            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TextBoxWithTooltip<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("input");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            if (!string.IsNullOrEmpty(metaData.Description))
                label.Attributes.Add("title", metaData.Description);
            label.Attributes.Add("type", "text");
            label.Attributes.Add("value", "");

            return MvcHtmlString.Create(label.ToString());
        }
        public static MvcHtmlString DynamicWithTooltip<TModel>(this HtmlHelper<TModel> helper, string expression)
        {

            helper.TextBox(expression);
            var metaData = ModelMetadata.FromStringExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("input");

            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            if (!string.IsNullOrEmpty(metaData.Description))
                label.Attributes.Add("title", metaData.Description);
            label.Attributes.Add("type", "text");
            label.Attributes.Add("value", "");

            return MvcHtmlString.Create(label.ToString());
        }


    }
}