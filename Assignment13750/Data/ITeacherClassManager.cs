using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface ITeacherClassManager
    {
        public void Add(TeacherClasses teacher);
        public void Update(TeacherClasses teacher);
        public void Delete(int id);
        public List<TeacherClasses> GetClassesByTeacherID(int id);
        public List<TeacherClasses> GetAllTeacherClasses();
        public TeacherClasses GetByClassID(int id);


    }
}
