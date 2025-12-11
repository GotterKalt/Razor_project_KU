using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor_project.Data;
using Razor_project.Models;

namespace Razor_project.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new TaskItem();

        public SelectList ProjectOptions { get; set; }

        public void OnGet(int? projectId)
        {
            ProjectOptions = new SelectList(_context.Projects, "Id", "Name", projectId);
            if (projectId.HasValue)
            {
                TaskItem.ProjectId = projectId.Value;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProjectOptions = new SelectList(_context.Projects, "Id", "Name", TaskItem.ProjectId);
                return Page();
            }

            _context.TaskItems.Add(TaskItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
