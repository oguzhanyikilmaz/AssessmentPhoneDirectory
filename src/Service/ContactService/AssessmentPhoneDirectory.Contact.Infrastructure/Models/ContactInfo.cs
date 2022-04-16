using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.Models
{
   public  class ContactInfo:BaseEntity
    {
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string InfoDescription { get; set; }
    }
}
