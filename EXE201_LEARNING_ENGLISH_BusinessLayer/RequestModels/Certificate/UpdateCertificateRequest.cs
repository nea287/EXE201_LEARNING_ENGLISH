using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Certificate
{
    public class UpdateCertificateRequest
    {
        [Required]
        public string CertificateName { get; set; }
        [Required]
        public string Image { get; set; }
        public int? TeacherId { get; set; }
        public int? Status { get; set; }
    }
}
