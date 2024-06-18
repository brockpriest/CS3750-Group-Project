using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment13750.Data;
using Assignment13750.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Assignment13750.Pages.Login
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private ICredentialManager credrepo;

        public DetailsModel(ICredentialManager credrepository)
        {
            this.credrepo = credrepository;
        }

        public Credentials Credentials { get; set; } = default!; 

        public  void OnGet()
        {
            int id = int.Parse(HttpContext.User.Identity.Name);
            Credentials= credrepo.GetCredById(id);
            
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            Console.WriteLine("running\n\n");
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/index");
        }
    }
}
