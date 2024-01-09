using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course
{
    public class CreateCourseRequest
    {

        [Required]
        public string CourseName { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? TeacherId { get; set; }
        public double? Duration { get; set; }
        public int? NumberOfLesson { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }
        public int? VouncherId { get; set; }
    }
}
