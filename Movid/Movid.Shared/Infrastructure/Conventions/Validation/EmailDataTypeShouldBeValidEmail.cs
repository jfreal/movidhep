using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Movid.Web.Conventions
{
    public class EmailDataTypeShouldBeValidEmail : ValidationConventionRuleBase
    {
        public override bool AppliesTo(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return metadata.DataTypeName == "EmailAddress";
        }

        public override IEnumerable<Attribute> Apply(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return new List<Attribute>(attributes) { new ValidateEmail() };
        }
    }
}