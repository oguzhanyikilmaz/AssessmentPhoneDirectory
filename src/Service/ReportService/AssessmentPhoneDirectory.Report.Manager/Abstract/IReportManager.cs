using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Manager.Abstract
{
    public interface IReportManager
    {
        Task<IEnumerable<ListReportQueryResponse>> GetAllReportAsync(ListReportQueryRequest requestModel);
        Task<ReportQueryResponse> GetReportAsync(GetReportQueryRequest requestModel);
        Task<CreateReportCommandResponse> CreateReportAsync(CreateReportCommandRequest requestModel);
        Task<EmptyResponse?> UpdateReportAsync(UpdateReportCommandRequest requestModel);
        Task<EmptyResponse?> DeleteReportAsync(DeleteReportCommandRequest requestModel);
    }
}
