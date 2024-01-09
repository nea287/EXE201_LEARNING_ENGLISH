using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot
{
    public class UpdateSlotRequest
    {
        public double? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentCourseId { get; set; }
        public bool? IsAttended { get; set; }
    }
}
