//using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class ReporteLogic
    {
        //    public FileContentResult GenerarReporte(ReportDataSource ReportDataSource, String Type, String Path)
        //    {

        //        LocalReport localReport = new LocalReport();
        //        localReport.ReportPath = Path;
        //        localReport.DataSources.Add(ReportDataSource);
        //        Type = Type == "PDF" ? "PDF" : "Excel";

        //        string reportType = Type;
        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;


        //        string deviceInfo =

        //        "<DeviceInfo>" +
        //        "  <OutputFormat>" + Type + "</OutputFormat>" +
        //        "  <PageWidth>21cm</PageWidth>" +
        //        "  <PageHeight>27.7cm</PageHeight>" +
        //        "  <MarginTop>0cm</MarginTop>" +
        //        "  <MarginLeft>cm</MarginLeft>" +
        //        "  <MarginRight>0cm</MarginRight>" +
        //        "  <MarginBottom>0cm</MarginBottom>" +
        //        "</DeviceInfo>";

        //        Warning[] warnings;
        //        string[] streams;
        //        byte[] renderedBytes;

        //        renderedBytes = localReport.Render(
        //            reportType,
        //            deviceInfo,
        //            out mimeType,
        //            out encoding,
        //            out fileNameExtension,
        //            out streams,
        //            out warnings);

        //        FileContentResult File = new FileContentResult(renderedBytes, mimeType);
        //        return File;

        //    }
    }
}