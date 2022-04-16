using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Common;
using MediatR;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request
{
    public class DeleteReportCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
    }
}
