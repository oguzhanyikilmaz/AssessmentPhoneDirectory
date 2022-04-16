using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using MediatR;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request
{
    public class GetReportQueryRequest:IRequest<ReportQueryResponse>
    {
        public string Id { get; set; }
    }
}
