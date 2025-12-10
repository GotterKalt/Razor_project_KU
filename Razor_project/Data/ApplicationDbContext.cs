using Microsoft.EntityFrameworkCore;
using Razor_project.Models;

namespace Razor_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
    }
}
