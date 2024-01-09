using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class StudentCourseFilter
    {
        public StudentCourseFilter()
        {
            Slots = new HashSet<SlotFilter>();
        }

        public int? StudentCourseId { get; set; }
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Link { get; set; }
        public ICollection<SlotFilter>? Slots { get; set; }
    }
}