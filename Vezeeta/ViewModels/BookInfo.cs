using System.ComponentModel.DataAnnotations;
using Vezeeta.Models;

namespace Vezeeta.ViewModels
{
    public class BookInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string phoneNumber { get; set; }
        public Gender Gender { get; set; }

    }
}
