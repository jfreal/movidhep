

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Brainnom.Core.Extensions
{
    /// <summary>
    /// Collection of extension methods for reflection over types and propertyinfo
    /// to read mostly metadata information
    /// </summary>
    public static class ReflectionMetadataExtensions
    {
        public static Type GetGenericCollectionType(this Type type)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(type)) return null;

            if (!type.IsGenericType) return null;

            var genericArguments = type.GetGenericArguments();

            return genericArguments.Count() == 1 ? genericArguments[0] : null;
        }

        public static Type GetGenericCollectionType(this PropertyInfo type)
        {
            return GetGenericCollectionType(type.PropertyType);
        }

        public static IEnumerable<string> FindKeyProperties(this Type type)
        {
            return from propertyInfo in type.GetMetadataType().GetProperties()
                   where propertyInfo.AttributeExists<KeyAttribute>()
                   select propertyInfo.Name;
        }


        /// <summary>
        /// Get the property value from an object using reflection
        /// </summary>
        /// <param name="source">source of value</param>
        /// <param name="propertyName">name of the property to read</param>
        /// <returns>the value of the property</returns>
        /// <remarks>returns null if property not found</remarks>
        public static object GetPropertyValueByName(this object source, string propertyName)
        {
            var property = source.GetType().GetProperty(propertyName);

            if (property == null)
                return null;

            return property.GetValue(source, new object[0]);
        }

        /// <summary>
        /// if the property doesn't exist, returns
        /// </summary>
        public static void SetPropertyValueByName(this object source, string propertyName, object value)
        {
            var property = source.GetType().GetProperty(propertyName);

            if (property == null)
                return;

            property.SetValue(source, value, new object[0]);
        }

        /// <summary>
        /// Helpful extension method for returning the Type 
        /// specified by the MetadataType attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetMetadataType(this Type type)
        {
            var buddyType = type.GetAttribute<MetadataTypeAttribute>();

            return (buddyType == null) ? type : buddyType.MetadataClassType;
        }

        public static bool HasMetadataType( this Type type)
        {
            return type.GetAttribute<MetadataTypeAttribute>() != null;
        }

        /// <summary>
        /// Reflects over a property info to find the first attribute of the provided type
        /// </summary>
        /// <typeparam name="T">the type of attribute your looking for</typeparam>
        /// <param name="propertyInfo">the propertyinfo haystack</param>
        /// <returns>the first instanse of the found attribute</returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        /// <summary>
        /// Reflects over a Type to find the first attribute of the provided type
        /// </summary>
        /// <typeparam name="T">the type of attribute your looking for</typeparam>
        /// <param name="type">the Type your searching</param>
        /// <returns>the first instanse of the found attribute</returns>
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), false).OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Reflects over a Type to find the all attribute of the provided type
        /// </summary>
        /// <typeparam name="T">the type of attribute your looking for</typeparam>
        /// <param name="type">the Type your searching</param>
        /// <returns>the all instanses of the found attribute</returns>
        public static IEnumerable<T> GetAttributes<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), false).OfType<T>();
        }

        /// <summary>
        /// Reflects over a Property info to return a boolean of whether or not an instance of the type exists
        /// </summary>
        /// <typeparam name="T">the type of attribute your looking for</typeparam>
        /// <param name="propertyInfo">the PropertyInfo your searching for</param>
        /// <returns>whether or not a single instanse of the type exists</returns>
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;

            return attribute != null;
        }

        /// <summary>
        /// Returns all the declared ( non inherited, public ) properties of a given type.
        /// </summary>
        /// <param name="type">the type to reflect over</param>
        /// <returns>all the declared ( non inherited, public ) properties</returns>
        public static PropertyInfo[] GetDeclaredProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }
    }
}


