using Movid.Web.Conventions;
using Movid.Web.Infrastructure.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Movid.Web.Infrastructure.Extensions
{
    /// <summary>
    /// The template for model generator.
    /// </summary>
    /// <typeparam name="TModel">the view model type</typeparam>
    /// <typeparam name="TValue">type of the property on the model to create a template for
    /// </typeparam>
    public class TemplateForModelGenerator<TModel, TValue>
    {

        /// <summary>
        /// allow exclusions.
        /// </summary>
        private readonly bool allowExclusions;

        /// <summary>
        /// The expression.
        /// </summary>
        private readonly Expression<Func<TModel, TValue>> expression;

        /// <summary>
        /// func to help generate template
        /// </summary>
        private readonly Func<HtmlHelper<TModel>, Expression<Func<TModel, TValue>>, MvcHtmlString> func;

        /// <summary>
        /// The html helper which was extended
        /// </summary>
        private readonly HtmlHelper<TModel> html;



        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateForModelGenerator{TModel,TValue}"/> class.
        /// </summary>
        /// <param name="html">The html helper being extended</param>
        /// <param name="expression">The expression.</param>
        /// <param name="func">The func to generate template</param>
        /// <param name="allowExclusions">true to allow exclusions.</param>
        public TemplateForModelGenerator(
            HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, 
            Func<HtmlHelper<TModel>, Expression<Func<TModel, TValue>>, MvcHtmlString> func, 
            bool allowExclusions)
        {
            this.html = html;
            this.func = func;
            this.allowExclusions = allowExclusions;
            this.expression = expression;

            this.GetTemplateFieldExclusionConvention().ClearExclusions();
        }



        /// <summary>
        /// Exclude properties
        /// </summary>
        /// <param name="propertySpecifier">The property specifier.</param>
        /// <returns>a template model generator</returns>
        public TemplateForModelGenerator<TModel, TValue> Exclude(Expression<Func<TValue, object>> propertySpecifier)
        {
            string propertyName = this.GetPropertyName(propertySpecifier);

            return (propertyName != null) ? this.Exclude(propertyName) : this;
        }

        /// <summary>
        /// Exclude a property
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>a template model generator</returns>
        public TemplateForModelGenerator<TModel, TValue> Exclude(string propertyName)
        {
            if (this.allowExclusions)
            {
                this.GetTemplateFieldExclusionConvention().AddExclusion(propertyName);
            }

            return this;
        }

        /// <summary>
        /// Exclude all properties
        /// </summary>
        /// <returns>A template model generator</returns>
        public TemplateForModelGenerator<TModel, TValue> ExcludeAll()
        {
            if (this.allowExclusions)
            {
                IEnumerable<string> propertyNames = typeof(TValue).GetProperties().Select(p => p.Name);

                this.GetTemplateFieldExclusionConvention().ClearExclusions();

                foreach (string propertyName in propertyNames)
                {
                    this.GetTemplateFieldExclusionConvention().AddExclusion(propertyName);
                }
            }

            return this;
        }

        /// <summary>
        /// Include a specific property
        /// </summary>
        /// <param name="propertySpecifier">The property specifier.</param>
        /// <returns>a template model generator</returns>
        public TemplateForModelGenerator<TModel, TValue> Include(Expression<Func<TValue, object>> propertySpecifier)
        {
            string propertyName = this.GetPropertyName(propertySpecifier);

            return (propertyName != null) ? this.Include(propertyName) : this;
        }

        /// <summary>
        /// Include a named property
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>a template model generator </returns>
        public TemplateForModelGenerator<TModel, TValue> Include(string propertyName)
        {
            if (this.allowExclusions)
            {
                this.GetTemplateFieldExclusionConvention().RemoveExclusion(propertyName);
            }

            return this;
        }

        /// <summary>
        /// Include all properties
        /// </summary>
        /// <returns>a template model generator</returns>
        public TemplateForModelGenerator<TModel, TValue> IncludeAll()
        {
            if (this.allowExclusions)
            {
                this.GetTemplateFieldExclusionConvention().ClearExclusions();
            }

            return this;
        }

        /// <summary>
        /// Convert to an html string
        /// </summary>
        /// <returns>
        /// The html string.
        /// </returns>
        public string ToHtmlString()
        {
            string htmlString = this.func.Invoke(this.html, this.expression).ToHtmlString();
            this.GetTemplateFieldExclusionConvention().ClearExclusions();

            return htmlString;
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>
        /// The converted string.
        /// </returns>
        public override string ToString()
        {
            return this.ToHtmlString();
        }



        /// <summary>
        /// From ColumnBuilder.cs in MvcContrib.
        /// </summary>
        /// <param name="lambdaExpression">the expression used to find a member</param>
        /// <returns>a member expression</returns>
        protected MemberExpression GetMemberExpression(LambdaExpression lambdaExpression)
        {
            return this.RemoveUnary(lambdaExpression.Body) as MemberExpression;
        }

        /// <summary>
        /// Get a property full name.
        /// </summary>
        /// <param name="propertySpecifier">The property specifier.</param>
        /// <returns>The get property full name.</returns>
        protected string GetPropertyFullName(Expression<Func<TValue, object>> propertySpecifier)
        {
            Expression currentExpression = this.GetMemberExpression(this.expression);
            string instanceName = string.Empty;
            string propertyName = this.GetPropertyName(propertySpecifier);

            while (currentExpression as MemberExpression != null)
            {
                var tempExpression = currentExpression as MemberExpression;
                instanceName = "." + tempExpression.Member.Name + instanceName;
                currentExpression = tempExpression.Expression;
            }

            return (instanceName.Length > 0) ? instanceName.Substring(1) + "." + propertyName : propertyName;
        }

        /// <summary>
        /// Get a property name
        /// </summary>
        /// <param name="propertySpecifier">The Property specifier</param>
        /// <returns>The get property name.</returns>
        /// <remarks>From ColumnBuilder.cs in MvcContrib.</remarks>
        protected string GetPropertyName(Expression<Func<TValue, object>> propertySpecifier)
        {
            MemberExpression memberExpression = this.GetMemberExpression(propertySpecifier);
            string propertyName = memberExpression == null ? null : memberExpression.Member.Name;

            return propertyName;
        }

        /// <summary>
        /// Gets a template field exclusion convention.
        /// </summary>
        /// <returns>a template field exclusion convention </returns>
        protected TemplateFieldExclusionConvention GetTemplateFieldExclusionConvention()
        {
            IEnumerable<TemplateFieldExclusionConvention> templateFieldExclusionConventions =
                ConventionMetadataProvider.Conventions.OfType<TemplateFieldExclusionConvention>();

            if (!templateFieldExclusionConventions.Any())
            {
                ConventionMetadataProvider.Conventions.Add(new TemplateFieldExclusionConvention());
                templateFieldExclusionConventions =
                    ConventionMetadataProvider.Conventions.OfType<TemplateFieldExclusionConvention>();
            }

            return templateFieldExclusionConventions.First();
        }

        /// <summary>
        /// Remove a unary expression
        /// </summary>
        /// <param name="body">an expression</param>
        /// <returns>an expression after removal </returns>
        /// <remarks>From ColumnBuilder.cs in MvcContrib.</remarks>
        protected Expression RemoveUnary(Expression body)
        {
            var unary = body as UnaryExpression;
            if (unary != null)
            {
                return unary.Operand;
            }

            return body;
        }

    }
}