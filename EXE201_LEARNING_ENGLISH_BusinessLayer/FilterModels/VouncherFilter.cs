using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class VouncherFilter
    {
        public VouncherFilter()
        {
            Courses = new HashSet<CourseFilter>();
            Orders = new HashSet<OrderFilter>();
        }

        public int? VouncherId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Status { get; set; }
        public string? VouncherName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }

        public ICollection<CourseFilter>? Courses { get; set; }
        public ICollection<OrderFilter>? Orders { get; set; }
    }
}
