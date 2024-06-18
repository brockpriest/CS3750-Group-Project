using Assignment13750.Models;

namespace Assignment13750.Data
{
    public interface ISubmissionManager
    {
        public void Add(Submissions submissions);
        public void Update(Submissions submissions);
		public void UpdateGrade(Submissions submissions);
		public void Delete(int id);
        public List<Submissions> GetSubmissionsByStudIDandClassID(int StudID, int ClassID, int AssignID);
        List<Submissions> GetSubmissionsForAssignment(int assignmentId);
        Submissions GetSubmissionById(int id);
        public List<Submissions> GetSubmissionsByGradeAndAssignment(int Grade, int AssignID);
    }
}
