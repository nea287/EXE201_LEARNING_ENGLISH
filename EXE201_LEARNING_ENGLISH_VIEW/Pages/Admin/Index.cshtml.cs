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
    public class IndexModel : PageModel
    {
        private readonly EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext _context;

        public IndexModel(EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Student)
                .Include(o => o.Vouncher).ToListAsync();
            }
        }
    }
}
