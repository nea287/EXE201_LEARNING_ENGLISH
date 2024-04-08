using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
