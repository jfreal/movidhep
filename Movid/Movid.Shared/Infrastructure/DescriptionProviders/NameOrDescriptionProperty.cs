using System;
using System.Linq;
using System.Reflection;
using Brainnom.Core.ObjectSummary;

namespace Movid.Web.Infrastructure.DescriptionProviders
{
    /// <summary>
    /// The name or description property.
    /// </summary>
    public class NameOrDescriptionProperty : IDescriptionProvider
    {


        /// <summary>
        /// The find description.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The description string.
        /// </returns>
        public string FindDescription(object entity)
        {
            PropertyInfo[] properties = this.FindDescriptionProperties(entity);
            string description = string.Empty;

            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(entity, new object[0]);

                description += propertyValue == null ? string.Empty : propertyValue.ToString();

                if (property != properties.Last())
                {
                    description += ",";
                }
            }

            return description;
        }

        /// <summary>
        /// The find description property names.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// an array of description names
        /// </returns>
        public string[] FindDescriptionPropertyNames(object entity)
        {
            return this.FindDescriptionProperties(entity).Select(p => p.Name).ToArray();
        }




        /// <summary>
        /// The find description properties.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// an array of property information
        /// </returns>
        protected PropertyInfo[] FindDescriptionProperties(object entity)
        {
            if (entity == null)
            {
                return new PropertyInfo[0];
            }

            Type entityType = entity.GetType();

            PropertyInfo textProperty = entityType.GetProperty("Name") ??
                                        entityType.GetProperty("Description") ?? entityType.GetProperty("Rank");
                
            // cfar rank
            if (textProperty == null)
            {
                // nothing left?  try to match props that end in name for wordy schemas
                textProperty = entityType.GetProperties().FirstOrDefault(@p => @p.Name.EndsWith("Name"));
            }

            if (textProperty != null)
            {
                return new[] { textProperty };
            }

            // is personish
            PropertyInfo firstNameProperty = entityType.GetProperty("NameFirst");
            PropertyInfo lastNameProperty = entityType.GetProperty("NameLast");

            if (firstNameProperty != null && lastNameProperty != null)
            {
                return new[] { lastNameProperty, firstNameProperty };
            }

            return new PropertyInfo[0];
        }

    }
}