using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movid.App.Models
{
    public class TypeAheadDatumViewModel
    {
        public string value { get; set; }
        public string[] tokens { get; set; }
        public string type { get; set; }
    }
        
}