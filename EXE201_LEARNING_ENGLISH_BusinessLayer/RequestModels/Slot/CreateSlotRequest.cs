using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot
{
    public class CreateSlotRequest
    {
        public double? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentCourseId { get; set; }
        public bool? IsAttended { get; set; }
    }
}
