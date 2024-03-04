using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vezeeta.Models
{
    public class Reviews
    {
        public int ID { get; set; }
        //[Required(ErrorMessage ="Enter the review please!"),MinLength(10,ErrorMessage ="Minimum length should be more than or equal 10 characters")]
        public string Review { get; set; }
        //[Required(ErrorMessage ="please add your rate")]
        public int Rate { get; set; }
        [ForeignKey("DoctorId")]
        public string DoctorId { get; set; }
        public virtual AppUser? Doctor { get; set; }

        [ForeignKey("PatientId")]
        public string PatientId { get; set; }
        public virtual AppUser Patient { get; set; }
    }
}
