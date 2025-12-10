
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Razor_project.Models
{
    public partial class Project
    {
        // papildomas metodas validacijai
        public IEnumerable<ValidationResult> ValidateDates()
        {
            if (EndDate.HasValue && EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate cannot be earlier than StartDate", new[] { nameof(EndDate) });
            }
        }

        // greitas helper pridėti užduotį
        public void AddTask(TaskItem task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            task.ProjectId = this.Id; // jei Id dar 0 - vėliau EF užpildys
            Tasks.Add(task);
        }
    }
}

