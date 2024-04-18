using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ExportRepository
    {
        public byte[] ExportToExcelAsync(DataTable data)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Specify the starting cell where you want to load the data (e.g., A1)
                        string startCellAddress = "A1";

                        // Load data from the DataTable into the specified range
                        worksheet.Cells[startCellAddress].LoadFromDataTable(data, true);

                        // Save the Excel package to a MemoryStream
                        var memoryStream = new MemoryStream();
                        package.SaveAs(memoryStream);

                        // Get the byte array from the MemoryStream
                        byte[] excelBytes = memoryStream.ToArray();

                        return excelBytes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] ExportToPdfAsync(DataTable data)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Specify the starting cell where you want to load the data (e.g., A1)
                        string startCellAddress = "A1";

                        // Load data from the DataTable into the specified range
                        worksheet.Cells[startCellAddress].LoadFromDataTable(data, true);

                        // Save the Excel package to a MemoryStream
                        var memoryStream = new MemoryStream();
                        package.SaveAs(memoryStream);

                        // Get the byte array from the MemoryStream
                        byte[] excelBytes = memoryStream.ToArray();

                        return excelBytes;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string? DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
