using Microsoft.EntityFrameworkCore;
using Vezeeta.Data;
using Vezeeta.Models;

namespace Vezeeta.RepoServices
{
    public class AppointmentsRepoService : IAppointmentsRepository
    {
        public string Id { get; set; }
        public ApplicationDbContext Context { get; set; }
        public AppointmentsRepoService(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<Appointments> GetAllAppointments(string id) // doctor id filter
        {
            //return Context.Appointments.Include(r => r.Patient)
            //                     .Include(r => r.Doctor)
            //                     .Where(r => r.DoctorId == id)
            //                     .ToList();

            var Appoints = Context.Appointments.Include(a=>a.Patient).Include(a => a.Doctor).Where(a => a.DoctorId == "0a753a31-8e7d-446d-b17e-130b24f3efa4").ToList();
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

        public void UpdateAppointment(int id, Appointments appointment)
        {
            throw new NotImplementedException();
        }
        public void DeleteAppointment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
