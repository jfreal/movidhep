using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Movid.Web.Conventions;

namespace Movid.Web.Conventions
{
    public class ConventionMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        //base: //null check
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            
            foreach( var convention in Conventions.Where(@c => @c.AppliesTo(attributes, containerType, modelAccessor, modelType, propertyName) ))
            {
                convention.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
            }

            //quickie hack because required attribute is broken in MVC 2 RC
            if (attributes.OfType<RequiredAttribute>().FirstOrDefault() != null)
                metadata.IsRequired = true;
            
            return metadata;
        }

        static ConventionMetadataProvider()
        {
            Conventions = new List<MetadataConventionBase>();    
        }

        public static List<MetadataConventionBase> Conventions { get; set; }


    }
}