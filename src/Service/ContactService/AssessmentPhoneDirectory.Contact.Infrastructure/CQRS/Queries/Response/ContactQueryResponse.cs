using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response
{
    public class ContactQueryResponse
    {
        public ContactQueryResponse()
        {
            ContactInfos = new List<ListContactInfoQueryResponse>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public IEnumerable<ListContactInfoQueryResponse> ContactInfos { get; set; }
    }
}
