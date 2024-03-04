using Microsoft.AspNetCore.Authorization;
using Vezeeta.Data;
using Vezeeta.Models;
//using Vezeeta.ViewModels;

namespace Vezeeta.RepoServices
{
    public class DoctorRepoService : IDoctorRepository
    {
        public ApplicationDbContext Context { get; }
        public DoctorRepoService(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<AppUser> GetAllDoctor()
        {
            return Context.Users.ToList();
        }
		public AppUser GetDoctorDetails(string id)
		{
			return Context.Users.Find(id);
		}
		public void InsertDoctor(AppUser std)
		{
			if (std != null)
			{
				Context.Users.Add(std);
				Context.SaveChanges();
			}
		}
		public void UpdateDoctor(string id, AppUser std)
		{
			AppUser UpdateDoc = Context.Users.Find(id);
			if (UpdateDoc != null)
			{
				Context.Users.Update(std);
				Context.SaveChanges();
			}
		}
		public void DeleteDoctor(string id)
		{
			Context.Users.Remove(Context.Users.Find(id));
			Context.SaveChanges();
		}
	}
}
