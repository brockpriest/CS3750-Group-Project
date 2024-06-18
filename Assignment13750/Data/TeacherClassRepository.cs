using Assignment13750.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Assignment13750.Data
{
    public class TeacherClassRepository : ITeacherClassManager
    {
        private readonly Assignment13750.Data.Assignment13750Context _context;

        public TeacherClassRepository(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }
        public void Add(TeacherClasses teacher)
        {
            _context.TeachersClasses.Add(teacher);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.TeachersClasses.Remove(_context.TeachersClasses.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }

        public List<TeacherClasses> GetAllTeacherClasses()
        {
            return _context.TeachersClasses.ToList();
        }

        public List <TeacherClasses> GetClassesByTeacherID(int id)
        {
            return _context.TeachersClasses.Where(f => f.TeacherID == id).ToList();
        }

        public void Update(TeacherClasses teacher)
        {
            throw new NotImplementedException();
        }
        public TeacherClasses GetByClassID (int id)
        {
            return _context.TeachersClasses.FirstOrDefault(x => x.ClassID == id);
        }
    }
}
