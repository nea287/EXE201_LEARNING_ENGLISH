using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class StudentCourseReponse
    {
        public StudentCourseReponse()
        {
            Slots = new HashSet<SlotReponse>();
        }

        public int? StudentCourseId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Link { get; set; }
        public ICollection<SlotReponse>? Slots { get; set; }
    }
}