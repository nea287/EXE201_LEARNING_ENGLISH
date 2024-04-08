using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class OrderReponse
    {
        public OrderReponse()
        {
            OrderDetails = new HashSet<OrderDetailReponse>();
        }

        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? VouncherId { get; set; }
        public string? Status { get; set; }
        public string? CourseId { get; set; }

        public ICollection<OrderDetailReponse>? OrderDetails { get; set; }
    }
}