using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class CourseReponse
    {
        public CourseReponse()
        {
            OrderDetails = new HashSet<OrderDetailReponse>();
            StudentCourses = new HashSet<StudentCourseReponse>();
        }

        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public double? Duration { get; set; }
        public int? NumberOfLesson { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? Status { get; set; }
        public int? VouncherId { get; set; }
        public decimal? TotalAmount { get; set; }
        public byte[] Image { get; set; }
        public string MomoNumber { get; set; }
        public ICollection<OrderDetailReponse>? OrderDetails { get; set; }
        public ICollection<StudentCourseReponse>? StudentCourses { get; set; }
    }
}