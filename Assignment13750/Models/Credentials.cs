using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Assignment13750.Models
{
    public class Credentials
    {
        
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime BirthDate { get; set; }
       
        [Required]
        public Boolean IsTeacher { get; set; }
        
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Link1 { get; set; }
        public string? Link2 { get; set; }
        public string? Link3 { get; set; }

        public string? ProfilePic { get; set; }
        public int? TuitionBalance { get; set; }
    }
}
