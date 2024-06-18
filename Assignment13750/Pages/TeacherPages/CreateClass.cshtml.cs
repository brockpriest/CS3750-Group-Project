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

namespace Assignment13750.Pages.TeacherPages
{
    public class CreateClassModel : PageModel
    {


        //Credrepo Initialization for Classes table
        private IClassManager credrepo;
        private ITeacherClassManager credrepoteach;
        public CreateClassModel(IClassManager classManager, ITeacherClassManager teacherManager)
        {
            this.credrepo = classManager;
            credrepoteach = teacherManager;
        }
        [BindProperty]
        public Classes Classes { get; set; } = default!;
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
        public bool NoDays { get; set; }
        public bool BadTime { get; set; }
       

        public List<TeacherClasses> TeacherClasses { get; set; }
        public List<Classes> TeachesClasses = new List<Classes> { };
        public int ClassCount;
        public void refreshClassList()
        {
            TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            ClassCount = TeacherClasses.Count();

            for (int i = 0; i < ClassCount; i++)
            {
                TeachesClasses.Add(credrepo.GetClassById(TeacherClasses[i].ClassID));
            }
        }
        public void OnGet()
        {
            refreshClassList();
        }
        
        public void OnPost() {
            if (ModelState.IsValid)
            {
                 List<string> SelectedDays = new List<string>();
                NoDays = false;
                BadTime = false;
                if (Monday) SelectedDays.Add("MO");
                if (Tuesday) SelectedDays.Add("TU");
                if (Wednesday) SelectedDays.Add("WE");
                if (Thursday) SelectedDays.Add("TH");
                if (Friday) SelectedDays.Add("FR");
                if (Saturday) SelectedDays.Add("SA");
                if (Sunday) SelectedDays.Add("SU");
                
                if(SelectedDays.Count == 0 || SelectedDays is null)// checking that a day is selected
                {
                    NoDays = true;
                }
                else if (Classes.StartTime>Classes.EndTime) // checking start & end time
                {
                    BadTime = true;
                }
                else
                {
                    Classes.MeetingDays = string.Join(", ", SelectedDays);
                    credrepo.Add(Classes);
                    List<Classes> ClassList = credrepo.GetAllClasses();
                    int i = 0;

                    foreach (Classes Id in ClassList)
                    {
                        if (ClassList[i].CourseName == Classes.CourseName && ClassList[i].CourseNo == Classes.CourseNo)
                        {

                            TeacherClasses NewLink = new TeacherClasses();
                            NewLink.TeacherID = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
                            NewLink.ClassID = ClassList[i].Id;
                            credrepoteach.Add(NewLink);
                            refreshClassList();
                        }
                        i++;
                    }

                    Response.Redirect("/Class.Create");
                }
            }
        }
    }
}
