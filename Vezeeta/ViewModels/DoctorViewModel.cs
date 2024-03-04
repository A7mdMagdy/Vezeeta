using System.ComponentModel.DataAnnotations;
using Vezeeta.Models;
namespace Vezeeta.ViewModels
{
    public class DoctorViewModel
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "You have to enter first name!"), MaxLength(50, ErrorMessage = "first name max length is 50"), MinLength(3, ErrorMessage = "first name min length is 3")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "You have to enter last name!"), MaxLength(50, ErrorMessage = "last name max length is 50"), MinLength(3, ErrorMessage = "last name min length is 3")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "You have to choose your bitrhdate!"), DataType(DataType.DateTime)]
        public DateTime birthDate { get; set; }
        [Required(ErrorMessage = "You have to enter Your Address!")]
        public string Address { get; set; }
        public Role Role { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "You have to enter Your Specialization!")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "You have to enter Your type of Specialization!")]
        public string typeOfSpecialization { get; set; }
        public int fees { get; set; }
        [Required(ErrorMessage = "You have to enter Your Image!")]
        public string Image { get; set; }

        public virtual List<Appointments>? DoctorAppointments { get; set; }
        public virtual List<Appointments>? PatientAppointments { get; set; }

        public virtual List<Reviews>? DoctorReviews { get; set; }
        public virtual List<Reviews>? PatientReviews { get; set; }
    }
}
