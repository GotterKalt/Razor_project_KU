using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_project.Data;
using Razor_project.Models;
using TaskStatus = Razor_project.Models.TaskStatus;


namespace Razor_project.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Bendros sumos
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }

        public int NewTasks { get; set; }
        public int ActiveTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int BlockedTasks { get; set; }
        public int OverdueTasks { get; set; }

        public double AverageTasksPerProject { get; set; }

        // Grupavimai
        public IList<ProjectTaskSummary> TasksByProject { get; set; } = new List<ProjectTaskSummary>();
        public IList<PrioritySummary> TasksByPriority { get; set; } = new List<PrioritySummary>();

        // DTO klasės atvaizdavimui
        public class ProjectTaskSummary
        {
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public int TaskCount { get; set; }
            public int CompletedCount { get; set; }
        }

        public class PrioritySummary
        {
            public PriorityLevel Priority { get; set; }
            public int Count { get; set; }
        }

        public async Task OnGetAsync()
        {
            // ---------- LAMBDA LINQ + agregacijos ----------
            TotalProjects = await _context.Projects.CountAsync();
            TotalTasks = await _context.TaskItems.CountAsync();

            NewTasks = await _context.TaskItems.CountAsync(t => t.Status == TaskStatus.New);
            ActiveTasks = await _context.TaskItems.CountAsync(t => t.Status == TaskStatus.Active);
            CompletedTasks = await _context.TaskItems.CountAsync(t => t.Status == TaskStatus.Completed);
            BlockedTasks = await _context.TaskItems.CountAsync(t => t.Status == TaskStatus.Blocked);

            OverdueTasks = await _context.TaskItems
                .CountAsync(t => t.Deadline != null &&
                                 t.Deadline < DateTime.UtcNow &&
                                 t.Status != TaskStatus.Completed);

            AverageTasksPerProject = TotalProjects == 0
                ? 0
                : Math.Round((double)TotalTasks / TotalProjects, 2);

            // ---------- QUERY SINTAKSĖ + JOIN + GROUP BY ----------

            // Užduotys pagal projektą (JOIN Projects ir TaskItems)
            var tasksByProjectQuery =
                from p in _context.Projects
                join t in _context.TaskItems
                    on p.Id equals t.ProjectId into projectTasks
                select new ProjectTaskSummary
                {
                    ProjectId = p.Id,
                    ProjectName = p.Name,
                    TaskCount = projectTasks.Count(),
                    CompletedCount = projectTasks.Count(x => x.Status == TaskStatus.Completed)
                };

            TasksByProject = await tasksByProjectQuery
                .OrderByDescending(x => x.TaskCount)
                .ToListAsync();

            // Užduotys pagal prioritetą (GROUP BY)
            var tasksByPriorityQuery =
                from t in _context.TaskItems
                group t by t.Priority into g
                select new PrioritySummary
                {
                    Priority = g.Key,
                    Count = g.Count()
                };

            TasksByPriority = await tasksByPriorityQuery
                .OrderByDescending(x => x.Count)
                .ToListAsync();
        }
    }
}
