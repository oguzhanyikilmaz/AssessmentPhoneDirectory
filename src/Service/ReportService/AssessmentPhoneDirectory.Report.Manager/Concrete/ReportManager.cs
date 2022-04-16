using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using AssessmentPhoneDirectory.Report.Manager.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Manager.Concrete
{
    public class ReportManager : IReportManager
    {
        private readonly IMediator _mediator;
        public ReportManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateReportCommandResponse> CreateReportAsync(CreateReportCommandRequest requestModel)
        {
            var model = await _mediator.Send(requestModel);

            return model;
        }

        public async Task<IEnumerable<ListReportQueryResponse>> GetAllReportAsync(ListReportQueryRequest requestModel)
        {
            var response = new List<ListReportQueryResponse>();
            var report = await _mediator.Send(requestModel);

            foreach (var item in report)
            {
                ListReportQueryResponse reportResult = item;
                response.Add(reportResult);
            }

            return response;
        }

        public async Task<ReportQueryResponse> GetReportAsync(GetReportQueryRequest requestModel)
        {
            var report = await _mediator.Send(requestModel);

            return report;
        }

        public async Task<EmptyResponse?> UpdateReportAsync(UpdateReportCommandRequest requestModel)
        {     
            return await _mediator.Send(requestModel);
        }
        public async Task<EmptyResponse?> DeleteReportAsync(DeleteReportCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

    }
}
