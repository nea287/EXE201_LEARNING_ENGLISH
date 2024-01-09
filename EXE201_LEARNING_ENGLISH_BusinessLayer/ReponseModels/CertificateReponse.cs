using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class CertificateReponse
    {
        public int? CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public string? Image { get; set; }
        public int? TeacherId { get; set; }
        public int? Status { get; set; }

    }
}