using System;
using System.Collections;
using System.Collections.Generic;
using Brainnom.Core;

namespace Movid.Web.Conventions
{
    public class ImpliedDescriptionFromTypeNameForViewModels : MetadataConventionBase
    {
        public override bool AppliesTo(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            //just for viewmodels that aren't properties

            return containerType == null;
        }

        public override void Apply(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            string modelName = modelType.Name;

            if( string.IsNullOrWhiteSpace(metadata.Description))
            {
                if (modelType.IsGenericType)
                {
                    if (typeof (ICollection).IsAssignableFrom(modelType.GetGenericTypeDefinition()))
                    {
                        modelName = modelType.GetGenericArguments()[0].Name;
                    }
                }

                modelName = modelName.Replace("ViewModel", "");
                modelName = modelName.Replace("GridViewModel", "");
                modelName = modelName.Replace("EditViewModel", "");
                modelName = modelName.Replace("AddViewModel", "");

                metadata.Description = modelName.HumanizeCamel();
            }

            base.Apply(metadata, attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }


}
