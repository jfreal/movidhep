using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movid.Web.Mvc.ViewModelExtenders
{
    public class CheckBoxOptions : Attribute
    {
        public string[] Choices { get; set; }
    }
}
