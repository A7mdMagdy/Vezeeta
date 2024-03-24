using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: ReviewsController
        public IReviewsRepository ReviewRepo { get; }
        public IDoctorRepository DoctorRepo { get; }
        public string? id { get; set; }
        public ReviewsController(IReviewsRepository reviewRepo, IDoctorRepository doctorRepo, IHttpContextAccessor httpContextAccessor)
        {
            ReviewRepo = reviewRepo;
            DoctorRepo = doctorRepo;
            this.id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // <<<  Replaced  >>> public ActionResult Index(string id) Doctor Id
        public ActionResult Index()    
        {
            ViewBag.id = id;
            ViewBag.doctor=DoctorRepo.GetDoctorDetails(id);
            var Review = ReviewRepo.GetAllReviews(DoctorRepo.GetDoctorDetails(id).Id);
            return View(Review);
        }

    }
}
