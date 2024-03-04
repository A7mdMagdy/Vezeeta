using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Vezeeta.Models;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class DoctorController : Controller
    {
        public IDoctorRepository DoctorRepo { get; }
        //private readonly IWebHostEnvironment HostingEnvironment;
        public DoctorController(IDoctorRepository doctorRepo)
        {
            DoctorRepo = doctorRepo;
            //HostingEnvironment = hostingEnvironment;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(string id)
        {
            id = User.Identity.Name;
            ViewBag.id = id;
            return View(DoctorRepo.GetDoctorDetails(id));
        }

        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorController/Create
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

        // GET: DoctorController/Edit/5
        public ActionResult Edit(string id)
        {
            id = User.Identity.Name;
            ViewBag.id = id;
            return View(DoctorRepo.GetDoctorDetails(id));
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, AppUser doctor, IFormFile imageFile)
        {
            try
            {
                id = User.Identity.Name;
                DoctorRepo.UpdateDoctor(id, doctor);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorController/Delete/5
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
