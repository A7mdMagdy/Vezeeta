using System.ComponentModel.DataAnnotations;
using Vezeeta.Models;

namespace Vezeeta.ViewModels
{
    public class PatientViewModel
    {
        //public string Id { get; set; }
        
        [Required(ErrorMessage = "You have to enter first name!"), MaxLength(50, ErrorMessage = "first name max length is 50"), MinLength(3, ErrorMessage = "first name min length is 3")]
        public string firstName { get; set; }
        
        [Required(ErrorMessage = "You have to enter last name!"), MaxLength(50, ErrorMessage = "last name max length is 50"), MinLength(3, ErrorMessage = "last name min length is 3")]
        public string lastName { get; set; }
        
        public string Email { get; set; }

        [Required(ErrorMessage = "You have to choose your bitrhdate!"), DataType(DataType.DateTime)]
        public DateTime birthDate { get; set; }
        
        [Required(ErrorMessage = "You have to enter Your Address!")]
        public string Address { get; set; }
        
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
        
        [EnumDataType(typeof(Gender))]
        
        public Gender Gender { get; set; }
        
        public string? Image { get; set; }
    }
}
