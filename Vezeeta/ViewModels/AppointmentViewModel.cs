using System.ComponentModel.DataAnnotations;

namespace Vezeeta.ViewModels
{
    public class AppointmentViewModel
    {
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public int Fees { get; set; }
        public string DoctorId { get; set; }
    }
}
