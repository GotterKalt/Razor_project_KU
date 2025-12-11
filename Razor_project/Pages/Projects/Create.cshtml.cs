using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_project.Data;
using Razor_project.Models;

namespace Razor_project.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = new Project();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // DEBUG: parodysim, jei kažkas negerai su ModelState
            if (!ModelState.IsValid)
            {
                // surenkam VISAS klaidas ir parodom virš formos
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        ModelState.AddModelError(string.Empty, $"Klaida lauke '{state.Key}': {error.ErrorMessage}");
                    }
                }

                return Page();
            }

            // JEIGU VALIDU, tiesiog įrašom
            _context.Projects.Add(Project);
            var rows = await _context.SaveChangesAsync();

            // galima patikrinti rows, bet mums užtenka, kad nenušoka į klaidą
            return RedirectToPage("Index");
        }

    }
}
