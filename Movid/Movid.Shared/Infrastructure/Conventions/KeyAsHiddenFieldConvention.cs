using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class KeyAsHiddenFieldConvention: MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.Any(@a => @a is KeyAttribute || (propertyName ?? "") == "Id");
        }

        public override void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.TemplateHint = "HiddenField";
        }
    }
}