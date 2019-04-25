using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movid.Web.Utility
{
    public class ModalViewEngine : RazorViewEngine 
    {
        private static string[] NewPartialViewFormats = new[] {
            "~/Views/Admin/{1}/{0}.cshtml",
            "~/Views/Admin/Shared/{0}.cshtml"
        };

        private static string[] NewViewFormats = new[] {
            "~/Views/Admin/{1}/{0}.cshtml",
            "~/Views/Admin/Shared/{0}.cshtml"
        };

        public ModalViewEngine()
        {     
           
        base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
            ViewLocationFormats = ViewLocationFormats.Union(NewViewFormats).ToArray();


            ///* {0} = view name or master page name 
            // * {1} = controller name      */  

            //// create our master page location  
            //MasterLocationFormats = new[] {  
            //    "~/Views/Shared/{0}.master"  
            //};  

            //// create our views and common shared locations  
            //ViewLocationFormats = new[] {  
            //    "~/Views/{1}/{0}.aspx",  
            //    "~/Views/Admin/{1}/{0}.aspx",  
            //    "~/Views/Shared/{0}.aspx",
            //    "~/Views/Admin/Shared/{0}.aspx"
            //};  

            //// create our partial views and common shared locations  
            //PartialViewLocationFormats = new[] {  
            //    "~/Views/{1}/{0}.ascx",
            //    "~/Views/Admin/{1}/{0}.ascx",  
            //    "~/Views/Shared/{0}.ascx",
            //    "~/Views/Admin/Shared/{0}.ascx"

            //}; 
        }


      

  

 

    

}


        
    }
