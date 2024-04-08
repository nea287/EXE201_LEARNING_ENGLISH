using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Teacher
    {
        public Teacher()
        {
            Certificates = new HashSet<Certificate>();
            Courses = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? TeacherCode { get; set; }
        public string? Level { get; set; }
        public string? MomoNumber { get; set; }

        public virtual Account? EmailNavigation { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
