using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels
{
    public class CartFilter
    {
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public decimal? Price { get; set; }
    }
}
