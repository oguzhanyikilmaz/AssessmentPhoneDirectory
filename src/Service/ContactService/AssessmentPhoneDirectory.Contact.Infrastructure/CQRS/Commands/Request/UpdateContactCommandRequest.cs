using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Contact.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request
{
   public class UpdateContactCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
