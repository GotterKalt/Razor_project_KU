using Razor_project.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Razor_project.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;

        public TaskStatus Status { get; set; } = TaskStatus.New;

        public DateTime? Deadline { get; set; }

        // Assignment
        public int? AssignedToId { get; set; }
        public ProjectManager? AssignedTo { get; set; }


        // Link to project
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        // konstruktoriai
        public TaskItem()
        {
        }

        public TaskItem(string title) : this()
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public TaskItem(string title, PriorityLevel priority) : this(title)
        {
            Priority = priority;
        }

        // metodai
        public bool IsOverdue()
        {
            if (!Deadline.HasValue) return false;
            return DateTime.UtcNow > Deadline.Value && Status != TaskStatus.Completed;
        }

        // loginis metodas su parametrais ir grąžina bool
        public bool MarkAsCompleted()
        {
            if (Status == TaskStatus.Completed) return false;
            Status = TaskStatus.Completed;
            return true;
        }

        public override string ToString()
        {
            return $"{Title} [{Priority}] - {Status}";
        }
    }
}
