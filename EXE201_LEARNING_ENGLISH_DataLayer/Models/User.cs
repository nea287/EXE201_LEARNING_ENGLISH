using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("SenderId")]
        public string SenderId { get; set; }

        [BsonElement("ReceiverId")]
        public string ReceiverId { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("Status")]
        public int Status { get; set; }
    }
}