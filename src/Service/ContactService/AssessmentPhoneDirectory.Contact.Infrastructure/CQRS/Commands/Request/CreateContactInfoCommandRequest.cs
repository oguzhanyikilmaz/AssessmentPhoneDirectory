using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request
{
   public class CreateContactInfoCommandRequest : IRequest<CreateContactInfoCommandResponse>
    {
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string InfoDescription { get; set; }
    }
}
