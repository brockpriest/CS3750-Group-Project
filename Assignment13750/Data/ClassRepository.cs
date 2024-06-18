using Assignment13750.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net;
using Topshelf;

namespace Assignment13750.Data
{
    public class ClassRepository : IClassManager
    {

        private readonly Assignment13750.Data.Assignment13750Context _context;

        public ClassRepository(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }
        public void Add(Classes classes)
        {
            _context.Classes.Add(classes);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Classes.Remove(_context.Classes.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
        }

        public List<Classes> GetAllClasses()
        {
            return _context.Classes.ToList();
        }

        public Classes GetClassById(int id)
        {
            return _context.Classes.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Classes classes)
        {
            _context.Entry(classes).State = EntityState.Modified;
      
                _context.Classes.Update(classes);
                _context.SaveChanges();
            
        }
    }
}
