using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Brainnom.Core.Extensions;
using Movid.Web.Conventions;
using Movid.Web.Infrastructure.Extensions;

namespace Movid.Web.Infrastructure.Conventions
{
    /// <summary>
    /// Space out camel case property name as display name convention
    /// </summary>
    public class SpaceOutCamelCasePropertyNameAsDisplayName : MetadataConventionBase
    {

        /// <summary>Apply the convention</summary>
        /// <param name="metadata">the model metadata</param>
        /// <param name="attributes">The model attributes.</param>
        /// <param name="containerType">The container type.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The model type.</param>
        /// <param name="propertyName">The property name.</param>
        public override void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var displayName = metadata.DisplayName ?? metadata.PropertyName.InsertSpacesIntoCamelCase();

            metadata.DisplayName = displayName;
        }

    }
}
