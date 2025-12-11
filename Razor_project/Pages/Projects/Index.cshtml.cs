using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_project.Data;
using Razor_project.Models;

namespace Razor_project.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> ProjectList { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            ProjectList = await _context.Projects
                .Include(p => p.Tasks)
                .ToListAsync();
        }
    }
}
