using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment13750.Models;

namespace Assignment13750.Data
{
    public class Assignment13750Context : DbContext
    {
        public Assignment13750Context (DbContextOptions<Assignment13750Context> options)
            : base(options)
        {
        }

        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<TeacherClasses> TeachersClasses { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Submissions> Submissions { get; set; }
		public DbSet <PaymentStatus> PaymentStatus { get; set; }
	}
}
