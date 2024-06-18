using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment13750.Data;
using Assignment13750.Models;

namespace Assignment13750.Pages.Login
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment13750.Data.Assignment13750Context _context;

        public DeleteModel(Assignment13750.Data.Assignment13750Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Credentials Credentials { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.FirstOrDefaultAsync(m => m.ID == id);

            if (credentials == null)
            {
                return NotFound();
            }
            else 
            {
                Credentials = credentials;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Credentials == null)
            {
                return NotFound();
            }
            var credentials = await _context.Credentials.FindAsync(id);

            if (credentials != null)
            {
                Credentials = credentials;
                _context.Credentials.Remove(Credentials);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
