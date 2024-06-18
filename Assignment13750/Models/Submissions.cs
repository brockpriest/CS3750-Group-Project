namespace Assignment13750.Models
{
    public class Submissions
    {
        public int Id { get; set; }
        public int ClassID { get; set; } //Classes.id of class
        public int StudentID { get; set; }  // The teachers ID taken from the Credentials table
        public int AssignmentID { get; set; } 
        public bool SubmissionType { get; set; }   // submission type foriegn key to assignment table
        public string Submitted { get; set; } // whatever was submitted, wether file or text
        public DateTime SubmissionDate { get; set; }
        public int Grade { get; set; }
        public string? Comments { get; set; }
    }
}
