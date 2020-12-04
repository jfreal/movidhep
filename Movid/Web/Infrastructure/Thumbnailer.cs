using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Json.Linq;

namespace Movid.App.Infrastructure
{
    public static class Thumbnailer
    {
        public static bool GenerateThumbs(int id, string videoId)
        {
            var largeThumb = new Vimeo().GenerateThumbnail(videoId, 475);
            var smallThumb = new Vimeo().GenerateThumbnail(videoId, 200);

            if (largeThumb == null || smallThumb == null)
                return false;

            largeThumb.Position = 0;
            smallThumb.Position = 0;
            MvcApplication.Store.DatabaseCommands.PutAttachment("largethumb/" + id, null, largeThumb, new RavenJObject() { });
            MvcApplication.Store..PutAttachment("smallthumb/" + id, null, smallThumb, new RavenJObject() { });

            return true;
        }

    }
}