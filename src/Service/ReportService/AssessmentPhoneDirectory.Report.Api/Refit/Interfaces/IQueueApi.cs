using AssessmentPhoneDirectory.Report.Api.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Api.Refit.Interfaces
{
    public interface IQueueApi 
    {
        [Post("/Queue/SendQueue")]
        Task<bool> SendQueue([FromBody] List<ListContactQueryResponse> requestModel);

        [Get("/Queue/ConsumeQueue")]
        Task<bool> ConsumeQueue();
    }
}
