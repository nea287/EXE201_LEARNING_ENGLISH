namespace EXE201_LEARNING_ENGLISH_Client.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StudentId { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? VouncherId { get; set; }
        public string? Status { get; set; }
        public string? CourseId { get; set; }
        public string? MomoNumber { get; set; }
    }
}
