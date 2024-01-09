namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class OrderDetailFilter
    {
        public int? OrderId { get; set; }
        public int? CourseId { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}