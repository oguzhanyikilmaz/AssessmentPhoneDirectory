using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.QueueService.Api.Models
{
    public class ListContactQueryResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public IEnumerable<ListContactInfoQueryResponse> ContactInfos { get; set; }
    }
}
