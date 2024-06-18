using Assignment13750.Models;

namespace Assignment13750.Data
{
    public class EnrollmentRepository : IEnrollmentManager
    {
        private readonly Assignment13750.Data.Assignment13750Context _context;

        public EnrollmentRepository(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }


        public void Add(Enrollment Enrollment)
        {
            _context.Enrollment.Add(Enrollment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Enrollment.Remove(_context.Enrollment.Find(id));
            _context.SaveChanges();
        }

        public List<Enrollment> GetAllStudentClasses()
        {
            return _context.Enrollment.ToList();
        }


        public List<Enrollment> GetClassesByStudentID(int id)
        {
            return _context.Enrollment.Where(f => f.StudentID == id).ToList();
        }


        public void Update(Enrollment Enrollment)
        {
            throw new NotImplementedException();
        }
    }
}
