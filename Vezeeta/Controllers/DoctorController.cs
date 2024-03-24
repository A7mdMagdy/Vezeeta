using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Vezeeta.Models;
using Vezeeta.RepoServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Vezeeta.Controllers
{
    [Authorize(Roles="Doctor")]
    public class DoctorController : Controller
    {
        public IDoctorRepository DoctorRepo { get; }
        public string? id { get; set; }
        //private readonly IWebHostEnvironment HostingEnvironment;
        public DoctorController(IDoctorRepository doctorRepo, IHttpContextAccessor httpContextAccessor)
        {
            DoctorRepo = doctorRepo;
            this.id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //HostingEnvironment = hostingEnvironment;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorController/Details/5
        // <<<  Replaced  >>> public ActionResult Details(string id)
        public ActionResult Details()
        {
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
        // <<<  Replaced  >>> public ActionResult Edit(string id)
        public ActionResult Edit()
        {
            ViewBag.id = id;
            return View(DoctorRepo.GetDoctorDetails(id));
        }

        // POST: DoctorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // <<<  Replaced  >>> public ActionResult Edit(string id, AppUser doctor, IFormFile imageFile)
        public ActionResult Edit(AppUser doctor, IFormFile imageFile)
        {
            try
            {
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
