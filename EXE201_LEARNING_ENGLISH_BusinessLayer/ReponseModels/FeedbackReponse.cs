using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class FeedbackReponse
    {
        public int? FeedbackId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public double? Grade { get; set; }
        public int? SlotId { get; set; }

    }
}