using PROMPERU.AULAVIRTUAL.WEB.Controllers;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Entity;

namespace PROMPERU.AULAVIRTUAL.WEB.ViewModel.Enrollment
{
    public class ResultCargaDatosUsuarioViewModel
    {
        public List<Int32> LstUsuarioIdExistente { get; set; }
        public List<Int32> LstUsuarioId{ get; set; }
        public List<Int32> LstUsuarioNoMatriculados { get; set; }
        public CursoOnline CursoOnline { get; set; }
        public List<MatriculaCursoOnline> LstMatriculaCursoOnline{ get; set; }

        public List<Usuario> LstUsuarioNoRegistrados { get; set; }
        public Int32 CursoOnlineId { get; set; }

        public ResultCargaDatosUsuarioViewModel () {
            LstUsuarioIdExistente = new List<Int32>();
            LstUsuarioId = new List<Int32>();
            LstUsuarioNoMatriculados = new List<Int32>();
            LstUsuarioNoRegistrados = new List<Usuario>();
        }

        public void CargarDatos(Int32 cursoOnlineId)
        {
            CursoOnlineId = cursoOnlineId;
            //CursoOnline = dataContext.context.CursoOnline.Find(CursoOnlineId);
            //var empresaId = dataContext.session.GetEmpresaId();
            //var query = dataContext.context.MatriculaCursoOnline.Include(x => x.Usuario).Where(x => x.CursoOnlineId == cursoOnlineId && LstUsuarioId.Contains(x.UsuarioId)).AsQueryable();
            //LstMatriculaCursoOnline = query.ToList();
        }
    }
}