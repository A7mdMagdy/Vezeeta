using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Vezeeta.Models;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class ResearchResultController : Controller
    {
        public IDoctorRepository DoctorRepo { get; }
        public IReviewsRepository ReviewRepo { get; }
        public IAppointmentsRepository AppointRepo { get; }
        

        public ResearchResultController(IDoctorRepository doctorRepo, IReviewsRepository reviewRepo, IAppointmentsRepository appointRepo)
        {
            DoctorRepo  = doctorRepo;
            ReviewRepo  = reviewRepo;
            AppointRepo = appointRepo;
        }
        // GET: ResearchResultController
        public ActionResult Index(string sortOrder)
        {
            var doctors = sort(sortOrder);
            return View(doctors);
        }
        private List<AppUser> sort(string sortOrder)
        {
            var doctors = DoctorRepo.GetAllDoctor();
            switch (sortOrder)
            {
                case "LowToHigh":
                    doctors = doctors.OrderBy(d => d.fees).ToList();
                    break;
                case "HighToLow":
                    doctors = doctors.OrderByDescending(d => d.fees).ToList();
                    break;
                default:
                    doctors = DoctorRepo.GetAllDoctor().ToList();
                    break;
            }

            ViewBag.SortOrder = sortOrder;
            return doctors;
        }
        // GET: ResearchResultController/Details/5
        public ActionResult Details(string id)
        {
            return View(DoctorRepo.GetDoctorDetails(id));
        }

        // GET: ResearchResultController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResearchResultController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResearchResultController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResearchResultController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResearchResultController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResearchResultController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
