using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class Encriptacion
    {
        private byte[] secretKey = {};
        private byte[] iv64 = {};
        private String _iv { get; set; }
        public Encriptacion() 
        {
            secretKey = System.Text.Encoding.UTF8.GetBytes("!R5_6Gfy4#2f?!fD");
            this._iv = System.Configuration.ConfigurationManager.AppSettings["IV"];
        }

        public String Desencriptar(String stringToDecrypt, String iv)
        {
            return Desencriptar(stringToDecrypt,iv,false);        
        }

        public String Desencriptar(String stringToDecrypt, Boolean allowNullOrEmpty)
        {
            return Desencriptar(stringToDecrypt, _iv, allowNullOrEmpty);
        }

        public String Desencriptar(String stringToDecrypt)
        {
            return Desencriptar(stringToDecrypt, _iv, false);
        }

        public String Desencriptar(String stringToDecrypt, String iv, Boolean allowNullOrEmpty)
        {
            if (allowNullOrEmpty && String.IsNullOrEmpty(stringToDecrypt))
                return String.Empty;

            stringToDecrypt = stringToDecrypt.Replace(" ", "+");
            iv64 = System.Text.Encoding.UTF8.GetBytes(iv);
            TripleDESCryptoServiceProvider cryptoSP = new TripleDESCryptoServiceProvider();
            byte[] inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateDecryptor(secretKey, iv64), CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        public String Encriptar(String stringToEncrypt)
        {
            return Encriptar(stringToEncrypt, _iv);
        }

        public String Encriptar(String stringToEncrypt, String iv)
        {
            try
            {
                iv64 = System.Text.Encoding.UTF8.GetBytes(iv);
                TripleDESCryptoServiceProvider cryptoSP = new TripleDESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoSP.CreateEncryptor(secretKey, iv64), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
