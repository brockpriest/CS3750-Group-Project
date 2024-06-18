using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment13750.Pages.TeacherPages
{
    public class EditAssignmentModel : PageModel
    {
        private IAssignmentManager assignrepo;

        public EditAssignmentModel(IAssignmentManager iassignmentManager)
        {
            assignrepo = iassignmentManager;
        }
        [FromRoute]
        public int Id { get; set; } //class ID that is being edited
        [BindProperty]
        public Assignment Assignment { get; set; }
        [BindProperty]
        public bool File { get; set; }
        [BindProperty]
        public bool Text { get; set; }
        public int ClassID { get; set; }    
        private void Refreshfields()
        {

            Assignment.ClassID = assignrepo.GetAssignmentById(Id).ClassID;
            ClassID = Assignment.ClassID;
        }
        public void OnGet()
        {
            Assignment = assignrepo.GetAssignmentById(Id);
            ClassID = Assignment.ClassID;
            if (Assignment.SubmissionType)
            {
                File = true;
            }
            else
            {
                Text = true;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!string.IsNullOrEmpty(Assignment.Name) && !string.IsNullOrEmpty(Assignment.Description) && Assignment.Maxpoints != default(int) && Assignment.Duedate != default(DateTime))
            {
                //Class = credrepo.GetClassById(Id);
                //Assignment.ClassID = Id;
                if (File)
                {
                    Assignment.SubmissionType = true;
                }
                
                Assignment.Id = Id;
                Console.WriteLine("\n\nClASSID:" + ClassID + "\n\n");
                Assignment.ClassID = ClassID;
                Console.WriteLine("\n\nAFterset:" + ClassID + "\n\n");
                Refreshfields();
                Console.WriteLine("\n\nClASSID2:" + ClassID + "\n\n");
                Assignment.ClassID = ClassID;
                Console.WriteLine("\n\nAFterset2:" + ClassID + "\n\n");
                assignrepo.Update(Assignment);
            }
            return RedirectToPage("/Login/Welcome");
        }
    }
}
