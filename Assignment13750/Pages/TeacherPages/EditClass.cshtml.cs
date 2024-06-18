using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment13750.Pages.TeacherPages
{
    public class EditClassModel : PageModel
    {
        private IClassManager credrepo;
        
        public EditClassModel(IClassManager classManager)
        {
            credrepo = classManager;
  
        }
        [FromRoute]
        public int Id { get; set; } //class ID that is being edited
        [BindProperty]
        public Classes Class { get; set; }
        public bool BadTime { get; set; } //checks time
        public bool NoDays { get; set; } // checks that 1 day is selected
        [BindProperty]
        public bool Monday { get; set; }
        [BindProperty]
        public bool Tuesday { get; set; }
        [BindProperty]
        public bool Wednesday { get; set; }
        [BindProperty]
        public bool Thursday { get; set; }
        [BindProperty]
        public bool Friday { get; set; }
        [BindProperty]
        public bool Saturday { get; set; }
        [BindProperty]
        public bool Sunday { get; set; }
        
        private void refreshfields()
        {
            Class = credrepo.GetClassById(Id);
            if (Class.MeetingDays == null) { return; }

            if (Class.MeetingDays.Contains("Monday")){Monday = true; } // I fully understand this looks terribly
            if (Class.MeetingDays.Contains("Tuesday")) { Tuesday = true; }
            if (Class.MeetingDays.Contains("Wednesday")) { Wednesday = true; }
            if (Class.MeetingDays.Contains("Thursday")) { Thursday = true; }
            if (Class.MeetingDays.Contains("Friday")) { Friday = true; }
            if (Class.MeetingDays.Contains("Saturday")) { Saturday = true; }
            if (Class.MeetingDays.Contains("Sunday")) { Sunday = true; }
        }

        public void OnGet()
        {
            refreshfields();
        }
        public async Task<IActionResult> OnPostAsync ()
        {
            
            if (ModelState.IsValid)
            {
                List<string> SelectedDays = new List<string>();
                NoDays = false;
                BadTime = false;
                if (Monday) SelectedDays.Add("Monday");
                if (Tuesday) SelectedDays.Add("Tuesday");
                if (Wednesday) SelectedDays.Add("Wednesday");
                if (Thursday) SelectedDays.Add("Thursday");
                if (Friday) SelectedDays.Add("Friday");
                if (Saturday) SelectedDays.Add("Saturday");
                if (Sunday) SelectedDays.Add("Sunday");

                if (SelectedDays.Count == 0 || SelectedDays is null)// checking that a day is selected
                {
                    NoDays = true;
                }
                else if (Class.StartTime > Class.EndTime) // checking start & end time
                {
                    BadTime = true;
                }
                else
                {
                    Class.Id = Id;
                    
                    Class.MeetingDays = string.Join(", ", SelectedDays);
                    credrepo.Update(Class);
                    refreshfields();
                    
                }

            }
            return RedirectToPage("/TeacherPages/CreateClass");

        }
    }
}
