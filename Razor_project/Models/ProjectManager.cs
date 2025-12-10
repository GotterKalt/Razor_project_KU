using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Razor_project.Models;
using System;
using System.Collections.Generic;

namespace Razor_project.Models
{
    public class ProjectManager : TeamMember
    {
        public List<int> ManagedProjectIds { get; set; } = new List<int>();

        public ProjectManager()
        {
        }

        public ProjectManager(string fullName, string email) : base(fullName, "ProjectManager")
        {
            Email = email;
        }

        public void AssignProject(int projectId)
        {
            if (!ManagedProjectIds.Contains(projectId))
                ManagedProjectIds.Add(projectId);
        }

        // override virtual metodo -> polimorfizmas
        public override string GetContactInfo()
        {
            return $"PM: {FullName} - {Email}";
        }
    }
}
