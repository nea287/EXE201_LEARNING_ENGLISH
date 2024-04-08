using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Certificate
    {
        public int CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public string? Image { get; set; }
        public int? TeacherId { get; set; }
        public int? Status { get; set; }

        public virtual Teacher? Teacher { get; set; }
    }
}
