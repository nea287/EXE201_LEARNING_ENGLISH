using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class StudentReponse
    {
        public StudentReponse()
        {
            Orders = new HashSet<OrderReponse>();
            StudentCourses = new HashSet<StudentCourseReponse>();
        }

        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }

        public ICollection<OrderReponse>? Orders { get; set; }
        public ICollection<StudentCourseReponse>? StudentCourses { get; set; }
    }
}