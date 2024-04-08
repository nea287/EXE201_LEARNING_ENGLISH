using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class CourseFilter
    {
        public CourseFilter()
        {
            OrderDetails = new HashSet<OrderDetailFilter>();
            StudentCourses = new HashSet<StudentCourseFilter>();
        }

        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? TeacherId { get; set; }
        public double? Duration { get; set; }
        public int? NumberOfLesson { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }
        public int? VouncherId { get; set; }
        public byte[]? Image { get; set; }
        public ICollection<OrderDetailFilter>? OrderDetails { get; set; }
        public ICollection<StudentCourseFilter>? StudentCourses { get; set; }
    }
}