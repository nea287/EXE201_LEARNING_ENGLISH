﻿using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers
{
    public class ExcelFileCreator
    {
        private static ExcelFileCreator instance = null;
        public static ExcelFileCreator Instance
        {
            get { return instance ?? (instance = new ExcelFileCreator()); }
        }

        public bool CreateExcelFile<TEntity>(ICollection<TEntity> entities, string filePath)
        {
            try
            {
                int row = 1;
                int col = 1;
                int start = 0;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // hoặc LicenseContext.Commercial

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    foreach (var entity in entities)
                    {
                        var properties = entity.GetType().GetProperties();
                        
                        foreach (var property in properties)
                        {
                            var getValue = property.GetValue(entity);

                            int grow = row;
                            if (start < 1)
                            {

                                worksheet.Cells[grow++, col].Value = property.Name;
                            }
                            

                            if (getValue is (DateTime))
                            {
                                worksheet.Cells[grow, col++].Value = Convert.ToString(property.GetValue(entity));

                            }
                            else
                            {
                                worksheet.Cells[grow, col++].Value = property.GetValue(entity);
                            }

                            
                        }
                        start++;
                        col = 1;

                        if (start < 2) row += 1;
                        row += 1;
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
