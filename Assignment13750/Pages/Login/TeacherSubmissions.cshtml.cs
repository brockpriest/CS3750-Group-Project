using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Assignment13750.Pages.Login
{
    public class TeacherSubmissionsModel : PageModel
    {

        [FromRoute]
        public int id { get; set; }

        private IAssignmentManager assignmentRepository;
        private ISubmissionManager submissionRepository;
        private IClassManager classesrepo;
        private ICredentialManager credentialrepo;


        public TeacherSubmissionsModel(IAssignmentManager assignmentRepo, ISubmissionManager submissionRepo, IClassManager classManager, ICredentialManager credentialManager)
        {
            this.assignmentRepository = assignmentRepo;
            submissionRepository = submissionRepo;
            classesrepo = classManager;
            credentialrepo = credentialManager;
        }

        public List<Assignment> Assigns { get; set; }
        public List<Submissions> Subs = new List<Submissions>();
        public Classes CurrentClass { get; set; }
        

        public void refrestList()
        {
            CurrentClass = classesrepo.GetClassById(id);
			//we have class id
			Assigns = assignmentRepository.GetAssignmentByClassId(id);
			//get assignments

            foreach (Assignment assignment in Assigns)
			{
				for (int i = 0; i < submissionRepository.GetSubmissionsForAssignment(assignment.Id).Count(); i++)
				{
                    Submissions testsss = submissionRepository.GetSubmissionsForAssignment(assignment.Id)[i];
                    Subs.Add(submissionRepository.GetSubmissionsForAssignment(assignment.Id)[i]);
                    Console.WriteLine("\n\n\n\nths   " + Subs.ToString());
                }
			}
			//get submissions
		}

		public void OnGet()
        {
            refrestList(); 
        }
        public string GetAssignName(int id)
        {
            return assignmentRepository.GetAssignmentById(id).Name;
        }
        public string GetUserName(int id)
        {
            return credentialrepo.GetCredById(id).FirstName;
        }
    }
}
