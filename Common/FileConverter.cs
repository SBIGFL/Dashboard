using CsvHelper;
using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Data;
using System.Formats.Asn1;
using System.IO;

public class FileConverter
{
    // Convert CSV stream to DataTable
    //public static DataTable ConvertCsvToDataTable(Stream csvStream)
    //{
    //    DataTable dataTable = new DataTable();
    //    using (var streamReader = new StreamReader(csvStream))
    //    {
    //        using (var csvReader = CsvReader.Create(streamReader))
    //        {
    //            dataTable.Load(csvReader);
    //        }
    //    }
    //    return dataTable;
    //}

    // Convert XLS stream to DataTable
    public static DataTable ConvertXlsToDataTable(Stream xlsStream)
    {
        DataTable dataTable = new DataTable();
        using (var excelReader = ExcelReaderFactory.CreateReader(xlsStream))
        {
            var dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            dataTable = dataSet.Tables[0];
        }
        return dataTable;
    }

    // Convert CSV stream to XLSX stream
    public static void ConvertCsvToXlsx(Stream csvStream, Stream xlsxStream)
    {
        using (var package = new ExcelPackage(xlsxStream))
        {
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            using (var reader = new StreamReader(csvStream))
            {
                int row = 1;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        worksheet.Cells[row, i + 1].Value = values[i];
                    }
                    row++;
                }
            }
            package.Save();
        }
    }
    public static void ConvertXlsToXlsx(Stream xlsStream, Stream xlsxStream)
    {
        // Load the XLS file using ExcelDataReader
        using (var excelReader = ExcelReaderFactory.CreateReader(xlsStream))
        {
            var dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            // Create a new ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Copy data from the XLS DataSet to the XLSX worksheet
                foreach (DataTable table in dataSet.Tables)
                {
                    for (int row = 0; row < table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            // Add data to the worksheet
                            worksheet.Cells[row + 1, col + 1].Value = table.Rows[row][col];
                        }
                    }
                }

                // Save the ExcelPackage to the output stream
                package.SaveAs(xlsxStream);
            }
        }
    }
}
