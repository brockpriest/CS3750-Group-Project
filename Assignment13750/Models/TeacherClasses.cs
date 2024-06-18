namespace Assignment13750.Models
{
    public class TeacherClasses
    {
        public int Id { get; set; }
        public int ClassID { get; set; } //Classes.id of class
        public int TeacherID { get; set; }  // The teachers ID taken from the Credentials table
    }
}
