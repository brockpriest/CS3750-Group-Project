using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Assignment13750.Pages
{
    public class GradeAssignmentModel : PageModel
    {
        private readonly ISubmissionManager _submissionManager;
        private readonly IAssignmentManager _assignmentManager;

        public GradeAssignmentModel(ISubmissionManager submissionManager, IAssignmentManager assignmentManager)
        {
            _submissionManager = submissionManager;
            _assignmentManager = assignmentManager;
        }

        [FromRoute]
        public int SubmissionId { get; set; }

        [BindProperty]
        public int Grade { get; set; }

        [BindProperty]
        public string Comments { get; set; }

        public Submissions Submission { get; set; }
        public Assignment Assignment { get; set; }

        public void OnGet()
        {
            Submission = _submissionManager.GetSubmissionById(SubmissionId);
            if (Submission == null)
            {
               RedirectToPage("/Index"); //if no submission, it returns user to main page
            }
            Assignment = _assignmentManager.GetAssignmentById(Submission.AssignmentID);

        }


        public async Task<IActionResult> OnPostAsync()
        {
            
            Submission = _submissionManager.GetSubmissionById(SubmissionId);


            if (Submission.Id == null || Submission.Id == 0)
            {
                return RedirectToPage("/Index");
            }


            Submission.Grade = Grade;
            Submission.Comments = Comments;
            _submissionManager.UpdateGrade(Submission);

            return RedirectToPage("/Courses");
        }
    }
}
