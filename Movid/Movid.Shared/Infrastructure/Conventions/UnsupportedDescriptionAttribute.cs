using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Infrastructure.Conventions
{
    public class UnsupportedDescriptionAttribute : MetadataConventionBase
    {
        public override void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var descriptionAttribute = attributes.OfType<DescriptionAttribute>().FirstOrDefault();

            metadata.Description = descriptionAttribute == null ? "" : descriptionAttribute.Description;
        }
    }
}