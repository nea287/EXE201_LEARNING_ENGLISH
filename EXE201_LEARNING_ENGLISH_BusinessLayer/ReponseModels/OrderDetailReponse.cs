using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class OrderDetailReponse
    {
        public int? OrderId { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}