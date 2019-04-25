
//using Movid.App.Models;
//using System.Linq;
//using System.Web.Mvc;

//namespace Movid.App.Controllers
//{
//    [Authorize]
//    public class PatientsController : AppController
//    {
//        [Route("patients")]
//        public ActionResult Grid()
//        {
//            var exercizes = RavenSession.Query<Patient>().Where(x => x.Group == LoggedInUser.Group).OrderBy(x => x.LastName).ToList();

//            ViewBag.GridName = "Patient Manager";
//            ViewBag.GridDescription = @"Use this screen to manage your patients. Adding patients here makes it  
//                                        easier to find them using autocomplete within the HEP Builder, making it
//                                        quicker; and allows us to more accurately track patient engagement metrics. ";

//            return View(exercizes);
//        }

//        [Route("patient/create")]
//        public ActionResult Create()
//        {
//            return View(new Patient());
//        }

//        [Route("patient/{email}")]
//        public ActionResult Single(string email)
//        {
//            var patient = RavenSession.Query<Patient>().FirstOrDefault(x => x.Email.StartsWith(email) && x.Group == LoggedInUser.Group);

//            if (patient == null)
//                return HttpNotFound("Patient not found");

//            return View(patient);
//        }

//        [Route("patient/create")]
//        [HttpPost]
//        public ActionResult Create(Patient postedModel)
//        {
//            try
//            {
//                if (RavenSession.Query<Patient>().Any(x => x.Email == postedModel.Email.Trim() && x.Group == LoggedInUser.Group))
//                {
//                    ModelState.AddModelError("Email", "This email is already in use by another patient.");
//                    return View(postedModel);
//                }

//                if (!ModelState.IsValid)
//                {
//                    return View(postedModel);
//                }

//                postedModel.Group = LoggedInUser.Group;
//                RavenSession.Store(postedModel);
//                RavenSession.SaveChanges();

//                HighFive("New Patient created ok.");

//                return RedirectToAction("Grid");
//            }
//            catch
//            {
//                return View(postedModel);
//            }
//        }

//        [Route("patient/edit/{id}")]
//        public ActionResult Edit(int id)
//        {
//            var Patient = RavenSession.Load<Patient>("patients/" + id);

//            return View(Patient);
//        }



//        [Route("patient/edit/{id}")]
//        [HttpPost]
//        public ActionResult Edit(Patient postedModel, Patient patient)
//        {
//            if (patient.Email != postedModel.Email)
//            {

//                if (
//                    RavenSession.Query<Patient>()
//                                .Any(x => x.Email == postedModel.Email.Trim() && x.Group == LoggedInUser.Group))
//                {
//                    ModelState.AddModelError("Email", "This email is already in use by another patient.");
//                    return View(postedModel);
//                }
//            }

//            if (!ModelState.IsValid)
//            {
//                return View(postedModel);
//            }



//            UpdateModel(patient);

//            HighFive("Patient edited ok.");

//            RavenSession.SaveChanges();

//            return RedirectToAction("Grid");
//        }

//        [Route("patient/delete/{id}")]
//        public ActionResult Delete(int id)
//        {
//            var exr = RavenSession.Load<Patient>("patients/" + id);

//            RavenSession.Delete(exr);
//            RavenSession.SaveChanges();

//            HighFive("Patient deleted.");

//            return RedirectToAction("Grid");

//        }

//        [Route("patient/search/{email}")]
//        public ActionResult Search(string email)
//        {
//            var exr = RavenSession.Query<Patient>().Where(x => x.Email.StartsWith(email) && x.Group == LoggedInUser.Group).Select(x => x.Email);

//            return Json(exr, JsonRequestBehavior.AllowGet);

//        }
//    }

//}

