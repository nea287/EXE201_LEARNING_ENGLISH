using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public double? Grade { get; set; }
        public int? SlotId { get; set; }

        public virtual Slot? Slot { get; set; }
    }
}
