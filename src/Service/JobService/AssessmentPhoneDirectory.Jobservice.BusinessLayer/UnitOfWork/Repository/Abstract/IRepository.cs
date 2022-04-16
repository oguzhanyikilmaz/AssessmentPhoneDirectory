using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Repository
{
    public interface IRepository 
    {
        bool Create(List<ListContactQueryResponse> entity);

    }
}
