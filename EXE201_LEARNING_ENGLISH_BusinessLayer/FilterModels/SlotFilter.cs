using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class SlotFilter
    {
        public SlotFilter()
        {
            Feedbacks = new HashSet<FeedbackFilter>();
        }

        public int? SlotId { get; set; }
        public double? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentCourseId { get; set; }
        public bool? IsAttended { get; set; }
        public ICollection<FeedbackFilter>? Feedbacks { get; set; }
    }
}