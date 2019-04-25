using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class EnumUsesEnumTemplateHint : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return modelType.IsEnum;
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.TemplateHint = "Enum";
        }
    }

    public class AllowHtmlUsesHtmlTemplate: MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<AllowHtmlAttribute>().Any();
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.TemplateHint = "Html";
        }
    }
}
