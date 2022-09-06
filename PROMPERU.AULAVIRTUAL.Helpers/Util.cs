using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Net;

namespace PROMPERU.AULAVIRTUAL.Helpers
{
    public class Util
    {
        public static string CadenaConexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Promperu_EvolConnectionString"].ConnectionString;
            }

        }


        public static string Base64toString(string base64cadena) {


            byte[] data = System.Convert.FromBase64String(base64cadena);
            var myarreglo = System.Text.ASCIIEncoding.ASCII.GetString(data);

            return myarreglo;

        }

        public static dynamic DeserealizeJscript(string arreglo) {

            var serializer = new JavaScriptSerializer();
            dynamic lista = serializer.DeserializeObject(arreglo);
            return lista;

        }

        public static int[] ConvertArrayDynamicToInt(dynamic lista)
        {
            int[] int_arreglo = new int[Enumerable.Count(lista)];
            int i = 0;

            foreach (var item in lista)
            {
                int_arreglo[i] = item;
                i++;
            }

            return int_arreglo;

        }

        public static int[] ConvertArrayTextToInt(dynamic lista)
        {
            int[] int_arreglo = new int[Enumerable.Count(lista)];
            int i = 0;

            foreach (var item in lista)
            {
                int_arreglo[i] = int.Parse(item);
                i++;
            }

            return int_arreglo;

        }

        public static void SaveErrorLog(Exception e)
        {

            String ruta = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            //string path = @"\logs\" + parametros.usuario + ".txt";
            //if (!File.Exists(path))
            //{
            // Create a file to write to.

            // new StreamWriter(fileName, true)
            String linea = "";

            using (StreamWriter sw = new StreamWriter(ruta, true))
            {
                /*   var st = new StackTrace(e, true);
                   // Get the top stack frame
                   var frame = st.GetFrame(0);
                   // Get the line number from the stack frame
                   var line = frame.GetFileLineNumber();*/

                DateTime dateTime = DateTime.Now;

                linea = dateTime.ToString("HH:mm:ss");
                sw.WriteLine("-----------------------------" + linea + "------------------------------------");
                linea = e.ToString();
                sw.WriteLine(linea);
                sw.WriteLine("--------------------------------------------------------------------------");
            }
        }

        public static void SaveBulkCsv(List<dynamic> Lista)
        {

            String ruta = RutaMasivo;

            using (var writer = new StreamWriter(ruta))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Lista);
            }

        }

        public static string  RutaMasivo
        {
            get
            {
                   return AppDomain.CurrentDomain.BaseDirectory + "\\Files\\masivo\\"+ DateTime.Today.ToString("yyyy.MM.dd") + ".csv";
            }

        }


        public static string RutaMasivoRemote
        {
            get
            {
                return ConfigurationManager.AppSettings["ruta_remota"] + DateTime.Today.ToString("yyyy.MM.dd") + ".csv";
            }

        }


        #region FTP PROCESO


        public static bool CopyToFTP(string fileSource) {

            try
            {

                using (var client = new WebClient())
                {
                    var Usuario = ConfigurationManager.AppSettings["USER_FTP"].ToString();
                    var Clave = ConfigurationManager.AppSettings["PWD_FTP"].ToString();
                    var Url = ConfigurationManager.AppSettings["URL_FTP"].ToString();


                    client.Credentials = new NetworkCredential(Usuario, Clave);
                    client.UploadFile(Url + DateTime.Today.ToString("yyyy.MM.dd") + ".csv", WebRequestMethods.Ftp.UploadFile, fileSource);
                    return true;
                }
            }
            catch (Exception e)
            {
                SaveErrorLog(e);
                return true;
            }


        }




        /*public static bool CopyToFTP(string fileName, string FileToCopy)
        {

            var Usuario = ConfigurationManager.AppSettings["USER_FTP"].ToString();
            var Clave = ConfigurationManager.AppSettings["PWD_FTP"].ToString();
            var Url = ConfigurationManager.AppSettings["URL_FTP"].ToString();

            return CopyFile(Url, fileName, FileToCopy, Usuario, Clave);

        }

        public static bool CopyFile(string urlFTP, string fileName, string FileToCopy, string userName, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFTP + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(userName, password);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                Upload(urlFTP + FileToCopy, ToByteArray(responseStream), userName, password);
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        public static bool Upload(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                System.IO.Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(Image, 0, Image.Length);

                clsStream.Close();
                clsStream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }*/


        #endregion





    }
}
