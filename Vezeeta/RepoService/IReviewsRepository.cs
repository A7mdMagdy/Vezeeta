using Vezeeta.Models;

namespace Vezeeta.RepoServices
{
    public interface IReviewsRepository
    {
        public List<Reviews> GetAllReviews(string id);  // doctor id
    }
}
