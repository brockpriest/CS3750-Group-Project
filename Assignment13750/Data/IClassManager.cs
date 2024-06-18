using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface IClassManager
    {

        public void Add(Classes classes);
        public void Update(Classes classes);
        public void Delete(int id);
        public Classes GetClassById(int id);
        public List<Classes> GetAllClasses();
    }
}
