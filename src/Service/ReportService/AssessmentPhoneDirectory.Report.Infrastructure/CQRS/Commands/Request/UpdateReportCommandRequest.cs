using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Common;
using MediatR;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request
{
    public class UpdateReportCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string GetReportDate { get; set; }
        public string ReportStatus { get; set; }
    }
}
