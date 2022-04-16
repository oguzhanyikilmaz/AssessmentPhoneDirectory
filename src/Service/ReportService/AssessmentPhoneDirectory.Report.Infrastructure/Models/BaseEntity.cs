using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AssessmentPhoneDirectory.Report.Infrastructure.Models
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string IsActive { get; set; }
    }
}
