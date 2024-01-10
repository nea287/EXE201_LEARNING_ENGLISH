using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order
{
    public class UpdateOrderRequest
    {
        [Required]
        public int Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? StudentId { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? VouncherId { get; set; }
        public ICollection<UpdateOrderDetailRequest>? OrderDetails { get; set; }
    }
}
