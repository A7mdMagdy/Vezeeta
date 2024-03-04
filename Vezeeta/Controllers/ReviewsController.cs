using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.RepoServices;

namespace Vezeeta.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: ReviewsController
        public IReviewsRepository ReviewRepo { get; }
        public IDoctorRepository DoctorRepo { get; }
        public ReviewsController(IReviewsRepository reviewRepo, IDoctorRepository doctorRepo)
        {
            ReviewRepo = reviewRepo;
            DoctorRepo = doctorRepo;
        }
        public ActionResult Index(string id)    // doctor id
        {
            id = User.Identity.Name;
            ViewBag.id = id;
            ViewBag.doctor=DoctorRepo.GetDoctorDetails(id);
            var Review = ReviewRepo.GetAllReviews(DoctorRepo.GetDoctorDetails(id).Id);
            return View(Review);
        }

        // GET: ReviewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewsController/Create
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

        // GET: ReviewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReviewsController/Edit/5
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

        // GET: ReviewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewsController/Delete/5
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
