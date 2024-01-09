using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class CategoryReponse
    {
        public CategoryReponse()
        {
            Courses = new HashSet<CourseReponse>();
            Vounchers = new HashSet<VouncherReponse>();
        }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<CourseReponse>? Courses { get; set; }
        public ICollection<VouncherReponse>? Vounchers { get; set; }
    }
}
