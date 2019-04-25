using System;

namespace Movid.Web.Mvc.ViewModelExtenders
{
    public class RadioButtonOptions : Attribute
    {
        public string[] Choices{ get; set;}
    }
}