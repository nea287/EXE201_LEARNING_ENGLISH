using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class StudentFilter
    {
        public StudentFilter()
        {
            Orders = new HashSet<OrderFilter>();
            StudentCourses = new HashSet<StudentCourseFilter>();
        }

        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }

        public ICollection<OrderFilter>? Orders { get; set; }
        public ICollection<StudentCourseFilter>? StudentCourses { get; set; }
    }
}