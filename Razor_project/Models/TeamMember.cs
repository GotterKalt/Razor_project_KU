using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Razor_project.Models
{
    public abstract class TeamMember
    {
        [Key]
        public int Id { get; set; }

        private string _fullName;              // private field

        [Required]
        public string FullName
        {
            get => _fullName;                 // public get accessor
            protected set                      // protected set -> kitų klasė gali nustatyti per konstruktorių arba paveldėjimą
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("FullName required");
                _fullName = value;
            }
        }

        public string Role { get; protected set; }  // protected set

        public string Email { get; set; }            // public get/set

        // konstruktoriai
        protected TeamMember()
        {
        }

        protected TeamMember(string fullName, string role)
        {
            FullName = fullName;
            Role = role;
        }

        // virtual metodas -> polimorfizmas
        public virtual string GetContactInfo()
        {
            return $"{FullName} ({Role}) - {Email}";
        }
    }
}
