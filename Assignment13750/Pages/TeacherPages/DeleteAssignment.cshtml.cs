using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment13750.Pages.TeacherPages
{
    public class DeleteAssignmentModel : PageModel
    {
        private IAssignmentManager assignrepo;

        public DeleteAssignmentModel(IAssignmentManager iassignmentManager)
        { 
            assignrepo = iassignmentManager;
        }
        [FromRoute]
        public int Id { get; set; } //class ID that is being edited
        [BindProperty]
        public Assignment Assignment { get; set; }
        public void OnGet()
        {
            Assignment = assignrepo.GetAssignmentById(Id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Assignment = assignrepo.GetAssignmentById(Id);
            assignrepo.Delete(Id);
            return Redirect("/Login/Course/" + Assignment.ClassID);
        }
    }
}
