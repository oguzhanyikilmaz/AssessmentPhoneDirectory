using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response;
using AssessmentPhoneDirectory.Contact.Manager.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Manager.Concrete
{
    public class ContactInfoManager : IContactInfoManager
    {
        private readonly IMediator _mediator;
        public ContactInfoManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateContactInfoCommandResponse> CreateContactInfoAsync(CreateContactInfoCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteContactInfoAsync(DeleteContactInfoCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListContactInfoQueryResponse>> GetAllContactInfoAsync(ListContactInfoQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<ContactInfoQueryResponse> GetContactInfoAsync(GetContactInfoQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateContactInfoAsync(UpdateContactInfoCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
