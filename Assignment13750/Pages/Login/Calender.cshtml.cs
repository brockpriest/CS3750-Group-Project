using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment13750.Models;
using Assignment13750.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace Assignment13750.Pages.Login
{
    public class Event
    {
        public int Id { get; set; }
        public string courseName { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public String MeetingDays { get; set; }
        public string courseNumber { get; set; }
    }

    public class CalenderModel : PageModel
    {
        private ICredentialManager credrepo;
        private IClassManager classrepo;
        private ITeacherClassManager credrepoteach;

        [BindProperty]
        public List<Event> Events { get; set; }

        public CalenderModel(ICredentialManager credrepository, ITeacherClassManager teacherManager, IClassManager classes)
        {
            this.credrepo = credrepository;
            credrepoteach = teacherManager;
            classrepo = classes;
        }

        bool isFirstAccess = true;
        public Assignment13750.Models.Credentials userlogin;

        public int loginID { get; set; }
        public List<TeacherClasses> TeacherClasses { get; set; }
        public List<Classes> TeachesClasses = new List<Classes> { };
        public int ClassCount;


        public void OnGet()
        {
            //var events = classrepo.GetAllClasses().Select(e => new
            //{
            // id = e.Id,
            // courseName = e.CourseName,
            // startTime = e.StartTime.ToString("MM/dd/yyyy"),
            // endTime = e.EndTime.ToString("MM/dd/yyyy"),
            // courseNumber = e.CourseNo,
            // meetingDays = e.MeetingDays
            //}
            //).ToList();

            if (isFirstAccess)
            {

                loginID = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
                userlogin = credrepo.GetCredById(loginID);
                isFirstAccess = false;
            }

            if (userlogin.IsTeacher)
            {
                TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
                ClassCount = TeacherClasses.Count();

                TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(User.Identity.Name)); //List of TeacherClasses NOT classes
                foreach (TeacherClasses poop in TeacherClasses)
                {
                    TeachesClasses.Add(classrepo.GetClassById(poop.ClassID)); //converts to classes
                }


                Events = new List<Event>();

                for (int i = 0; i < TeachesClasses.Count(); i++)
                {
                    Event e = new Event();
                    var y = classrepo.GetAllClasses().Where(x => x.CourseNo == TeachesClasses[i].CourseNo).ToList()[0];
                    //e.Id = classrepo.GetClassById(TeacherClasses[i].Id);
                    e.Id = y.Id;
                    e.courseNumber = y.CourseName;
                    e.startTime = y.StartTime;
                    e.endTime = y.EndTime;
                    e.MeetingDays = y.MeetingDays;
                    //e.courseName = y.CourseName;

                    



                    Events.Add(e);
                }
            }
            System.Diagnostics.Debug.WriteLine(Events);


        }


    }

}
