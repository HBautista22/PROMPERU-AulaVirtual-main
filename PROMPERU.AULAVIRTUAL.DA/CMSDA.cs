using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.CMS;
using PROMPERU.AULAVIRTUAL.BE;
using PROMPERU.AULAVIRTUAL.Helpers;
namespace PROMPERU.AULAVIRTUAL.DA
{
    public class CMSDA
    {


        public CMS_Page SeleccionarPagina(string url)
        {
            CMS_Page retVal = new CMS_Page();

            CMSDA cmsda = new CMSDA();

            DataTable dt = new DataTable();

            string cnxconSQL = Util.CadenaConexion;

            using (SqlConnection conSQL = new SqlConnection(cnxconSQL))
            {
                conSQL.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conSQL;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Page_SEL";

                    cmdSQL.Parameters.Add("@URL_PAGE", SqlDbType.VarChar, -1).Value = url;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conSQL.State == ConnectionState.Open)
                {
                    conSQL.Close();
                }
            }

            if (dt.Rows.Count > 0)
            {
                retVal.Nombre = dt.Rows[0]["Nombre"].ToString();
                retVal.Url = dt.Rows[0]["Url"].ToString();
                retVal.RutaLayout = dt.Rows[0]["RutaLayout"].ToString();
                retVal.RutaPagina = dt.Rows[0]["RutaPagina"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dt.Rows[0]["EsPublicado"].ToString());
            }

            return retVal;
        }

        public List<CMS_Testimonio> ListarTestimoniosDisponibles()
        {

            List<CMS_Testimonio> retVal = new List<CMS_Testimonio>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Testimonio_LIS";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_Testimonio cmsTestimonio = new CMS_Testimonio();
                cmsTestimonio.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsTestimonio.Titulo = dr["Titulo"].ToString();
                cmsTestimonio.Descripcion = dr["Descripcion"].ToString();
                cmsTestimonio.Imagen = dr["Imagen"].ToString();
                cmsTestimonio.Nombre = dr["Nombre"].ToString();
                cmsTestimonio.Empresa = dr["Empresa"].ToString();
                cmsTestimonio.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());

                retVal.Add(cmsTestimonio);

            }


            return retVal;
        }

        public List<CMS_Banner> ListarBannerDisponibles()
        {

            List<CMS_Banner> retVal = new List<CMS_Banner>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Banner_LIS";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_Banner cmsBanner = new CMS_Banner();
                cmsBanner.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsBanner.Nombre = dr["Nombre"].ToString();
                cmsBanner.Imagen = dr["Imagen"].ToString();
                cmsBanner.TituloSuperior = dr["TituloSuperior"].ToString();
                cmsBanner.TituloInferior = dr["TituloInferior"].ToString();
                cmsBanner.SubTitulo = dr["SubTitulo"].ToString();
                cmsBanner.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                retVal.Add(cmsBanner);
            }


            return retVal;
        }

        public void ActualizarMenuCMS(int menuId, bool estado)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_MenuCMS_UPD";
                    cmdSQL.Parameters.Add("@MenuID", SqlDbType.Int).Value = menuId;
                    cmdSQL.Parameters.Add("@Activo", SqlDbType.Bit).Value = estado;
                    
                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            
        }

        public List<CMS_MenuCMS> ObtenerMenuCMS()
        {

            List<CMS_MenuCMS> retVal = new List<CMS_MenuCMS>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_MenuCMS_LIS";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_MenuCMS cmsBanner = new CMS_MenuCMS();
                cmsBanner.MenuID = Convert.ToInt32(dr["MenuID"].ToString());
                cmsBanner.URL = dr["URL"].ToString();
                cmsBanner.Nombre = dr["Nombre"].ToString();
                cmsBanner.Seccion = dr["Seccion"].ToString();
                cmsBanner.Class = dr["Class"].ToString();
                cmsBanner.Perfil = dr["Perfil"].ToString();
                cmsBanner.MenuIDPadre = (dr["MenuIDPadre"].ToString() != String.Empty ? (int?)(dr["MenuIDPadre"]) : null);
                cmsBanner.Activo = Convert.ToBoolean(dr["Activo"].ToString());
                retVal.Add(cmsBanner);
            }


            return retVal;
        }

        public int ActualizarLabel(CMS_Label CMS_LabelObj)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Label_UPD";
                    cmdSQL.Parameters.Add("@CMS_LabelId", SqlDbType.Int).Value = CMS_LabelObj.CMS_LabelId;
                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar, -1).Value = CMS_LabelObj.Codigo;
                    cmdSQL.Parameters.Add("@EsRaw", SqlDbType.Bit).Value = CMS_LabelObj.EsRaw;
                    cmdSQL.Parameters.Add("@Texto", SqlDbType.VarChar, -1).Value = CMS_LabelObj.Texto;
                    cmdSQL.Parameters.Add("@TipoComponente", SqlDbType.VarChar, -1).Value = CMS_LabelObj.TipoComponente;
                    cmdSQL.Parameters.Add("@FechaModificacion", SqlDbType.DateTime).Value = CMS_LabelObj.FechaModificacion;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public CMS_Label ObtenerCMS_LabelPorId(int traduccionIdInt)
        {
            CMS_Label retVal = new CMS_Label();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Label_SEL_PorId";

                    cmdSQL.Parameters.Add("@CMS_LabelId", SqlDbType.Int).Value = traduccionIdInt;


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                retVal.CMS_LabelId = Convert.ToInt32(dr["CMS_LabelId"].ToString());
                retVal.Codigo = dr["Codigo"].ToString();
                retVal.Texto = dr["Texto"].ToString();
                retVal.EsRaw = Convert.ToBoolean(dr["EsRaw"].ToString());
                retVal.TipoComponente = dr["TipoComponente"].ToString();
            }


            return retVal;
        }

        public List<CMS_Certificate> ListarCertificateDisponibles()
        {

            List<CMS_Certificate> retVal = new List<CMS_Certificate>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Certificate_LIS";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_Certificate cmsCertificate = new CMS_Certificate();
                cmsCertificate.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsCertificate.Nombre = dr["Nombre"].ToString();
                cmsCertificate.Imagen = dr["Imagen"].ToString();
                cmsCertificate.Sumilla = dr["Sumilla"].ToString();
                cmsCertificate.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                retVal.Add(cmsCertificate);
            }


            return retVal;
        }

        public List<CMS_MenuItem> ListarMenuItem(string Codigo)
        {

            List<CMS_MenuItem> retVal = new List<CMS_MenuItem>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_MenuItem_SEL";

                    cmdSQL.Parameters.Add("@Codigo", SqlDbType.VarChar);
                    cmdSQL.Parameters["@Codigo"].Value = Codigo;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_MenuItem cmsMenuItem = new CMS_MenuItem();
                cmsMenuItem.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsMenuItem.MenuId = Convert.ToInt32(dr["MenuId"].ToString());


                if (int.TryParse(dr["PageId"].ToString(), out int tempResult))
                {
                    cmsMenuItem.PageId = Convert.ToInt32(dr["PageId"].ToString());
                }
                else
                {
                    cmsMenuItem.PageId = null;
                }



                if (int.TryParse(dr["ParentMenuItemId"].ToString(), out int tempResult0))
                {
                    cmsMenuItem.ParentMenuItemId = Convert.ToInt32(dr["ParentMenuItemId"].ToString());
                }
                else
                {
                    cmsMenuItem.ParentMenuItemId = null;
                }

                cmsMenuItem.CustomUrl = dr["CustomUrl"].ToString();
                cmsMenuItem.Titulo = dr["Titulo"].ToString();
                cmsMenuItem.Orden = Convert.ToInt32(dr["Orden"].ToString());
                cmsMenuItem.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                cmsMenuItem.CMS_Page = new CMS_Page();
                cmsMenuItem.CMS_Page.Nombre = dr["Nombre"].ToString();
                cmsMenuItem.CMS_Page.RutaLayout = dr["RutaLayout"].ToString();
                cmsMenuItem.CMS_Page.RutaPagina = dr["RutaPagina"].ToString();
                cmsMenuItem.CMS_Page.Url = dr["Url"].ToString();

                retVal.Add(cmsMenuItem);
            }


            return retVal;
        }

        public List<CMS_Label> ListarLabel()
        {

            List<CMS_Label> retVal = new List<CMS_Label>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Label_LIS";


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_Label cmsLabel = new CMS_Label();
                cmsLabel.CMS_LabelId = Convert.ToInt32(dr["CMS_LabelId"].ToString());
                cmsLabel.Codigo = dr["Codigo"].ToString();
                cmsLabel.Texto = dr["Texto"].ToString();
                cmsLabel.EsRaw = Convert.ToBoolean(dr["EsRaw"].ToString());
                cmsLabel.TipoComponente = dr["TipoComponente"].ToString();
                retVal.Add(cmsLabel);
            }


            return retVal;
        }

        public List<CMS_Agenda> ListarAgenda()
        {

            List<CMS_Agenda> retVal = new List<CMS_Agenda>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Agenda_LIS";


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_Agenda cmsAgenda = new CMS_Agenda();
                cmsAgenda.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsAgenda.Nombre = dr["Nombre"].ToString();
                cmsAgenda.FechaInicio = Convert.ToDateTime(dr["FechaInicio"].ToString());
                cmsAgenda.FechaFin = Convert.ToDateTime(dr["FechaFin"].ToString());
                cmsAgenda.Imagen = dr["Imagen"].ToString();
                cmsAgenda.Sumilla = dr["Sumilla"].ToString();
                cmsAgenda.Archivo = dr["Archivo"].ToString();
                cmsAgenda.RutaInscripcion = dr["RutaInscripcion"].ToString();
                cmsAgenda.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                cmsAgenda.FechaEdicion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                cmsAgenda.UsuarioEdicionId = Convert.ToInt32(dr["UsuarioEdicionId"].ToString());
                                
                retVal.Add(cmsAgenda);
            }


            return retVal;
        }

        public List<CMS_PreguntaFrecuente> ListarPreguntaFrecuente()
        {

            List<CMS_PreguntaFrecuente> retVal = new List<CMS_PreguntaFrecuente>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_PreguntaFrecuente_LIS";


                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }


            foreach (DataRow dr in dt.Rows)
            {
                CMS_PreguntaFrecuente cmsPreguntaFrecuente = new CMS_PreguntaFrecuente();
                cmsPreguntaFrecuente.Id = Convert.ToInt32(dr["Id"].ToString());
                cmsPreguntaFrecuente.Pregunta = dr["Pregunta"].ToString();
                cmsPreguntaFrecuente.Respuesta = dr["Respuesta"].ToString();
                cmsPreguntaFrecuente.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                cmsPreguntaFrecuente.FechaEdicion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                cmsPreguntaFrecuente.UsuarioEdicionId = Convert.ToInt32(dr["UsuarioEdicionId"].ToString());

                retVal.Add(cmsPreguntaFrecuente);
            }


            return retVal;
        }

        public CMS_Banner ObtenerBannerPorId (Int32? id)
        {
            CMS_Banner retVal = new CMS_Banner();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Banner_SEL_PorId";

                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
              
                retVal.Id = Convert.ToInt32(dr["Id"].ToString());
                retVal.Nombre = dr["Nombre"].ToString();
                retVal.Imagen = dr["Imagen"].ToString();
                retVal.TituloSuperior = dr["TituloSuperior"].ToString();
                retVal.TituloInferior = dr["TituloInferior"].ToString();
                retVal.SubTitulo = dr["SubTitulo"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
            }

            if (dt.Rows.Count == 0)
            {
                retVal = null;
            }

            return retVal;

        }

        public CMS_Agenda ObtenerAgendaPorId(Int32? id)
        {
            CMS_Agenda retVal = new CMS_Agenda();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Agenda_SEL_PorId";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = id;



                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Id = Convert.ToInt32(dr["Id"].ToString());
                retVal.Nombre = dr["Nombre"].ToString();
                retVal.FechaInicio = Convert.ToDateTime(dr["FechaInicio"].ToString());
                retVal.FechaFin = Convert.ToDateTime(dr["FechaFin"].ToString());
                retVal.Imagen = dr["Imagen"].ToString();
                retVal.Sumilla = dr["Sumilla"].ToString();
                retVal.Archivo = dr["Archivo"].ToString();
                retVal.RutaInscripcion = dr["RutaInscripcion"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                retVal.FechaEdicion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                retVal.UsuarioEdicionId = Convert.ToInt32(dr["UsuarioEdicionId"].ToString());

            }

            if(dt.Rows.Count==0)
            {
                retVal = null;
            }

            return retVal;

        }

        public CMS_Testimonio ObtenerTestimonioPorId(Int32? id)
        {
            CMS_Testimonio retVal = new CMS_Testimonio();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Testimonio_SEL_PorId";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Id = Convert.ToInt32(dr["Id"].ToString());
                retVal.Titulo = dr["Titulo"].ToString();
                retVal.Descripcion = dr["Descripcion"].ToString();
                retVal.Imagen = dr["Imagen"].ToString();
                retVal.Nombre = dr["Nombre"].ToString();
                retVal.Empresa = dr["Empresa"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
            }

            if (dt.Rows.Count == 0)
            {
                retVal = null;
            }

            return retVal;

        }

        public CMS_PreguntaFrecuente ObtenerPreguntaFrecuentePorId(Int32? id)
        {
            CMS_PreguntaFrecuente retVal = new CMS_PreguntaFrecuente();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_PreguntaFrecuente_SEL_PorId";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Id = Convert.ToInt32(dr["Id"].ToString());
                retVal.Pregunta = dr["Pregunta"].ToString();
                retVal.Respuesta = dr["Respuesta"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
                retVal.FechaEdicion = Convert.ToDateTime(dr["FechaEdicion"].ToString());
                retVal.UsuarioEdicionId = Convert.ToInt32(dr["UsuarioEdicionId"].ToString());
            }

            if (dt.Rows.Count == 0)
            {
                retVal = null;
            }

            return retVal;

        }

        public CMS_Certificate ObtenerCertificatePorId(Int32? id)
        {
            CMS_Certificate retVal = new CMS_Certificate();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Certificate_SEL_PorId";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSQL))
                    {
                        da.Fill(dt);
                    }
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Id = Convert.ToInt32(dr["Id"].ToString());
                retVal.Nombre = dr["Nombre"].ToString();
                retVal.Imagen = dr["Imagen"].ToString();
                retVal.Sumilla = dr["Sumilla"].ToString();
                retVal.EsPublicado = Convert.ToBoolean(dr["EsPublicado"].ToString());
            }

            if (dt.Rows.Count == 0)
            {
                retVal = null;
            }

            return retVal;

        }

        public int InsertarBanner(CMS_Banner banner)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Banner_INS";
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = banner.Nombre;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = banner.Imagen;
                    cmdSQL.Parameters.Add("@TituloSuperior", SqlDbType.VarChar, -1).Value = banner.TituloSuperior;
                    cmdSQL.Parameters.Add("@TituloInferior", SqlDbType.VarChar, -1).Value = banner.TituloInferior;
                    cmdSQL.Parameters.Add("@SubTitulo", SqlDbType.VarChar, -1).Value = banner.SubTitulo;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = banner.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = banner.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = banner.UsuarioEdicionId;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarBanner(CMS_Banner banner)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Banner_UPD";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = banner.Id;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = banner.Nombre;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = banner.Imagen;
                    cmdSQL.Parameters.Add("@TituloSuperior", SqlDbType.VarChar, -1).Value = banner.TituloSuperior;
                    cmdSQL.Parameters.Add("@TituloInferior", SqlDbType.VarChar, -1).Value = banner.TituloInferior;
                    cmdSQL.Parameters.Add("@SubTitulo", SqlDbType.VarChar, -1).Value = banner.SubTitulo;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = banner.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = banner.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = banner.UsuarioEdicionId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarAgenda(CMS_Agenda agenda)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Agenda_INS";
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = agenda.Nombre;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = agenda.FechaInicio;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = agenda.FechaFin;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = agenda.Imagen;
                    cmdSQL.Parameters.Add("@Sumilla", SqlDbType.VarChar, -1).Value = agenda.Sumilla;
                    cmdSQL.Parameters.Add("@Archivo", SqlDbType.VarChar, -1).Value = agenda.Archivo;
                    cmdSQL.Parameters.Add("@RutaInscripcion", SqlDbType.VarChar, -1).Value = agenda.RutaInscripcion;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = agenda.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = agenda.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = agenda.UsuarioEdicionId;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarAgenda(CMS_Agenda agenda)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Agenda_UPD";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = agenda.Id;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = agenda.Nombre;
                    cmdSQL.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = agenda.FechaInicio;
                    cmdSQL.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = agenda.FechaFin;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = agenda.Imagen;
                    cmdSQL.Parameters.Add("@Sumilla", SqlDbType.VarChar, -1).Value = agenda.Sumilla;
                    cmdSQL.Parameters.Add("@Archivo", SqlDbType.VarChar, -1).Value = agenda.Archivo;
                    cmdSQL.Parameters.Add("@RutaInscripcion", SqlDbType.VarChar, -1).Value = agenda.RutaInscripcion;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = agenda.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = agenda.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = agenda.UsuarioEdicionId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarTestimonio(CMS_Testimonio testimonio)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Testimonio_INS";
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar, -1).Value = testimonio.Titulo;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = testimonio.Descripcion;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = testimonio.Imagen;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = testimonio.Nombre;
                    cmdSQL.Parameters.Add("@Empresa", SqlDbType.VarChar, -1).Value = testimonio.Empresa;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = testimonio.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = testimonio.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = testimonio.UsuarioEdicionId;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarTestimonio(CMS_Testimonio testimonio)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Testimonio_UPD";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = testimonio.Id;
                    cmdSQL.Parameters.Add("@Titulo", SqlDbType.VarChar, -1).Value = testimonio.Titulo;
                    cmdSQL.Parameters.Add("@Descripcion", SqlDbType.VarChar, -1).Value = testimonio.Descripcion;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = testimonio.Imagen;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = testimonio.Nombre;
                    cmdSQL.Parameters.Add("@Empresa", SqlDbType.VarChar, -1).Value = testimonio.Empresa;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = testimonio.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = testimonio.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = testimonio.UsuarioEdicionId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }

        public int InsertarPreguntaFrecuente(CMS_PreguntaFrecuente preguntaFrecuente)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_PreguntaFrecuente_INS";
                    cmdSQL.Parameters.Add("@Pregunta", SqlDbType.VarChar, -1).Value = preguntaFrecuente.Pregunta;
                    cmdSQL.Parameters.Add("@Respuesta", SqlDbType.VarChar, -1).Value = preguntaFrecuente.Respuesta;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = preguntaFrecuente.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = preguntaFrecuente.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = preguntaFrecuente.UsuarioEdicionId;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarPreguntaFrecuente(CMS_PreguntaFrecuente preguntaFrecuente)
        {

            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_PreguntaFrecuente_UPD";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = preguntaFrecuente.Id;
                    cmdSQL.Parameters.Add("@Pregunta", SqlDbType.VarChar, -1).Value = preguntaFrecuente.Pregunta;
                    cmdSQL.Parameters.Add("@Respuesta", SqlDbType.VarChar, -1).Value = preguntaFrecuente.Respuesta;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = preguntaFrecuente.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = preguntaFrecuente.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = preguntaFrecuente.UsuarioEdicionId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int InsertarCertificate(CMS_Certificate certificate)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Certificate_INS";
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = certificate.Nombre;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = certificate.Imagen;
                    cmdSQL.Parameters.Add("@Sumilla", SqlDbType.VarChar, -1).Value = certificate.Sumilla;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = certificate.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = certificate.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = certificate.UsuarioEdicionId;


                    lintResultado = Convert.ToInt32(cmdSQL.ExecuteScalar().ToString());

                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;
        }

        public int ActualizarCertificate(CMS_Certificate certificate)
        {
            int lintResultado = 0;

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_CMS_Certificate_UPD";
                    cmdSQL.Parameters.Add("@Id", SqlDbType.Int).Value = certificate.Id;
                    cmdSQL.Parameters.Add("@Nombre", SqlDbType.VarChar, -1).Value = certificate.Nombre;
                    cmdSQL.Parameters.Add("@Imagen", SqlDbType.VarChar, -1).Value = certificate.Imagen;
                    cmdSQL.Parameters.Add("@Sumilla", SqlDbType.VarChar, -1).Value = certificate.Sumilla;
                    cmdSQL.Parameters.Add("@EsPublicado", SqlDbType.Int).Value = certificate.EsPublicado;
                    cmdSQL.Parameters.Add("@FechaEdicion", SqlDbType.DateTime).Value = certificate.FechaEdicion;
                    cmdSQL.Parameters.Add("@UsuarioEdicionId", SqlDbType.Int).Value = certificate.UsuarioEdicionId;

                    lintResultado = cmdSQL.ExecuteNonQuery();
                }
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lintResultado;

        }














    }
}
