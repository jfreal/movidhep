using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Movid.Web.Conventions
{
    public abstract class ValidationConventionRuleBase
    {
        public virtual bool AppliesTo(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return true;
        }

        public virtual IEnumerable<Attribute> Apply(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            return attributes;
        }
    }
}