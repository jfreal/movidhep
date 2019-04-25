using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Movid.Web.Infrastructure.Extensions
{
    /// <summary>
    /// The excludible editor extension.
    /// </summary>
    public static class ExcludibleEditorExtension
    {

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper to extend</param>
        /// <param name="expression">The expression.</param>
        /// <param name="allowExclusions">true to allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
            this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.EditorFor(e), allowExclusions);
        }

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">true to allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            object additionalViewData, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.EditorFor(e, additionalViewData), allowExclusions);
        }

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="allowExclusions">The allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.EditorFor(e, templateName), allowExclusions);
        }

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">The allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            object additionalViewData, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.EditorFor(e, templateName, additionalViewData), allowExclusions);
        }

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name. </param>
        /// <param name="htmlFieldName">The html field name.</param>
        /// <param name="allowExclusions">The allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
            this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            string templateName, 
            string htmlFieldName, 
            bool allowExclusions)
        {
            return new TemplateForModelGenerator<TModel, TValue>(
                html, expression, (h, e) => h.EditorFor(e, templateName, htmlFieldName), allowExclusions);
        }

        /// <summary>
        /// Builds an editor for a property on a view model
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="htmlFieldName">The html field name.</param>
        /// <param name="additionalViewData">The additional view data.</param>
        /// <param name="allowExclusions">The allow exclusions.</param>
        /// <typeparam name="TModel">the view model type</typeparam>
        /// <typeparam name="TValue">the type of the property on the view model to edit</typeparam>
        /// <returns>a template for model generator</returns>
        public static TemplateForModelGenerator<TModel, TValue> EditorFor<TModel, TValue>(
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
                (h, e) => h.EditorFor(e, templateName, htmlFieldName, additionalViewData), 
                allowExclusions);
        }

    }
}