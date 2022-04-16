using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Jobservice.BusinessLayer
{
    public class ListContactInfoQueryResponse
    {
        public string Id { get; set; }
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string InfoDescription { get; set; }
    }
}
