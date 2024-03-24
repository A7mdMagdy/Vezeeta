using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.Models;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class AppointmentsController : Controller
    {
        public IAppointmentsRepository AppointmentsRepo { get; }
        public string? id { get; set; }
        public AppointmentsController(IAppointmentsRepository appointmentRepo, IHttpContextAccessor httpContextAccessor)
        {
            AppointmentsRepo = appointmentRepo;
            this.id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        // GET: AppointmentsController
        // <<<  Replaced  >>> public ActionResult Index(string id)
        public ActionResult Index()
        {
            ViewBag.id = id;
            var appointments = AppointmentsRepo.GetAllAppointments(id);
            return View(appointments);
        }

        // GET: AppointmentsController/Create
        // <<<  Replaced  >>> public ActionResult Create(string id)
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.id = id;
            return View();
        }
        // POST: AppointmentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Appointments appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppointmentsRepo.InsertAppointment(appointment);
                    return RedirectToAction("Index", new { id =appointment.DoctorId });
                }
                catch
                {
                    return RedirectToAction("Index", new { id = appointment.DoctorId });
                    //return RedirectToAction("Index", new { id = 3 });
                }
            }
            else
                return View();

            //return View();
        }

    }
}
