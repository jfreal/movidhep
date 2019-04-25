// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbKeyFieldAsClassProperty.cs" company="Brainnom">
//   Copyright (c) 2010 All Rights Reserved
// </copyright>
// <summary>
//   The db key field as class property key provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Brainnom.Core.ObjectSummary.KeyProviders
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The db key field as class property key provider.
    /// </summary>
    public class DbKeyFieldAsClassPropertyKeyProvider : IKeyProvider
    {


        /// <summary>
        /// The find keys.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>object summary keys</returns>
        /// <exception cref="ArgumentNullException">thrown if the entity is null</exception>
        public ObjectSummaryKeys FindKeys(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Can't summarize a null entity");
            }

            var key = new ObjectSummaryKeys();

            foreach (
                PropertyInfo propertyInfo in
                    entity.GetType().GetProperties().Where(
                        @p => @p.Name.ToUpper().EndsWith("ID") || @p.Name.ToUpper().EndsWith("PK")))
            {
                key[propertyInfo.Name] = propertyInfo.GetValue(entity, new object[0]);
            }

            return key;
        }


    }
}