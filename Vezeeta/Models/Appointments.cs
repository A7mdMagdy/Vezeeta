using System.ComponentModel.DataAnnotations.Schema;

namespace Vezeeta.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public bool Booked { get; set; }
        public bool isPaid { get; set; }
        public int Fees { get; set; }

        [ForeignKey("DoctorId")]
        public string DoctorId { get; set; }
        public virtual AppUser Doctor { get; set; }

        [ForeignKey("PatientId")]
        public string? PatientId { get; set; }
        public virtual AppUser? Patient { get; set; }
    }
}
