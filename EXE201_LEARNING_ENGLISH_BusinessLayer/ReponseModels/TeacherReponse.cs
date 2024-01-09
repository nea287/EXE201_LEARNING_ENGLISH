using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class TeacherReponse
    {
        public TeacherReponse()
        {
            Certificates = new HashSet<CertificateReponse>();
            Courses = new HashSet<CourseReponse>();
        }

        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? Level { get; set; }

        public ICollection<CertificateReponse>? Certificates { get; set; }
        public ICollection<CourseReponse>? Courses { get; set; }
    }
}