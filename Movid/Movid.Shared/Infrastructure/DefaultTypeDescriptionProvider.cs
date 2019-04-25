using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Movid.Web
{
    public class DefaultTypeDescriptionProvider : System.ComponentModel.TypeDescriptionProvider
    {
        private Type Type { get; set; }

        public DefaultTypeDescriptionProvider(Type type)
            : this(type, TypeDescriptor.GetProvider(type))
        {
            this.Type = type;
        }

        public DefaultTypeDescriptionProvider(Type type, TypeDescriptionProvider parentProvider)
            : base(parentProvider)
        {
            this.Type = type;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            var typeDescriptor = base.GetTypeDescriptor(objectType, instance);

            return new DefaultTypeDescriptor(typeDescriptor, this.Type);
        }
    }
}
