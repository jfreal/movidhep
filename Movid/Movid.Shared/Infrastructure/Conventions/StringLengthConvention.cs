using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Movid.Web.Conventions;

namespace Movid.Web.Infrastructure.Conventions
{
    

    public class StringLengthConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<StringLengthAttribute>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {

            metadata.AdditionalValues["StringLength.MinimumLength"] = attributes.OfType<StringLengthAttribute>().First().MinimumLength;
            metadata.AdditionalValues["StringLength.MaximumLength"] = attributes.OfType<StringLengthAttribute>().First().MaximumLength;

            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
