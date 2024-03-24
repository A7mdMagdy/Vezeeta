using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Security.Claims;
using Vezeeta.Data;
using Vezeeta.Models;
using Vezeeta.ViewModels;

namespace Vezeeta.Controllers
{
    public class pateintController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> userManager;

        public ApplicationDbContext Context { get; }

        public pateintController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            this.userManager = userManager;
            Context = context;
        }
        //public pateintController(ApplicationDbContext context)
        //{
        //    Context = context;
        //}

        // <<<<<<<<<  Patient Appointments  >>>>>>>>>>>>>
        [Authorize(Roles ="Patient")]
        public ActionResult Appointments()
        {
            var patientappointments = Context.Appointments.Include(a=>a.Patient).Include(a=>a.Doctor).ToList();
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
        public async Task<ActionResult> Book(BookInfo bookInfo)
        {

            var user = await userManager.GetUserAsync(User);
            var domain = "http://localhost:7024/";
            //var product = products.Find(p => p.Id == id);
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 100,
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = bookInfo.Name,
                        Description = bookInfo.Id+" | "+bookInfo.Name+ " | " + bookInfo.Email+ " | " + bookInfo.Address+ " | " + bookInfo.phoneNumber+ " | " + bookInfo.Gender
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
                var paymentdata = Session.LineItems;
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
