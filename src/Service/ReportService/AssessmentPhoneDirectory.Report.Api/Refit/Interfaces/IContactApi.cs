using AssessmentPhoneDirectory.Report.Api.Models.Request;
using AssessmentPhoneDirectory.Report.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Api
{
    [Headers("Content-Type: application/json; charset=UTF-8")]
    public interface IContactApi
    {
        [Post("/Contact/ContactAll")]
        Task<IEnumerable<ListContactQueryResponse>> ContactAll([FromBody] ListContactQueryRequest requestModel);    
    }
}
