namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response
{
    public class ListContactInfoQueryResponse
    {
        public string Id { get; set; }
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string InfoDescription { get; set; }
    }
}