// <copyright file="ModelMetadataAdditionalValuesExtensions.cs" company="Brainnom">
//   Copyright (c) 2010 All rights reserved
// </copyright>
// <summary>
//  model metadata additional values extensions.
// </summary>

namespace Movid.Web.Conventions
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Experimental model metadata additional values extensions.
    /// </summary>
    public static class ModelMetadataAdditionalValuesExtensions
    {

        /// <summary>Get group descriptions.</summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <returns>and enumererable filled with the group descriptions</returns>
        public static IEnumerable<string> GetGroupDescriptions(this ModelMetadata modelMetadata)
        {
            if (modelMetadata.HasGrouping())
            {
                return modelMetadata.AdditionalValues["GroupDescriptions"] as IEnumerable<string>;
            }

            return new List<string>();
        } 

        /// <summary>Gets group names.</summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <returns>An enumerable filled with the supplied group names </returns>
        public static IEnumerable<string> GetGroupNames(this ModelMetadata modelMetadata)
        {
            if (modelMetadata.HasGrouping())
            {
                return modelMetadata.AdditionalValues["Groups"] as IEnumerable<string>;
            }

            return new List<string>();
        }

        /// <summary>Checks to see if the model metadata contains the the key 'Group' and if if contains supplied group name</summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <param name="groupName">The group name.</param>
        /// <returns>true if group name found in AdditionalValues, false otherwise </returns>
        public static bool HasGroup(this ModelMetadata modelMetadata, string groupName)
        {
            if (!modelMetadata.AdditionalValues.ContainsKey("Group"))
            {
                return false;
            }

            return string.Compare(groupName, modelMetadata.AdditionalValues["Group"] as string, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>Checks if model metadata contains the key 'Groups'</summary>
        /// <param name="modelMetadata">The model metadata.</param>
        /// <returns>True if Grouping attribute found in AdditionalValues, false otherwise</returns>
        public static bool HasGrouping(this ModelMetadata modelMetadata)
        {
            return modelMetadata.AdditionalValues.ContainsKey("Groups");
        }

    }
}