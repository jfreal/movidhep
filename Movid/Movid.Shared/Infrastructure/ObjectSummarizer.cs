using System;
using System.Collections.Generic;
using System.Linq;
using Brainnom.Core.ObjectSummary;

namespace Movid.Web.Infrastructure
{
    public static class ObjectSummarizer
    {

        /// <summary>
        /// Initializes static members of the <see cref="ObjectSummarizer"/> class.
        /// </summary>
        static ObjectSummarizer()
        {
            KeyProviders = new List<RegisteredKeyProvider>();
            DescriptionProviders = new List<RegisteredDescriptionProvider>();
        }



        /// <summary>
        /// Gets or sets DescriptionProviders.
        /// </summary>
        public static List<RegisteredDescriptionProvider> DescriptionProviders { get; set; }

        /// <summary>
        /// Gets or sets KeyProviders.
        /// </summary>
        public static List<RegisteredKeyProvider> KeyProviders { get; set; }



        /// <summary>
        /// The create entity summary.
        /// </summary>
        /// <typeparam name="T">object type
        /// </typeparam>
        /// <returns>an object summary
        /// </returns>
        public static ObjectSummary CreateEntitySummary<T>()
        {
            return CreateEntitySummary(typeof(T));
        }

        /// <summary>
        /// The create entity summary.
        /// </summary>
        /// <param name="type">
        /// The object type
        /// </param>
        /// <returns>
        /// an object summary
        /// </returns>
        public static ObjectSummary CreateEntitySummary(Type type)
        {
            return CreateEntitySummary(Activator.CreateInstance(type));
        }

        /// <summary>
        /// The create entity summary.
        /// </summary>
        /// <param name="entity">
        /// The object to summarize
        /// </param>
        /// <returns>
        /// an object summary
        /// </returns>
        /// <exception cref="ArgumentNullException">thrown if the object to summarize is null
        /// </exception>
        public static ObjectSummary CreateEntitySummary(object entity)
        {
            CheckConfiguration();

            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Cannot summarize a null entity.");
            }

            return CreateImplementation(entity);
        }

        /// <summary>
        /// The create entity summary.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="keyProviders">
        /// The key providers.
        /// </param>
        /// <param name="descriptionProviders">
        /// The description providers.
        /// </param>
        /// <returns>
        /// an object summary
        /// </returns>
        /// <exception cref="ArgumentNullException">thrown if the object to summarize is null
        /// </exception>
        public static ObjectSummary CreateEntitySummary(
            object entity, 
            List<IKeyProvider> keyProviders = null, 
            List<IDescriptionProvider> descriptionProviders = null)
        {
            CheckConfiguration();

            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Cannot summarize a null entity.");
            }

            return CreateImplementation(entity, keyProviders, descriptionProviders);
        }

        /// <summary>
        /// The create implementation.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="keyProviders">
        /// The key providers.
        /// </param>
        /// <param name="descriptionProviders">
        /// The description providers.
        /// </param>
        /// <returns>
        /// an object summary
        /// </returns>
        public static ObjectSummary CreateImplementation(object entity, List<IKeyProvider> keyProviders = null, List<IDescriptionProvider> descriptionProviders = null)
        {
            var objectSummary = new ObjectSummary
                {
                   EntityTypeName =entity.GetType().Name 
                };

            foreach (RegisteredKeyProvider registeredKeyProvider in KeyProviders.OrderBy(@kp => @kp.Order))
            {
                ObjectSummaryKeys foundKeys = registeredKeyProvider.Provider.FindKeys(entity);

                bool keysFound = foundKeys != null;

                if (keysFound)
                {
                    objectSummary.Keys = foundKeys;
                }

                if (registeredKeyProvider.Breaking && keysFound)
                {
                    break;
                }
            }

            foreach (RegisteredDescriptionProvider registeredKeyProvider in DescriptionProviders.OrderBy(@dp => @dp.Order))
            {
                string foundDisplayText = registeredKeyProvider.Provider.FindDescription(entity);
                string[] foundDisplayNames = registeredKeyProvider.Provider.FindDescriptionPropertyNames(entity);

                if (foundDisplayText != null)
                {
                    objectSummary.DisplayText = foundDisplayText;
                }

                if (foundDisplayNames != null && foundDisplayNames.Any())
                {
                    objectSummary.DisplayFields = foundDisplayNames;
                }

                if (registeredKeyProvider.Breaking && foundDisplayText != null)
                {
                    break;
                }
            }

            return objectSummary;
        }



        /// <summary>
        /// The check configuration.
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if no key or description providers are registered</exception>
        private static void CheckConfiguration()
        {
            if (KeyProviders.Count == 0)
            {
                //throw new InvalidOperationException("No Key Providers are Registered");
            }

            if (DescriptionProviders.Count == 0)
            {
                //throw new InvalidOperationException("No Description Providers are Registered");
            }
        }

    }
}