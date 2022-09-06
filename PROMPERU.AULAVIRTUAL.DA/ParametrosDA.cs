using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
using PROMPERU.AULAVIRTUAL.Helpers;

namespace PROMPERU.AULAVIRTUAL.DA
{
    public class ParametrosDA
    {
        public List<Parametro> ListarParametro()
        {

            List<Parametro> retVal = new List<Parametro>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Parametro_LIS";

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
                Parametro parametro = new Parametro();
                parametro.ParametroId = Convert.ToInt32(dr["ParametroId"].ToString());
                parametro.ParametroGrupoId = Convert.ToInt32(dr["ParametroGrupoId"].ToString());
                //parametro.ParametroPadreId = Convert.ToInt32(dr["ParametroPadreId"].ToString());
                parametro.Codigo = dr["Codigo"].ToString();
                parametro.Descripcion = dr["Descripcion"].ToString();
                parametro.DescripcionCorta = dr["DescripcionCorta"].ToString();
                parametro.DescripcionEN = dr["DescripcionEN"].ToString();
                parametro.DescripcionCortaEN = dr["DescripcionCortaEN"].ToString();
                parametro.Valor = dr["Valor"].ToString();
                parametro.Orden = Convert.ToInt32(dr["Orden"].ToString());
                parametro.Propiedades = dr["Propiedades"].ToString();
                parametro.EsActivo = Convert.ToBoolean(dr["EsActivo"].ToString());
                retVal.Add(parametro);

            }

            return retVal;
        }

        public List<RutexSector> ListarRutexSector()
        {

            List<RutexSector> retVal = new List<RutexSector>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_RutexSector_LIS";

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
                RutexSector rutexSector = new RutexSector();
                rutexSector.SectorId = Convert.ToInt32(dr["SectorId"].ToString());
                rutexSector.Descripcion = dr["Descripcion"].ToString();
                retVal.Add(rutexSector);

            }

            return retVal;
        }

        public List<Departamento> ListarDepartamento()
        {

            List<Departamento> retVal = new List<Departamento>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Departamento_LIS";

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
                Departamento obj = new Departamento();
                obj.DepartamentoId = (int)(dr["DepartamentoId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.Acronimo = (string)(dr["Acronimo"]);
                obj.Region = (string)(dr["Region"]);
                obj.EstadoId = (int)(dr["EstadoId"]);

                retVal.Add(obj);

            }

            return retVal;
        }

        public List<Provincia> ListarProvincia()
        {

            List<Provincia> retVal = new List<Provincia>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Provincia_LIS";

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
                Provincia obj = new Provincia();
                obj.ProvinciaId = (int) (dr["ProvinciaId"]);
                obj.DepartamentoId = (int)(dr["DepartamentoId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.EstadoId = (int)(dr["EstadoId"]);
                retVal.Add(obj);
            }

            return retVal;
        }

        public List<Distrito> ListarDistrito()
        {

            List<Distrito> retVal = new List<Distrito>();

            DataTable dt = new DataTable();

            string cadenaConexion = Util.CadenaConexion;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (SqlCommand cmdSQL = new SqlCommand())
                {
                    cmdSQL.Connection = conexion;
                    cmdSQL.CommandType = CommandType.StoredProcedure;
                    cmdSQL.CommandText = "USP_Distrito_LIS";

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
                Distrito obj = new Distrito();
                obj.DistritoId = (int)(dr["DistritoId"]);
                obj.ProvinciaId = (int)(dr["ProvinciaId"]);
                obj.DepartamentoId = (int)(dr["DepartamentoId"]);
                obj.Nombre = (string)(dr["Nombre"]);
                obj.EstadoId = (int)(dr["EstadoId"]);
                obj.Ubigeo = (string)(dr["Ubigeo"]);

                retVal.Add(obj);
            }

            return retVal;
        }


    }
}
