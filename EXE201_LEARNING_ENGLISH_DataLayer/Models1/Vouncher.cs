using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Vouncher
    {
        public Vouncher()
        {
            Courses = new HashSet<Course>();
            Orders = new HashSet<Order>();
        }

        public int VouncherId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Status { get; set; }
        public string? VouncherName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
