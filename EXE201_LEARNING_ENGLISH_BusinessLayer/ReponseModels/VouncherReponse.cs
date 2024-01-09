using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class VouncherReponse
    {
        public VouncherReponse()
        {
            Courses = new HashSet<CourseReponse>();
            Orders = new HashSet<OrderReponse>();
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

        public ICollection<CourseReponse>? Courses { get; set; }
        public ICollection<OrderReponse>? Orders { get; set; }
    }
}