using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail
{
    public class UpdateOrderDetailRequest
    {
        public decimal? UnitPrice { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
