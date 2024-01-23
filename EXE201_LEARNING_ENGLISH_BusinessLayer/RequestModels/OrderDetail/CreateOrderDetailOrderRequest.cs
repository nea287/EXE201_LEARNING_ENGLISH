using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail
{
    public class CreateOrderDetailOrderRequest
    {
        [Required]
        public int CourseId { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
