using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Request
{
   public class ListContactQueryRequest : IRequest<IEnumerable<ListContactQueryResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
