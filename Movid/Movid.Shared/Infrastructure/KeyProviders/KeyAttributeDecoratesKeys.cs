using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Brainnom.Core.Extensions;
using Brainnom.Core.ObjectSummary;

namespace Movid.Web.Infrastructure.KeyProviders
{
    /// <summary>
    /// The key attribute decorates keys.
    /// </summary>
    public class KeyAttributeDecoratesKeys : IKeyProvider
    {


        /// <summary>
        /// The find keys.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Object summary keys</returns>
        /// <exception cref="ArgumentNullException">thrown if the entity is null </exception>
        public ObjectSummaryKeys FindKeys(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Can't find keys for a null entity");
            }

            IEnumerable<PropertyInfo> keyProperties =
                entity.GetType().GetProperties().Where(p => p.GetAttribute<KeyAttribute>() != null);

            var entityKeys = new ObjectSummaryKeys();
            foreach (PropertyInfo keyProperty in keyProperties)
            {
                entityKeys[keyProperty.Name] = keyProperty.GetValue(entity, null);
            }

            return entityKeys;
        }


    }
}