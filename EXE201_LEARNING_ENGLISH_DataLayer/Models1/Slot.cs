using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Slot
    {
        public Slot()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int SlotId { get; set; }
        public double? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentCourseId { get; set; }
        public bool? IsAttended { get; set; }

        public virtual StudentCourse? StudentCourse { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
