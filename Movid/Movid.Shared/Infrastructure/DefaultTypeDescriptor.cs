using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Movid.Web
{

    public class DefaultTypeDescriptor : CustomTypeDescriptor
    {
        private Type Type { get; set; }

        public DefaultTypeDescriptor(ICustomTypeDescriptor parent, Type type)
            : base(parent)
        {
            this.Type = type;
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> propertyDescriptors = new List<PropertyDescriptor>();

            foreach (PropertyDescriptor propDescriptor in base.GetProperties())
            {
                List<Attribute> newAttributes = new List<Attribute>();

                if (propDescriptor.DisplayName == "Id")
                    newAttributes.Add(new ScaffoldColumnAttribute(false));

                if(propDescriptor.DisplayName.Contains("Description"))
                    newAttributes.Add( new DataTypeAttribute( DataType.MultilineText ) );

                if (propDescriptor.DisplayName.Contains("Version"))
                {
                    newAttributes.Add(new UIHintAttribute("Version"));                    
                }

                //  Display Name Rules ...
                //  If the property doesn't already have a DisplayNameAttribute defined
                //  go ahead and auto-generate one based on the property name
                if (!HasAttribute<DisplayNameAttribute>(propDescriptor))
                {
                    //  generate the display name
                    string friendlyDisplayName = ToHumanFromPascal(propDescriptor.Name);

                    //  add it to the list
                    newAttributes.Add(new DisplayNameAttribute(friendlyDisplayName));
                }

                //  Display Format Rules ...
                //  If the property doesn't already have a DisplayFormatAttribute defined
                //  go ahead and auto-generate one based on the property type
                if (!HasAttribute<DisplayFormatAttribute>(propDescriptor))
                {
                    //  get the default format for the property type
                    string displayFormat = GetDisplayFormat(propDescriptor.PropertyType);

                    //  add it to the list
                    newAttributes.Add(new DisplayFormatAttribute() { DataFormatString = displayFormat });
                }

                propertyDescriptors.Add(new WrappedPropertyDescriptor(propDescriptor, newAttributes.ToArray()));
            }

            //  return the descriptor collection
            return new PropertyDescriptorCollection(propertyDescriptors.ToArray(), true);
        }

        private static bool HasAttribute<T>(PropertyDescriptor descriptor) where T : Attribute
        {
            bool value = false;
            for (int i = 0; i < descriptor.Attributes.Count && !value; i++)
            {
                value = (descriptor.Attributes[i] is T);
            }

            return value;
        }

        private static string GetDisplayFormat(Type type)
        {
            string defaultFormat = "{0}";

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                defaultFormat = "{0:d}";
            }
            else if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                defaultFormat = "{0:c}";
            }

            return defaultFormat;
        }

        /// <summary>
        /// Taken from http://www.darkhelmetlive.com/blog/2008/08/02/pascal-case-to-human-readable-with-csharp-extension-methods
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ToHumanFromPascal(string s)
        {
            StringBuilder sb = new StringBuilder();
            char[] ca = s.ToCharArray();
            sb.Append(ca[0]);
            for (int i = 1; i < ca.Length - 1; i++)
            {
                char c = ca[i];
                if (char.IsUpper(c) && (char.IsLower(ca[i + 1]) || char.IsLower(ca[i - 1])))
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }
            sb.Append(ca[ca.Length - 1]);

            return sb.ToString();
        }

        private class WrappedPropertyDescriptor : PropertyDescriptor
        {
            private PropertyDescriptor _wrappedPropertyDescriptor;

            public WrappedPropertyDescriptor(PropertyDescriptor wrappedPropertyDescriptor, Attribute[] attributes)
                : base(wrappedPropertyDescriptor, attributes)
            {
                _wrappedPropertyDescriptor = wrappedPropertyDescriptor;
            }

            public override bool CanResetValue(object component)
            {
                return _wrappedPropertyDescriptor.CanResetValue(component);
            }

            public override Type ComponentType
            {
                get { return _wrappedPropertyDescriptor.ComponentType; }
            }

            public override object GetValue(object component)
            {
                return _wrappedPropertyDescriptor.GetValue(component);
            }

            public override bool IsReadOnly
            {
                get { return _wrappedPropertyDescriptor.IsReadOnly; }
            }

            public override Type PropertyType
            {
                get { return _wrappedPropertyDescriptor.PropertyType; }
            }

            public override void ResetValue(object component)
            {
                _wrappedPropertyDescriptor.ResetValue(component);
            }

            public override void SetValue(object component, object value)
            {
                _wrappedPropertyDescriptor.SetValue(component, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return _wrappedPropertyDescriptor.ShouldSerializeValue(component);
            }
        }

    }
}