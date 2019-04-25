namespace Movid.Web.Conventions.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PhoneNumberDataTypeIsValidType : ValidationConventionRuleBase
    {
        public override bool AppliesTo(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return metadata.DataTypeName == "PhoneNumber";
        }

        public override IEnumerable<Attribute> Apply(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return new List<Attribute>(attributes) { new ValidatePhoneNumber() };
        }
    }
}