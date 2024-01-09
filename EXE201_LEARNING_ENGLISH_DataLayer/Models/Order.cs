using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StudentId { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? VouncherId { get; set; }

        public virtual Student? QuantityNavigation { get; set; }
        public virtual Vouncher? Vouncher { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
