using AssessmentPhoneDirectory.Jobservice.BusinessLayer.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Jobservice.BusinessLayer.UnitOfWork.Repository.Concrete
{
    public class EXRepository : IRepository
    {
        private readonly Microsoft.Office.Interop.Excel.Application _xlApp;
        private readonly Microsoft.Office.Interop.Excel.Workbook _xlWorkBook;
        private readonly Microsoft.Office.Interop.Excel.Worksheet _xlWorkSheet;
        object _misValue = null;

        public EXRepository()
        {
            _misValue = System.Reflection.Missing.Value;
            _xlApp = new Microsoft.Office.Interop.Excel.Application();
            _xlWorkBook = _xlApp.Workbooks.Add(_misValue);
            _xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(1);
        }

        public bool Create(List<ListContactQueryResponse> entity)
        {
            try
            {
                _xlWorkSheet.Cells[1, 1] = "Konum Bilgisi";
                _xlWorkSheet.Cells[1, 2] = "Konumdaki Kişi Sayısı";
                _xlWorkSheet.Cells[1, 3] = "Konumdaki Telefon Numarası Sayısı";


                var contactInfoQueryResponses = CHelper.GetContactInfo(entity);

                var groupByContactInfos = contactInfoQueryResponses.Where(x => x.InfoType.ToLower() == "konum").GroupBy(z=>z.InfoDescription).ToList();

                for (int i = 1; i <= groupByContactInfos.Count(); i++)
                {
                    _xlWorkSheet.Cells[i+1, 1] = groupByContactInfos[i-1].Key.ToString();
                    _xlWorkSheet.Cells[i + 1, 2] = CHelper.GetContactInfoGroupByLocationCount(contactInfoQueryResponses, groupByContactInfos[i - 1].Key.ToString());
                    _xlWorkSheet.Cells[i+1, 3] = CHelper.GetContactInfoGroupByPhoneCount(contactInfoQueryResponses, groupByContactInfos[i - 1].Key.ToString());
                }

                string path = CHelper.GetPath();

                _xlWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, _misValue, _misValue, _misValue, _misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, _misValue, _misValue, _misValue, _misValue, _misValue); ;
                _xlWorkBook.Close(true, _misValue, _misValue);
                _xlApp.Quit();

                Marshal.ReleaseComObject(_xlWorkSheet);
                Marshal.ReleaseComObject(_xlWorkBook);
                Marshal.ReleaseComObject(_xlApp);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
