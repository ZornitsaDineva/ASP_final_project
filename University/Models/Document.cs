using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite.Internal.PatternSegments;
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
        [Required]
        [Display(Name = "Title")]
        public string DocumentTitle { get; set; }

        [Display(Name = "Description")]
        public string DocumentDescription { get; set; }

        [Display(Name = "Creation Timestamp")]
        [Required]
        public DateTime DateTime { get; set; }

        public byte[] Content { get; set; }

        [BsonIgnore]
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string Checksum { get; set; }
    }
}

