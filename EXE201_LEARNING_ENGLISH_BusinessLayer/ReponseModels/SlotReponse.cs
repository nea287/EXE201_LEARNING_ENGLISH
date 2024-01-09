using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class SlotReponse
    {
        public SlotReponse()
        {
            Feedbacks = new HashSet<FeedbackReponse>();
        }

        public int? SlotId { get; set; }
        public double? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentCourseId { get; set; }
        public bool? IsAttended { get; set; }
        public ICollection<FeedbackReponse>? Feedbacks { get; set; }
    }
}