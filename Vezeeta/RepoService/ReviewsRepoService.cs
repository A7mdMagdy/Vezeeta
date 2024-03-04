using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Models;

namespace Vezeeta.RepoServices
{
    public class ReviewsRepoService : IReviewsRepository
    {
        public string Id { get; set; }
        public ApplicationDbContext Context { get; set; }
        public ReviewsRepoService(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<Reviews> GetAllReviews(string id)   // doctor id
        {
            string Id=id.ToString();
            return Context.Reviews.Include(r => r.Patient)
                                 .Include(r => r.Doctor)
                                 .Where(r => r.DoctorId == Id)
                                 .ToList();
        }
    }
}
