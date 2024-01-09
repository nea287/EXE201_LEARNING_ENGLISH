using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Feedback
{
    public class UpdateFeedbackRequest
    {
        [Required]
        public string Title { get; set; }
        public string? Content { get; set; }
        public double? Grade { get; set; }
        public int? SlotId { get; set; }
    }
}
