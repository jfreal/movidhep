using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Movid.Web.Infrastructure.Extensions
{
    /// <summary>
    /// The excludible display extension.
    /// </summary>
    public static class ExcludibleDisplayExtension
    {

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="allowExclusions"> The allow exclusions. </param>
        /// <typeparam name="TModel">view model type </typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display </typeparam>
        /// <returns>a template for model generator </returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.DisplayFor(e), allowExclusions);
        }

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">true toallow exclusions.</param>
        /// <typeparam name="TModel">view model type </typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display</typeparam>
        /// <returns>a template for model generator
        /// </returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            object additionalViewData, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.DisplayFor(e, additionalViewData), allowExclusions);
        }

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="allowExclusions">true to allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.DisplayFor(e, templateName), allowExclusions);
        }

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">true to allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            object additionalViewData, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, 
                expression, 
                (h, e) => h.DisplayFor(e, templateName, additionalViewData), 
                allowExclusions);
        }

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="htmlFieldName">The html field name.</param>
        /// <param name="allowExclusions">The allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            string htmlFieldName, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, 
                expression, 
                (h, e) => h.DisplayFor(e, templateName, htmlFieldName), 
                allowExclusions);
        }

        /// <summary>
        /// Creates a display for a model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="htmlFieldName">The html field name.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">True to allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">type of the property on the view model to display</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> DisplayFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            string htmlFieldName, 
            object additionalViewData, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, 
                expression, 
                (h, e) => h.DisplayFor(e, templateName, htmlFieldName, additionalViewData), 
                allowExclusions);
        }

    }
}