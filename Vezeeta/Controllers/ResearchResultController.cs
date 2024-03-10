using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Numerics;
using Vezeeta.Models;
using Vezeeta.RepoServices;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    public class ResearchResultController : Controller
    {
        public IDoctorRepository DoctorRepo { get; }
        public IReviewsRepository ReviewRepo { get; }
        public IAppointmentsRepository AppointmentRepo { get; }
        public ResearchResultController(IDoctorRepository doctorRepo, IReviewsRepository reviewRepo, IAppointmentsRepository appointmentRepo)
        {
            DoctorRepo = doctorRepo;
            ReviewRepo = reviewRepo;
            AppointmentRepo = appointmentRepo;
        }
        public ActionResult Index(string sortOrder, string filterGender, string filterFees, string filterspecialist)
        {
            ViewBag.Appointment = AppointmentRepo.GetAllAppointments();
            var doctors = DoctorRepo.GetAllDoctor();

            if (!string.IsNullOrEmpty(sortOrder))
            {
                doctors = sort(sortOrder);
            }
            else if (!string.IsNullOrEmpty(filterGender))
            {
                doctors = filterByGender(filterGender);
            }
            else if (!string.IsNullOrEmpty(filterFees))
            {
                doctors = filterByFees(filterFees);
            }
            else if (!string.IsNullOrEmpty(filterspecialist))
            {
                doctors = filterBySpecialist(filterspecialist);
            }

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
        private List<AppUser> filterByGender(string filterGender)
        {
            var doctors = DoctorRepo.GetAllDoctor();
            switch (filterGender)
            {
                case "Male":
                    doctors = doctors.Where(m => m.Gender == Gender.Male).ToList();
                    break;
                case "Female":
                    doctors = doctors.Where(m => m.Gender == Gender.Female).ToList();
                    break;
                default:
                    break;
            }
            return doctors;
        }
        private List<AppUser> filterByFees(string filterFees)
        {
            var doctors = DoctorRepo.GetAllDoctor();
            switch (filterFees)
            {
                case "Lessthan50":
                    doctors = doctors.Where(m => m.fees < 50).ToList();
                    break;
                case "From100to200":
                    doctors = doctors.Where(m => m.fees >= 100 && m.fees <= 200).ToList();
                    break;
                case "From200to300":
                    doctors = doctors.Where(m => m.fees >= 200 && m.fees <= 300).ToList();
                    break;
                case "Greaterthan500":
                    doctors = doctors.Where(m => m.fees >= 500).ToList();
                    break;
                default:
                    break;
            }
            return doctors;
        }
        private List<AppUser> filterBySpecialist(string filterFees)
        {
            var doctors = DoctorRepo.GetAllDoctor();
            switch (filterFees)
            {
                case "Dermatology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Dermatology").ToList();
                    break;
                case "Dentistry":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Dentistry").ToList();
                    break;
                case "Psychiatry":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Psychiatry").ToList();
                    break;
                case "Pediatrics and New Born":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Pediatrics and New Born").ToList();
                    break;
                case "Neurology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Neurology").ToList();
                    break;
                case "Orthopedics":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Orthopedics").ToList();
                    break;
                case "Gynaecology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Gynaecology").ToList();
                    break;
                case "Ear, Nose and Throat":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Ear, Nose and Throat").ToList();
                    break;
                case "Allergy and Immunology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Allergy and Immunology").ToList();
                    break;
                case "udiology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "udiology").ToList();
                    break;
                case "Chest and Respiratory":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Chest and Respiratory").ToList();
                    break;
                case "Diabetes and Endocrinology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Diabetes and Endocrinology").ToList();
                    break;
                case "Diagnostic Radiology":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Diagnostic Radiology").ToList();
                    break;
                case "Dietitian and Nutrition":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Dietitian and Nutrition").ToList();
                    break;
                case "Family Medicine":
                    doctors = doctors.Where(m => m.typeOfSpecialization == "Family Medicine").ToList();
                    break;

                default:
                    break;
            }
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
