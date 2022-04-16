using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using MediatR;
using System.Collections.Generic;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request
{
    public class ListReportQueryRequest : IRequest<IEnumerable<ListReportQueryResponse>>
    {
        public string GetReportDate { get; set; }
        public string ReportStatus { get; set; }
    }
}
