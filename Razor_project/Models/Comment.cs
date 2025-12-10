using Razor_project.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Razor_project.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        [Required]
        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public bool IsInternal { get; set; } = false; // primitive bool

        public double EffortEstimateHours { get; set; } // primitive double

        public Comment()
        {
        }

        public Comment(int taskItemId, string author, string text)
        {
            TaskItemId = taskItemId;
            Author = author;
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString() => $"{Author} @ {CreatedAt}: {Text}";
    }
}
