using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class NullDisplayTextIsEmptyStringForStringsConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return modelType == typeof (string);
        }

        public override void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.NullDisplayText = "";
        }
    }
}