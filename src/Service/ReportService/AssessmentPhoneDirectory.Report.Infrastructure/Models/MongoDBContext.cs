using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AssessmentPhoneDirectory.Report.Infrastructure.Models
{
    public class MongoDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            var connectionString = _configuration.GetValue<string>("MongoDBConfiguration:ConnectionString");
            var db = _configuration.GetValue<string>("MongoDBConfiguration:Database");
            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(db);
        }

        public IMongoCollection<Report> Report => _mongoDatabase.GetCollection<Report>(nameof(Report));
       
    }
}
