using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Models;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class AppointmentsController : Controller
    {
        public IAppointmentsRepository AppointmentsRepo { get; }
        public AppointmentsController(IAppointmentsRepository appointmentRepo)
        {
            AppointmentsRepo = appointmentRepo;
        }
        // GET: AppointmentsController
        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            var appointments = AppointmentsRepo.GetAllAppointments(id);
            return View(appointments);
        }

        // GET: AppointmentsController/Create
        [HttpGet]
        public ActionResult Create(string id)
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

        // GET: AppointmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Edit/5
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

        // GET: AppointmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Delete/5
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
