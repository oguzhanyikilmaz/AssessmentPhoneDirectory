using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Jobservice.BusinessLayer.Helpers
{
   public static class CHelper
    {
        public static string GetPath()
        {
            var file = Directory.GetParent("~\response.xls");
            var path = file.Parent + $@"\AssessmentPhoneDirectory.Jobservice.BusinessLayer\response.xls";
            return path;
        }

        public static List<ListContactInfoQueryResponse>  GetContactInfo(List<ListContactQueryResponse> entity)
        {
            List<ListContactInfoQueryResponse> contactInfoQueryResponses = new List<ListContactInfoQueryResponse>();

            foreach (var contact in entity)
            {
                contactInfoQueryResponses.AddRange(contact.ContactInfos.ToList());
            }
            return contactInfoQueryResponses;
        }

        public static int GetContactInfoGroupByLocationCount(List<ListContactInfoQueryResponse> contactInfoQueryResponses,string key)
        {
           return contactInfoQueryResponses.Count(x => x.InfoDescription ==key.ToString());
        }

        public static int GetContactInfoGroupByPhoneCount(List<ListContactInfoQueryResponse> contactInfoQueryResponses, string key)
        {
            var contactIds = contactInfoQueryResponses.Where(x => x.InfoType.ToLower() == "telefon").Select(z => z.ContactId);

            return contactInfoQueryResponses.Count(x => contactIds.Contains(x.ContactId) && x.InfoDescription == key.ToString());
        }
    }
}
