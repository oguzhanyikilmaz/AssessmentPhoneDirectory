using AssessmentPhoneDirectory.Jobservice.BusinessLayer;
using AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.JobService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IRepository _repository;
        /// <summary>
        /// Provide search functions
        /// </summary>
        /// <param name="Repository"></param>
        public JobController(IRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CreateReport([FromQuery] List<ListContactQueryResponse> requestModel)
        {
           bool result= _repository.Create(requestModel);
            if (!result)
                return false;

            return true;
        }
    }
}
