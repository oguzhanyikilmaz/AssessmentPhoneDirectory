using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateContactCommandRequest, AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>();
            CreateMap<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact, ListContactQueryResponse>();
            CreateMap<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact, ContactQueryResponse>();

            CreateMap<CreateContactInfoCommandRequest, AssessmentPhoneDirectory.Contact.Infrastructure.Models.ContactInfo>();
            CreateMap<AssessmentPhoneDirectory.Contact.Infrastructure.Models.ContactInfo, ListContactInfoQueryResponse>();
            CreateMap<AssessmentPhoneDirectory.Contact.Infrastructure.Models.ContactInfo, ContactInfoQueryResponse>();
        }
    }
}
