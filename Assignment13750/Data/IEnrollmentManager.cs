using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface IEnrollmentManager
    {
        public void Add(Enrollment Enrollment);
        public void Update(Enrollment Enrollment);
        public void Delete(int id);
        public List<Enrollment> GetClassesByStudentID(int id);
        public List<Enrollment> GetAllStudentClasses();

    }
}
