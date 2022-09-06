using Microsoft.Reporting.WebForms;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class ReporteLogic_new
    {
        public String ExtensionReporte(String Type)
        {
            switch (Type.ToUpper())
            {
                case "WORD": return ".doc";
                case "EXCEL": return ".xls";
                case "PDF": return ".pdf";
            }
            return "";
        }

        public FileContentResult GenerarReporte(ReportDataSource ReportDataSource, String Type, String Path, Boolean bDateParameter = false, String TituloArchivo = null)
        {

            LocalReport localReport = new LocalReport();
            ReportDataSource rds = ReportDataSource;
           

            
            localReport.ReportPath = Path;

            if (bDateParameter)
            {
                ReportParameter parameter = new ReportParameter();
                parameter = new ReportParameter("DateParameter", DateTime.Now.ToShortDateString());


                localReport.SetParameters(parameter);
            }

            //localReport.DataSources.Clear();
            localReport.DataSources.Add(ReportDataSource);

            switch (Type)
            {
                case ConstantHelpers.TIPO_DOCUMENTO.PDF: Type = "PDF"; break;
                case ConstantHelpers.TIPO_DOCUMENTO.EXCEL: Type = "Excel"; break;
                case ConstantHelpers.TIPO_DOCUMENTO.WORD: Type = "Word"; break;
                default: Type = "Excel"; break;
            }

            string reportType = Type;
            string mimeType;
            string encoding;
            string fileNameExtension;


            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Type + "</OutputFormat>" +
            "  <PageWidth>29.7cm</PageWidth>" +
            "  <PageHeight>21cm</PageHeight>" +
            "  <MarginTop>2cm</MarginTop>" +
            "  <MarginLeft>cm</MarginLeft>" +
            "  <MarginRight>0cm</MarginRight>" +
            "  <MarginBottom>0cm</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            FileContentResult File = new FileContentResult(renderedBytes, mimeType) { FileDownloadName = TituloArchivo + ExtensionReporte(Type) };
            return File;

        }

        public FileContentResult GenerarReporte(List<ReportDataSource> LstReportDataSource, String Type, String Path, List<ReportParameter> LstParametros, Boolean bDateParameter = false, String TituloArchivo = null)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Path;

            if (bDateParameter)
            {
                ReportParameter parameter = new ReportParameter();
                parameter = new ReportParameter("DateParameter", DateTime.Now.ToString("dd/MM/yyyy"));

                localReport.SetParameters(parameter);

            }

            
            
            if (LstParametros.Count > 0)
                foreach (var parametro in LstParametros)
                    localReport.SetParameters(parametro);

            
            if (LstReportDataSource.Count > 0)
                foreach (var rpt in LstReportDataSource)
                    localReport.DataSources.Add(rpt);

            switch (Type)
            {
                case ConstantHelpers.TIPO_DOCUMENTO.PDF: Type = "PDF"; break;
                case ConstantHelpers.TIPO_DOCUMENTO.EXCEL: Type = "Excel"; break;
                case ConstantHelpers.TIPO_DOCUMENTO.WORD: Type = "Word"; break;
                default: Type = "Excel"; break;
            }

            string reportType = Type;
            string mimeType;
            string encoding;
            string fileNameExtension;


            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Type + "</OutputFormat>" +
            "  <PageWidth>21cm</PageWidth>" +
            "  <PageHeight>27.7cm</PageHeight>" +
            "  <MarginTop>0cm</MarginTop>" +
            "  <MarginLeft>cm</MarginLeft>" +
            "  <MarginRight>0cm</MarginRight>" +
            "  <MarginBottom>0cm</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            FileContentResult File = new FileContentResult(renderedBytes, mimeType) { FileDownloadName = TituloArchivo + ExtensionReporte(Type) };
            return File;

        }

        //    public Byte[] GenerarReporteByte(ReportDataSource ReportDataSource, String Type, String Path, String NombreArchivo)
        //    {
        //        return GenerarReporteByte(ReportDataSource, Type, Path, NombreArchivo, null);
        //    }
        //    public Byte[] GenerarReporteByte(ReportDataSource ReportDataSource, String Type, String Path, String NombreArchivo, List<ReportParameter> ReportParameters)
        //    {

        //        LocalReport localReport = new LocalReport();
        //        localReport.ReportPath = Path;

        //        if (ReportParameters != null)
        //        {
        //            localReport.SetParameters(ReportParameters);
        //        }

        //        localReport.DataSources.Add(ReportDataSource);

        //        string reportType = Type;
        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;
        //        string deviceInfo =

        //        "<DeviceInfo>" +
        //        "  <OutputFormat>" + Type + "</OutputFormat>" +
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
        //        return File.FileContents;
        //    }
    }
}