using Vezeeta.Models;
using Vezeeta.ViewModels;

namespace Vezeeta.RepoServices
{
    public interface IDoctorRepository
    {
        public List<AppUser> GetAllDoctor();
		public AppUser GetDoctorDetails(string id);
		public void InsertDoctor(AppUser std);
		public void UpdateDoctor(string id, AppUser std);
		public void DeleteDoctor(string id);
	}
}
