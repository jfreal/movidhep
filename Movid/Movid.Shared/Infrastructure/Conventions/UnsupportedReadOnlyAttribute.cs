using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class UnsupportedReadOnlyAttribute : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return !string.IsNullOrWhiteSpace(propertyName) 
                && containerType != null 
                && attributes.OfType<ReadOnlyAttribute>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.IsReadOnly = true;

            metadata.ShowForEdit = false;
            metadata.ShowForDisplay = true;

            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
