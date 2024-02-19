using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_VIEW.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext _context;

        public CreateModel(EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
        ViewData["VouncherId"] = new SelectList(_context.Vounchers, "VouncherId", "VouncherId");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Orders == null || Order == null)
            {
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
