using System;
using System.Collections.Generic;
using System.Linq;
using Movid.Web.Conventions;
using Movid.Web.Infrastructure.ViewModelAttributes;

namespace Movid.Shared.Infrastructure.Conventions
{
    public class MarkdownFieldConvention : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<MarkdownAttribute>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.TemplateHint = "Markdown";

            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
