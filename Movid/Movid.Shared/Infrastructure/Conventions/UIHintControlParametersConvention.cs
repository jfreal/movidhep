using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Infrastructure.Conventions
{
    /// <summary>
    /// The ui hint control parameters convention.
    /// </summary>
    public class UIHintControlParametersConvention : MetadataConventionBase
    {

        public override bool AppliesTo(
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            return attributes.Any(attr => attr.GetType() == typeof(UIHintAttribute));
        }

        public override void Apply(
            ModelMetadata metadata, 
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            var uiHintAttribute =
                attributes.SingleOrDefault(attr => attr.GetType() == typeof(UIHintAttribute)) as UIHintAttribute;

            if (uiHintAttribute == null)
            {
                return;
            }

            foreach (string key in uiHintAttribute.ControlParameters.Keys)
            {
                metadata.AdditionalValues[key] = uiHintAttribute.ControlParameters[key];
            }
        }

    }
}