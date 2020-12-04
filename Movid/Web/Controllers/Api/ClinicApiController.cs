using Movid.App.Models;
using Movid.App.Models.Api;
using Movid.App.Models.ViewModels;
using Movid.Shared;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Movid.App.Controllers.Api
{
    public class ClinicApiController : BaseApiController
    {
        [Route("api/clinics")]
        public ClinicWithUsersViewModel GetUsers()
        {
            var users = RavenSession.Query<User>().Where(x => x.AccountId == LoggedInUser.AccountId).ToList();

            var clinics = RavenSession.Query<Clinic>().Where(x => x.AccountId == LoggedInUser.AccountId).OrderBy(x=>x.Name).ToList();

            var vm = new ClinicWithUsersViewModel(users, clinics);

            return vm;
        }

        [Route("api/clinics/add")]
        [HttpPost]
        public ApiResponse Add(Clinic postedModel)
        {
            var clinic = new Clinic()
                             {
                                 AccountId = Account.Id,
                                 Name = postedModel.Name
                             };

            RavenSession.Store(clinic);
            RavenSession.SaveChanges();

            return new ApiResponse(success: postedModel.Name + " created successfuly"){Model = clinic};
        }

        [Route("api/clinics/delete/{clinicId}")]
        [HttpPost]
        public ApiResponse Add(int clinicId)
        {
            var clinic = RavenSession.Load<Clinic>("clinics/" + clinicId);
            
            RavenSession.Delete(clinic);
            RavenSession.SaveChanges();

            return new ApiResponse(success: clinic.Name + " deleted") { Model = clinic };
        }

        [Route("api/clinics/{clinicId}")]
        public Clinic GetClinic(int clinicId)
        {
            var clinic = RavenSession.Load<Clinic>("clinics/" + clinicId);

            return clinic;
        }

        [Route("api/clinic/save")]
        [HttpPost]
        public ApiResponse Save(Clinic postedModel)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(ModelState);
            }

            var clinic = RavenSession.Load<Clinic>("clinics/" + postedModel.Id);            

            clinic.Name = postedModel.Name;
            clinic.Address = postedModel.Address;
            clinic.Address2 = postedModel.Address2;
            clinic.CityStateZip = postedModel.CityStateZip;
            clinic.Phone = postedModel.Phone;
            clinic.Email = postedModel.Email;
            clinic.Website = postedModel.Website;

            RavenSession.SaveChanges();

            return new ApiResponse(success: "Clinic settings updated");
        }

        [HttpPost]
        [Route("api/clinics/attachment/{clinicId}")]
        public async Task<HttpResponseMessage> SaveAttachment(int clinicId)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentType != null)
                {
                    var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                    var buffer = file.ReadAsStreamAsync().Result;

                    MvcApplication.Store.DatabaseCommands.PutAttachment("cliniclogo/" + clinicId, null, buffer, new RavenJObject());
                }
                
                //Do whatever you want with filename and its binaray data.
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ApiResponse(success: "New logo uploaded ok.")); ;
        }
    }
}