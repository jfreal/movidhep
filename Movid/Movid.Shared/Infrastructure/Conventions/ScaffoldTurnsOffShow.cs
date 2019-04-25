using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Shared.Infrastructure.Conventions
{
    public class ScaffoldTurnsOffShow : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            return attributes.OfType<ScaffoldColumnAttribute>().Any(x=>!x.Scaffold);
        }

        public override void Apply(ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            metadata.ShowForDisplay = false;
            metadata.ShowForEdit = false;
        }
    }
}

