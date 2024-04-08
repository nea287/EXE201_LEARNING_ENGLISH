using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
            Vounchers = new HashSet<Vouncher>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Vouncher> Vounchers { get; set; }
    }
}
