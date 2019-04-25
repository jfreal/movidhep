using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Movid.App.Controllers
{
    public class VideoController : Controller
    {
        [Route("localvideo/{path}")]
        public ActionResult Index(string path)
        {

            return new FilePathResult(@"C:\Users\John\Dropbox\Movid\New Videos\" + path, "video/mp4");
        }

    }
}
