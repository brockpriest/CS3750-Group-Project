using Assignment13750.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment13750.Data
{
	public class SubmissionRepository : ISubmissionManager
	{
		private readonly Assignment13750.Data.Assignment13750Context _context;

		public SubmissionRepository(Assignment13750.Data.Assignment13750Context context)
		{
			_context = context;
		}
		public void Add(Submissions submissions)
		{
			_context.Submissions.Add(submissions);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public List<Submissions> GetSubmissionsByStudIDandClassID(int StudID, int ClassID, int AssignID)
		{
			List<Submissions> result = (from Submissions in _context.Submissions where StudID == Submissions.StudentID && ClassID == Submissions.ClassID && AssignID == Submissions.AssignmentID select Submissions).ToList();
			return result;
		}

		public void Update(Submissions submissions)
		{
            _context.ChangeTracker.Clear();
			//_context.Submissions.Remove((from Submissions in _context.Submissions where submissions.StudentID == Submissions.StudentID && submissions.ClassID == Submissions.ClassID && submissions.AssignmentID == Submissions.AssignmentID select Submissions).First());
            _context.Entry(submissions).State = EntityState.Modified;
            _context.Submissions.Update(submissions);
			_context.SaveChanges();
		}
        public List<Submissions> GetSubmissionsForAssignment(int assignmentId)
        {
            return _context.Submissions
                .Where(s => s.AssignmentID == assignmentId)
                .ToList();
        }
        public Submissions GetSubmissionById(int id)
        {
            return _context.Submissions.FirstOrDefault(d => d.Id == id);
        }
		public void UpdateGrade(Submissions submissions)
		{
			_context.Submissions.Update(submissions);
			_context.SaveChanges();
		}
		public List<Submissions> GetSubmissionsByGradeAndAssignment(int Grade, int AssignID)
		{
            return _context.Submissions
               .Where(s => s.Grade == Grade && s.AssignmentID == AssignID)
               .ToList();
        }
	}
}
