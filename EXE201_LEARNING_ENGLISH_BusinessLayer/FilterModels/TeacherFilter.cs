using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using System.Diagnostics;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class TeacherFilter
    {
        public TeacherFilter()
        {
            Certificates = new HashSet<CertificateFilter>();
            Courses = new HashSet<CourseFilter>();
        }

        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? Level { get; set; }

        public ICollection<CertificateFilter>? Certificates { get; set; }
        public ICollection<CourseFilter>? Courses { get; set; }
    }
}