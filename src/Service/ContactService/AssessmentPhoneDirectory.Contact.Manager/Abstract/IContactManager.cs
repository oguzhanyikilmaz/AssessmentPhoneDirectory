using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Manager.Abstract
{
    public interface IContactManager
    {
        Task<IEnumerable<ListContactQueryResponse>> GetAllContactAsync(ListContactQueryRequest requestModel);
        Task<ContactQueryResponse> GetContactAsync(GetContactQueryRequest requestModel);
        Task<CreateContactCommandResponse> CreateContactAsync(CreateContactCommandRequest requestModel);
        Task<EmptyResponse?> UpdateContactAsync(UpdateContactCommandRequest requestModel);
        Task<EmptyResponse?> DeleteContactAsync(DeleteContactCommandRequest requestModel);
    }
}
