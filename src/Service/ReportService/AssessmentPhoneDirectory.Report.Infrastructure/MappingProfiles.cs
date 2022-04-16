using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using AutoMapper;

namespace AssessmentPhoneDirectory.Report.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateReportCommandRequest, AssessmentPhoneDirectory.Report.Infrastructure.Models.Report>();
            CreateMap<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report, ListReportQueryResponse>();
            CreateMap<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report, ReportQueryResponse>();
        }
    }
}
