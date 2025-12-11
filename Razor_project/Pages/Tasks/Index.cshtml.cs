using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_project.Data;
using Razor_project.Models;

namespace Razor_project.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // sąrašas, kurį naudoja vaizdas
        public IList<TaskItem> TaskList { get; set; } = new List<TaskItem>();

        // filtravimas pagal projektą (ateina iš projekto Details)
        [BindProperty(SupportsGet = true)]
        public int? ProjectId { get; set; }

        // filtravimas pagal statusą (dropdown'e viršuje)
        [BindProperty(SupportsGet = true)]
        public Razor_project.Models.TaskStatus? StatusFilter { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.TaskItems
                .Include(t => t.Project)
                .AsQueryable();

            if (ProjectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == ProjectId.Value);
            }

            if (StatusFilter.HasValue)
            {
                query = query.Where(t => t.Status == StatusFilter.Value);
            }

            TaskList = await query
                .OrderBy(t => t.Deadline)
                .ToListAsync();
        }
    }
}
