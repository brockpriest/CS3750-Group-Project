using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Assignment13750.Pages
{
    public class CoursesModel : PageModel
    {

        public List<TeacherClasses> TeacherClasses { get; set; }
        public List<Classes> TeachesClasses = new List<Classes> { };
        public int ClassCount;
        public List<Assignment> Assignments { get; set; }
        private IClassManager credrepo;
        private ITeacherClassManager credrepoteach;
        private IAssignmentManager assignmentRepository;
        public CoursesModel(IClassManager classManager, ITeacherClassManager teacherManager, IAssignmentManager assignmentManager)
        {
            this.credrepo = classManager;
            credrepoteach = teacherManager;
            assignmentRepository = assignmentManager;
        }
        public void refreshClassList()
        {
            TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            ClassCount = TeacherClasses.Count();
            Assignments = new List<Assignment>();

            for (int i = 0; i < ClassCount; i++)
            {
                TeachesClasses.Add(credrepo.GetClassById(TeacherClasses[i].ClassID));

                // Fetch assignments associated with this class and add them to the Assignments list
                var classAssignments = assignmentRepository.GetAssignmentByClassId(TeacherClasses[i].ClassID);
                Assignments.AddRange(classAssignments);
            }
        }
        
        public void OnGet()
        {
            refreshClassList();
        }
    }
}
