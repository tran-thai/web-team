﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebTeam.Data
{
    [Table("Faculty")]
    public partial class Faculty
    {
        public int FacultyId { get; set; }

        public string FacultyName { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
