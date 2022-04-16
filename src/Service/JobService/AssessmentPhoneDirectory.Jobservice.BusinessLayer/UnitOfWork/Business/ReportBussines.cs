using AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Repository;
using AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Business
{
   public class ReportBussines
    {
        private IRepository _reportRepository;
        public ReportBussines()
        {
            _reportRepository = new EXRepository();
        }

        public bool CreateExcel(List<ListContactQueryResponse> entity)
        {
           bool response= _reportRepository.Create(entity);

            return response;
        }
    }
}
