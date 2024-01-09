using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Vouncher
{
    public class CreateVouncherRequest
    {
        [Required]
        public int VouncherId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Status { get; set; }
        public string? VouncherName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
    }
}
