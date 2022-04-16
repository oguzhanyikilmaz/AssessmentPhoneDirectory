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
    public class ContactManager : IContactManager
    {
        private readonly IMediator _mediator;
        public ContactManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateContactCommandResponse> CreateContactAsync(CreateContactCommandRequest requestModel)
        {
            var model = await _mediator.Send(requestModel);
            foreach (var contactInfo in requestModel.ContactInfos)
            {

                var request = new CreateContactInfoCommandRequest
                {
                    ContactId = model.Id,
                    InfoType = contactInfo.InfoType,
                    InfoDescription=contactInfo.InfoDescription
                };
                await _mediator.Send(request);
            }

            return model;
        }

        public async Task<IEnumerable<ListContactQueryResponse>> GetAllContactAsync(ListContactQueryRequest requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.FirstName))
            {
                //return await _mediator.Send(new searchtable);
            }
            var response = new List<ListContactQueryResponse>();
            var Contact = await _mediator.Send(requestModel);

            foreach (var item in Contact)
            {
                var ContactInfoQueryRequest = new ListContactInfoQueryRequest { ContactId = item.Id };
                var contactInfosresponse = await _mediator.Send(ContactInfoQueryRequest);
                ListContactQueryResponse Contactresult = item;
                Contactresult.ContactInfos = contactInfosresponse;

                response.Add(Contactresult);
            }

            return response;
        }

        public async Task<ContactQueryResponse> GetContactAsync(GetContactQueryRequest requestModel)
        {
            var contact = await _mediator.Send(requestModel);

            var contactInfoQueryRequest = new ListContactInfoQueryRequest { ContactId = requestModel.Id };
            var contactInfosresponse = await _mediator.Send(contactInfoQueryRequest);
            contact.ContactInfos = contactInfosresponse;

            return contact;
        }

        public async Task<EmptyResponse?> UpdateContactAsync(UpdateContactCommandRequest requestModel)
        {
            var request = new DeleteContactInfoCommandRequest
            {
                ContactId = requestModel.Id
            };

            await _mediator.Send(request);

            foreach (var contactInfo in requestModel.ContactInfos)
            {

                var requestContactInfo = new CreateContactInfoCommandRequest
                {
                    ContactId = requestModel.Id,
                    InfoType = contactInfo.InfoType,
                    InfoDescription = contactInfo.InfoDescription
                };
                await _mediator.Send(requestContactInfo);
            }

            return await _mediator.Send(requestModel);
        }
        public async Task<EmptyResponse?> DeleteContactAsync(DeleteContactCommandRequest requestModel)
        {
            var requestContactInfo = new DeleteContactInfoCommandRequest
            {
                ContactId = requestModel.Id
            };

            await _mediator.Send(requestContactInfo);

            return await _mediator.Send(requestModel);
        }

    }
}
