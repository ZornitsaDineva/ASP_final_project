using System;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Title")]
        public string DocumentTitle { get; set; }

        [Display(Name = "Description")]
        public string DocumentDescription { get; set; }

        [Display(Name = "Creation Timestamp")]
        public DateTime DateTime { get; set; }
    }
}

