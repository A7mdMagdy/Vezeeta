using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.Models;
using Vezeeta.RepoServices;
using Vezeeta.ViewModels;

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
        public ActionResult Create(AppointmentViewModel _appointment)
        {
            Appointments appointment = new Appointments();
            appointment.Date = _appointment.Date;
            appointment.Time = _appointment.Time;
            appointment.Fees = _appointment.Fees;
            appointment.DoctorId = _appointment.DoctorId;
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AppointmentsRepo.DeleteAppointment(id);
            return RedirectToAction("Index", new { id = this.id });
        }

    }
}
