using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
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
  //      public List<AppUser> GetAllDoctor()
  //      {
  //          return Context.Users.Where(d=>d.Role == Role.Doctor).ToList();
  //      }
		//public AppUser GetDoctorDetails(string id)
		//{
		//	return Context.Users.Find(id);
		//}
		public List<AppUser> GetAllDoctor()
		{
            return Context.Users.Include(user => user.DoctorAppointments.Where(appointment => !appointment.Booked))
                                .Where(user => user.Role == Role.Doctor).ToList();
        }
		public AppUser GetDoctorDetails(string id)
		{
            return Context.Users.Include(user => user.DoctorAppointments.Where(appointment => !appointment.Booked))
                                .ThenInclude(appointment => appointment.Patient)
                                .Include(user => user.DoctorReviews)
                                .ThenInclude(review => review.Patient)
                                .Where(user => user.Id == id)
                                .FirstOrDefault();
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
            AppUser UpdateDoc = Context.Users.FirstOrDefault(d=>d.Id == id);
			if (UpdateDoc != null)
			{
                UpdateDoc.UserName = std.UserName;
                UpdateDoc.firstName = std.firstName;
                UpdateDoc.lastName = std.lastName;
                UpdateDoc.Email = std.Email;
                UpdateDoc.Address = std.Address;
                UpdateDoc.fees = std.fees;
                UpdateDoc.birthDate = std.birthDate;
                UpdateDoc.PhoneNumber = std.PhoneNumber;
                UpdateDoc.Gender = std.Gender;
                UpdateDoc.Image = std.Image;
                UpdateDoc.Specialization = std.Specialization;
                UpdateDoc.typeOfSpecialization = std.typeOfSpecialization;
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
