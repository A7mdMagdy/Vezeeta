using Vezeeta.Models;

namespace Vezeeta.RepoServices
{
    public interface IAppointmentsRepository
    {
        public List<Appointments> GetAllAppointments(string id); // doctor id filter
        public Appointments GetAppointmentDetails(int id);
        public void InsertAppointment(Appointments appointment);
        public void UpdateAppointment(int id, Appointments appointment);
        public void DeleteAppointment(int id);
    }
}
