using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Contact.Manager.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoManager _ContactInfoManager;
        public ContactInfoController(IContactInfoManager ContactInfo)
        {
            _ContactInfoManager = ContactInfo;
        }
        /// <summary>
        /// List
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListContactInfoQueryRequest requestModel)
        {
            var result = await _ContactInfoManager.GetAllContactInfoAsync(requestModel);
            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var requestModel = new GetContactInfoQueryRequest
            {
                Id = id
            };

            var result = await _ContactInfoManager.GetContactInfoAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateContactInfoCommandRequest requestModel)
        {
            var result = await _ContactInfoManager.CreateContactInfoAsync(requestModel);
            if (result == null)
                return NotFound();

            return StatusCode(201, result);
        }
        /// <summary>
        /// Put
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateContactInfoCommandRequest requestModel)
        {
            var result = await _ContactInfoManager.UpdateContactInfoAsync(requestModel);
            if (result == null)
                return NotFound();

            return Ok();
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var requestModel = new DeleteContactInfoCommandRequest
            {
                Id = id
            };

            var result = await _ContactInfoManager.DeleteContactInfoAsync(requestModel);

            if (result == null)
                return NotFound();

            return Ok();
        }
    }
}
