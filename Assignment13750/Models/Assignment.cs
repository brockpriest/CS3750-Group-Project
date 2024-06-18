using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment13750.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Classes")]
        public int ClassID { get; set; }
        public DateTime Duedate { get; set; }
        public int Maxpoints { get; set; }
        public bool SubmissionType { get; set; } // 1 for file 0 for text

        public Classes Classes { get; set; }
    }
}
