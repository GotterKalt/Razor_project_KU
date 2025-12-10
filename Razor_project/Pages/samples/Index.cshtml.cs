using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using Razor_project.Models;

namespace Razor_project.Pages.samples
{
    public class IndexModel : PageModel
    {
        public IList<Product> Products { get; set; } = new List<Product>();



        [BindProperty]

        public Product Product { get; set; } = new Product();

        public readonly DemoDbContext context = new DemoDbContext();
        public void OnGet()
        {
            int[] numbers = { 3, 7, 1, 9, 4 };

            Products.Add(new Product() { ProductId = 1, Name = "Keyboard" });
            Products.Add(new Product() { ProductId = 2, Name = "Mouse" });
            Products.Add(new Product() { ProductId = 3, Name = "Monitor" });

            var items = context.Items.ToList();

            //context.Items.Add(new Item() { Id = 10, Name = "Garlic" });
            //context.SaveChanges(); //visada turi buti vykdoma, norint issaugoti
            ViewData["numbers"] = numbers;
        }

        public void OnPost()
        {
            if (!ModelState.IsValid) //backend validacija, patkrinimas
            {

            }
        }
    }
}
