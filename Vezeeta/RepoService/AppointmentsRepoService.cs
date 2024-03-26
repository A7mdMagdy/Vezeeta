using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vezeeta.Data;
using Vezeeta.Models;

namespace Vezeeta.RepoServices
{
    public class AppointmentsRepoService : IAppointmentsRepository
    {
        public string Id { get; set; }
        public ApplicationDbContext Context { get; set; }
        public AppointmentsRepoService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            this.Id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public List<Appointments> GetAllAppointments(string id) // doctor id filter
        {
            //return Context.Appointments.Include(r => r.Patient)
            //                     .Include(r => r.Doctor)
            //                     .Where(r => r.DoctorId == id)
            //                     .ToList();
            var Appoints = Context.Appointments.Include(a=>a.Patient).Include(a => a.Doctor).Where(a => a.DoctorId == this.Id).ToList();
            return Appoints;
        }

        public List<Appointments> GetAllAppointments()
        {

            var Appoints = Context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList();
            return Appoints;
        }

        public Appointments GetAppointmentDetails(int id)
        {
            return Context.Appointments.Find(id);
        }

        public void InsertAppointment(Appointments appointment)
        {
            if (appointment != null)
            {
                Context.Appointments.Add(appointment);
                Context.SaveChanges();
            }
        }

        public void UpdateAppointment(Appointments appointment)
        {
            if (appointment!=null)
            {
                Context.Appointments.Update(appointment);
                Context.SaveChanges();
            }
        }
        public void DeleteAppointment(int id)
        {
			var Appoint = Context.Appointments.FirstOrDefault(a=>a.Id == id);
            if(Appoint != null)
            {
                Context.Appointments.Remove(Appoint);
                Context.SaveChanges();
            } 
		}
	}
}
