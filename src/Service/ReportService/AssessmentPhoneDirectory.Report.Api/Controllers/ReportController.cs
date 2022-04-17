using AssessmentPhoneDirectory.Report.Api.Models.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Report.Manager.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AssessmentPhoneDirectory.Report.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportManager _reportManager;
        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="ReportManager"></param>
        public ReportController(IReportManager reportManager)
        {
            _reportManager = reportManager;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateReport([FromQuery] ListContactQueryRequest requestModel)
        {
            var resultContactReport = await  RefitApiServiceDependency.ContactApi.List(requestModel);

            if (resultContactReport != null && resultContactReport.Count() > 0)
            {
                CreateReportCommandRequest createReportCommandRequest = new CreateReportCommandRequest()
                {
                    GetReportDate = DateTime.Now.ToString("U"),
                    ReportStatus = "Başladı.",
                };
                var result = await _reportManager.CreateReportAsync(createReportCommandRequest);

                var resultExcelReport = await RefitApiServiceDependency.JobApi.CreateReport(resultContactReport.ToList());

                UpdateReportCommandRequest updateReportCommandRequest = new UpdateReportCommandRequest()
                {
                    Id = result.Id,
                    ReportStatus = "Hazırlanıyor."
                };

                if (!resultExcelReport)
                {
                    updateReportCommandRequest.ReportStatus = "Hata Alındı.";
                    var resultUpdateReportCommandRequestError = await _reportManager.UpdateReportAsync(updateReportCommandRequest);
                    return NotFound();
                }

                var resultUpdateReportCommandRequestStart = await _reportManager.UpdateReportAsync(updateReportCommandRequest);

                updateReportCommandRequest.ReportStatus = "Tamamlandı.";
                var resultUpdateReportCommandRequestCompleted = await _reportManager.UpdateReportAsync(updateReportCommandRequest);
                return StatusCode(201, result);

            }
            else
            {
                return NotFound();
            }
        }
    }
}
