using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScottPlot;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace Assignment13750.Pages.StudentPages
{
    public class Submit : PageModel
    {
        private ISubmissionManager credreposubmit;
		private IAssignmentManager credrepoassign;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private IClassManager repoclass;
		public Submit(IAssignmentManager assignManager, ISubmissionManager submitmanager, IWebHostEnvironment webhost, IClassManager classManager)
		{
			_hostingEnvironment = webhost;
			credrepoassign = assignManager;
			credreposubmit = submitmanager;
			repoclass = classManager;
		}
		[FromRoute]
        public int id { get; set; }
		[BindProperty]
		public IFormFile Upload { get; set; }
		public Assignment Assignment { get; set; } = default(Assignment);
		[BindProperty]
		public Submissions submission { get; set; } = default!;
		public bool AssignSubmitted = false;

        public string uniqueFileName;
		public string GraphFile;
		private void recheck()
		{
            Assignment = credrepoassign.GetAssignmentById(id);
            if (credreposubmit.GetSubmissionsByStudIDandClassID(int.Parse(User.Identity.Name), Assignment.ClassID, Assignment.Id).Any())
            {

                AssignSubmitted = true;
                submission = credreposubmit.GetSubmissionsByStudIDandClassID(int.Parse(User.Identity.Name), Assignment.ClassID, Assignment.Id).First();
				if (submission.Grade != 0) // if graded
				{
					//should add code to get num of students in class
					double[] gradecount = new double[Assignment.Maxpoints]; 
					double[] positions = new double[Assignment.Maxpoints]; // will break if grade is above max
                    var myPlot = new ScottPlot.Plot(600, 400);
					List<Submissions> subs = credreposubmit.GetSubmissionsForAssignment(id);
					
					for (int i=1; i<Assignment.Maxpoints;i++) // set to 1 so defualt 0 not counted
					{
						gradecount[i-1] = credreposubmit.GetSubmissionsByGradeAndAssignment(i, id).Count();
						if (credreposubmit.GetSubmissionsByGradeAndAssignment(i, id).Any())
						{
							positions[i - 1] = i;
                        }
						else { positions[i - 1] = 0; }
					}
                    double[] values = { 26, 20, 23, 7, 16 };
                    double[] positions2 = { 10, 20, 30, 40, 50 };

                    // add a bar graph to the plot
                    
					double[] positionCurrent = { submission.Grade  };
					double[] count = { gradecount[positions.ToList().IndexOf(submission.Grade)] };
					gradecount[positions.ToList().IndexOf(submission.Grade)] = gradecount[positions.ToList().IndexOf(submission.Grade)] - 1;

                    var currentstud = myPlot.AddBar(count, positionCurrent);//Adds Red bar for current student
					currentstud.FillColor = Color.Red;
                    var bar = myPlot.AddBar(gradecount, positions);//Adds bars for other students
                    bar.FillColor = Color.Blue;					
                    bar.BarWidth = Assignment.Maxpoints * .1;
                    currentstud.BarWidth = Assignment.Maxpoints * .1;
                    myPlot.SetAxisLimits(xMax: Assignment.Maxpoints);
                    myPlot.SetAxisLimits(yMin: 0);
					
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Graphs");
                    string uniqueFileName = $"GradeGraph{User.Identity.Name}{id}.png";
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
					GraphFile = $"/Images/Graphs/GradeGraph{User.Identity.Name}{id}.png";
                    myPlot.SaveFig(filePath);
                }
            }
        }
        public void OnGet()
        {
			recheck();        
        }
		public async Task OnPostAsync()
		{

			recheck();
            if (Assignment.SubmissionType == true) // for file submission
			{
               uniqueFileName = $"{id}_{Guid.NewGuid()}{Path.GetExtension(Upload.FileName)}";

				string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "Submissions");

				// Combine the folder path and file name
				string filePath = Path.Combine(uploadFolder, uniqueFileName);

				// Save the image
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await Upload.CopyToAsync(fileStream);
				}
                submission.Submitted = uniqueFileName;
				submission.Id = default(int);
			
			}
            if (!(string.IsNullOrEmpty(submission.Submitted)))
			{
				submission.AssignmentID = id;
				submission.ClassID = Assignment.ClassID;
				submission.StudentID = int.Parse(User.Identity.Name);
				submission.SubmissionType = Assignment.SubmissionType;
                submission.Comments = "";
				submission.SubmissionDate= DateTime.Now;
                if (AssignSubmitted)
				{
					credreposubmit.Update(submission);
				}
				else
				{
                    credreposubmit.Add(submission);
				}
			}
			Response.Redirect("/welcome");
		}
	}
}
