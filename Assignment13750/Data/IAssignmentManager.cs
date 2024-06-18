using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface IAssignmentManager
    {
        public void Add(Assignment assignment);
        public void Update(Assignment assignment);
        public void Delete(int id);
        public List<Assignment> GetAssignmentByClassId(int id);
        public List<Assignment> GetAssignmentsByStudentId(int studentId);
        public Assignment GetAssignmentById(int id);
        public void DeleteByClassID(int id);

    }
}
