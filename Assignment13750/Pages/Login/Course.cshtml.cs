using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Assignment13750.Pages.Login
{
    public class CourseModel : PageModel
    {
        
        private IClassManager credrepo;
        private ITeacherClassManager credrepoteach;
        private IAssignmentManager assignmentrepo;
        private ICredentialManager credentialmanager;
        private ISubmissionManager submissionmanager;
      

        public CourseModel(IClassManager classManager, ITeacherClassManager teacherManager, IAssignmentManager assignmentManager, ICredentialManager credrepository)
        {
            this.credrepo = classManager;
            credrepoteach = teacherManager;
            assignmentrepo = assignmentManager;
            credentialmanager = credrepository;
        }
        [FromRoute]
        public int Id { get; set; }
        public List<Assignment> Assignments = new List<Assignment> { };
        [BindProperty]
        public int id2 { get; set; }
        [BindProperty]
        public Assignment Assignment{ get; set; } = default!;
        
        public Classes Class { get; set; }
        [BindProperty]
        public bool File { get; set; }
        [BindProperty]
        public bool Text { get; set; }
        public Assignment13750.Models.Credentials userlogin;
        bool isFirstAccess = true;
        public void RefreshList()
        {
            Assignments=assignmentrepo.GetAssignmentByClassId(Id);
            
        }
        public void OnGet()
        {
            Class = credrepo.GetClassById(Id);
            RefreshList();
            
            //Console.WriteLine("\nthis is a test" +  userlogin.IsTeacher +"\n\n\n");
            if (isFirstAccess)
            {
                id2 = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
                userlogin = credentialmanager.GetCredById(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
                isFirstAccess = false;
            }
            
        }
        public void OnPost()
        {
            userlogin = credentialmanager.GetCredById(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            if (!string.IsNullOrEmpty(Assignment.Name) && !string.IsNullOrEmpty(Assignment.Description) && Assignment.Maxpoints != default(int) && Assignment.Duedate != default(DateTime))
            {
                //Class = credrepo.GetClassById(Id);
                Assignment.ClassID = Id;
                if (File)
                {
                    Assignment.SubmissionType = true;
                }
                assignmentrepo.Add(Assignment);
                Response.Redirect("/Login/Course/" + Id);
            }
            Class = credrepo.GetClassById(Id);
            RefreshList();
        }
    }
}
