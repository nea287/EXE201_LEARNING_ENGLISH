using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Student
    {
        public Student()
        {
            Orders = new HashSet<Order>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }

        public virtual Account? EmailNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
