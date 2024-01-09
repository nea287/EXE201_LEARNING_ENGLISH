using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public partial class Course
    {
        public Course()
        {
            OrderDetails = new HashSet<OrderDetail>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? TeacherId { get; set; }
        public double? Duration { get; set; }
        public int? NumberOfLesson { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }
        public int? VouncherId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Vouncher? Vouncher { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
