using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class OrderFilter
    {
        public OrderFilter()
        {
            OrderDetails = new HashSet<OrderDetailFilter>();
        }

        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StudentId { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? VouncherId { get; set; }

        public ICollection<OrderDetailFilter>? OrderDetails { get; set; }
    }
}