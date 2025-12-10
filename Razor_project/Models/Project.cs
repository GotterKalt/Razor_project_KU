using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Razor_project.Models
{
    public partial class Project
    {
        [Key]
        public int Id { get; private set; }             // inkapsuliacija: public get, private set

        [Required]
        [StringLength(200)]
        public string Name { get; set; }                // inkapsuliacija: public get/set

        public string Description { get; set; }

        public DateTime StartDate { get; private set; } // public get, private set - kita inkapsuliacija
        public DateTime? EndDate { get; set; }

        // Composer: Project turi Tasks (kompozicija)
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        // Navigacija: projekto vadovas (optional)
        public int? ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }


        // konstruktoriai
        public Project()
        {
            // EF Core reikia parameterless konstruktoriaus
        }

        public Project(string name, DateTime startDate) : this()
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
            Name = name;
            StartDate = startDate;
        }

        public Project(string name, DateTime startDate, DateTime endDate) : this(name, startDate)
        {
            EndDate = endDate;
        }

        // metodas: apskaičiuoja progreso procentus (custom)
        public double CalculateProgress()
        {
            if (Tasks == null || Tasks.Count == 0) return 0.0;
            var total = Tasks.Count;
            var completed = Tasks.Count(t => t.Status == TaskStatus.Completed);
            return Math.Round((double)completed / total * 100.0, 2);
        }

        public bool IsOverdue()
        {
            if (!EndDate.HasValue) return false;
            return DateTime.UtcNow.Date > EndDate.Value.Date && CalculateProgress() < 100.0;
        }

        public override string ToString()
        {
            return $"{Name} ({Id}) - {CalculateProgress()}%";
        }
    }
}
