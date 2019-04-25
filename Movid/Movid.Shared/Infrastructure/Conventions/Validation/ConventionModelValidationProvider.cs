using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Movid.Web.Conventions.Validation
{
    public class ConventionModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        static ConventionModelValidatorProvider()
        {
            Conventions = new List<ValidationConventionRuleBase>();
            
        }

        public static List<ValidationConventionRuleBase> Conventions { get; set; }

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            IEnumerable<Attribute> newAttributes = new List<Attribute>(attributes);
            foreach (var compoundRule in Conventions.Where(@c => @c.AppliesTo(metadata, context, attributes)))
            {
                newAttributes = compoundRule.Apply(metadata, context, attributes);
            }

            return base.GetValidators(metadata, context, newAttributes);
        }
    }
}