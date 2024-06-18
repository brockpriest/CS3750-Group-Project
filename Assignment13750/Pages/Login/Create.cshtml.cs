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

namespace Assignment13750.Pages.Login
{
    public class CreateModel : PageModel
    {
        //Credrepo Initialization
		private ICredentialManager credrepo;

		public CreateModel(ICredentialManager credrepository)
		{
			this.credrepo = credrepository;
		}
        public bool Under18 = false;
		public IActionResult OnGet()
        {
          
            
            return Page();
        }

        [BindProperty]
        public Credentials Credentials { get; set; } = default!;
        [BindProperty]
        public string ConfirmPassword { get; set; }
        public bool Confirmerror = false;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            Console.WriteLine("password: " + ConfirmPassword +"\n\n");
            if (!ModelState.IsValid || Credentials == null)
            {
                return Page();
            }
            
            if (Credentials.Password!=ConfirmPassword)
            {
                Confirmerror = true;
                return Page();
            }
            else
            {
                Confirmerror = false;
            }
            DateTime dt = Credentials.BirthDate;
            DateTime dt_now = DateTime.Now;

            DateTime dt_18 = dt.AddYears(18); //here add years, not subtract

            if (!(dt_18.Date >= dt_now.Date)) //here you want to compare dt_now
            {
                Under18 = false;
                Credentials.Password = Credentials.Password.Sha256(); //Hashes Password
                credrepo.Add(Credentials);
                Console.WriteLine(credrepo.GetCredById(Credentials.ID).Password);
                return RedirectToPage("./Index");
            }
            else
            {
                Under18 = true;
            }
            return null;
        }
    }
}
