using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Contact.Infrastructure.Models
{
   public class Contact:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
