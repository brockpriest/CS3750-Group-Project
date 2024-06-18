using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment13750.Data;
using Assignment13750.Models;

namespace Assignment13750.Pages.Login
{
    public class EditModel : PageModel
    {
        private ICredentialManager credrepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(ICredentialManager credrepository, IWebHostEnvironment hostingEnvironment)
        {
            this.credrepo = credrepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Credentials Credentials { get; set; } = default!;

        [BindProperty]
        public IFormFile ProfilePicFile { get; set; }

        public void OnGet()
        {
            // Retrieve the user's credentials by ID
            int id = int.Parse(HttpContext.User.Identity.Name);
            Credentials = credrepo.GetCredById(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //profile picture upload
                if (ProfilePicFile != null && ProfilePicFile.Length > 0)
                {
                    // Get  file name using the users ID
                    string uniqueFileName = $"{Credentials.ID}_{Guid.NewGuid()}{Path.GetExtension(ProfilePicFile.FileName)}";

                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "profilepics");

                    // Combine the folder path and file name
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the image
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePicFile.CopyToAsync(fileStream);
                    }

                    // Update the profilePic
                    Credentials.ProfilePic = uniqueFileName;
                }

                // Update the credentials 
                credrepo.Update(Credentials);

                // Redirect to Profile
                return RedirectToPage("Details");
            }
            return Page();
        }
    }
}

