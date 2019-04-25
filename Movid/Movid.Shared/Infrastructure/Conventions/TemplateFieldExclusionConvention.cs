using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Infrastructure.Conventions
{
    /// <summary>
    /// The template field exclusion convention.
    /// </summary>
    public class TemplateFieldExclusionConvention : MetadataConventionBase
    {

        /// <summary>
        /// Gets ExcludedProperties.
        /// </summary>
        private static List<string> ExcludedProperties
        {
            get
            {
                var excludedProperties = HttpContext.Current.Session["ExcludedProperties"] as List<string>;

                if (excludedProperties == null)
                {
                    excludedProperties = new List<string>();
                    HttpContext.Current.Session["ExcludedProperties"] = excludedProperties;
                }

                return excludedProperties;
            }
        }



        /// <summary>
        /// Add an exclusion.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void AddExclusion(string propertyName)
        {
            if (!ExcludedProperties.Any(p => p == propertyName))
            {
                ExcludedProperties.Add(propertyName);
            }
        }

        /// <summary>
        /// Determines if the convention applies to a object
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The container type.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>true if the convention applies, false otherwise</returns>
        public override bool AppliesTo(
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            return ExcludedProperties.Any(p => p == propertyName);
        }

        /// <summary>
        /// Applies the convention to an object
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The container type.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        public override void Apply(
            ModelMetadata metadata, 
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            metadata.ShowForEdit = false;
            metadata.ShowForDisplay = false;
        }

        /// <summary>
        /// Clears any exclusions.
        /// </summary>
        public void ClearExclusions()
        {
            ExcludedProperties.Clear();
        }

        /// <summary>
        /// Removes an exclusion.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void RemoveExclusion(string propertyName)
        {
            if (ExcludedProperties.Any(p => p == propertyName))
            {
                ExcludedProperties.Remove(propertyName);
            }
        }

    }
}