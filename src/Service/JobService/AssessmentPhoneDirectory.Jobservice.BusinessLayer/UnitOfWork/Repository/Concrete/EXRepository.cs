using System;
using System.Collections.Generic;
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
                _xlWorkSheet.Cells[1, 1] = "FirstName";
                _xlWorkSheet.Cells[1, 2] = "LastName";
                _xlWorkSheet.Cells[1, 3] = "Company";
                _xlWorkSheet.Cells[1, 4] = "InfoType";
                _xlWorkSheet.Cells[1, 4] = "InfoDescription";

                for (int i = 1; i <= entity[i].ContactInfos.Count(); i++)
                {
                    foreach (var contactInfo in entity[i].ContactInfos)
                    {
                        _xlWorkSheet.Cells[i, 1] = entity[i].FirstName;
                        _xlWorkSheet.Cells[i, 2] = entity[i].LastName;
                        _xlWorkSheet.Cells[i, 3] = entity[i].Company;
                        _xlWorkSheet.Cells[i, 4] = contactInfo.InfoType;
                        _xlWorkSheet.Cells[i, 5] = contactInfo.InfoDescription;
                    }
                }

                _xlWorkBook.SaveAs("D:\\deneme-dosya.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, _misValue, _misValue, _misValue, _misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, _misValue, _misValue, _misValue, _misValue, _misValue);
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
