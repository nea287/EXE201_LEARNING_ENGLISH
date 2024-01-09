using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class CategoryFilter
    {
        public CategoryFilter()
        {
            Courses = new HashSet<CourseFilter>();
            Vounchers = new HashSet<VouncherFilter>();
        }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<CourseFilter>? Courses { get; set; }
        public ICollection<VouncherFilter>? Vounchers { get; set; }
    }
}
