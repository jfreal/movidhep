using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class BooleanFieldsArentImplicitlyRequired_MostOfTheTimeTheyDefaultToFalse : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            //bool properties that are not required 
            //default to false and shouldn't be marked as required

            if (modelType == typeof(bool) )
            {
                var hasRequired = attributes.OfType<RequiredAttribute>().Any();

                if (!hasRequired)
                    return true;
            }
            return false;
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.IsRequired = false;
        }

    }
}
