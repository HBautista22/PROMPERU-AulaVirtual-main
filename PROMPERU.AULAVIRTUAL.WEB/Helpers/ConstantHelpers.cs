using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Helpers
{
    public static class ConstantHelpers
    {
        public static readonly Int32 DEFAULT_PAGE_SIZE = 12;

        public static class TIPO_DOCUMENTO
        {
            public const String PDF = "PDF";
            public const String EXCEL = "EXCEL";
            public const String WORD = "WORD";
        }

        public const string Expresion_Estandar = @"[a-zA-Z0-9\s]+";
        public const string Mesage_Expresion_Estandar = "No se aceptan caracteres especiales";

        public static readonly Int32 TIEMPO_ESTADIA_UNIDAD = 1;

        public const String DEFAULT_SERVER_PATH = "~/Content/recursos/"; /* TODO: Modificar cuando esté implementado el ADMIN de cursos "/Content/recursos/";*/

        public static String GET_SERVER_COURSE_RESOUCE_PATH(String CodigoCursoOnline, String UnidadCursoOnlineGUID)
        {
            return System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~"), "Cursos" + "/" + CodigoCursoOnline + "/" + UnidadCursoOnlineGUID);
        }

        public static DateTime GET_FECHA_INICIO_MATRICULA_VIGENTE()
        {
            //TODO: Que esta pasando aca
            return DateTime.Today.AddHours(18).AddMinutes(59).AddSeconds(59).AddDays(-500);
        }

        public static String GET_RELATIVE_COURSE_RESOUCE_PATH(String CodigoCursoOnline, String UnidadCursoOnlineGUID)
        {
            return CodigoCursoOnline;
            //return CodigoCursoOnline + "/" + UnidadCursoOnlineGUID;
        }

        public static class CMI
        {
            public static class CORE
            {
                public const String LESSON_LOCATION = "cmi.core.lesson_location";
                public const String SCORE_RAW = "cmi.core.score.raw";
                public const String LESSON_STATUS = "cmi.core.lesson_status";
            }
        }

        public const String STATUS_PASSED = "passed";
        public const String STATUS_COMPLETED = "completed";

        public static class ROL
        {
            public const String ADMINISTRADOR = "ADM";
            public const String SUPERVISOR = "SUP";
            public const String ALUMNO = "ALU";
            public const String EMPRESA = "EMP";
            public const String ASESOR = "ASE";
            public const string PROFESOR = "PRO";
        }

        public static class ESTADO
        {
            public const String ACTIVO = "ACT";
            public const String INACTIVO = "INA";
            public const String ELIMINADO = "ELI";
            public const String PENDIENTE = "PEN";
        }

        public static class ESTADO_MATRICULA
        {
            public const String COMPLETADO = "FIN";
            public const String PENDIENTE = "PEN";
            public const String APROBADO = "APR";
            public const String DESAPROBADO = "DES";
            public const String INACTIVO = "INA";
        }

        public static class ESTADO_UNIDAD
        {
            public const String COMPLETADO = "FIN";
            public const String PENDIENTE = "PEN";
            public const String ACTIVADO = "ACT";
            public const String INACTIVO = "INA";
        }
        public static class TIPO_PREGUNTA
        {
            public const String SIMPLE = "SIM";
            public const String MULTIPLE = "MUL";
            public const String VERDADERO_FALSO = "VOF";
            public const String COMPLETAR = "COM";
            public const String RELACIONAR = "REL";
        }
        public static class TIPO_UNIDAD
        {
            public const String SCORM = "SCORM";
            public const String TIEMPO = "TIEMPO";


            public const Int32 DB_TEMA = 1;
            public const Int32 DB_SUBTEMA = 2;
            public const Int32 DB_CONTENIDO = 3;

        }
        public static class TIPO_ACCESO
        {
            public const String LOGIN = "LOGIN";
            public const String PORTAL = "PORTAL";
        }

        public static class MENSAJE_CURSO
        {
            public const String EXAMEN = "Usted podrá tomar el examen una vez terminado el curso";
            public const String CARTIFICADO = "Usted podrá descargar el certificado una vez que haya terminado el curso y aprobado el examen";
            public const String MATRICULA = "Usted se esta matriculando en el curso ";
        }
        public static class Layout
        {
            public static readonly String MODAL_LAYOUT_PATH = "~/Views/Shared/_ModalLayout.cshtml";
            public static readonly String MODAL_LAYOUT_PATH_NEW = "~/Views/Shared/_ModalLayoutNew.cshtml";
            public static readonly String MODAL_EMAIL_PATH = "~/Views/Shared/_MailLayout.cshtml";
            public static readonly String DEFAULT_LAYOUT_PATH = "~/Views/Shared/_Layout.cshtml";
        }

        public static class DASHBOARD
        {
            public static readonly Int32 MAX_ITEMS_CURSO = 24;
        }


        public static DateTime MAXDATE = DateTime.MaxValue;

        public static PagedListRenderOptions Bootstrap3Pager
        {
            get
            {



                PagedListRenderOptions link = new PagedListRenderOptions();




                return new PagedListRenderOptions
                {
                    DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                    DisplayLinkToLastPage = PagedListDisplayMode.Always,
                    DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                    DisplayLinkToNextPage = PagedListDisplayMode.Always,
                    DisplayLinkToIndividualPages = true,
                    DisplayPageCountAndCurrentLocation = false,
                    MaximumPageNumbersToDisplay = 5,
                    DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                    EllipsesFormat = "&#8230;",
                    LinkToFirstPageFormat = "<i class='fas fa-angle-double-left'></i>",
                    LinkToPreviousPageFormat = "<i class='fas fa-angle-left'></i>",
                    LinkToIndividualPageFormat = "{0}",
                    LinkToNextPageFormat = "<i class='fas fa-angle-right'></i>",
                    LinkToLastPageFormat = "<i class='fas fa-angle-double-right'></i>",
                    PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                    ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                    FunctionToDisplayEachPageNumber = null,
                    ClassToApplyToFirstListItemInPager = null,
                    ClassToApplyToLastListItemInPager = null,
                    ContainerDivClasses = new[] { "paginador" },
                    //UlElementClasses = new[] { "pagination" },
                    UlElementClasses = Enumerable.Empty<string>(),
                    //LiElementClasses = Enumerable.Empty<string>(),
                    LiElementClasses = Enumerable.Empty<string>(),
                    FunctionToTransformEachPageLink = (li, a) =>
                    {

                        if (a.InnerHtml == "<i class='fas fa-angle-double-left'></i>")
                            a.AddCssClass("btns primero");
                        else if (a.InnerHtml == "<i class='fas fa-angle-double-right'></i>")
                            a.AddCssClass("btns anterior");
                        else if (a.InnerHtml == "<i class='fas fa-angle-right'></i>")
                            a.AddCssClass("btns siguiente");
                        else if (a.InnerHtml == "<i class='fas fa-angle-double-right'></i>")
                            a.AddCssClass("btns ultimo");
                        else
                            a.AddCssClass("pagina");

                       if (li.Attributes.Count()>0 && li.Attributes["class"]== "active")
                            a.AddCssClass("current");

                        li.InnerHtml = a.ToString();
                        return li;

                    },


                };
            }
        }


        public static String TextoRol(String rol)
        {
            switch (rol)
            {
                case ROL.ADMINISTRADOR: return "Administrador";
                case ROL.SUPERVISOR: return "Supervisor";
                case ROL.ALUMNO: return "Alumno";
                case ROL.EMPRESA: return "Empresa";
                case ROL.ASESOR: return "Asesor";
            }
            return "Rol incorrecto";
        }

        public static String TextoEstado(String estado)
        {
            switch (estado)
            {
                case ESTADO_MATRICULA.COMPLETADO: return "Completo";
                case ESTADO_MATRICULA.APROBADO: return "Aprobado";
                case ESTADO_MATRICULA.PENDIENTE: return "Pendiente";
                case ESTADO_MATRICULA.DESAPROBADO: return "Desaprobado";
                case ESTADO_MATRICULA.INACTIVO: return "Inactivo";
            }
            return "Estado incorrecto";
        }

        public static String TextoColorEstado(String estado)
        {
            switch (estado)
            {
                case ESTADO_MATRICULA.COMPLETADO: return "completado";
                case ESTADO_MATRICULA.APROBADO: return "completado";
                case ESTADO_MATRICULA.PENDIENTE: return "pendiente";
                case ESTADO_MATRICULA.DESAPROBADO: return "inactivo";
                case ESTADO_MATRICULA.INACTIVO: return "inactivo";
            }
            return "Estado incorrecto";
        }


        public static String TextoTipoPregunta(String tipo)
        {
            switch (tipo)
            {
                case TIPO_PREGUNTA.SIMPLE: return "Simple";
                case TIPO_PREGUNTA.MULTIPLE: return "Multiple";
                case TIPO_PREGUNTA.VERDADERO_FALSO: return "Verdadero o Falso";
                case TIPO_PREGUNTA.COMPLETAR: return "Completar";
                case TIPO_PREGUNTA.RELACIONAR: return "Relacionar";
            }
            return "Tipo incorrecto";
        }

        public static List<String> ListEstadoMatricula()
        {
            var lstEstadoMatricula = new List<String>();
            lstEstadoMatricula.Add(ESTADO_MATRICULA.PENDIENTE);
            lstEstadoMatricula.Add(ESTADO_MATRICULA.COMPLETADO);
            lstEstadoMatricula.Add(ESTADO_MATRICULA.APROBADO);
            lstEstadoMatricula.Add(ESTADO_MATRICULA.DESAPROBADO);
            lstEstadoMatricula.Add(ESTADO_MATRICULA.INACTIVO);
            return lstEstadoMatricula;
        }

        public static List<String> ListTipoUnidad()
        {
            var listTipoUnidad = new List<String>();
            listTipoUnidad.Add(TIPO_UNIDAD.SCORM);
            listTipoUnidad.Add(TIPO_UNIDAD.TIEMPO);
            return listTipoUnidad;

        }

    }
}