using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using PROMPERU.AULAVIRTUAL.WEB.Models;
using System.IO;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BL;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public enum TipoDocumento { Constancia, Certificado, Diploma }
    
    public class Documento
    {
        public TipoDocumento Tipo { get; set; }
        public Usuario Usuario { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public DateTime FechaFin { get; set; }
        public Decimal Nota { get; set; }
        public Int32 MatriculaCursoOnlineId { get; set; }

        public Documento()
        {
        }
    }

    public class DocumentsLogic
    {
        protected CursosBL cursosBL = new CursosBL();

        public List<Documento> ObtenerDocumentos(Int32 EmpresaId, Int32 CursoOnlineId)
        {
            var documentos = new List<Documento>();

            //var context = new EvolEntities();

            
            //var lstAlumnoCursoOnline = context.MatriculaCursoOnline.Include(x => x.Usuario)
            List<MatriculaCursoOnline> lstAlumnoCursoOnline =  new List<MatriculaCursoOnline>()// cursoOnlineBL.ListarMatriculaCursoOnlinePorEmpresaId() //TODO
                .Where(x => x.Estado == ConstantHelpers.ESTADO_MATRICULA.COMPLETADO
                    && x.CursoOnlineId == CursoOnlineId
                    && x.Nota != null
                    && x.Nota >= x.CursoOnline.PorcentajeAprobacion
                    && x.Usuario.EmpresaId == EmpresaId
                    && x.Usuario.Estado == ConstantHelpers.ESTADO.ACTIVO).ToList();



            foreach (var item in lstAlumnoCursoOnline)
            {
                var documento = new Documento();

                documento.Tipo = TipoDocumento.Certificado;
                //documento.CursoOnline = context.CursoOnline.Find(CursoOnlineId);
                documento.CursoOnline = cursosBL.ObtenerCursoOnlinePorId(CursoOnlineId);
                documento.FechaFin = item.FechaAprobado.Value;
                documento.Usuario = item.Usuario;
                documento.Nota = item.Nota.Value;

                documentos.Add(documento);

            }

            return documentos;
        }


        public ViewUsuarioMatriculaCursoOnline ObtenerDocumento(Int32 UsuarioId, Int32 CursoOnlineId)
        {
            //var context = new EvolEntities();

            //return context.ViewUsuarioMatriculaCursoOnline.FirstOrDefault(x => x.UsuarioId == UsuarioId && x.CursoOnlineId == CursoOnlineId);

            
            return new ViewUsuarioMatriculaCursoOnline();  // cursosBL.ObtenerViewUsuarioMatriculaCursoOnlinePorUsuarioPorCursoOnline(UsuarioId, CursoOnlineId); //TODO
            /*
            
            
            var documento = new Documento();

            documento.Tipo = TipoDocumento.Certificado;
            documento.CursoOnline = context.CursoOnline.Find(CursoOnlineId);
            documento.FechaFin = AlumnoCursoOnline.FechaAprobado.Value;
            documento.Usuario = AlumnoCursoOnline.Usuario;
            documento.Nota = AlumnoCursoOnline.Nota.Value;
            return documento;*/

            return null;
        }
    }
}