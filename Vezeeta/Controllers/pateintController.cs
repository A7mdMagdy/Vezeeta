using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Security.Claims;
using Vezeeta.Data;
using Vezeeta.Models;
using Vezeeta.RepoServices;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    public class pateintController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> userManager;
        public ApplicationDbContext Context { get; }
        public IAppointmentsRepository AppointmentsRepo { get; }
        public string PatientId { get; set; }
        public pateintController(IHttpContextAccessor httpContextAccessor, IAppointmentsRepository appointmentRepo, SignInManager<AppUser> signInManager,UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            AppointmentsRepo = appointmentRepo;
            _signInManager = signInManager;
            this.userManager = userManager;
            Context = context;
            this.PatientId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
     
        // <<<<<<<<<  Patient Appointments  >>>>>>>>>>>>>
        [Authorize(Roles ="Patient")]
        public ActionResult Appointments()
        {
            var patientappointments = Context.Appointments.Include(a=>a.Patient).Include(a=>a.Doctor).Where(u=> u.isPaid).ToList();
            return View(patientappointments);
        }

        public string Currentdoc { get; set; }

        public void OnGet(string Currentdoc)
        {
            this.Currentdoc = Currentdoc;
        }

        // <<<<<<<<<  Book Page  >>>>>>>>>>>>>
        [HttpGet]
        public async Task<ActionResult> Book(string Currentdoc, string Appo)
        {
            var doctor = await userManager.FindByIdAsync(Currentdoc);
            ViewBag.currentDoctor = doctor;
            ViewBag.Appo = Appo;
            var patient = await userManager.FindByEmailAsync(User.Identity.Name);
            return View(patient);
        }


        // <<<<<<<<<  Stripe Page  >>>>>>>>>>>>>
        [HttpPost]
        public async Task<ActionResult> Book(IFormCollection bookInfo, string DocId)
        {
            var doctor = Context.Users.FirstOrDefault(u=>u.Id == DocId);
            var user = await userManager.GetUserAsync(User);
            var domain = "http://localhost:7024/";
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 100,
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = bookInfo["firstName"] +" "+ bookInfo["lastName"],
                        Description = bookInfo["Email"]+ " | " + bookInfo["Address"]+ " | " + bookInfo["PhoneNumber"]
                    }
                },
                Quantity = 1,
            };
            var option = new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:7024/pateint/Success",
                CancelUrl = "https://localhost:7024/pateint/Failed",
                LineItems = new List<SessionLineItemOptions>
                {
                    sessionListItem
                },
                Mode = "payment",
                
                ClientReferenceId = DocId,
            };
            // creating object of stripe with needed settings (options)
            Session session = new SessionService().Create(option);
            TempData["session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        // <<<<<<<<<  Success Page  >>>>>>>>>>>>>
        //[HttpPost]
        public ActionResult Success()
        {
            var Session = new SessionService().Get(TempData["session"].ToString());
            if (Session.PaymentStatus == "paid")
            {
                var appointment = Context.Appointments.FirstOrDefault(appoint => appoint.DoctorId == Session.ClientReferenceId);
                appointment.isPaid = true;
                appointment.Booked = true;
                appointment.PatientId = this.PatientId;
                AppointmentsRepo.UpdateAppointment(appointment);
                ViewBag.data = Session;
                return View();
            }
            return View("Failed");
        }
        //[HttpGet]
        // <<<<<<<<<  Failed Page  >>>>>>>>>>>>>
        public ActionResult Failed()
        {
            return View();
        }


    }
}
