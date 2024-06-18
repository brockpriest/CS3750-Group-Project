
using Assignment13750.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment13750.Data
{
	public class CredRepository : ICredentialManager
	{
		private readonly Assignment13750.Data.Assignment13750Context _context;

		public CredRepository(Assignment13750.Data.Assignment13750Context context)
		{
			_context = context;
		}

		public void Add(Credentials credentials)
		{
			_context.Credentials.Add(credentials);
            _context.SaveChanges();
        }

		public void Delete(int id)
		{
            //_context.Credentials.Remove(id);
            //_context.SaveChanges();
        }

		public List<Credentials> GetAllCredentials()
		{
			return _context.Credentials.ToList();
		}

		public Credentials GetCredById(int ID)
		{
			
			return _context.Credentials.FirstOrDefault(x => x.ID == ID);
            
        }
		public void Update(Credentials credentials)
		{
			_context.ChangeTracker.Clear();
			
            _context.Credentials.Update(credentials);
            _context.SaveChanges();
        }
		public void DropClassTuition(Credentials credentials) // subtracts 100 from tuition if there is some to take
		{
			if (credentials.TuitionBalance>=100)
			{
				credentials.TuitionBalance = credentials.TuitionBalance - 100;
			}
			else
			{
				return;
			}
			_context.Entry(credentials).State = EntityState.Modified;
			_context.Credentials.Update(credentials);
			_context.SaveChanges();
		}
		public void AddClassTuition(Credentials credentials) // Adds 100 to tuition
		{
			_context.ChangeTracker.Clear();
			credentials.TuitionBalance = credentials.TuitionBalance + 100;
			_context.Entry(credentials).State = EntityState.Modified;
			_context.Credentials.Update(credentials);
			_context.SaveChanges();
		}

	}
}
