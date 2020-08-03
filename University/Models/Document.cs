using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContosoUniversity.Models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DocumentID { get; set; }

        [BsonElement("Title")]
        public string DocumentTitle { get; set; }

        public int DocumentDescription { get; set; }

        public DateTime DateTime { get; set; }
    }
}

