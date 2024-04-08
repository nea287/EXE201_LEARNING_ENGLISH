using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonElement("CourseId")]
        public int CourseId { get; set; }
        [BsonElement("CourseName")]
        public string CourseName { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }

    }
}
