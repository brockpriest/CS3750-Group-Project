using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment13750.Data;
using Assignment13750.Models;
using System.Diagnostics.Tracing;
using System.Security.AccessControl;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Assignment13750.Pages.Login
{
    
    public class IndexModel : PageModel
    {
        
		private ICredentialManager credrepo;

		public IndexModel(ICredentialManager credrepository)
		{
			this.credrepo = credrepository;
		}
        public bool Rememberlogin;

        public bool BadAttempt = false;
		private int userid { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Password { get; set; }

     

        public void OnGet()
        {
           
            ModelState.Clear();
            BadAttempt = false;
        }
        public async void OnPost()
        {
            
            if (ModelState.IsValid)
            {
				List<Credentials> Credentials = credrepo.GetAllCredentials();
				

				foreach (Credentials ID in Credentials)
                {
                     
                    if (Credentials[userid].Email.ToLower() == Username.ToLower() && Credentials[userid].Password==Password.Sha256())
                    {
                      
                        BadAttempt = false;
						Response.Redirect("Welcome");
						var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,Credentials[userid].ID.ToString()),//Sets ID as Name claimtype. Used to see who is logged in
                            new Claim(ClaimTypes.Role,Credentials[userid].IsTeacher.ToString()) // Sets IsTeacher to Role ClaimType. If Role is true, then user is a teacher
                        };
                        var identity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                       
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal,
                            new AuthenticationProperties { IsPersistent = Rememberlogin}
                            );
                        HttpContext.User = principal;            
                        return; 
                    }
                    userid++;
                }
                BadAttempt = true;
                return;
            }
        }
        
    }
}
