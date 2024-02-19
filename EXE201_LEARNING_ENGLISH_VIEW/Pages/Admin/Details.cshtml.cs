using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_VIEW.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext _context;

        public DetailsModel(EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext context)
        {
            _context = context;
        }

      public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }
    }
}
