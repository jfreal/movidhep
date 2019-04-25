using System;
using System.Collections.Generic;
using System.Linq;
using Movid.Web.Conventions;
using Movid.Web.Infrastructure.ViewModelExtenders;
using Movid.Web.Mvc.ViewModelExtenders;

namespace Movid.Web.Conventions
{
    public class NumericOnlyConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<Integer>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.AdditionalValues["Integer"] = true;

            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
