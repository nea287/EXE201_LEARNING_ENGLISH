using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail
{
    public class OrderDetailRequest
    {
        public int OrderId { get; set; }
        public int CourseId { get; set; }
    }
}
