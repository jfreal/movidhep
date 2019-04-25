using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Movid.Web.Conventions
{
    public abstract class MetadataConventionBase
    {
        public virtual bool AppliesTo( IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return true;
        }

        public virtual void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            
        }
    }
}