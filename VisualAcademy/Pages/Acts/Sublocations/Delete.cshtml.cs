﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VisualAcademy.Data;
using VisualAcademy.Models;

namespace VisualAcademy.Pages.Acts.Sublocations
{
    public class DeleteModel : PageModel
    {
        private readonly VisualAcademy.Data.ApplicationDbContext _context;

        public DeleteModel(VisualAcademy.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sublocation Sublocation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sublocation = await _context.Sublocations
                .Include(s => s.LocationRef).FirstOrDefaultAsync(m => m.Id == id);

            if (Sublocation == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sublocation = await _context.Sublocations.FindAsync(id);

            if (Sublocation != null)
            {
                _context.Sublocations.Remove(Sublocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
