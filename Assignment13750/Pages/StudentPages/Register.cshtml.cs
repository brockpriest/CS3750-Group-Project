using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment13750.Data;
using Assignment13750.Models;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment13750.Data;
using Assignment13750.Models;
using System.Security.Claims;
using Assignment13750.Pages.TeacherPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment13750.Pages.StudentPages
{
    public class IndexModel : PageModel
    {
        private IClassManager classRepo;
        private IEnrollmentManager enrollRepo;
        private ITeacherClassManager teacherClassRepo;
        private ICredentialManager credentialRepo;

        public List<Classes> SHOWNCLASSES;
        public List<Classes> ALLCLASSES;
        public List<Enrollment> ENROLLMENTS;
        public List<TeacherClasses> TEACHERCLASSES;
        
        public IndexModel(IClassManager _classmanager, IEnrollmentManager _enrollmanager, ITeacherClassManager _teacherManager, ICredentialManager _credentialManager)
        {
            classRepo = _classmanager; //this is for class table
            enrollRepo = _enrollmanager; // this is for enrollment table
            teacherClassRepo = _teacherManager;
            credentialRepo = _credentialManager;

            ALLCLASSES = classRepo.GetAllClasses();
            SHOWNCLASSES = classRepo.GetAllClasses();
            ENROLLMENTS = enrollRepo.GetAllStudentClasses();
            TEACHERCLASSES = teacherClassRepo.GetAllTeacherClasses();

            //credentialRepo.GetCredById(1).
        }

        public String GetTeacherByClassID(int classID)
        {

            int teacherID = -1;

            for(int i = 0; i < TEACHERCLASSES.Count; i++)
            {
                if (TEACHERCLASSES[i].ClassID == classID)
                {
                    teacherID = TEACHERCLASSES[i].TeacherID; 
                    break;
                }
            }

            return credentialRepo.GetCredById(teacherID).FirstName + " " +
                   credentialRepo.GetCredById(teacherID).LastName;
        }

        public bool DoesStudentTakeClass(int classID)
        {
            int studentID = int.Parse(HttpContext.User.Identity.Name);

            return ENROLLMENTS.Any(e => e.StudentID == studentID && e.ClassID == classID);
        }

        public void OnPostAddClass(int classId)
        {
            Enrollment addedClass = new Enrollment();
            addedClass.ClassID = classId;
            addedClass.StudentID = int.Parse(HttpContext.User.Identity.Name);
			credentialRepo.AddClassTuition(credentialRepo.GetCredById(Int32.Parse(User.Identity.Name)));

			enrollRepo.Add(addedClass);

            Response.Redirect("/Register");
        }

        public void OnPostDropClass(int classId)
        {
            for(int i = 0; i < ENROLLMENTS.Count; i++)
            {
                if (ENROLLMENTS[i].StudentID == int.Parse(HttpContext.User.Identity.Name) &&
                    ENROLLMENTS[i].ClassID == classId)
                {
                    credentialRepo.DropClassTuition(credentialRepo.GetCredById(Int32.Parse(User.Identity.Name)));
                    enrollRepo.Delete(ENROLLMENTS[i].Id);
                }
            }

            Response.Redirect("/Register");
        }

        public void OnPostSearchClasses(String searchField)
        {
            SHOWNCLASSES.Clear();

            for(int i = 0; i < ALLCLASSES.Count; i++)
            {
                if (searchField != null && !ALLCLASSES[i].CourseName.Contains(searchField))
                {
                    continue;
                }

                SHOWNCLASSES.Add(ALLCLASSES[i]);
            }

            Response.Redirect("/Register");
        }

        public void OnGet()
        {
            //TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            //ClassCount = TeacherClasses.Count();

            //for (int i = 0; i < ClassCount; i++)
            //{
            //    TeachesClasses.Add(credrepo.GetClassById(TeacherClasses[i].ClassID));
            //}
        }
    }
}
