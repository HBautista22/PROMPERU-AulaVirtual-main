using Newtonsoft.Json;
using PROMPERU.AULAVIRTUAL.BE.CURSOS;
using PROMPERU.AULAVIRTUAL.BL;
using PROMPERU.AULAVIRTUAL.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PROMPERU.AULAVIRTUAL.BE.USUARIO;

namespace PROMPERU.AULAVIRTUAL.WEB.Controllers
{
    public class GrupoController : BaseController
    {
        public class RespuestaJsonViewModel
        {
            public string Estado { get; set; }
            public string Informacion { get; set; }
        }

        // GET: Grupo
        private readonly GrupoBL _grupoBl = new GrupoBL();
        private readonly UsuarioBL _usuarioBl = new UsuarioBL();

        public ContentResult Grupo(int grupoId)
        {

            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                Grupo grupo = _grupoBl.ObtenerGrupoPorId(grupoId);

                return Content(JsonConvert.SerializeObject(grupo), "application/json");

            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult CursoAgregado(int grupoId)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                List<CursoGrupo> cursoOnlineMaestro = cursosBl.ListarCursoGrupoGrupo()
                    .Where(p=>p.GrupoId == grupoId && p.Estado != ConstantHelpers.ESTADO.ELIMINADO)
                    .ToList();

                if (cursoOnlineMaestro.Count <= 0)
                    return Content(JsonConvert.SerializeObject(cursoOnlineMaestro), "application/json");

                foreach (CursoGrupo cursoGrupo in cursoOnlineMaestro)
                {
                    if (cursoGrupo.CursoOnlineMaestroId != null)
                    {
                        List<DetalleCursoOnlineMaestro> detalleCursoMaestro =
                            cursosBl.ListarDetallCursoOnlineMaestroCursoOnline()
                                .Where(x => x.CursoOnlineMaestroId == cursoGrupo.CursoOnlineMaestroId 
                                            && x.Estado != ConstantHelpers.ESTADO.ELIMINADO 
                                            && x.CursoOnlineId == cursoGrupo.CursoOnlineId)
                                .ToList();

                        foreach (DetalleCursoOnlineMaestro detalle in detalleCursoMaestro)
                        {
                            CursoOnline cursosOnline = cursosBl
                                .ListarCursoOnline()
                                .FirstOrDefault(x => x.CursoOnlineId == detalle.CursoOnlineId);

                            detalle.CursoOnline = cursosOnline;

                            cursoGrupo.CursoOnlineMaestro.DetalleCursoOnlineMaestro.Add(detalle);
                        }
                    }

                    if (cursoGrupo.CursoOnlineId == null) continue;
                    {
                        CursoOnline cursoOnline = cursosBl
                            .ListarCursoOnline()
                            .FirstOrDefault(x => x.CursoOnlineId == cursoGrupo.CursoOnlineId);

                        List<DetalleCursoOnlineMaestro> detalleCursoOnlineMaestro = new List<DetalleCursoOnlineMaestro>
                        {
                            new DetalleCursoOnlineMaestro
                            {
                                CursoOnline = cursoOnline
                            }
                        };

                        cursoGrupo.CursoOnlineMaestro.DetalleCursoOnlineMaestro = detalleCursoOnlineMaestro;
                    }
                }

                return Content(JsonConvert.SerializeObject(cursoOnlineMaestro), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult UsuarioAgregado(int grupoId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();

            try
            {
                List<UsuarioGrupo> usuarios = _usuarioBl.ListarUsuarioGrupos(grupoId);

                var data = new
                {
                    alumnos = usuarios
                };

                return Content(JsonConvert.SerializeObject(data), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult CursoDisponible(int grupoId)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            List<CursoOnline> cursosOnline = new List<CursoOnline>();

            try
            {
                List<CursoOnlineMaestro> queryCursoOnline = cursosBl.ListarCursoOnlineMaestro(); // .ListarCursoOnlineMaestro().AsQueryable();
                //queryCursoOnline = queryCursoOnline.Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO);//aquí se agrego que no muestre los eliminados

                // foreach (CursoOnlineMaestro cursoOnlineMaestro in queryCursoOnline)
                // {
                //     List<CursoOnline> lst = cursosBl.ListarCursoOnlineDetalleCursoOnlineMaestroPorCursoOnlineMaestroId(cursoOnlineMaestro.CursoOnlineMaestroId);
                //
                //     cursosOnline.AddRange(lst);
                // }

                List<CursoOnlineMaestro> lstCursoGrupo = queryCursoOnline.ToList();

                var data = new
                {
                    maestros = lstCursoGrupo,
                    cursos = cursosOnline
                };

                return Content(JsonConvert.SerializeObject(data), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult UsuarioDisponible(int grupoId)
        {
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();

            try
            {
                Grupo grupo = _grupoBl.ObtenerGrupoPorId(grupoId);
                List<Usuario> usuarios = _usuarioBl.ListarAlumnosActivos();

                if (grupo.TipoRegistro != "GNR")
                {
                    usuarios = usuarios
                        .Where(x => x.Rol == grupo.TipoRegistro)
                        .ToList();
                }

                var data = new
                {
                    alumnos = usuarios
                };

                return Content(JsonConvert.SerializeObject(data), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult CursoAgregadoMaestro(int cursoOnlineId)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                //List<CursoOnlineMaestro> LstCursoOnlineMaestro;
                //IQueryable<CursoOnlineMaestro> queryCursoOnline1 = cursosBL.SPUSP_AddCursoMaestro_LIS(grupoID).AsQueryable();
                //LstCursoOnlineMaestro = queryCursoOnline1.ToList();

                IQueryable<DetalleCursoOnlineMaestro> queryCursoOnline = cursosBl.ListarDetallCursoOnlineMaestroCursoOnline().AsQueryable();
                queryCursoOnline = queryCursoOnline.OrderBy(x => x.Orden).Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.CursoOnlineMaestroId == cursoOnlineId);//aqui se agrego que no muestre los eliminados
                List<DetalleCursoOnlineMaestro> lstDetalleOnline = queryCursoOnline.ToList();


                return Content(JsonConvert.SerializeObject(lstDetalleOnline), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult CursoDisponibleMaestro(int cursoOnlineId)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                //List<CursoGrupo> LstCursoGrupo;
                //IQueryable<CursoGrupo> queryCursoOnline = cursosBL.ListarCursoGrupoGrupo().AsQueryable();
                //queryCursoOnline = queryCursoOnline.Where(x => x.Estado != ConstantHelpers.ESTADO.ELIMINADO && x.GrupoId == grupoID);//aqui se agrego que no muestre los eliminados
                //LstCursoGrupo = queryCursoOnline.ToList();

                IQueryable<CursoOnline> queryCursoOnline1 = cursosBl.AddCursoOnline((int)cursoOnlineId).AsQueryable(); //dataContext.context.CursoOnline.SqlQuery("USP_AddCursoOnline_LIS {0}", CursoOnlineId).AsQueryable();

                List<CursoOnline> lstCursoOnline = queryCursoOnline1.ToList();


                return Content(JsonConvert.SerializeObject(lstCursoOnline), "application/json");
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }
        }

        public ContentResult AddCursoGrupoMaestro(int grupoId,int cursoOnlineMaestroId, string nombre, int cursoOnlineId)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                CursoGrupo item = new CursoGrupo
                {
                    GrupoId = grupoId,
                    CursoOnlineMaestroId = cursoOnlineMaestroId == 0 ? (int?)null : cursoOnlineMaestroId,
                    Nombre = nombre,
                    Estado = ConstantHelpers.ESTADO.ACTIVO,
                    CursoOnlineId = cursoOnlineId == 0 ? (int?)null : cursoOnlineId
                };

                IQueryable<CursoGrupo> queryCursoOnline1 = cursosBl.ListarCursoGrupoGrupo().ToList().AsQueryable();
                CursoGrupo existeItem = queryCursoOnline1.FirstOrDefault(x => x.GrupoId == grupoId 
                                                                              && x.CursoOnlineMaestroId == cursoOnlineMaestroId
                                                                              && x.CursoOnlineId == cursoOnlineId);
                
                int ret;

                if (existeItem!=null)
                {
                    item.CursoGrupoId = existeItem.CursoGrupoId;
                    ret = cursosBl.ActualizarCursoGrupoMaestro(item);
                }
                else
                {
                    ret = cursosBl.InsertarCursoGrupoMaestro(item);
                }                

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrió un error en la operación.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }

            retVal.Estado = "OK";
            retVal.Informacion = "";

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult AddUsuarioGrupoMaestro(int grupoId, int usuarioId, string nombre)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();

            try
            {
                UsuarioGrupo item = new UsuarioGrupo
                {
                    GrupoId = grupoId,
                    UsuarioId = usuarioId,
                    Estado = ConstantHelpers.ESTADO.ACTIVO,
                    Nombre = nombre
                };

                UsuarioGrupo existeItem = _usuarioBl
                    .ListarUsuarioGrupos(grupoId)
                    .FirstOrDefault(x => x.UsuarioId == usuarioId);
                
                int ret;

                if (existeItem!=null)
                {
                    item.UsuarioGrupoId = existeItem.UsuarioGrupoId;
                    ret = cursosBl.ActualizarUsuarioGrupoMaestro(item);
                }
                else
                {
                    ret = cursosBl.InsertarUsuarioGrupoMaestro(item);
                }                

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrió un error en la operación.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }

            retVal.Estado = "OK";
            retVal.Informacion = "";

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult EliminarCursoGrupoMaestro(int cursoGrupoId, int cursoOnlineId, string nombre)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                CursoGrupo item = new CursoGrupo
                {
                    CursoGrupoId = cursoGrupoId,
                    Nombre = nombre,
                    Estado = ConstantHelpers.ESTADO.ELIMINADO,
                    CursoOnlineId = cursoOnlineId
                };

                int ret = cursosBl.ActualizarCursoGrupoMaestro(item);

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrió un error en la operación.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }

            retVal.Estado = "OK";
            retVal.Informacion = "";

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }

        public ContentResult EliminarUsuarioGrupoMaestro(int usuarioGrupoId, int usuarioId, int grupoId, string nombre)
        {
            CursosBL cursosBl = new CursosBL();
            RespuestaJsonViewModel retVal = new RespuestaJsonViewModel();
            try
            {
                UsuarioGrupo item = new UsuarioGrupo
                {
                    UsuarioGrupoId = usuarioGrupoId,
                    Nombre = nombre,
                    Estado = ConstantHelpers.ESTADO.ELIMINADO,
                    UsuarioId = usuarioId,
                    GrupoId = grupoId
                };

                int ret = cursosBl.ActualizarUsuarioGrupoMaestro(item);

                if (ret == 0)
                {
                    retVal.Estado = "Error";
                    retVal.Informacion = "Ocurrió un error en la operación.";

                    return Content(JsonConvert.SerializeObject(retVal), "application/json");
                }
            }
            catch (Exception ex)
            {
                retVal.Estado = "Error";
                retVal.Informacion = ex.Message;

                return Content(JsonConvert.SerializeObject(retVal), "application/json");
            }

            retVal.Estado = "OK";
            retVal.Informacion = "";

            return Content(JsonConvert.SerializeObject(retVal), "application/json");
        }
    }
}