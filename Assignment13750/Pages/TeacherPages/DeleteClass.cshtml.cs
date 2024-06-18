using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment13750.Pages.TeacherPages
{
    public class DeleteClassModel : PageModel
    {
        private IClassManager credrepo;
        private ITeacherClassManager Iteachrepo;
        private IAssignmentManager assignrepo;

        public DeleteClassModel(IClassManager classManager, ITeacherClassManager teachrepo, IAssignmentManager iassignmentManager)
        {
            credrepo = classManager;
            Iteachrepo = teachrepo;
            assignrepo = iassignmentManager;
        }
        [FromRoute]
        public int Id { get; set; } //class ID that is being edited
        [BindProperty]
        public Classes Class { get; set; }
        public void OnGet()
        {
            Class = credrepo.GetClassById(Id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Class = credrepo.GetClassById(Id);
         
            Iteachrepo.Delete((Iteachrepo.GetByClassID(Id)).Id); // deletes link in teacher classes table
            assignrepo.DeleteByClassID(Id);
            credrepo.Delete(Id);
            return RedirectToPage("/TeacherPages/CreateClass");
        }
    }
}
