//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class ExcelLogic
    {

        //    public static DataTable ExtractExcelToDataTable(HttpPostedFileBase file)
        //    {

        //        file.InputStream.Position = 0;

        //        String fileExtension = System.IO.Path.GetExtension(file.FileName);

        //        if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //        {
        //            IExcelDataReader excelReader = null;

        //            if (fileExtension == ".xls")
        //                excelReader = ExcelReaderFactory.CreateBinaryReader(file.InputStream);
        //            else if (fileExtension == ".xlsx")
        //                excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);

        //            DataSet result = excelReader.AsDataSet();

        //            file.InputStream.Position = 0;
        //            return result.Tables[0];
        //        }

        //        return new DataTable();


        //        /*DataTable dataTable = new DataTable();
        //        SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(inputStream, false);
        //        WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
        //        IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
        //        string relationshipId = sheets.First().Id.Value;
        //        WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
        //        Worksheet workSheet = worksheetPart.Worksheet;
        //        SheetData sheetData = workSheet.GetFirstChild<SheetData>();
        //        IEnumerable<Row> rows = sheetData.Descendants<Row>();

        //        foreach (Cell cell in rows.ElementAt(0))
        //        {
        //            dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
        //        }

        //        foreach (Row row in rows)
        //        {
        //            var countEmptyCells = 0;
        //            DataRow dataRow = dataTable.NewRow();
        //            for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
        //            {
        //                if (!String.IsNullOrEmpty(row.Descendants<Cell>().ElementAt(i).InnerText))
        //                {
        //                    dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
        //                }
        //                else { countEmptyCells += 1; }
        //            }
        //            if (countEmptyCells == row.Descendants<Cell>().Count())
        //            { break; }
        //            dataTable.Rows.Add(dataRow);
        //        }

        //        return dataTable;*/
        //    }
        //    /*
        //    private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        //    {
        //        SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
        //        string value = cell.CellValue.InnerXml;

        //        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        //        {
        //            return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
        //        }
        //        else
        //        {
        //            return value;
        //        }
        //    }*/
    }
}