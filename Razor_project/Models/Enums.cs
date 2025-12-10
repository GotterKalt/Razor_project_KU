using System;
namespace Razor_project.Models
{
    public enum TaskStatus
    {
        New,
        Active,
        Completed,
        Blocked
    }

    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}
