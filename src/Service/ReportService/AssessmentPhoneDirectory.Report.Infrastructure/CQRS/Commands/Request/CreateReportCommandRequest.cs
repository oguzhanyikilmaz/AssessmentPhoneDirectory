using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Response;
using MediatR;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request
{
    public class CreateReportCommandRequest : IRequest<CreateReportCommandResponse>
    {
        public string GetReportDate { get; set; }
        public string ReportStatus { get; set; }
    }
}
