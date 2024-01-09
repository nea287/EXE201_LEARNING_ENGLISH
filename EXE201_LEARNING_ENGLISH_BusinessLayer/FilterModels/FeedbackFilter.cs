namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class FeedbackFilter
    {
        public int? FeedbackId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public double? Grade { get; set; }
        public int? SlotId { get; set; }
    }
}