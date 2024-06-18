using Assignment13750.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Assignment13750.Data
{
    public class AssignmentRepository : IAssignmentManager
    {
        private readonly Assignment13750.Data.Assignment13750Context _context;
        public AssignmentRepository(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }
        public void Add(Assignment assignment)
        {
            _context.Assignment.Add(assignment);
            _context.SaveChanges();
        }

        public void DeleteByClassID(int id)
        {
            List<Assignment> DelList = (from Assignment in _context.Assignment where id == Assignment.ClassID select Assignment).ToList();
            foreach (var assignment in DelList) 
            { 
                _context.Assignment.Remove(assignment);
            }
            
            _context.SaveChanges();
        }

        public List<Assignment> GetAssignmentByClassId(int id)
        {
            List<Assignment> result =(from Assignment in _context.Assignment where id == Assignment.ClassID select Assignment).ToList();
            return result;
        }

        public void Update(Assignment assignment)
        {
            _context.ChangeTracker.Clear();
            _context.Entry(assignment).State = EntityState.Modified;

            _context.Assignment.Update(assignment);
            _context.SaveChanges();
        }

        //ToDo List for student
        public List<Assignment> GetAssignmentsByStudentId(int studentId)
        {
            DateTime currentDate = DateTime.Now;
            List<Assignment> result = (
            from a in _context.Assignment
            where _context.Enrollment.Any(e => e.StudentID == studentId && e.ClassID == a.ClassID)
          && a.Duedate > currentDate
            orderby a.Duedate
            select a
            ).Take(5).ToList();

            foreach (var assignment in result)
            {
                _context.Entry(assignment).Reference(a => a.Classes).Load();
            }

            return result;
        }
        public Assignment GetAssignmentById(int id)
        {
            return _context.Assignment.FirstOrDefault(c => c.Id == id);
		}
        public void Delete(int id)
        {
            _context.Assignment.Remove(_context.Assignment.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }
        
    }
}


