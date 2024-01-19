using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers
{
    public class ExcelFileCreator
    {
        private static ExcelFileCreator instance = null;
        public static ExcelFileCreator Instance
        {
            get { return instance ?? (instance = new ExcelFileCreator()); }
        }

        public bool CreateExcelFile<TEntity>(IQueryable<TEntity> entities, string filePath)
        {
            try
            {
                int row = 1;
                int col = 1;
                int start = 0;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    foreach (var entity in entities)
                    {
                        var properties = entity.GetType().GetProperties();

                        foreach (var property in properties)
                        {
                            row = 1;
                            if (start < 1)
                            {

                                worksheet.Cells[row++, col].Value = property.Name;
                            }
                            worksheet.Cells[row, col++].Value = property.GetValue(property);
                        }
                        start++;
                        col = 1;
                    }

                    var exelFile = new System.IO.FileInfo(filePath);
                    package.SaveAs(exelFile);
                }

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
