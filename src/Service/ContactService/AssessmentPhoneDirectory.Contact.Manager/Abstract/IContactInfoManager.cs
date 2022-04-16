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
   public interface IContactInfoManager
    {
        Task<IEnumerable<ListContactInfoQueryResponse>> GetAllContactInfoAsync(ListContactInfoQueryRequest requestModel);
        Task<ContactInfoQueryResponse> GetContactInfoAsync(GetContactInfoQueryRequest requestModel);
        Task<CreateContactInfoCommandResponse> CreateContactInfoAsync(CreateContactInfoCommandRequest requestModel);
        Task<EmptyResponse?> UpdateContactInfoAsync(UpdateContactInfoCommandRequest requestModel);
        Task<EmptyResponse?> DeleteContactInfoAsync(DeleteContactInfoCommandRequest requestModel);
    }
}
