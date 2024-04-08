using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public partial class StudentCourse
    {
        public StudentCourse()
        {
            Slots = new HashSet<Slot>();
        }

        public int StudentCourseId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Link { get; set; }


        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
    }
}
