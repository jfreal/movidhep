using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    using Movid.Web.Mvc.ViewModelExtenders;

    public class CheckBoxOptionsConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<CheckBoxOptions>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {

            metadata.AdditionalValues["CheckBoxOptions.Choices"] = attributes.OfType<CheckBoxOptions>().First().Choices;
            metadata.TemplateHint = "CheckBoxOptions";
            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
