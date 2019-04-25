using System;
using System.Collections.Generic;
using System.Linq;
using Movid.Web.Conventions;
using Movid.Web.Mvc.ViewModelExtenders;

namespace Movid.Web.Conventions
{
    public class RadioButtonOptionsConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<RadioButtonOptions>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {

            metadata.AdditionalValues["RadioButtonOptions.Choices"] = attributes.OfType<RadioButtonOptions>().First().Choices;
            metadata.TemplateHint = "RadioButtonOptions";
            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}