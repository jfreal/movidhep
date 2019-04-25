using Movid.App.Infrastructure;
using Movid.Shared.Model;
using System;
using System.Web.Mvc;

namespace Movid.App.Controllers
{
    public class ImagesController : PublicController
    {
        [Route("images/{*path}")]
        public ActionResult Index(string path)
        {
            var attachment = MvcApplication.Store.DatabaseCommands.GetAttachment(path);
            
            if ((path.Contains("largethumb") || path.Contains("smallthumb")) && attachment == null)
            {
                var split = path.Split(new[] {"/"}, StringSplitOptions.None);

                var attachmentPath = split[0];
                var id = split[1];
                var exercise = RavenSession.Load<Exercise>("exercises/" + id);

                if (exercise == null)
                    exercise = RavenSession.Load<MasterExercise>("masterexercises/" + id);

                var originalExerciseAttachment =
                    MvcApplication.Store.DatabaseCommands.GetAttachment(attachmentPath + "/" + exercise.OriginalExercise);

                if( originalExerciseAttachment != null)
                    return new FileStreamResult(originalExerciseAttachment.Data(), "image/jpeg");

                Thumbnailer.GenerateThumbs(exercise.Id, exercise.VideoId);

                attachment = MvcApplication.Store.DatabaseCommands.GetAttachment(path);
            }

            if (attachment == null)
            {
                var blankImagePath = Server.MapPath("/content/img/blank.png");
                return new FilePathResult(blankImagePath, "image/png");
            }

            return new FileStreamResult(attachment.Data(),"image/jpeg");
        }
    }
}
